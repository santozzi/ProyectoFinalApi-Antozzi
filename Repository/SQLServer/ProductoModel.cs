using ProyectoFinalAPI_Antozzi.Entities;
using ProyectoFinalAPI_Antozzi.Repository.Exceptions;
using ProyectoFinalAPI_Antozzi.Repository.Interfaces;
using ProyectoFinalAPI_Antozzi.Services;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinalAPI_Antozzi.Repository.SQLServer
{
    public class ProductoModel : ConexionString, IProductoModel
    {
     


        public Producto Add(Producto entity)
        {

            Int64 idproducto = 0;
            string sql = $"INSERT " +
                         $"INTO Producto (Descripciones, Costo, PrecioVenta, Stock, IdUsuario ) " +
                         $"VALUES (@Descripcion, @Costo,@PrecioDeVenta, @Stock, @IdUsuario); " +
                         $"SELECT @@IDENTITY";
            if (entity.Costo <= 0) {
                throw new InvalidParametersException("El costo del producto debe ser mayor que 0");
            }
            if (entity.PrecioDeVenta <= 0) {
                throw new InvalidParametersException("El precio de venta debe ser mayor que 0");
            }
            if (entity.Stock <= 0) {
                throw new InvalidParametersException("El stock debe ser mayor que 0");
            }

            if (UsuarioServices.Instance().isExistUsusario(entity.IdUsuario))
            {

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {





                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        cmd.Parameters.Add(new SqlParameter("Descripcion", SqlDbType.Text) { Value = entity.Descripcion });
                        cmd.Parameters.Add(new SqlParameter("Costo", SqlDbType.Decimal) { Value = entity.Costo });
                        cmd.Parameters.Add(new SqlParameter("PrecioDeVenta", SqlDbType.Decimal) { Value = entity.PrecioDeVenta });
                        cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int) { Value = entity.Stock });

                        cmd.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.Int) { Value = entity.IdUsuario });
                        idproducto = Convert.ToInt64(cmd.ExecuteScalar());
                        connection.Close();
                        entity.Id = idproducto;

                    }
                }


                Console.WriteLine("hola soy sergio");
            }
            else {
                throw new TheItemDoesNotExistException("El el usuario que realiza la compra no existe");
            }

            return entity;
        }

        //Borra el producto por id y su dependencia en ProductoVendido usar con precaución, se recomienda hacer un backup de la base antes de utilizar este metodo.
        public bool Delete(Int64 id)
        {
            bool idExist = false;
            try
            {
                if (this.Get(id) != null)
                {
                    string deleteProductoVendidoSql = "DELETE FROM ProductoVendido WHERE IdProducto = @IdProducto";
                    string deleteProductoSql = "DELETE FROM Producto WHERE Id = @Id";
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmdProductoVendido = new SqlCommand(deleteProductoVendidoSql, connection))
                        {
                            connection.Open();
                            cmdProductoVendido.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.BigInt) { Value = id });
                            cmdProductoVendido.ExecuteNonQuery();
                            connection.Close();
                        }
                        using (SqlCommand cmdProducto = new SqlCommand(deleteProductoSql, connection))
                        {
                            connection.Open();
                            cmdProducto.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt) { Value = id });
                            cmdProducto.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                    idExist = true;
                }
            }
            catch (TheItemDoesNotExistException ex)
            {
                throw new TheItemDoesNotExistException(ex.Message);
            }
            return idExist;
        }


        public Producto Get(Int64 id)
        {
            Producto producto = null;
            string sql = $"SELECT * FROM Producto WHERE id LIKE @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();

                cmd.CommandText = sql;
                cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    producto = new Producto();
                    producto.Id = reader.GetInt64(0);
                    producto.Descripcion = reader.GetString(1);
                    producto.Costo = (double)reader.GetDecimal(2);
                    producto.PrecioDeVenta = (double)reader.GetDecimal(3);
                    producto.Stock = reader.GetInt32(4);
                    producto.IdUsuario = reader.GetInt64(5);


                }
                if (producto == null)
                {
                    throw new TheItemDoesNotExistException($"El id del producto ingresado no existe.");
                }
                connection.Close();

            }
            return producto;
        }

        public List<Producto> GetAll()
        {
            List<Producto> productos = new List<Producto>();
            string sql = "SELECT * FROM producto";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Producto producto = new Producto();
                    producto.Id = reader.GetInt64(0);
                    producto.Descripcion = reader.GetString(1);
                    producto.Costo = (double)reader.GetDecimal(2);
                    producto.PrecioDeVenta = (double)reader.GetDecimal(3);
                    producto.Stock = reader.GetInt32(4);
                    producto.IdUsuario = reader.GetInt64(5);
                    productos.Add(producto);

                }
                connection.Close();

            }
            return productos;
        }

        public List<Producto> GetByIdUsuario(Int64 id)
        {
            List<Producto> productos = new List<Producto>();
            string sql = "SELECT * FROM Producto WHERE IdUsuario = @IdUsuario";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    cmd.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = id });
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Producto producto = new Producto();
                        producto.Id = reader.GetInt64(0);
                        producto.Descripcion = reader.GetString(1);
                        producto.Costo = (double)reader.GetDecimal(2);
                        producto.PrecioDeVenta = (double)reader.GetDecimal(3);
                        producto.Stock = reader.GetInt32(4);
                        producto.IdUsuario = reader.GetInt64(5);
                        productos.Add(producto);

                    }
                    connection.Close();
                }
            }
            return productos;
        }

        public void SubstractProductStock(Int64 idProducto, int quantity)
        {
            try
            {
             
                    Producto productoBase = this.Get(idProducto);
                    if (productoBase.Stock < quantity)
                    {
                        throw new InsufficientQuantityOfProductsException($"El producto {productoBase.Descripcion} no tiene suficiente Stock para realizar la venta");
                    }
                    else
                    {
                        productoBase.Stock -= quantity;
                        this.Update(productoBase, productoBase.Id);
                    }
                



            }
            catch (TheItemDoesNotExistException ex)
            {
                throw new TheItemDoesNotExistException(ex.Message);
            }
        }

        public bool Update(Producto entity, Int64 id)
        {
            bool seActualizo = false;

            try
            {
                Producto producto = Get(id);



                string sql = $"UPDATE Producto " +
                             $"SET " +
                             $"Descripciones = @Descripciones, " +
                             $"Costo = @Costo, " +
                             $"PrecioVenta = @PrecioVenta, " +
                             $"Stock = @Stock, " +
                             $"IdUsuario = @IdUsuario " +
                             $"WHERE Id like @id";

                if (producto != null)
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {



                        using (SqlCommand cmd = new SqlCommand(sql, connection))
                        {
                            connection.Open();
                            cmd.Parameters.Add(new SqlParameter("Descripciones", SqlDbType.Text) { Value = entity.Descripcion });
                            cmd.Parameters.Add(new SqlParameter("Costo", SqlDbType.Decimal) { Value = entity.Costo });
                            cmd.Parameters.Add(new SqlParameter("PrecioVenta", SqlDbType.Decimal) { Value = entity.PrecioDeVenta });
                            cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int) { Value = entity.Stock });
                            cmd.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = entity.IdUsuario });
                            cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });

                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();


                            connection.Close();
                        }
                    }
                    seActualizo = true;
                }
            }
            catch (TheItemDoesNotExistException ex)
            {
                throw new TheItemDoesNotExistException(ex.Message);
            }
            return seActualizo;
        }
    }
}
