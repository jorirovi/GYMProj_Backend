using AutoMapper;
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

        public Task<ZonaCorporalContract> Create(ZonaCorporalContract entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<ZonaCorporalContract>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ZonaCorporalContract> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task Remove(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ZonaCorporalContract> Update(ZonaCorporalContract entity)
        {
            throw new NotImplementedException();
        }
    }
}