using System.Runtime.CompilerServices;
using gymAPI.Comunes.Classes.Constantes;
using gymAPI.Comunes.Classes.Contracts;
using gymAPI.Dominio.Service.GYM.General;
using gymAPI.Dominio.Service.GYM.PerfilUsuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gymAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PerfilController : ControllerBase
    {
        private readonly ICrudService<PerfilUsuarioContract> _servicio;
        private readonly IPerfilUsuarioService _puService;
        public PerfilController(ICrudService<PerfilUsuarioContract> servicio, IPerfilUsuarioService puService)
        {
            _servicio = servicio;
            _puService = puService;
        }

        [HttpPost]
        public async Task<IActionResult> Crear_Perfil(PerfilUsuarioContract entity)
        {
            return Ok(await _servicio.Create(entity));
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar_Perfil(PerfilUsuarioContract entity)
        {
            return Ok(await _servicio.Update(entity));
        } 

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar_Perfil(string id)
        {
            await _servicio.Remove(id);
            return Ok(GymConstantes.registroElimnado);
        }

        [HttpGet]
        public async Task<IActionResult> Obtener_Perfiles()
        {
            return Ok(await _puService.GetAllProfiles());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener_Perfil(string id){
            return Ok(await _puService.GetProfileByID(id));
        }

        [HttpGet("PerfilO/{id}")]
        public async Task<IActionResult> Obtener_PerfilSolo(string id)
        {
            return Ok(await _servicio.GetById(id));
        }
    }
}