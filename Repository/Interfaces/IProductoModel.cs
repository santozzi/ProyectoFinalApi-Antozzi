using ProyectoFinalAPI_Antozzi.Entities;


namespace ProyectoFinalAPI_Antozzi.Repository.Interfaces
{
    public interface IProductoModel : ICRUD<Producto>
    {
        /// <summary>
        ///    Se ingresa por parametro el Id del usuario y devuelve la 
        ///    lista con productos comprados por el usuario
        /// </summary>
        List<Producto> GetByIdUsuario(Int64 id);
        /// <summary>
        ///  Quita cantidad quantity de productos del stock de producto
        ///  lanza InsufficientQuantityOfProductsException de no haber la cantidad suficiente de productos para descontar
        ///   
        /// </summary>
        /// <param name="producto"></param>
        /// <param name="quantity"></param>
        void SubstractProductStock(Int64 idProducto, int quantity);
     
    }
}
