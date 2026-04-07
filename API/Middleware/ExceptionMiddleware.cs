using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
       
        public ExceptionMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleGeneralExceptionAsync(context, ex);
            }
        }

        private static async Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            // Structure pour ProblemDetails
            var errors = ex.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray()
                );

            var validationProblemDetails = new ValidationProblemDetails(errors)
            {
                Type = "ValidationFailure",
                Title = "Validation error",
                Status = StatusCodes.Status400BadRequest,
                Detail = "One or more validation errors occurred.",
                Instance = context.Request.Path
            };

            // Option : format JSON simple pour front-end
            var simpleResponse = new
            {
                success = false,
                message = "Validation failed",
                errors = ex.Errors.Select(e => new { field = e.PropertyName, error = e.ErrorMessage })
            };

            // Choix : soit ProblemDetails, soit simple JSON
            await context.Response.WriteAsJsonAsync(validationProblemDetails); // standard RFC 7807
           // await context.Response.WriteAsJsonAsync(simpleResponse); // front-end friendly
        }

        private static async Task HandleGeneralExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new
            {
                success = false,
                message = "Internal server error",
                detail = ex.Message // optionnel : enlever en prod
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
