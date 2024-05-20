using gymAPI.Infraestructura.Database.Entidades;

namespace gymAPI.Infraestructura.Repositorios
{
    public interface IUsuarioRepository
    {
        Task<UsuariosEntity> GetUsuarioByEmail(string email);
        Task<UsuariosEntity> GetUserByEmailPass(string email, string password);
    }
}