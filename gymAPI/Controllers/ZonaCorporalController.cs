using gymAPI.Comunes.Classes.Constantes;
using gymAPI.Comunes.Classes.Contracts;
using gymAPI.Dominio.Service.GYM.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gymAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ZonaCorporalController : ControllerBase
    {
        private readonly ICrudService<ZonaCorporalContract> _servicio;
        public ZonaCorporalController(ICrudService<ZonaCorporalContract> servicio)
        {
            _servicio = servicio;
        }
        [HttpPost]
        public async Task<IActionResult> Adicionar_ZC(ZonaCorporalContract entity)
        {
            return Ok(await _servicio.Create(entity));
        }
        [HttpPut]
        public async Task<IActionResult> Modificar_ZC(ZonaCorporalContract entity)
        {
            return Ok(await _servicio.Update(entity));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar_ZC(string id)
        {
            await _servicio.Remove(id);
            return Ok(GymConstantes.registroElimnado);
        }
        [HttpGet]
        public async Task<IActionResult> Obtener_ZCs()
        {
            return Ok(await _servicio.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener_ZC(string id)
        {
            return Ok(await _servicio.GetById(id));
        }
    }
}