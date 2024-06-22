using AutoMapper;
using gymAPI.Comunes.Classes.Constantes;
using gymAPI.Comunes.Classes.Contracts;
using gymAPI.Comunes.Classes.Enum;
using gymAPI.Dominio.Service.GYM.General;
using gymAPI.Infraestructura.Database.Entidades;
using gymAPI.Infraestructura.Repositorios.DetalleRutinas;
using gymAPI.Infraestructura.Repositorios.General;
using gymAPI.Infraestructura.Repositorios.UnidadRepository;
using gymAPI.Infraestructura.Repositorios.ZonaCorporal;

namespace gymAPI.Dominio.Service.GYM.DetalleRutinas
{
    public class DetalleRutinaService : ICrudService<DetalleRutinaContract>, IDetalleRService
    {
        private readonly ICrudRepository<DetalleRutinasEntity> _crudRepository;
        private readonly IDetalleRRepository _drRepository;
        private readonly ICrudRepository<UsuariosEntity> _uRepository;
        private readonly ICrudRepository<RutinasEntity> _rRepository;
        private readonly IUnidadRepository _uPRepository;
        private readonly IZonaCorporalRepository _zcRepository;
        private readonly IMapper _mapper;
        public DetalleRutinaService(ICrudRepository<DetalleRutinasEntity> crudRepository, IDetalleRRepository drReposiotry,
            ICrudRepository<UsuariosEntity> uReposiotry, ICrudRepository<RutinasEntity> rReposiotry,
            IMapper mapper, IUnidadRepository uPRepository, IZonaCorporalRepository zcRepository)
        {
            _crudRepository = crudRepository;
            _drRepository = drReposiotry;
            _uRepository = uReposiotry;
            _rRepository = rReposiotry;
            _mapper = mapper;
            _uPRepository = uPRepository;
            _zcRepository = zcRepository;
        }

        public async Task<DetalleRutinaContract> Create(DetalleRutinaContract entity)
        {
            DetalleRutinasEntity dRutina = new DetalleRutinasEntity();
            entity.unidadPeso = (int)UnidadEnum.Kg;
            dRutina = await _crudRepository.CreateAsync(_mapper.Map<DetalleRutinasEntity>(entity));
            return _mapper.Map<DetalleRutinaContract>(dRutina);
        }

        public Task<List<DetalleRutinaContract>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<DetalleRutinaContract> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DetalleRTDOContract>> GetDRByRutina(string idRutina)
        {
            List<DetalleRTDOContract> detalleRutina = _mapper.Map<List<DetalleRTDOContract>>(await _drRepository.GetDRbyIdRutina(idRutina));
            detalleRutina.ForEach(dr => {
                dr.rutina = _mapper.Map<RutinasContract>(_rRepository.GetUserByID(dr.idRutina).Result);
                dr.usuario = _mapper.Map<UsuarioTDOContract>(_uRepository.GetUserByID(dr.idUsuario).Result);
                dr.unidad = _mapper.Map<UnidadContract>(_uPRepository.GetUnidadByNumero(dr.unidadPeso).Result);
                dr.dZonaCorporal = _mapper.Map<ZonaCorporalContract>(_zcRepository.GetByNZC(dr.zonaCorporal).Result);
            });
            return detalleRutina;
        }

        public async Task<List<DetalleRTDOContract>> GetDRByUsuario(string idUsuario)
        {
            List<DetalleRTDOContract> detalleRutinaU = _mapper.Map<List<DetalleRTDOContract>>(await _drRepository.GetDRByIdUsuario(idUsuario));
            detalleRutinaU.ForEach(drU => {
                drU.rutina = _mapper.Map<RutinasContract>(_rRepository.GetUserByID(drU.idRutina).Result);
                drU.usuario = _mapper.Map<UsuarioTDOContract>(_uRepository.GetUserByID(drU.idUsuario).Result);
                drU.unidad = _mapper.Map<UnidadContract>(_uPRepository.GetUnidadByNumero(drU.unidadPeso).Result);
                drU.dZonaCorporal = _mapper.Map<ZonaCorporalContract>(_zcRepository.GetByNZC(drU.zonaCorporal).Result);
            });
            return detalleRutinaU;
        }

        public async Task<List<DetalleRTDOContract>> GetDRByZonaCorporal(int zCorporal)
        {
            List<DetalleRTDOContract> detalleRutinaZC = _mapper.Map<List<DetalleRTDOContract>>(await _drRepository.GetDRbyZonaCorporal(zCorporal));
            detalleRutinaZC.ForEach(drZC => {
                drZC.rutina = _mapper.Map<RutinasContract>(_rRepository.GetUserByID(drZC.idRutina).Result);
                drZC.usuario = _mapper.Map<UsuarioTDOContract>(_uRepository.GetUserByID(drZC.idUsuario).Result);
                drZC.unidad = _mapper.Map<UnidadContract>(_uPRepository.GetUnidadByNumero(drZC.unidadPeso).Result);
                drZC.dZonaCorporal = _mapper.Map<ZonaCorporalContract>(_zcRepository.GetByNZC(drZC.zonaCorporal).Result);
            });
            return detalleRutinaZC;
        }

        public async Task Remove(string id)
        {
            DetalleRutinasEntity detalleRutinaE = await _crudRepository.GetUserByID(id);
            if (detalleRutinaE != null)
            {
                await _crudRepository.RemoveAsync(detalleRutinaE);
            }
            else
            {
                throw new Exception(GymConstantes.registroNoEncontrado);
            }
        }

        public async Task<DetalleRutinaContract> Update(DetalleRutinaContract entity)
        {
            DetalleRutinasEntity detalleRutinaA = await _crudRepository.GetUserByID(entity.Id);
            if (detalleRutinaA != null)
            {
                DetalleRutinasEntity detalleRutinaM = new DetalleRutinasEntity(){
                    Id = detalleRutinaA.Id,
                    idRutina = entity.idRutina,
                    idUsuario = detalleRutinaA.idUsuario,
                    ejercicio = entity.ejercicio,
                    maquina = entity.maquina,
                    peso = entity.peso,
                    unidadPeso = (int)UnidadEnum.Kg,
                    repeticiones = entity.repeticiones,
                    ilustracion = entity.ilustracion,
                    zonaCorporal = entity.zonaCorporal
                };
                await _crudRepository.UpdateAsync(detalleRutinaM);
                return _mapper.Map<DetalleRutinaContract>(detalleRutinaM);
            }
            else
            {
                throw new Exception(GymConstantes.registroNoEncontrado);
            }
        }
    }
}