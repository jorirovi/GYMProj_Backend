using AutoMapper;
using gymAPI.Comunes.Classes.Constantes;
using gymAPI.Comunes.Classes.Contracts;
using gymAPI.Dominio.Service.GYM.General;
using gymAPI.Infraestructura.Database.Entidades;
using gymAPI.Infraestructura.Repositorios.General;

namespace gymAPI.Dominio.Service.GYM.Rutinas
{
    public class RutinasService : ICrudService<RutinasContract>
    {
        private readonly ICrudRepository<RutinasEntity> _crudRepository;
        private readonly IMapper _mapper;
        public RutinasService (ICrudRepository<RutinasEntity> crudRepository, IMapper mapper)
        {
            _crudRepository = crudRepository;
            _mapper = mapper;
        }

        public async Task<RutinasContract> Create(RutinasContract entity)
        {
            RutinasEntity rutina = _mapper.Map<RutinasEntity>(entity);
            await _crudRepository.CreateAsync(rutina);
            return entity;
        }

        public async Task<List<RutinasContract>> GetAll()
        {
            List<RutinasContract> listadoRutinas =  _mapper.Map<List<RutinasContract>>(await _crudRepository.GetAllAsync());
            return listadoRutinas;
        }

        public async Task<RutinasContract> GetById(string id)
        {
            RutinasContract rutina = _mapper.Map<RutinasContract>(await _crudRepository.GetUserByID(id));
            if(rutina != null)
            {
                return rutina;
            }
            else 
            {
                throw new Exception(GymConstantes.registroNoEncontrado);
            }
        }

        public async Task Remove(string id)
        {
            RutinasEntity rutina = await _crudRepository.GetUserByID(id);
            if(rutina != null)
            {
                await _crudRepository.RemoveAsync(rutina);
            }
            else 
            {
                throw new Exception(GymConstantes.registroNoEncontrado);
            }
        }

        public async Task<RutinasContract> Update(RutinasContract entity)
        {
            RutinasEntity rutinaExistente = await _crudRepository.GetUserByID(entity.Id);
            if(rutinaExistente != null)
            {
                RutinasEntity rutinaMod = new RutinasEntity() {
                    Id = rutinaExistente.Id,
                    rutina = entity.rutina
                };
                await _crudRepository.UpdateAsync(rutinaMod);
                return _mapper.Map<RutinasContract>(rutinaMod);
            }
            else 
            {
                throw new Exception(GymConstantes.registroNoEncontrado);
            }
        }
    }
}