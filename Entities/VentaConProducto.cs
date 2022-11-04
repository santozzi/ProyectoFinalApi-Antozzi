namespace ProyectoFinalAPI_Antozzi.Entities
{
    public class VentaConProducto:Producto
    {
        protected Int64 _id;
        protected string _comentarios;
        protected Int64 _idUsuario;


        public Int64 Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Comentarios
        {
            get { return _comentarios; }
            set { _comentarios = value; }
        }
        public Int64 IdUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }
        public override string ToString()
        {
            return $"id: {_id} - comentarios: {_comentarios} - idUsuario: {_idUsuario} - descripciones: {this.Comentarios} - stock: {this.Stock} ";
        }
    }
}
