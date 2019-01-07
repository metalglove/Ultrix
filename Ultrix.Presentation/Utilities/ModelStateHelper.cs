using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.Utilities
{
    public static class ModelStateHelper
    {
        public static Dictionary<string, string[]> ErrorMessages(this ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return modelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );
            }
            return null;
        }

        public static ServiceResponseDto DefaultInvalidModelStateWithErrorMessages(this ModelStateDictionary modelState)
        {
            return new ServiceResponseDto { Success = false, Message = "Invalid request", Errors = modelState.ErrorMessages() };
        }
    }
}
