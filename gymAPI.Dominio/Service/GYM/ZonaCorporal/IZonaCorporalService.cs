using gymAPI.Comunes.Classes.Contracts;

namespace gymAPI.Dominio.Service.GYM.ZonaCorporal
{
    public interface IZonaCorporalService
    {
        Task<EliminarContract> RemoveWMensaje(string id);
    }
}