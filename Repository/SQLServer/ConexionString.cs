namespace ProyectoFinalAPI_Antozzi.Repository.SQLServer
{
    public abstract class ConexionString
    {
        protected string _connectionString = "Server=localhost;" +
                                     "Database=SistemaGestion;" +
                                     "Trusted_Connection=True;";
        
    }
}
