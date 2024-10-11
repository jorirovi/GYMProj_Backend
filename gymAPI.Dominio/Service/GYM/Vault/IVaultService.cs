using gymAPI.Comunes.Classes.Contracts;

namespace gymAPI.Dominio.Service.GYM.Vault
{
    public interface IVaultService
    {
        Task<EliminarContract> RemoveWAnswer(string id);
    }
}