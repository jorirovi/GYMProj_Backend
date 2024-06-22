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
    public class UnidadController : ControllerBase
    {
        private readonly ICrudService<UnidadContract> _servicio;
        public UnidadController(ICrudService<UnidadContract> servicio)
        {
            _servicio = servicio;
        }
        [HttpPost]
        public async Task<IActionResult> Crear_Unidad(UnidadContract entity)
        {
            return Ok(await _servicio.Create(entity));
        }
        [HttpPut]
        public async Task<IActionResult> Modificar_Unidad(UnidadContract entity)
        {
            return Ok(await _servicio.Update(entity));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar_Unidad(string id)
        {
            await _servicio.Remove(id);
            return Ok(GymConstantes.registroElimnado);
        }
        [HttpGet]
        public async Task<IActionResult> Obtener_Unidades()
        {
            return Ok(await _servicio.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener_Unidad(string id)
        {
            return Ok(await _servicio.GetById(id));
        }
    }
}