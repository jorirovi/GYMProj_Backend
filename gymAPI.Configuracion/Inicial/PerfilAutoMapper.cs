using AutoMapper;
using gymAPI.Comunes.Classes.Contracts;
using gymAPI.Infraestructura.Database.Entidades;

namespace gymAPI.Configuracion.Inicial
{
    public class PerfilAutoMapper : Profile
    {
        public PerfilAutoMapper()
        {
            CreateMap<UsuariosEntity, UsuariosContract>().ReverseMap();
        }
    }
}