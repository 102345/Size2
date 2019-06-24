using Size.ContaDigital.DAL.Repositories;
using Size.ContaDigital.Model;
using Size.ContaDigital.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Size.ContaDigital.Service
{
    public class UserService : IUserService
    {
        public User BuscarUsuario(string login, string senha)
        {

            List<User> users = new UserRepository().GetAll().ToList();

            return users.Find(a => a.Login == login && a.Senha == senha);
        }

        public bool ExisteUsuario(string login, string senha)
        {
            bool ret = false;

            List<User> users = new UserRepository().GetAll().ToList();

            var user = users.Find(a => a.Login == login && a.Senha == senha);

            if (user != null) ret = true;

            return ret;

        }
    }
}
