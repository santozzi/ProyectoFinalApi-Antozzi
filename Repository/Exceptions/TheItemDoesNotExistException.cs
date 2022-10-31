namespace ProyectoFinalAPI_Antozzi.Repository.Exceptions
{
    public class TheItemDoesNotExistException: Exception
    {
        public TheItemDoesNotExistException(string msg) : base(msg) { }
    }
}
