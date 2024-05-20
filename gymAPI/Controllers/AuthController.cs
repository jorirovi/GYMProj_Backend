using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gymAPI.Comunes.Classes.Contracts;
using gymAPI.Dominio.Service.GYM.Login;
using Microsoft.AspNetCore.Mvc;

namespace gymAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _servicio;
        public AuthController(ILoginService servicio)
        {
            _servicio = servicio;
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerToken(LoginContract entity)
        {
            return Ok(await _servicio.LoginApp(entity));
        }
    }
}