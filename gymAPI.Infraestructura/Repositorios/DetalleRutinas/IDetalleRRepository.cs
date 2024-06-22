using gymAPI.Infraestructura.Database.Entidades;

namespace gymAPI.Infraestructura.Repositorios.DetalleRutinas
{
    public interface IDetalleRRepository
    {
        Task<List<DetalleRutinasEntity>> GetDRbyIdRutina (string idRutina);
        Task<List<DetalleRutinasEntity>> GetDRByIdUsuario (string idUsuario);
        Task<List<DetalleRutinasEntity>> GetDRbyZonaCorporal (int zCorporal);
    }
}