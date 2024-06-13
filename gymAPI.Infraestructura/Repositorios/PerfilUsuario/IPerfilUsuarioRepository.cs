using gymAPI.Infraestructura.Database.Entidades;

namespace gymAPI.Infraestructura.Repositorios.PerfilUsuario
{
    public interface IPerfilUsuarioRepository
    {
        Task<PerfilUsuarioEntity> GetPerfilByIDU(string idU);
    }
}