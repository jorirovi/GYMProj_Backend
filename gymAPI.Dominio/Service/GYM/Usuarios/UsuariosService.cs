using AutoMapper;
using gymAPI.Comunes.Classes.Constantes;
using gymAPI.Comunes.Classes.Contracts;
using gymAPI.Comunes.Classes.Helpers;
using gymAPI.Dominio.Service.GYM.General;
using gymAPI.Infraestructura.Database.Entidades;
using gymAPI.Infraestructura.Repositorios;
using gymAPI.Infraestructura.Repositorios.General;
using gymAPI.Infraestructura.Repositorios.Usuarios;

namespace gymAPI.Dominio.Service.GYM.Usuarios
{
    public class UsuariosService : ICrudService<UsuariosContract>, IUsuariosService
    {
        private readonly ICrudRepository<UsuariosEntity> _repository;
        private readonly IUsuarioRepository _uRepository;
        private readonly IMapper _mapper;
        private readonly ICifradoHelper _cifrado;
        public UsuariosService(ICrudRepository<UsuariosEntity> repository, IUsuarioRepository uRepository,
            IMapper mapper, ICifradoHelper cifrado)
        {
            _repository = repository;
            _uRepository = uRepository;
            _mapper = mapper;
            _cifrado = cifrado;
        }

        public async Task<UsuariosContract> Create(UsuariosContract entity)
        {
            UsuariosEntity usuario = await _uRepository.GetUsuarioByEmail(entity.email);
            if (usuario == null)
            {
                string pass = entity.password;
                entity.password = _cifrado.EncryptString(pass);
                await _repository.CreateAsync(_mapper.Map<UsuariosEntity>(entity));
                return entity;
            }
            else
            {
                return _mapper.Map<UsuariosContract>(usuario);
            }
        }

        public async Task<List<UsuariosContract>> GetAll()
        {
            List<UsuariosContract> listaUsurios = _mapper.Map<List<UsuariosContract>>(await _repository.GetAllAsync());
            return listaUsurios;
        }

        public async Task<UsuariosContract> GetById(string id)
        {
            UsuariosContract usuario = _mapper.Map<UsuariosContract>(await _repository.GetUserByID(id));
            if (usuario != null)
            {
                return usuario;
            }
            else
            {
                throw new Exception(GymConstantes.registroNoEncontrado);
            }
        }

        public async Task Remove(string id)
        {
            UsuariosEntity usuarioE = await _repository.GetUserByID(id);
            if (usuarioE != null)
            {
                await _repository.RemoveAsync(usuarioE);
            }
            else
            {
                throw new Exception(GymConstantes.registroNoEncontrado);
            }
        }

        public async Task<UsuariosContract> Update(UsuariosContract entity)
        {
            UsuariosEntity usuarioA = await _repository.GetUserByID(entity.Id);
            if (usuarioA != null)
            {
                entity.Id = usuarioA.Id;
                if(entity.password != usuarioA.password)
                {
                    string pass = _cifrado.EncryptString(entity.password);
                    entity.password = pass;
                }
                await _repository.UpdateAsync(_mapper.Map<UsuariosEntity>(entity));
                return entity;
            }
            else
            {
                throw new Exception(GymConstantes.registroNoEncontrado);
            }
        }

        public async Task<UsuariosContract> UpdatePass(LoginContract entity)
        {
            UsuariosEntity usuario = await _uRepository.GetUsuarioByEmail(entity.email);
            if (usuario != null){
                string pass = _cifrado.EncryptString(entity.password);
                UsuariosEntity pmUsuario = new UsuariosEntity(){
                    Id = usuario.Id,
                    nombre = usuario.nombre,
                    apellidos = usuario.apellidos,
                    email = usuario.email,
                    password = pass
                };
                await _repository.UpdateAsync(pmUsuario);
                return _mapper.Map<UsuariosContract>(pmUsuario);
            }
            else
            {
                throw new Exception(GymConstantes.EmailNoEncontrado);
            }
        }
    }
}