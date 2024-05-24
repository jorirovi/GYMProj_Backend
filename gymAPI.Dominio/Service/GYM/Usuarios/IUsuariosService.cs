using gymAPI.Comunes.Classes.Contracts;
using gymAPI.Infraestructura.Repositorios.Usuarios;

namespace gymAPI.Dominio.Service.GYM.Usuarios
{
    public interface IUsuariosService
    {
        Task<UsuariosContract> UpdatePass(string email, string newPass);
    }
}