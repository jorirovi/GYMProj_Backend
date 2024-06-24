using AutoMapper;
using gymAPI.Comunes.Classes.Constantes;
using gymAPI.Comunes.Classes.Contracts;
using gymAPI.Dominio.Service.GYM.General;
using gymAPI.Infraestructura.Database.Entidades;
using gymAPI.Infraestructura.Repositorios.General;
using gymAPI.Infraestructura.Repositorios.PerfilUsuario;

namespace gymAPI.Dominio.Service.GYM.PerfilUsuario
{
    public class PerfilUsuarioService : ICrudService<PerfilUsuarioContract>, IPerfilUsuarioService
    {
        private readonly ICrudRepository<PerfilUsuarioEntity> _crudRepository;
        private readonly IPerfilUsuarioRepository _puRepository;
        private readonly ICrudRepository<UsuariosEntity> _usuariosRepository;
        private readonly IMapper _mapper;
        public PerfilUsuarioService(ICrudRepository<PerfilUsuarioEntity> crudRepository, IPerfilUsuarioRepository puRepository,
            IMapper mapper, ICrudRepository<UsuariosEntity> usuarioRepository)
        {
            _crudRepository = crudRepository;
            _puRepository = puRepository;
            _mapper = mapper;
            _usuariosRepository = usuarioRepository;
        }

        public async Task<PerfilUsuarioContract> Create(PerfilUsuarioContract entity)
        {
            UsuariosEntity usuario = await _usuariosRepository.GetUserByID(entity.idUsuario);
            if (usuario != null)
            {
                PerfilUsuarioEntity perfilUsuario = await _puRepository.GetPerfilByIDU(usuario.Id);
                if (perfilUsuario == null)
                {
                    await _crudRepository.CreateAsync(_mapper.Map<PerfilUsuarioEntity>(entity));
                    return entity;
                }
                else 
                {
                    return _mapper.Map<PerfilUsuarioContract>(perfilUsuario);
                }
            }
            else
            {
                throw new Exception(GymConstantes.registroExistente);
            }
        }

        public async Task<List<PerfilUsuarioContract>> GetAll()
        {
            List<PerfilUsuarioContract> perfilUsuario = _mapper.Map<List<PerfilUsuarioContract>>(await _crudRepository.GetAllAsync());
            return perfilUsuario;
        }

        public async Task<List<PerfilUTDOContract>> GetAllProfiles()
        {
            List<PerfilUTDOContract> profilesUsers = _mapper.Map<List<PerfilUTDOContract>>(await _crudRepository.GetAllAsync());
            profilesUsers.ForEach(pU => {
                pU.datosUsuario = _mapper.Map<UsuarioTDOContract>(_usuariosRepository.GetUserByID(pU.idUsuario).Result);
            });
            return profilesUsers;
        }

        public async Task<PerfilUsuarioContract> GetById(string id)
        {
            PerfilUsuarioContract perfilUsuario = _mapper.Map<PerfilUsuarioContract>(await _crudRepository.GetUserByID(id));
            if (perfilUsuario != null)
            {
                return perfilUsuario;
            }
            else
            {
                throw new Exception(GymConstantes.registroNoEncontrado);
            }
        }

        public async Task<PerfilUTDOContract> GetProfileByID(string id)
        {
            PerfilUTDOContract perfilUCompleto = _mapper.Map<PerfilUTDOContract>(await _puRepository.GetPerfilByIDU(id));
            if(perfilUCompleto != null)
            {
                perfilUCompleto.datosUsuario = _mapper.Map<UsuarioTDOContract>(_usuariosRepository.GetUserByID(perfilUCompleto.idUsuario).Result);
                return perfilUCompleto;
            }
            else
            {
                throw new Exception(GymConstantes.registroNoEncontrado);
            }
        }

        public async Task Remove(string id)
        {
            PerfilUsuarioEntity perfilUsuario = await _crudRepository.GetUserByID(id);
            if(perfilUsuario != null)
            {
                await _crudRepository.RemoveAsync(perfilUsuario);
            }
            else 
            {
                throw new Exception (GymConstantes.registroExistente);
            }
        }

        public async Task<PerfilUsuarioContract> Update(PerfilUsuarioContract entity)
        {
            PerfilUsuarioEntity perfilU = await _crudRepository.GetUserByID(entity.Id);
            if(perfilU != null)
            {
                PerfilUsuarioEntity perfilUM = new PerfilUsuarioEntity()
                {
                    Id = perfilU.Id,
                    edad = entity.edad,
                    sexo = entity.sexo,
                    peso = entity.peso,
                    idUsuario = perfilU.idUsuario
                };
                await _crudRepository.UpdateAsync(perfilUM);
                return _mapper.Map<PerfilUsuarioContract>(perfilUM);
            }
            else 
            {
                throw new Exception(GymConstantes.registroNoEncontrado);
            }
        }
    }
}