using gymAPI.Comunes.Classes.Constantes;
using gymAPI.Comunes.Classes.Contracts;
using gymAPI.Dominio.Service.GYM.General;
using gymAPI.Dominio.Service.GYM.ZonaCorporal;
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
        private readonly IZonaCorporalService _zcService;
        public ZonaCorporalController(ICrudService<ZonaCorporalContract> servicio, IZonaCorporalService zcService)
        {
            _servicio = servicio;
            _zcService = zcService;
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
            var response = new { message = GymConstantes.registroElimnado };
            return Ok(response);
        }
        [HttpDelete("Mensaje/{id}")]
        public async Task<IActionResult> Eliminar_Con_Mensaje(string id)
        {
            return Ok(await _zcService.RemoveWMensaje(id));
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