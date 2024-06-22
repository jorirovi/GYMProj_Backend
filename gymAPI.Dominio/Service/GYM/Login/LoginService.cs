using gymAPI.Comunes.Classes.Constantes;
using gymAPI.Comunes.Classes.Contracts;
using gymAPI.Comunes.Classes.Helpers;
using gymAPI.Infraestructura.Database.Entidades;
using gymAPI.Infraestructura.Repositorios;
using Microsoft.Extensions.Configuration;

namespace gymAPI.Dominio.Service.GYM.Login
{
    public class LoginService : ILoginService
    {
        private readonly IUsuarioRepository _uRepository;
        private readonly IConfiguration _config;
        private readonly ICifradoHelper _cifrado;
        public LoginService(IUsuarioRepository uRepository, IConfiguration config, ICifradoHelper cifrado)
        {
            _uRepository = uRepository;
            _config = config;
            _cifrado = cifrado;
        }

        public async Task<TokenContract> LoginApp(LoginContract entity)
        {
            string pass = _cifrado.EncryptString(entity.password);
            UsuariosEntity usuario = await _uRepository.GetUserByEmailPass(entity.email, pass);
            if (usuario != null)
            {
                TokenContract TokenAuth = new TokenContract{
                    IdU = usuario.Id,
                    email = usuario.email,
                    token = JWTHelper.GenerarToken(usuario.nombre, _config)
                };
                return TokenAuth;
            }
            else
            {
                throw new Exception(GymConstantes.registroNoEncontrado);
            }

        }
    }
}