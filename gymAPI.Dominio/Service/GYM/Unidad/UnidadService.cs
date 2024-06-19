using AutoMapper;
using gymAPI.Comunes.Classes.Constantes;
using gymAPI.Comunes.Classes.Contracts;
using gymAPI.Dominio.Service.GYM.General;
using gymAPI.Infraestructura.Database.Entidades;
using gymAPI.Infraestructura.Repositorios.General;
using gymAPI.Infraestructura.Repositorios.UnidadRepository;

namespace gymAPI.Dominio.Service.GYM.Unidad
{
    public class UnidadService : ICrudService<UnidadContract>
    {
        private readonly ICrudRepository<UnidadPesoEntity> _crudRepository;
        private readonly IUnidadRepository _uRepository;
        private readonly IMapper _mapper;
        public UnidadService(IMapper mapper, ICrudRepository<UnidadPesoEntity> crudRepository,
            IUnidadRepository uRepository)
        {
            _mapper = mapper;
            _crudRepository = crudRepository;
            _uRepository = uRepository;
        }

        public async Task<UnidadContract> Create(UnidadContract entity)
        {
            UnidadPesoEntity unidad = await _uRepository.GetUnidadByName(entity.unidad);
            if (unidad == null)
            {
                List<UnidadPesoEntity> unidades = await _crudRepository.GetAllAsync();
                if (unidades.Count == 0)
                {
                    entity.numero = 1;
                }
                else
                {
                    int ultimoNumero = unidades.Last().numero;
                    ultimoNumero += 1;
                    entity.numero = ultimoNumero;
                }
                unidad = await _crudRepository.CreateAsync(_mapper.Map<UnidadPesoEntity>(entity));
                return _mapper.Map<UnidadContract>(unidad);
            }
            else 
            {
                return _mapper.Map<UnidadContract>(unidad);
            }
        }

        public async Task<List<UnidadContract>> GetAll()
        {
            List<UnidadContract> unidades = _mapper.Map<List<UnidadContract>>(await _crudRepository.GetAllAsync());
            return unidades;
        }

        public async Task<UnidadContract> GetById(string id)
        {
            UnidadContract unidad = _mapper.Map<UnidadContract>(await _crudRepository.GetUserByID(id));
            return unidad;
        }

        public async Task Remove(string id)
        {
            UnidadPesoEntity unidad = await _crudRepository.GetUserByID(id);
            if (unidad != null)
            {
                await _crudRepository.RemoveAsync(unidad);
            }
            else 
            {
                throw new Exception(GymConstantes.registroNoEncontrado);
            }
        }

        public async Task<UnidadContract> Update(UnidadContract entity)
        {
            UnidadPesoEntity unidadA = await _crudRepository.GetUserByID(entity.Id);
            if (unidadA != null)
            {
                UnidadPesoEntity unidadM = new UnidadPesoEntity(){
                    Id = unidadA.Id,
                    unidad = entity.unidad,
                    numero = unidadA.numero,
                };
                await _crudRepository.UpdateAsync(unidadM);
                return _mapper.Map<UnidadContract>(unidadM);
            }
            else 
            {
                throw new Exception(GymConstantes.registroNoEncontrado);
            }
        }
    }
}