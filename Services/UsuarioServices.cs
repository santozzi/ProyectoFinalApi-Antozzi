﻿using ProyectoFinalAPI_Antozzi.Entities;
using ProyectoFinalAPI_Antozzi.Repository.Interfaces;
using ProyectoFinalAPI_Antozzi.Repository.SQLServer;


namespace ProyectoFinalAPI_Antozzi.Services
{
    public class UsuarioServices
    {
        protected readonly IUsuarioModel _usuarioModel;
        protected static UsuarioServices _instance;
        private UsuarioServices() {
           
            _usuarioModel = new UsuarioModel();

        }
        public static UsuarioServices Instance() {
            if (_instance == null)
            {
                _instance = new UsuarioServices();
            }
            return _instance;

        }
        public bool isExistUsusario(Int64 id) { 
           return _usuarioModel.isExistUsuario(id);
        }
        public Usuario IsUsernameAndPassword(string userName, string password) { 
         return _usuarioModel.IsUsernameAndPassword(userName, password);
        }

        public List<Usuario> GetAll() {
            return _usuarioModel.GetAll();
        }

        public Usuario GetByNombreUsuario(string nombreUsuario) { 
          return _usuarioModel.GetByNombreUsuario(nombreUsuario);
        }
        public long GetIdByNombreUsuario(string nombreUsuario) {
            return _usuarioModel.GetIdByNombreUsuario(nombreUsuario);
        }
        public Usuario Add(Usuario entity) { 
           return _usuarioModel.Add(entity);
        }
        public bool Update(Usuario entity, Int64 id) {
            return _usuarioModel.Update(entity, id);
        }


        public bool Delete(Int64 id) {
            return _usuarioModel.Delete(id);
        }
        public Usuario Get(Int64 id) { 
          return _usuarioModel.Get(id);
        }
    }
}
