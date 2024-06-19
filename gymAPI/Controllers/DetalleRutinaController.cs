using gymAPI.Comunes.Classes.Constantes;
using gymAPI.Comunes.Classes.Contracts;
using gymAPI.Dominio.Service.GYM.DetalleRutinas;
using gymAPI.Dominio.Service.GYM.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gymAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DetalleRutinaController : ControllerBase
    {
        private readonly ICrudService<DetalleRutinaContract> _servicio;
        private readonly IDetalleRService _servicioDR;
        public DetalleRutinaController(ICrudService<DetalleRutinaContract> servicio, IDetalleRService servicioDR)
        {
            _servicio = servicio;
            _servicioDR = servicioDR;
        }
        [HttpPost]
        public async Task<IActionResult> Crear_DetalleRutina(DetalleRutinaContract entity)
        {
            return Ok(await _servicio.Create(entity));
        }
        [HttpPut]
        public async Task<IActionResult> Modificar_DetalleRutina(DetalleRutinaContract entity)
        {
            return Ok(await _servicio.Update(entity));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar_DetalleRutina(string id)
        {
            await _servicio.Remove(id);
            return Ok(GymConstantes.registroElimnado);
        } 
        [HttpGet("rutina/{idRutina}")]
        public async Task<IActionResult> Obtener_DRbyRutina(string idRutina)
        {
            return Ok(await _servicioDR.GetDRByRutina(idRutina));
        }
        [HttpGet("usuario/{idUsuario}")]
        public async Task<IActionResult> Obtener_DRbyUsuario(string idUsuario)
        {
            return Ok(await _servicioDR.GetDRByUsuario(idUsuario));
        }
    }
}