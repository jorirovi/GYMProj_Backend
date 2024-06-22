using gymAPI.Comunes.Classes.Contracts;

namespace gymAPI.Dominio.Service.GYM.DetalleRutinas
{
    public interface IDetalleRService
    {
        Task<List<DetalleRTDOContract>> GetDRByRutina(string idRutina);
        Task<List<DetalleRTDOContract>> GetDRByUsuario(string idUsuario);
        Task<List<DetalleRTDOContract>> GetDRByZonaCorporal(int zCorporal);
    }
}