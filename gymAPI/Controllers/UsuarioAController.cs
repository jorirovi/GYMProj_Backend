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
    }
}