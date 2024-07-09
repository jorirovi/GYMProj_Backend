using gymAPI.Comunes.Classes.Constantes;
using gymAPI.Comunes.Classes.Contracts;
using gymAPI.Dominio.Service.GYM.General;
using gymAPI.Dominio.Service.GYM.Rutinas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gymAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RutinasController : ControllerBase
    {
        private readonly ICrudService<RutinasContract> _servicio;
        private readonly IRutinaService _rutinaService;
        public RutinasController(ICrudService<RutinasContract> servicio, IRutinaService rutinaService)
        {
            _servicio = servicio;
            _rutinaService = rutinaService;
        }
        [HttpPost]
        public async Task<IActionResult> Crear_Rutina(RutinasContract entity)
        {
            return Ok(await _servicio.Create(entity));
        }
        [HttpPut]
        public async Task<IActionResult> Modificar_Rutina(RutinasContract entity)
        {
            return Ok(await _servicio.Update(entity));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Elimnar_Rutina(string id)
        {
            await _servicio.Remove(id);
            var response = new {message = GymConstantes.registroElimnado};
            return Ok(response);
        }
        [HttpDelete("Mensaje/{id}")]
        public async Task<IActionResult> Eliminar_Con_Mensaje(string id)
        {
            return Ok(await _rutinaService.RemoveWAnswer(id));
        }
        [HttpGet]
        public async Task<IActionResult> Obtener_Rutinas()
        {
            return Ok(await _servicio.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener_Rutina(string id)
        {
            return Ok(await _servicio.GetById(id));
        }        
    }
}