using System;
using System.Net;
using System.Threading.Tasks;
using dotnetServer.Application.Exceptions;
using dotnetServer.Domain.Exceptions;
using dotnetServer.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace dotnetServer.Application.Middlewares
{
    public class ExceptionMeddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMeddleware(RequestDelegate next, ILogger<ExceptionMeddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context){
            try
            {
                await _next(context);
            }
            catch (FieldModelException exception)
            {
                
                await HandleException(exception, context);
            }
            catch (DomainException exception)
            {
                
                await HandleException(exception, context);
            }
            catch (Exception exception)
            {
                
                await HandleException(exception, context);
            }
        }

        public async Task HandleException(Exception exception, HttpContext context){
            _logger.LogError(exception.ToString());
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            
            await context.Response.WriteAsync(JsonCamelCaseConverter.Serialize(
                new {
                    Error = "Ops! An error occoured.",
                }
            ));

            return;
        }

        public async Task HandleException(DomainException exception, HttpContext context){
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.BadRequest;

            await context.Response.WriteAsync(JsonCamelCaseConverter.Serialize(
                new {
                    Error = exception.Message,
                }
            ));

            return;
        }
        public async Task HandleException(FieldModelException exception, HttpContext context){
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            
            await context.Response.WriteAsync(JsonCamelCaseConverter.Serialize(
                new {
                    Errors = exception.FieldErrors,
                },
                processDictionaryKeys: true
            ));

            return;
        }
    }
}