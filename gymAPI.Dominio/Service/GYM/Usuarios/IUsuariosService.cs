using gymAPI.Infraestructura.Repositorios.Usuarios;

namespace gymAPI.Dominio.Service.GYM.Usuarios
{
    public interface IUsuariosService
    {
        Task<UsuarioRepository> UpdatePass(string email);
    }
}