using ProyectoFinalAPI_Antozzi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAPI_Antozzi.Repository.Interfaces
{
    public interface IProductoVendido: ICRUD<ProductoVendido>
    {
        /// <summary>
        ///    Se ingresa por parametro el NombreUsuario y devuelve una 
        ///    lista con los productos vendidos por ese esuario
        /// </summary>
       // List<Producto> GetByUserName(string userName);
        /// <summary>
        ///    Se ingresa por parametro el Id del usuario y devuelve una 
        ///    lista con los productos vendidos por ese esuario
        /// </summary>
        List<ProductoVendido> GetByIdUsuario(Int64 userName);
        /// <summary>
        ///  Elimina todas las ventas y suma el stock del producto al producto correspondiente
        /// </summary>
        /// <param name="id"></param>
        public void EliminarVenta(Int64 id);
    }
}
