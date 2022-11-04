namespace ProyectoFinalAPI_Antozzi.Repository.Exceptions
{
    public class AuthorizationErrorException: Exception
    {
        public AuthorizationErrorException(string msg) : base(msg) { }
    }
}
