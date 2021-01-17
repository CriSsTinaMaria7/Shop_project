using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, 
        ILogger<ExceptionMiddleware>logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        //pentru a utiliza metoda Middleware
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // dacă nu există nicio excepție, cererea va trece la etapa următoare
                await _next(context);
            }
            catch(Exception ex) //daca există o excepție dorim să o prindem
            {   //output în sistemul de înregistrare(consola)
                 _logger.LogError(ex, ex.Message);
                 //raspunsurile vor fi trimise ca răspunsuri formatate json
                 context.Response.ContentType = "application/json";
                 //setăm codul de stare să fie o eroare de server intern 400
                 context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                 //am scris raspunsul
                 //verificam din nou dacă rulăm în modul de dezvoltare
                 var response = _env.IsDevelopment()
                 //daca rulăm în modul de dezvoltare
                 
                 ? new ApiException((int)HttpStatusCode.InternalServerError,
                 //am preluat mesajul de la exceptie 
                 ex.Message, ex.StackTrace.ToString())
                 //alternativa persupune faptul ca ne aflam in modul productie
                 : new ApiException((int)HttpStatusCode.InternalServerError);
                 // o mică ajustare în ceea ce privește mesajul în Postman
                 var options = new JsonSerializerOptions{PropertyNamingPolicy = 
                 JsonNamingPolicy.CamelCase};
                 //serializarea raspunsului într-un format json
                 var json = JsonSerializer.Serialize(response, options);
                 await context.Response.WriteAsync(json);
            }
        }
    }
}