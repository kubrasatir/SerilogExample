using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using SerilogExample.Models;
using System.Net;

namespace SerilogExample.Middlewares
{
    public static class CustomExceptionHandler
    {
        public static void UseApiExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        string errorMessage = contextFeature.Error.Message;
                        Response response = new Response(contextFeature.Error.Message, contextFeature.Error);
                        var error = JsonConvert.SerializeObject(response);
                        logger.Error(error);
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        await context.Response.WriteAsync(error);
                    }
                });
            });
        }
    }
}
