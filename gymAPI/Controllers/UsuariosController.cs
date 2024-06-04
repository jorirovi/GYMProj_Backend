using gymAPI.Comunes.Classes.Constantes;
using gymAPI.Comunes.Classes.Contracts;
using gymAPI.Dominio.Service.GYM.General;
using gymAPI.Dominio.Service.GYM.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace gymAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly ICrudService<UsuariosContract> _servicio;
        private readonly IUsuariosService _usuarioServicio;
        public UsuariosController(ICrudService<UsuariosContract> servicio, IUsuariosService usuarioServicio)
        {
            _servicio = servicio;
            _usuarioServicio = usuarioServicio;
        }
        [HttpPost]
        public async Task<IActionResult> CrearUSuario(UsuariosContract entity)
        {
            return Ok(await _servicio.Create(entity));
        }
        [HttpPut("pass")]
        public async Task<IActionResult> ActualizaPass(LoginContract entity)
        {
            return Ok(await _usuarioServicio.UpdatePass(entity));
        }
    }
}