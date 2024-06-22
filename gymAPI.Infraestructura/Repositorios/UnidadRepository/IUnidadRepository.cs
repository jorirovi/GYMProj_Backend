using gymAPI.Infraestructura.Database.Entidades;

namespace gymAPI.Infraestructura.Repositorios.UnidadRepository
{
    public interface IUnidadRepository
    {
        Task<UnidadPesoEntity> GetUnidadByNumero(int numero);
        Task<UnidadPesoEntity> GetUnidadByName(string name);
    }
}