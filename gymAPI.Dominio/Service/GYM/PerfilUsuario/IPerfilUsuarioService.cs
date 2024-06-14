using gymAPI.Comunes.Classes.Contracts;

namespace gymAPI.Dominio.Service.GYM.PerfilUsuario
{
    public interface IPerfilUsuarioService
    {
        Task<List<PerfilUTDOContract>> GetAllProfiles();
        Task<PerfilUTDOContract> GetProfileByID(string id);
    }
}