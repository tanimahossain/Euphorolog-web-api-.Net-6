using Euphorolog.Services.CustomExceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace Euphorolog.GlobalExceptionHandler
{
    public static class CustomGlobalErrorHandler
    {
        public static void CustomErrorHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature == null)
                    {
                        context.Response.ContentType = Text.Plain;
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        await context.Response.WriteAsync("Something went wrong.");
                    }
                    else
                    {
                        switch (contextFeature.Error)
                        {
                            case NotFoundException:
                                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                                break;
                            case UnAuthorizedException:
                                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                break;
                            case ForbiddenException:
                                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                                break;
                            case BadRequestException:
                                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                                break;
                            default:
                                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                break;
                        }
                        context.Response.ContentType = Text.Plain;
                        await context.Response.WriteAsync(contextFeature.Error.Message);

                    }
                });
            });
        }
    }
}
