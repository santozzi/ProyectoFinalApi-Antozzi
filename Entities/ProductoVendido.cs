using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAPI_Antozzi.Entities
{
    public class ProductoVendido
    {
        protected Int64 _id;
        protected Int64 _idProducto;
        protected int _stock;
        protected Int64 _idVenta;
        public ProductoVendido() { }
        public ProductoVendido(long idProducto, int stock, long idVenta)
        {
          
            IdProducto = idProducto;
            Stock = stock;
            IdVenta = idVenta;

        }

        public Int64 Id
        {
            get { return _id; }
            set { _id = value; }
        }        
        
        public Int64 IdProducto
        {
            get { return _idProducto; }
            set { _idProducto = value; }
        }        
        
        public int Stock
        {
            get { return _stock; }
            set { _stock = value; }
        }    
        public Int64 IdVenta
        {
            get { return _idVenta; }
            set { _idVenta = value; }
        }
        public override string ToString()
        {
            return $"Id: {_id} - IdProducto: {_idProducto} - Stock: {_stock} - IdVenta: {_idVenta}";
        }
    }
}
