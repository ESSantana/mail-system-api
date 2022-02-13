using FluentValidation;
using MailSystem.Core.Resources;

namespace MailSystem.API.DTO.Validators
{
    /// <summary>
    /// Class to validate DocumentDTO
    /// </summary>
    public class DocumentDTOValidator : AbstractValidator<DocumentDTO>
    {
        private readonly IResourceLocalizer _localizer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="localizer">Object that contains all static messages</param>
        public DocumentDTOValidator(IResourceLocalizer localizer)
        {
            _localizer = localizer;

            RuleFor(x => x.Value)
               .NotNull()
               .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "VALUE"))
               .NotEmpty()
               .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "VALUE"))
               .MaximumLength(64)
               .WithMessage(string.Format(_localizer.GetString("SIZE_RULE"), "VALUE", 64));

            RuleFor(x => x.Type)
               .NotNull()
               .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "TYPE"))
               .NotEmpty()
               .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "TYPE"))
               .MaximumLength(20)
               .WithMessage(string.Format(_localizer.GetString("SIZE_RULE"), "TYPE", 20));
        }
    }
}
