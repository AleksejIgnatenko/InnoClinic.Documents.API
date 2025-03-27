using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace InnoClinic.Documents.Application.Validators
{
    internal class PhotoValidator : AbstractValidator<IFormFile>
    {
        public PhotoValidator()
        {
            RuleFor(x => x.Length)
                .GreaterThan(0).WithMessage("Файл не может быть пустым.");

            RuleFor(x => x.FileName)
                .Must(BeAValidImage).WithMessage("Файл должен быть изображением (jpg, jpeg, png, gif).");
        }

        private bool BeAValidImage(string fileName)
        {
            var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = System.IO.Path.GetExtension(fileName).ToLowerInvariant();
            return validExtensions.Contains(extension);
        }
    }
}
