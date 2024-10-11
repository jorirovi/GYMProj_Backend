using gymAPI.Comunes.Classes.Contracts;
using gymAPI.Dominio.Service.GYM.General;
using gymAPI.Dominio.Service.GYM.Vault;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gymAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class VaultController : ControllerBase
    {
        private readonly ICrudService<VaultContract> _servicio;
        private readonly IVaultService _vService;
        public VaultController(ICrudService<VaultContract> servicio, IVaultService vService) 
        {
            _servicio = servicio;
            _vService = vService;
        }
        [HttpPost]
        public async Task<IActionResult> Crear_Joya(VaultContract entity)
        {
            return Ok(await _servicio.Create(entity));
        }
        [HttpPut]
        public async Task<IActionResult> Modificar_Joya(VaultContract entity)
        {
            return Ok(await _servicio.Update(entity));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar_Joya(string id)
        {
            return Ok(await _vService.RemoveWAnswer(id));
        }
        [HttpGet]
        public async Task<IActionResult> obtener_Joyas()
        {
            return Ok(await _servicio.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> obtener_joya(string id)
        {
            return Ok(await _servicio.GetById(id));
        }
    }
}