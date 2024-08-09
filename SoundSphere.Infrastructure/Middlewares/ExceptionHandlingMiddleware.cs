using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoundSphere.Infrastructure.Exceptions;
using System.Net.Mime;
using System.Text.Json;

namespace SoundSphere.Infrastructure.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try { await next(context); }
            catch (Exception exception)
            {
                ProblemDetails problem = exception switch
                {
                    ResourceNotFoundException ex => new() { Title = "Resource not found", Detail = ex.Message, Status = StatusCodes.Status404NotFound },
                    InvalidRequestException ex => new() { Title = "Invalid request", Detail = ex.Message, Status = StatusCodes.Status400BadRequest },
                    _ => new ProblemDetails { Title = "Internal Server Error", Detail = exception.Message, Status = StatusCodes.Status500InternalServerError }
                };
                context.Response.ContentType = MediaTypeNames.Application.Json;
                context.Response.StatusCode = problem.Status ?? StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
            }
        }
    }
}