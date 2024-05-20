using gymAPI.Comunes.Classes.Constantes;
using gymAPI.Comunes.Classes.Contracts;
using gymAPI.Dominio.Service.GYM.General;
using Microsoft.AspNetCore.Mvc;

namespace gymAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly ICrudService<UsuariosContract> _servicio;
        public UsuariosController(ICrudService<UsuariosContract> servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> obtenerUsuarios()
        {
            return Ok(await _servicio.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> CrearUSuario(UsuariosContract entity)
        {
            return Ok(await _servicio.Create(entity));
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarUsuario(UsuariosContract entity)
        {
            return Ok(await _servicio.Update(entity));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerUsuarioPorID(string id)
        {
            return Ok(await _servicio.GetById(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> eleminarUsuario(string id)
        {
            await _servicio.Remove(id);
            return Ok(GymConstantes.registroElimnado);
        }
    }
}