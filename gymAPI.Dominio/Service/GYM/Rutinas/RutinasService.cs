using AutoMapper;
using gymAPI.Comunes.Classes.Constantes;
using gymAPI.Comunes.Classes.Contracts;
using gymAPI.Dominio.Service.GYM.General;
using gymAPI.Infraestructura.Database.Entidades;
using gymAPI.Infraestructura.Repositorios.General;
using gymAPI.Infraestructura.Repositorios.Rutinas;

namespace gymAPI.Dominio.Service.GYM.Rutinas
{
    public class RutinasService : ICrudService<RutinasContract>, IRutinaService
    {
        private readonly ICrudRepository<RutinasEntity> _crudRepository;
        private readonly IRutinaRepository _rutinaRepository;
        private readonly IMapper _mapper;
        public RutinasService (ICrudRepository<RutinasEntity> crudRepository, IMapper mapper,
            IRutinaRepository rutinaRepository)
        {
            _crudRepository = crudRepository;
            _rutinaRepository = rutinaRepository;
            _mapper = mapper;
        }

        public async Task<RutinasContract> Create(RutinasContract entity)
        {
            string rut = entity.rutina;
            RutinasEntity rutina = await _rutinaRepository.GetByRutina(rut.ToLower());
            if(rutina == null){
                entity.rutina = rut.ToLower();
                rutina = await _crudRepository.CreateAsync(_mapper.Map<RutinasEntity>(entity));
                return _mapper.Map<RutinasContract>(rutina);
            }
            else {
                throw new Exception("La Rutina: " + entity.rutina + " ya existe");
            }
            
            
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

        public async Task<EliminarContract> RemoveWAnswer(string id)
        {
            RutinasEntity rutinaEliminar = await _crudRepository.GetUserByID(id);
            if (rutinaEliminar != null)
            {
                EliminarContract objEliminar = new EliminarContract(){
                    registro = rutinaEliminar.rutina,
                    mensaje = GymConstantes.registroElimnado
                };
                await _crudRepository.RemoveAsync(rutinaEliminar);
                return objEliminar;
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