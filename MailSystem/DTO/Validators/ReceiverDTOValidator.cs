using FluentValidation;
using MailSystem.Core.Resources;

namespace MailSystem.API.DTO.Validators
{
    public class ReceiverDTOValidator : AbstractValidator<ReceiverDTO>
    {
        private readonly IResourceLocalizer _localizer;

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
