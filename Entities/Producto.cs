using System;


namespace ProyectoFinalAPI_Antozzi.Entities
{
    public class Producto
    {
        protected Int64 id;
        protected string descripcion;
        protected double costo;
        protected double precioDeVenta;
        protected int stock;
        protected Int64 idUsuario;
        public Producto(string descripcion, double costo, double precioDeVenta, int stock, int idUsuario)
        {

            this.descripcion = descripcion;
            this.costo = costo;
            this.precioDeVenta = precioDeVenta;
            this.stock = stock;
            this.idUsuario = idUsuario;
        }
        public Producto() { }
        public Int64 Id 
        { 
            get { return id; } 
            set { id = value; } 
        }
        
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
       

        public double Costo
        {
            get { return costo; }
            set { costo = value; }
        }
      

        public double PrecioDeVenta
        {
            get { return precioDeVenta; }
            set { precioDeVenta = value; }
        }

       

        public int Stock
        {
            get { return stock; }
            set { stock = value; }
        }

     

        public Int64 IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }

        public override string ToString() {
            string cadena = $"id: {this.Id} Descripcion: {this.Descripcion} Costo: {this.Costo} Venta: {this.PrecioDeVenta} Stock: {this.Stock}";

            return cadena;
        }
    }
}
