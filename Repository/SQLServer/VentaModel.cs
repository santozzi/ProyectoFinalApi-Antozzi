using System.Data.SqlClient;
using System.Data;
using ProyectoFinalAPI_Antozzi.Repository.Interfaces;
using ProyectoFinalAPI_Antozzi.Entities;
using ProyectoFinalAPI_Antozzi.Services;
using ProyectoFinalAPI_Antozzi.Repository.Exceptions;
using System.Reflection.PortableExecutable;

namespace ProyectoFinalAPI_Antozzi.Repository.SQLServer
{
    public class VentaModel : ConexionString, IVentaModel
    {
        private const string TABLE = "Venta";


        public List<VentaConProducto> TraerVenta(Int64 idUsuario)
        {
            List<VentaConProducto> ventas = new List<VentaConProducto>();

            if (this.Get(idUsuario) != null)
            {


                string deleteVentaSql = $@"SELECT
                                           Venta.Id,
                                           Venta.Comentarios,
                                           Venta.IdUsuario,
                                           Producto.Descripciones,
                                           ProductoVendido.Stock
                                           FROM ProductoVendido 
                                           INNER JOIN Venta  ON ProductoVendido.IdVenta = Venta.Id
                                           INNER JOIN Producto  ON ProductoVendido.IdProducto = Producto.Id
                                           WHERE Venta.IdUsuario = @Id";


                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmdVendido = new SqlCommand(deleteVentaSql, connection))
                    {
                        connection.Open();
                        cmdVendido.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt) { Value = idUsuario });
                        SqlDataReader reader = cmdVendido.ExecuteReader();

