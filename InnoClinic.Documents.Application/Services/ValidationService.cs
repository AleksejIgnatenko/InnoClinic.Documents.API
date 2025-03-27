using InnoClinic.Documents.Application.Core;
using InnoClinic.Documents.Application.Validators;
using Microsoft.AspNetCore.Http;

namespace InnoClinic.Documents.Application.Services
{
    public class ValidationService : IValidationService
    {
        public Dictionary<string, string> Validation(IFormFile photo)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            PhotoValidator validations = new PhotoValidator();
            FluentValidation.Results.ValidationResult validationResult = validations.Validate(photo);
            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    errors[failure.PropertyName] = failure.ErrorMessage;
                }
            }

            return errors;
        }
    }
}
