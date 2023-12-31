﻿using OrderWebApp.Exceptions;
using System.Net;
using System.Text.Json;

namespace OrderWebApp
{
    public class KnownExceptionsHandlerMeddleware
    {
        private readonly RequestDelegate _next;

        public KnownExceptionsHandlerMeddleware(RequestDelegate next)
        { 
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AppException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(ex.ToString()));
            }
        }
    }
}
