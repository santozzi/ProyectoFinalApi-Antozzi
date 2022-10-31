using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAPI_Antozzi.Entities
{
    public class Venta
    {
        protected Int64 _id;
        protected string _comentarios;
        protected Int64 _idUsuario;
        public Venta(string comentarios,Int64 idUsuario) {
            _idUsuario = idUsuario;
            _comentarios = comentarios;
        }
        public Venta() { }
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
            return $"id: {_id} - comentarios: {_comentarios} - idUsuario: {_idUsuario}";
        }
    }
}
