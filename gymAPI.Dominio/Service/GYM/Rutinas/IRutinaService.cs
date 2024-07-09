using gymAPI.Comunes.Classes.Contracts;

namespace gymAPI.Dominio.Service.GYM.Rutinas
{
    public interface IRutinaService
    {
        Task<EliminarContract> RemoveWAnswer(string id);
    }
}