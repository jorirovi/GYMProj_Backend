using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gymAPI.Infraestructura.Database.Entidades;

namespace gymAPI.Infraestructura.Repositorios.Rutinas
{
    public interface IRutinaRepository
    {
        Task<RutinasEntity> GetByRutina(string rutina);
    }
}