                        while (reader.Read())
                        {
                            VentaConProducto productoVendido = new VentaConProducto();

                            productoVendido.Id = reader.GetInt64(0);
                            productoVendido.Comentarios = reader.GetString(1);
                            productoVendido.IdUsuario = reader.GetInt64(2);
                            productoVendido.Descripciones = reader.GetString(3);
                            productoVendido.Stock = reader.GetInt32(4);
                            
                            ventas.Add(productoVendido);
                        };
                        connection.Close();
                    }

                }

            }
            return ventas;
        }



        public Venta Add(Venta entity)
        {

            Int64 idVenta = 0;
            string sql = $"INSERT " +
                         $"INTO {TABLE} (Comentarios, IdUsuario ) " +
                         $"VALUES (@Comentarios, @IdUsuario); " +
                         $"SELECT @@IDENTITY";

            if (UsuarioServices.Instance().isExistUsusario(entity.IdUsuario))
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        cmd.Parameters.Add(new SqlParameter("Comentarios", SqlDbType.Text) { Value = entity.Comentarios });
                        cmd.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = entity.IdUsuario });

                        idVenta = Convert.ToInt64(cmd.ExecuteScalar());
                        connection.Close();
                    }
                }
                entity.Id = idVenta;
                if (idVenta == 0)
                {
                    return null;
                }
            }
            else
            {
                throw new TheItemDoesNotExistException("El usuario no existe");
            }
            return entity;
        }

        public bool Add(List<Producto> productosVendidos, Int64 idUsuario)
        {
            bool resultadoExitoso = false;
            try
            {
                resultadoExitoso = verificarStock(productosVendidos);



                if (resultadoExitoso)
                {

                    Venta venta = new Venta();
                    venta.IdUsuario = idUsuario;
                    venta.Comentarios = "";
                    venta.Id = this.Add(venta).Id;



                    foreach (Producto productoVendido in productosVendidos)
                    {

                        if (productoVendido.Stock <= 0)
                        {
                            throw new InsufficientQuantityOfProductsException("El stock del producto debe ser mayor a 0");
                        }
                        ProductoVendido prodVend = new ProductoVendido
                        {
                            IdProducto = productoVendido.Id,
                            Stock = productoVendido.Stock,

                            IdVenta = venta.Id,

                        };
                        ProductoVendidoServices.Instance().Add(prodVend);
                        //resto a producto el stock, ya se ferificó que exista el stock, dudo de la importancia de este try.

                        ProductoServices.Instance().SubstractProductStock(productoVendido.Id, productoVendido.Stock);


                    }


                }
                else
                {
                    throw new InsufficientQuantityOfProductsException("No hay stock para cubrir la operacion");
                }

            }
            catch (TheItemDoesNotExistException ex)
            {
                throw new TheItemDoesNotExistException(ex.Message);
            }
            catch (InsufficientQuantityOfProductsException ex)
            {
                throw new InsufficientQuantityOfProductsException(ex.Message);
            }

            return resultadoExitoso;
        }

        private bool verificarStock(List<Producto> productosVendidos)
        {
            bool hayStockDeTodo = false;
            foreach (Producto productoVendido in productosVendidos)
            {
                try
                {
                    Producto productoDeProducto = ProductoServices.Instance().Get(productoVendido.Id);

                    hayStockDeTodo = productoDeProducto.Stock >= productoVendido.Stock;
                    if (!hayStockDeTodo)
                    {
                        break;
                    }
                }
                catch (TheItemDoesNotExistException ex)
                {
                    throw new TheItemDoesNotExistException(ex.Message);
                }
            }



            return hayStockDeTodo;
        }

        public bool DeleteVenta(Venta venta)
        {
            bool idExist = false;
            try
            {
                if (this.Get(venta.Id) != null)
                {
                    ProductoVendidoServices.Instance().EliminarVenta(venta.Id);

                    this.Delete(venta.Id);

                }
            }
            catch (TheItemDoesNotExistException ex)
            {
                throw new TheItemDoesNotExistException(ex.Message);
            }
            return idExist;
        }

        public bool Delete(Int64 id)
        {
            bool idExist = false;
            if (this.Get(id) != null)
            {


                string deleteVentaSql = "DELETE FROM Venta WHERE Id = @Id";


                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmdVendido = new SqlCommand(deleteVentaSql, connection))
                    {
                        connection.Open();
                        cmdVendido.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt) { Value = id });
                        cmdVendido.ExecuteNonQuery();
                        connection.Close();
                    }

                }
                idExist = true;
            }
            return idExist;
        }

        public bool DeleteByIdUser(Int64 id)
        {
            bool idExist = false;
            if (this.Get(id) != null)
            {


                string deleteVentaSql = "DELETE FROM Venta WHERE IdUsuario = @Id";


                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmdVendido = new SqlCommand(deleteVentaSql, connection))
                    {
                        connection.Open();
                        cmdVendido.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt) { Value = id });
                        cmdVendido.ExecuteNonQuery();
                        connection.Close();
                    }

                }
                idExist = true;
            }
            return idExist;
        }

        public Venta Get(Int64 id)
        {

            Venta venta = null;
            string sql = $"SELECT * FROM {TABLE} WHERE id LIKE @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();

                cmd.CommandText = sql;
                cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    venta = new Venta
                    {
                        Id = reader.GetInt64(0),
                        Comentarios = reader.GetString(1),
                        IdUsuario = reader.GetInt64(2)
                    };
                }

                connection.Close();
                if (venta == null)
                {
                    throw new TheItemDoesNotExistException("No existe venta con ese id");
                }
            }
            return venta;
        }

        public List<Venta> GetAll()
        {
            List<Venta> ventas = new List<Venta>();
            string sql = $"SELECT * FROM {TABLE}";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Venta venta = new Venta
                    {
                        Id = reader.GetInt64(0),
                        Comentarios = reader.GetString(1),
                        IdUsuario = reader.GetInt64(2)
                    };
                    ventas.Add(venta);
                };

                connection.Close();
            }

            return ventas;
        }

        public List<Venta> GetVentasByIdUsuer(Int64 idUsuario)
        {
            List<Venta> ventas = new List<Venta>();
            string sql = $"SELECT * " +
                         $"FROM {TABLE} " +
                         $"WHERE IdUsuario = @IdUsuario";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = idUsuario });
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Venta venta = new Venta
                    {
                        Id = reader.GetInt64(0),
                        Comentarios = reader.GetString(1),
                        IdUsuario = reader.GetInt64(2)
                    };
                    ventas.Add(venta);
                };

                connection.Close();
            }

            return ventas;
        }

        public bool Update(Venta entity, Int64 id)
        {

            Venta venta = Get(id);

            string sql = $"UPDATE {TABLE} " +
                         $"SET " +
                         $"Comentarios = @Comentarios, " +
                         $"IdUsuario = @IdUsuario," +
                         $"WHERE Id like @id";

            if (venta != null)
            {

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        cmd.Parameters.Add(new SqlParameter("Comentarios", SqlDbType.Text) { Value = entity.Comentarios });
                        cmd.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.Text) { Value = entity.IdUsuario });
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                }

            }


            return venta != null;
        }



    }
}
