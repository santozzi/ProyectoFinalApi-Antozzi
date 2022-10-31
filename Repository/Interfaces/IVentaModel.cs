using ProyectoFinalAPI_Antozzi.Entities;


namespace ProyectoFinalAPI_Antozzi.Repository.Interfaces
{
    public interface IVentaModel: ICRUD<Venta>
    {
        /// <summary>
        ///    Elimina la venta por el id de ususario y todas sus dependencias, devuelve true si el id existe y false de lo contrario.
        /// </summary>
        bool DeleteByIdUser(Int64 id);

        /// <summary>
        ///    Devuelve una lista de ventas segun IdUsuario
        /// </summary>
        List<Venta> GetVentasByIdUsuer(Int64 idUsuario);

        /// <summary>
        ///   Agrega todas las ventas del usuario recibido en venta, de los productos recibidos por produductosVendidos
        ///   y descuenta el stock los productos de la tabla Producto.
        ///   
        /// </summary>
        /// <param name="venta"></param>
        /// <param name="productosVendidos"></param>
        /// <returns>
        ///  Devuelve true si, los productos existen y tienen stock suficiente para la venta, y false de lo contrario
        /// </returns>
        bool Add(Venta venta, List<Producto> productosVendidos);
    }
}
