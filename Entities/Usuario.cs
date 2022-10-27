namespace ProyectoFinalAPI_Antozzi.Entities
{
    public class Usuario
    {

        protected Int64 _id;
        protected string _nombre;
        protected string _apellido;
        protected string _nombreUsuario;
        protected string _contraseña;
        protected string _mail;

        public Usuario (string nombre, string apellido, string nombreUsuario, string contraseña, string mail)
        {
            this._nombre = nombre;
            this._apellido = apellido;
            this._nombreUsuario = nombreUsuario;
            this._contraseña = contraseña;
            this._mail = mail;
 
        }
        public Usuario() { }
        public Int64 Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }

        public string NombreUsuario
        {
            get { return _nombreUsuario; }
            set { _nombreUsuario = value; }
        }
        public string Contraseña
        {
            get { return _contraseña; }
            set { _contraseña = value; }
        }
        public string Mail
        {
            get { return _mail; }
            set { _mail = value; }
        }

        public override string ToString()
        {
            return $"Id: {this.Id} - Nombre: {this.Nombre} - Apellido: {this.Apellido} - NombreUsuario: {this.NombreUsuario} - Contraseña: {this.Contraseña} - Mail: {this.Mail}";
        }

    }
}
