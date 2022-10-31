using ProyectoFinalAPI_Antozzi.Entities;
using ProyectoFinalAPI_Antozzi.Repository.Exceptions;
using ProyectoFinalAPI_Antozzi.Repository.Interfaces;
using ProyectoFinalAPI_Antozzi.Services;
using System.Data;
using System.Data.SqlClient;


namespace ProyectoFinalAPI_Antozzi.Repository.SQLServer
{
    public class UsuarioModel : ConexionString, IUsuarioModel
    {
        private const string TABLE = "Usuario";

        public Usuario Add(Usuario entity)
        {

            Int32 idUsuario = 0;
            string sql = $"INSERT " +
                         $"INTO Usuario (Nombre, Apellido, NombreUsuario, Contraseña, Mail ) " +
                         $"VALUES (@Nombre, @Apellido, @NombreUsuario, @Contraseña, @Mail); " +
                         $"SELECT @@IDENTITY";

            if (this.GetByNombreUsuario(entity.NombreUsuario) == null)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        cmd.Parameters.Add(new SqlParameter("Nombre", SqlDbType.Text) { Value = entity.Nombre });
                        cmd.Parameters.Add(new SqlParameter("Apellido", SqlDbType.Text) { Value = entity.Apellido });
                        cmd.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.Text) { Value = entity.NombreUsuario });
                        cmd.Parameters.Add(new SqlParameter("Contraseña", SqlDbType.Text) { Value = entity.Contraseña });
                        cmd.Parameters.Add(new SqlParameter("Mail", SqlDbType.Text) { Value = entity.Mail });
                        idUsuario = Convert.ToInt32(cmd.ExecuteScalar());
                        connection.Close();
                    }
                }
                entity.Id = idUsuario;
                if (idUsuario == 0)
                {
                    return null;
                }
            }
            return entity;
        }

        public bool Delete(Int64 id)
        {
            bool idExist = false;
            if (this.Get(id) != null)
            {
                //borrado en cascada
                //borro los usuarios de prodcutos
                IProductoModel model = new ProductoModel();
                List<Producto> productos = model.GetByIdUsuario(id);
                foreach (Producto producto in productos)
                {
                    if (producto.IdUsuario == id)
                    {
                        model.Delete(Convert.ToInt32(producto.Id));
                    }
                }

                // borro las ventas que contienen a usuarios
                VentaServices.Instance().DeleteByIdUser(id);
                //borro los usuarios sin dependencias
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

        public Usuario Get(Int64 id)
        {
            Usuario usuario = null;
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
                    usuario = new Usuario();
                    usuario.Id = reader.GetInt64(0);
                    usuario.Nombre = reader.GetString(1);
                    usuario.Apellido = reader.GetString(2);
                    usuario.NombreUsuario = reader.GetString(3);
                    usuario.Contraseña = reader.GetString(4);
                    usuario.Mail = reader.GetString(5);


                }

                connection.Close();

            }

            return usuario;
        }

        public List<Usuario> GetAll()
        {
            List<Usuario> usuarios = new List<Usuario>();
            string sql = $"SELECT * FROM {TABLE}";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = reader.GetInt64(0);
                    usuario.Nombre = reader.GetString(1);
                    usuario.Apellido = reader.GetString(2);
                    usuario.NombreUsuario = reader.GetString(3);
                    usuario.Contraseña = reader.GetString(4);
                    usuario.Mail = reader.GetString(5);
                    usuarios.Add(usuario);

                }
                connection.Close();

            }
            return usuarios;
        }

        public Usuario GetByNombreUsuario(string nombreUsuario)
        {
            Usuario usuario = null;
            string sql = $"SELECT * FROM {TABLE} WHERE NombreUsuario LIKE @NombreUsuario";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();

                cmd.CommandText = sql;
                cmd.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.Text) { Value = nombreUsuario });
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    usuario = new Usuario
                    {
                        Id = reader.GetInt64(0),
                        Nombre = reader.GetString(1),
                        Apellido = reader.GetString(2),
                        NombreUsuario = reader.GetString(3),
                        Contraseña = reader.GetString(4),
                        Mail = reader.GetString(5),
                    };
                }

                connection.Close();

            }
            return usuario;
        }

        public long GetIdByNombreUsuario(string nombreUsuario)
        {
            Int64 idUsuario = 0;
            string sql = $"SELECT Id " +
                         $"FROM {TABLE} " +
                         $"WHERE NombreUsuario = @NombreUsuario";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    cmd.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = nombreUsuario });

                    idUsuario = Convert.ToInt64(cmd.ExecuteScalar());
                    connection.Close();
                }
            }

            return idUsuario;
        }

        public bool isExistUsuario(long id)
        {
            Usuario usuarioEncontrado = this.Get(id);
            return usuarioEncontrado != null;
        }

        public Usuario IsUsernameAndPassword(string username, string password)
        {
            Usuario usuarioARetornar = new Usuario();

            string sql = $"SELECT * " +
                         $"FROM {TABLE} " +
                         $"WHERE NombreUsuario = @NombreUsuario " +
                         $"AND Contraseña = @Contraseña";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();

                cmd.CommandText = sql;
                cmd.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = username });
                cmd.Parameters.Add(new SqlParameter("Contraseña", SqlDbType.VarChar) { Value = password });
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                    usuarioARetornar.Id = reader.GetInt64(0);
                    usuarioARetornar.Nombre = reader.GetString(1);
                    usuarioARetornar.Apellido = reader.GetString(2);
                    usuarioARetornar.NombreUsuario = reader.GetString(3);
                    usuarioARetornar.Contraseña = reader.GetString(4);
                    usuarioARetornar.Mail = reader.GetString(5);

                }

                connection.Close();

            }



            return usuarioARetornar;
        }

        public bool Update(Usuario entity, Int64 id)
        {
            bool seActualizo = false;


            Usuario usuario = Get(id);

            string sql = $"UPDATE {TABLE} " +
                         $"SET " +
                         $"Nombre = @Nombre, " +
                         $"Apellido = @Apellido," +
                         $"NombreUsuario = @NombreUsuario," +
                         $"Contraseña = @Contraseña, " +
                         $"Mail = @Mail " +
                         $"WHERE Id like @id";

            if (usuario != null)
            {
                Usuario usuarioAEditar = this.GetByNombreUsuario(entity.NombreUsuario);
                if (
                    usuarioAEditar == null ||
                    usuarioAEditar != null && usuarioAEditar.Id == id
                    )
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {

                        using (SqlCommand cmd = new SqlCommand(sql, connection))
                        {
                            connection.Open();
                            cmd.Parameters.Add(new SqlParameter("Nombre", SqlDbType.Text) { Value = entity.Nombre });
                            cmd.Parameters.Add(new SqlParameter("Apellido", SqlDbType.Text) { Value = entity.Apellido });
                            cmd.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.Text) { Value = entity.NombreUsuario });
                            cmd.Parameters.Add(new SqlParameter("Contraseña", SqlDbType.Text) { Value = entity.Contraseña });
                            cmd.Parameters.Add(new SqlParameter("Mail", SqlDbType.Text) { Value = entity.Mail });
                            cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });



                            cmd.ExecuteNonQuery();


                            connection.Close();
                        }
                    }
                    seActualizo = true;
                }

            }

            return seActualizo;
        }

    }

}
