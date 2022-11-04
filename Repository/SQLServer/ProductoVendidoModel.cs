using System.Data.SqlClient;
using System.Data;
using ProyectoFinalAPI_Antozzi.Repository.Interfaces;
using ProyectoFinalAPI_Antozzi.Services;
using ProyectoFinalAPI_Antozzi.Entities;
using ProyectoFinalAPI_Antozzi.Repository.Exceptions;

namespace ProyectoFinalAPI_Antozzi.Repository.SQLServer
{
    public class ProductoVendidoModel : ConexionString, IProductoVendido
    {
        private const string TABLE = "ProductoVendido";

        //asumo que el id esta verificado
        public void EliminarVenta(Int64 id)
        {
           
            string sql = "SELECT * FROM ProductoVendido WHERE IdVenta = @IdVenta";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    cmd.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.BigInt) { Value = id });
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ProductoVendido productoVendido = new ProductoVendido();
                        productoVendido.Id = reader.GetInt64(0);
                        productoVendido.Stock = reader.GetInt32(1);
                        productoVendido.IdProducto = reader.GetInt64(2);
                        productoVendido.IdVenta = reader.GetInt64(3);
                        try
                        {
                            ProductoServices.Instance().AddProductStock(productoVendido.IdProducto, productoVendido.Stock);
                            this.Delete(productoVendido.Id);
                        }
                        catch(TheItemDoesNotExistException ex) {
                            throw new TheItemDoesNotExistException(ex.Message);
                        }

                    }
                    connection.Close();
                }
            }
        }
        public ProductoVendido Add(ProductoVendido entity)
        {
            Int64 idProductoVendido = 0;
            string sql = $"INSERT " +
                         $"INTO {TABLE} (Stock, IdProducto, IdVenta) " +
                         $"VALUES (@Stock, @IdProducto, @IdVenta); " +
                         $"SELECT @@IDENTITY";

            using (SqlConnection connection = new SqlConnection(_connectionString))

            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                connection.Open();
                cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int) { Value = entity.Stock });
                cmd.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.BigInt) { Value = entity.IdProducto });
                cmd.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.BigInt) { Value = entity.IdVenta });
                idProductoVendido = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            ProductoVendido productoVendido = new ProductoVendido
            {
                Id = idProductoVendido,
                IdProducto = entity.IdProducto,
                Stock = entity.Stock,
                IdVenta = entity.IdVenta
            };

            if (idProductoVendido == 0)
            {
                return null;
            }

            return productoVendido;
        }

        public bool Delete(Int64 id)
        {


            bool idExist = false;
            if (this.Get(id) != null)
            {


                string deleteUsuarioSql = $"DELETE FROM {TABLE} WHERE Id = @Id";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {

                    using (SqlCommand cmdUsuario = new SqlCommand(deleteUsuarioSql, connection))
                    {
                        connection.Open();
                        cmdUsuario.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt) { Value = id });
                        cmdUsuario.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                idExist = true;
            }
            return idExist;
        }

        public ProductoVendido Get(Int64 id)
        {
            ProductoVendido productoVendido = null;
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
                    productoVendido = new ProductoVendido
                    {
                        Id = reader.GetInt64(0),
                        Stock = reader.GetInt32(1),
                        IdProducto = reader.GetInt64(2),
                        IdVenta = reader.GetInt64(3)
                    };
                }
                connection.Close();
            }
            return productoVendido;
        }

        public List<ProductoVendido> GetAll()
        {
            List<ProductoVendido> productoVendidos = new List<ProductoVendido>();
            string sql = $"SELECT * FROM {TABLE}";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ProductoVendido productoVendido = new ProductoVendido
                    {
                        Id = reader.GetInt64(0),
                        Stock = reader.GetInt32(1),
                        IdProducto = reader.GetInt64(2),
                        IdVenta = reader.GetInt64(3)
                    };
                    productoVendidos.Add(productoVendido);

                }
                connection.Close();

            }
            return productoVendidos;
        }
        public List<ProductoVendido> GetByIdUsuario(Int64 id)
        {
            List<ProductoVendido> productosVendidos = new List<ProductoVendido>();
            string sql = $"SELECT " +

                         $"   ProductoVendido.Id, " +
                         $"   ProductoVendido.Stock, " +
                         $"   ProductoVendido.IdProducto, " +
                         $"   ProductoVendido.IdVenta " +
                         $"FROM ProductoVendido " +
                       
                         $"INNER JOIN Venta ON ProductoVendido.IdVenta = Venta.Id " +
                         $"WHERE Venta.IdUsuario = @Id ";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                cmd.CommandText = sql;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ProductoVendido productoVendido = new ProductoVendido();


                    productoVendido.Id = reader.GetInt64(0);
                    productoVendido.Stock = reader.GetInt32(1);
                    productoVendido.IdProducto = reader.GetInt64(2);
                    productoVendido.IdVenta = reader.GetInt64(3);
                    productosVendidos.Add(productoVendido);

                }
                connection.Close();

            }
            return productosVendidos;
        }
        /*
        public List<Producto> GetByIdUsuario(Int64 id)
        {
            List<Producto> productosVendidos = new List<Producto>();
            string sql = $"SELECT " +
            
                         $"   Producto.Id, " +
                         $"   Producto.Descripciones, " +
                         $"   Producto.Costo, " +
                         $"   Producto.PrecioVenta, " +
                         $"   Producto.Stock, " +
                         $"   Producto.IdUsuario " +
                        
                         $"FROM ProductoVendido " +
                         $"INNER JOIN Producto  ON ProductoVendido.IdProducto = Producto.Id " +
                         $"INNER JOIN Venta ON ProductoVendido.IdVenta = Venta.Id " +
                         $"WHERE Venta.IdUsuario = @Id ";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                cmd.CommandText = sql;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Producto productoVendido = new Producto();

                
                    productoVendido.Id = reader.GetInt64(0);
                    productoVendido.Descripciones = reader.GetString(1);
                    productoVendido.Costo = (double)reader.GetDecimal(2);
                    productoVendido.PrecioVenta = (double)reader.GetDecimal(3);
                    productoVendido.Stock = reader.GetInt32(4);
                    productoVendido.IdUsuario = reader.GetInt64(5);
                    productosVendidos.Add(productoVendido);

                }
                connection.Close();

            }
            return productosVendidos;
        }
        */
        /*
         public List<Producto> GetByUserName(string userName)
         {
             List<Producto> productos = new List<Producto>();
             Int64 id = UsuarioServices.Instance().GetIdByNombreUsuario(userName);
             if (id > 0)
             {
                 productos = this.GetByIdUsuario(id);
             }
             return productos;

         }
        */
        public bool Update(ProductoVendido entity, Int64 id)
        {
            ProductoVendido productoVendido = this.Get(id);


            string sql = $"UPDATE {TABLE} " +
                         $"SET " +
                         $"Stock = @Stock, " +
                         $"IdProducto = @IdProducto, " +
                         $"IdVenta = @IdVenta," +
                         $"WHERE Id like @id";


            if (productoVendido != null)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int) { Value = entity.Stock });
                        cmd.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.BigInt) { Value = entity.IdProducto });
                        cmd.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.BigInt) { Value = entity.IdVenta });
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }



            return productoVendido != null;
        }
    }

}
