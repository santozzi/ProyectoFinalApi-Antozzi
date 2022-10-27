using System.Data.SqlClient;
using System.Data;
using ProyectoFinalAPI_Antozzi.Repository.Interfaces;
using ProyectoFinalAPI_Antozzi.Entities;

namespace ProyectoFinalAPI_Antozzi.Repository.SQLServer
{
    public class VentaModel : ConexionString, IVentaModel
    {
        private const string TABLE = "Venta";
        public Venta Add(Venta entity)
        {

            Int32 idUsuario = 0;
            string sql = $"INSERT " +
                         $"INTO {TABLE} (Comentarios, IdUsuario ) " +
                         $"VALUES (@Comentarios, @IdUsuario); " +
                         $"SELECT @@IDENTITY";


            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    cmd.Parameters.Add(new SqlParameter("Comentarios", SqlDbType.Text) { Value = entity.Comentarios });
                    cmd.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = entity.IdUsuario });

                    idUsuario = Convert.ToInt32(cmd.ExecuteScalar());
                    connection.Close();
                }
            }
            entity.Id = idUsuario;
            if (idUsuario == 0)
            {
                return null;
            }

            return entity;
        }

        public bool Delete(int id)
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

        public bool DeleteByIdUser(int id)
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

        public Venta Get(int id)
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

        public List<Venta> GetVentasByIdUsuer(int idUsuario)
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

        public bool Update(Venta entity, int id)
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
