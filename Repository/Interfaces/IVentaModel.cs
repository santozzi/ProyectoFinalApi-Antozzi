using ProyectoFinalAPI_Antozzi.Entities;


namespace ProyectoFinalAPI_Antozzi.Repository.Interfaces
{
    public interface IVentaModel: ICRUD<Venta>
    {
        /// <summary>
        ///    Elimina la venta por el id de ususario y todas sus dependencias, devuelve true si el id existe y false de lo contrario.
        /// </summary>
        bool DeleteByIdUser(int id);

        /// <summary>
        ///    Devuelve una lista de ventas segun IdUsuario
        /// </summary>
        List<Venta> GetVentasByIdUsuer(int idUsuario);
    }
}
