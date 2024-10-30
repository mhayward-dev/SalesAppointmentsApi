using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace SalesAppointments.Core;

public static class ServiceResultExtensions
{
    public static async Task<IActionResult> OnValidatedOk(this Task<ValidationResult> validationResult, Func<Task<IActionResult>> onValid)
    {
        var result = await validationResult;

        if (!result.IsValid)
        {
            return new BadRequestObjectResult(
                new ProblemDetails { 
                    Title = "An error occured", 
                    Detail = string.Join(" ", result.Errors) 
                });
        }

        return await onValid();
    }
}
