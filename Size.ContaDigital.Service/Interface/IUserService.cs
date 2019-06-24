using Size.ContaDigital.Model;

namespace Size.ContaDigital.Service.Interface
{
    public interface IUserService
    {
        bool ExisteUsuario(string login, string senha);
        User BuscarUsuario(string login, string senha); 
    }
}
