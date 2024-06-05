using System.Reflection;
using System.Text;
using AutoMapper;
using gymAPI.Infraestructura.Database.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NetCore.AutoRegisterDi;

namespace gymAPI.Configuracion.Inicial
{
    public class Container
    {
        public static void ConfiguracionDependencias(IServiceCollection service, IConfiguration configuration)
        {
            #region [Automapper]
            var configMapper = new MapperConfiguration(cfg => cfg.AddProfile(new PerfilAutoMapper()));
            var mapper = configMapper.CreateMapper();
            service.AddSingleton(mapper);
            #endregion

            #region [Inyectar Dependencias de base de Datos]
            service.Configure<GymDBSettings>(options => {
                options.connectionString = configuration.GetSection("GymDataBase:ConnectionString").Value;
                options.databaseName = configuration.GetSection("GymDataBase:DatabaseName").Value;
            });
            service.AddSingleton<IConfiguration>(configuration);
            #endregion

            #region [registro de inyeccion de dependencias]
            var assembliesToScan = new[]
            {
                Assembly.GetExecutingAssembly(),
                Assembly.Load("gymAPI.Dominio"),
                Assembly.Load("gymAPI.Infraestructura"),
                Assembly.Load("gymAPI.Comunes"),
            };
            service.RegisterAssemblyPublicNonGenericClasses(assembliesToScan)
                .Where(c => c.Name.EndsWith("Repository") ||
                       c.Name.EndsWith("Service") ||
                       c.Name.EndsWith("Helper"))
                .AsPublicImplementedInterfaces();
            #endregion

            #region [CORS]
            service.AddCors(options => {
                options.AddPolicy("CorsPolicy", builder => {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            });
            #endregion

            #region [JWT]
            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.IncludeErrorDetails = true;
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters(){
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidIssuer = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });
            #endregion
        }
    }
}