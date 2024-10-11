using gymAPI.Infraestructura.Database.Entidades;

namespace gymAPI.Infraestructura.Repositorios.Vault
{
    public interface IVaultRepository
    {
        Task<vaultEntity> GetVaultByTitle(string title);
    }
}