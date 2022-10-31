namespace ProyectoFinalAPI_Antozzi.Repository.Exceptions
{
    public class InsufficientQuantityOfProductsException: Exception
    {
        public InsufficientQuantityOfProductsException(string msg) : base(msg) { }
    }
}
