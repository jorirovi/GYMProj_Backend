using AutoMapper;
using gymAPI.Comunes.Classes.Constantes;
using gymAPI.Comunes.Classes.Contracts;
using gymAPI.Comunes.Classes.Helpers;
using gymAPI.Dominio.Service.GYM.General;
using gymAPI.Infraestructura.Database.Entidades;
using gymAPI.Infraestructura.Repositorios.General;
using gymAPI.Infraestructura.Repositorios.Vault;

namespace gymAPI.Dominio.Service.GYM.Vault
{
    public class vaultService : ICrudService<VaultContract>, IVaultService
    {
        private readonly ICrudRepository<vaultEntity> _vaultRepository;
        private readonly IVaultRepository _vIRepository;  
        private readonly IMapper _mapper;
        private readonly ICifradoHelper _cifradoHelper;
        public vaultService(ICrudRepository<vaultEntity> vaultRepository, IMapper mapper, 
            ICifradoHelper cifradoHelper, IVaultRepository vIRepository)
        {
            _vaultRepository = vaultRepository;
            _mapper = mapper;
            _cifradoHelper = cifradoHelper;
            _vIRepository = vIRepository;
        }

        public async Task<VaultContract> Create(VaultContract entity)
        {
            vaultEntity vault = await _vIRepository.GetVaultByTitle(entity.tituloV);
            if (vault == null)
            {
                string pass = _cifradoHelper.EncryptString(entity.passV);
                entity.passV = pass;
                vault = await _vaultRepository.CreateAsync(_mapper.Map<vaultEntity>(entity));
                return _mapper.Map<VaultContract>(vault);
            }
            else 
            {
                return _mapper.Map<VaultContract>(vault);
            }
        }

        public async Task<List<VaultContract>> GetAll()
        {
            List<VaultContract> joyas = _mapper.Map<List<VaultContract>>(await _vaultRepository.GetAllAsync());
            return joyas;
        }

        public async Task<VaultContract> GetById(string id)
        {
            vaultEntity joya = await _vaultRepository.GetUserByID(id);
            if (joya != null)
            {
                string pass = _cifradoHelper.DecryptString(joya.passV);
                joya.passV = pass;
                return _mapper.Map<VaultContract>(joya);
            }
            else 
            {
                throw new Exception(GymConstantes.registroNoEncontrado);
            }
        }

        public async Task Remove(string id)
        {
            vaultEntity joya = await _vaultRepository.GetUserByID(id);
            if (joya != null)
            {
                await _vaultRepository.RemoveAsync(joya);
            }
            else
            {
                throw new Exception(GymConstantes.registroNoEncontrado);
            }
        }

        public async Task<EliminarContract> RemoveWAnswer(string id)
        {
            vaultEntity joya = await _vaultRepository.GetUserByID(id);
            if (joya != null) 
            {
                EliminarContract joyaE = new EliminarContract(){
                    registro = joya.tituloV,
                    mensaje = GymConstantes.registroElimnado
                };
                await _vaultRepository.RemoveAsync(joya);
                return joyaE;
            }
            else 
            {
                throw new Exception(GymConstantes.registroNoEncontrado);
            }

        }

        public async Task<VaultContract> Update(VaultContract entity)
        {
            vaultEntity joyaC = await _vaultRepository.GetUserByID(entity.Id);
            if (joyaC != null)
            {
                string pass = _cifradoHelper.EncryptString(entity.passV);
                if (joyaC.passV != pass)
                {
                    vaultEntity nuevaJoya = new vaultEntity(){
                        Id = joyaC.Id,
                        tituloV = entity.tituloV,
                        passV = pass,
                        urlV = entity.urlV,
                        usuarioV = entity.usuarioV
                    };
                    await _vaultRepository.UpdateAsync(nuevaJoya);
                }
                else 
                {
                    vaultEntity nuevaJoya = new vaultEntity(){
                        Id = joyaC.Id,
                        tituloV = entity.tituloV,
                        passV = joyaC.passV,
                        urlV = entity.urlV,
                        usuarioV = entity.usuarioV
                    };
                    await _vaultRepository.UpdateAsync(nuevaJoya);
                }
                return entity;
            }
            else 
            {
                throw new Exception(GymConstantes.registroNoEncontrado);
            }
        }
    }
}