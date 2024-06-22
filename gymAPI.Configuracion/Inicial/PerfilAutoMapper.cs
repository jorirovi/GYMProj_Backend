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
            CreateMap<UsuariosEntity, UsuarioTDOContract>().ReverseMap();
            CreateMap<PerfilUsuarioEntity, PerfilUsuarioContract>().ReverseMap();
            CreateMap<PerfilUsuarioEntity, PerfilUTDOContract>().ReverseMap();
            CreateMap<UsuariosEntity, PerfilUTDOContract>().ReverseMap();
            CreateMap<RutinasEntity, RutinasContract>().ReverseMap();
            CreateMap<DetalleRutinasEntity, DetalleRutinaContract>().ReverseMap();
            CreateMap<DetalleRutinasEntity, DetalleRTDOContract>().ReverseMap();
            CreateMap<UnidadPesoEntity, UnidadContract>().ReverseMap();
            CreateMap<ZonaCorporalEntity, ZonaCorporalContract>().ReverseMap();
        }
    }
}