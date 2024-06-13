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
    public class UsuarioAController : ControllerBase
    {
        private readonly ICrudService<UsuariosContract> _servicio;
        public UsuarioAController(ICrudService<UsuariosContract> servcio){
            _servicio = servcio;
        }
        [HttpGet]
        public async Task<IActionResult> obtenerUsuarios(){
            return Ok(await _servicio.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerUsuariosByID(string id){
            return Ok(await _servicio.GetById(id));
        }
        [HttpPost]
        public async Task<IActionResult> Crear_Usuario(UsuariosContract entity){
            return Ok(await _servicio.Create(entity));
        }
        [HttpPut]
        public async Task<IActionResult> Actualizar_Usuario(UsuariosContract entity){
            return Ok(await _servicio.Update(entity));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar_Usuario(string id){
            await _servicio.Remove(id);
            return Ok(GymConstantes.registroElimnado);
        }
    }
}