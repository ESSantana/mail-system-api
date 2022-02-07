using FluentValidation;
using MailSystem.Core.Resources;

namespace MailSystem.API.DTO.Validators
{
    public class AddressDTOValidator : AbstractValidator<AddressDTO>
    {
        private readonly IResourceLocalizer _localizer;

        public AddressDTOValidator(IResourceLocalizer localizer)
        {
            _localizer = localizer;

            RuleFor(x => x.Street)
                .NotNull()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "STREET"))
                .NotEmpty()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "STREET"))
                .MaximumLength(50)
                .WithMessage(string.Format(_localizer.GetString("SIZE_RULE"), "STREET", 50));

            RuleFor(x => x.Neighborhood)
                .NotNull()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "NEIGHBORHOOD"))
                .NotEmpty()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "NEIGHBORHOOD"))
                .MaximumLength(50)
                .WithMessage(string.Format(_localizer.GetString("SIZE_RULE"), "NEIGHBORHOOD", 50));

            RuleFor(x => x.Number)
                .NotNull()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "NUMBER"))
                .NotEmpty()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "NUMBER"))
                .MaximumLength(5)
                .WithMessage(string.Format(_localizer.GetString("SIZE_RULE"), "NUMBER", 5))
                .Must((number) =>
                {
                    int.TryParse(number, out int result);

                    if (result > 0 || (result < 1 && number.ToUpper().Equals("SN")))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });

            RuleFor(x => x.ZipCode)
                .NotNull()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "ZIPCODE"))
                .NotEmpty()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "ZIPCODE"))
                .MaximumLength(8)
                .WithMessage(string.Format(_localizer.GetString("SIZE_RULE"), "ZIPCODE", 8))
                .Must((number) =>
                {
                    int.TryParse(number, out int result);

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });
        }
    }
}
