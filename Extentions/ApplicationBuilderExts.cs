using Microsoft.AspNetCore.Diagnostics;
using OrderWebApp.Exceptions;
using System.Net;
using System.Text.Json;

namespace OrderWebApp.Extentions
{
    public static class ApplicationBuilderExts
    {
        public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder app, bool returnException)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var error = exceptionHandlerPathFeature.Error;

                    var returnStatusCode = (int)HttpStatusCode.InternalServerError;
                    var returnMessage = string.Empty;

                    if (error is UnknownException unknownException)
                    {
                        returnStatusCode = (int)HttpStatusCode.InternalServerError;
                        returnMessage = JsonSerializer.Serialize(unknownException.ToString());
                    }
                    else
                    {
                        returnMessage = JsonSerializer.Serialize(error.ToString());
                    }

                    context.Response.StatusCode = returnStatusCode;

                    if (returnException)
                    {
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(returnMessage);
                    }
                });
            });

            return app;
        }
    }
}
