using Vacancy.Api.Helpers;
using Vacancy.Service.Exceptions;

namespace Vacancy.Api.MIddlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (VacancyException ex)
            {
                context.Response.StatusCode = ex.StatusCode;
                await context.Response.WriteAsJsonAsync(new Response
                {
                    Code = ex.StatusCode,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                if (ex.Message.Contains("error occurred while saving the entity changes. See the inner exception for details"))
                {
                    await context.Response.WriteAsJsonAsync(new Response
                    {
                        Code = 500,
                        Message = "Already Exist"
                    });
                }
                else
                {
                    await context.Response.WriteAsJsonAsync(new Response
                    {
                        Code = 500,
                        Message = ex.Message
                    });
                }
            }
        }
    }
}
