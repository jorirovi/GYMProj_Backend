using System.Net.Http.Headers;
using AutoMapper;
using gymAPI.Comunes.Classes.Constantes;
using gymAPI.Comunes.Classes.Contracts;
using gymAPI.Dominio.Service.GYM.General;
using gymAPI.Infraestructura.Database.Entidades;
using gymAPI.Infraestructura.Repositorios.General;
using gymAPI.Infraestructura.Repositorios.ZonaCorporal;

namespace gymAPI.Dominio.Service.GYM.ZonaCorporal
{
    public class ZonaCorporalService : ICrudService<ZonaCorporalContract>
    {
        private readonly ICrudRepository<ZonaCorporalEntity> _crudRepository;
        private readonly IZonaCorporalRepository _zCRepository;
        private readonly IMapper _mapper;
        public ZonaCorporalService(ICrudRepository<ZonaCorporalEntity> crudRepository, IZonaCorporalRepository zCRepository,
            IMapper mapper)
        {
            _crudRepository = crudRepository;
            _zCRepository = zCRepository;
            _mapper = mapper;
        }

        public async Task<ZonaCorporalContract> Create(ZonaCorporalContract entity)
        {
            ZonaCorporalEntity zCorporal =  await _zCRepository.GetByNZC(entity.numeroZC);
            if (zCorporal == null)
            {
                zCorporal = await _crudRepository.CreateAsync(_mapper.Map<ZonaCorporalEntity>(entity));
                return _mapper.Map<ZonaCorporalContract>(zCorporal);
            }
            else
            {
                return _mapper.Map<ZonaCorporalContract>(zCorporal);
            }
        }

        public async Task<List<ZonaCorporalContract>> GetAll()
        {
            List<ZonaCorporalContract> listaZC = _mapper.Map<List<ZonaCorporalContract>>(await _crudRepository.GetAllAsync());
            return listaZC;
        }

        public async Task<ZonaCorporalContract> GetById(string id)
        {
            ZonaCorporalContract zonaCorporal = _mapper.Map<ZonaCorporalContract>(await _crudRepository.GetUserByID(id));
            return zonaCorporal;
        }

        public async Task Remove(string id)
        {
            ZonaCorporalEntity zonaCorporal = await _crudRepository.GetUserByID(id);
            if (zonaCorporal != null)
            {
                await _crudRepository.RemoveAsync(zonaCorporal);
            }
            else 
            {
                throw new Exception(GymConstantes.registroNoEncontrado);
            }
        }

        public async Task<ZonaCorporalContract> Update(ZonaCorporalContract entity)
        {
            ZonaCorporalEntity zCorporalA = await _crudRepository.GetUserByID(entity.Id);
            if (zCorporalA != null)
            {
                ZonaCorporalEntity zCorporalM = new ZonaCorporalEntity()
                {
                    Id = zCorporalA.Id,
                    zonaCorporal = entity.zonaCorporal,
                    numeroZC = entity.numeroZC
                };
                zCorporalM = await _crudRepository.UpdateAsync(zCorporalM);
                return _mapper.Map<ZonaCorporalContract>(zCorporalM);
            }
            else
            {
                throw new Exception(GymConstantes.registroNoEncontrado);
            }
        }
    }
}