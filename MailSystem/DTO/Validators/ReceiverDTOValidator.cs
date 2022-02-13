using FluentValidation;
using MailSystem.Core.Resources;

namespace MailSystem.API.DTO.Validators
{
    /// <summary>
    /// Class to validate ReceiverDTO
    /// </summary>
    public class ReceiverDTOValidator : AbstractValidator<ReceiverDTO>
    {
        private readonly IResourceLocalizer _localizer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="localizer">Object that contains all static messages</param>
        public ReceiverDTOValidator(IResourceLocalizer localizer)
        {
            _localizer = localizer;

            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "NAME"))
                .NotEmpty()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "NAME"))
                .MaximumLength(100)
                .WithMessage(string.Format(_localizer.GetString("SIZE_RULE"), "NAME", 100));

            RuleFor(x => x.Documents)
                .ForEach(x => x.SetValidator(new DocumentDTOValidator(localizer)));

            RuleFor(x => x.Address)
                .SetValidator(new AddressDTOValidator(localizer));
        }
    }
}
