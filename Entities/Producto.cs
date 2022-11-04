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
        
        public string Descripciones
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
       

        public double Costo
        {
            get { return costo; }
            set { costo = value; }
        }
      

        public double PrecioVenta
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
            string cadena = $"id: {this.Id} Descripcion: {this.Descripciones} Costo: {this.Costo} Venta: {this.PrecioVenta} Stock: {this.Stock}";

            return cadena;
        }
    }
}
