using ProyectoFinalAPI_Antozzi.Entities;


namespace ProyectoFinalAPI_Antozzi.Repository.Interfaces
{
    public interface IProductoModel : ICRUD<Producto>
    {
        /// <summary>
        ///    Se ingresa por parametro el Id del usuario y devuelve la 
        ///    lista con productos comprados por el usuario
        /// </summary>
        List<Producto> GetByIdUsuario(int id);
       
    }
}
