using gymAPI.Infraestructura.Database.Entidades;

namespace gymAPI.Infraestructura.Repositorios.ZonaCorporal
{
    public interface IZonaCorporalRepository
    {
        Task<ZonaCorporalEntity> GetByZC(string zonaCorporal);
        Task<ZonaCorporalEntity> GetByNZC(int numero);
    }
}