using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


namespace gymAPI.Comunes.Classes.Excepciones
{
    public class MiddlewareExceptions
    {
        private readonly RequestDelegate _next;
        public MiddlewareExceptions(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                var response = httpContext.Response;
                response.ContentType = "application/json";
                string bodyResponse = string.Empty;
                switch (e)
                {
                    case ArgumentException:
                    case ValidationException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        bodyResponse = JsonConvert.SerializeObject(new { code = response.StatusCode, message = e.Message });
                        break;
                    case UnauthorizedAccessException ex:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        bodyResponse = JsonConvert.SerializeObject(new { code = response.StatusCode, message = ex.Message });
                        break;
                    default:
                        //otros tipos de excepcion
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        bodyResponse = JsonConvert.SerializeObject(new { code = response.StatusCode, message = e?.Message });
                        break;
                }
                await response.WriteAsync(bodyResponse);
            }
        }
    }
}