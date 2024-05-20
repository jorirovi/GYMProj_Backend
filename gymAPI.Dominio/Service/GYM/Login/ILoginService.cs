using gymAPI.Comunes.Classes.Contracts;

namespace gymAPI.Dominio.Service.GYM.Login
{
    public interface ILoginService
    {
        Task<TokenContract> LoginApp(LoginContract entity);
    }
}