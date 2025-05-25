using MunicipalityTax.Exceptions;
using System.Net;

namespace MunicipalityTax.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound; //you can just write 404
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync($"Something went wrong: {ex.Message}");
            }
            catch(BadRequestException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest; //you can just write 400
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync($"Something went wrong: {ex.Message}");
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; //500
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync($"Something went wrong: {ex.Message}");
            }
        }
    }
}
