using FluentValidation;
using MailSystem.Core.Resources;
using System;

namespace MailSystem.API.DTO.Validators
{
    /// <summary>
    /// Class to validate DeliveryDTO
    /// </summary>
    public class DeliveryDTOValidator : AbstractValidator<DeliveryDTO>
    {
        private readonly IResourceLocalizer _localizer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="localizer">Object that contains all static messages</param>
        public DeliveryDTOValidator(IResourceLocalizer localizer)
        {
            _localizer = localizer;

            RuleFor(x => x.Description)
                .NotNull()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "DESCRIPTION"))
                .NotEmpty()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "DESCRIPTION"))
                .MaximumLength(100)
                .WithMessage(string.Format(_localizer.GetString("SIZE_RULE"), "DESCRIPTION", 100));

            RuleFor(x => x.TrackCode)
                .NotNull()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "TRACKCODE"))
                .NotEmpty()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "TRACKCODE"))
                .MaximumLength(45)
                .WithMessage(string.Format(_localizer.GetString("SIZE_RULE"), "TRACKCODE", 45));

            RuleFor(x => x.Type)
                .NotNull()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "TYPE"))
                .NotEmpty()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "TYPE"))
                .MaximumLength(50)
                .WithMessage(string.Format(_localizer.GetString("SIZE_RULE"), "TYPE", 50));

            RuleFor(x => x.DeliveredTo)
                .NotNull()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "DELIVERED_TO"))
                .NotEmpty()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "DELIVERED_TO"))
                .MaximumLength(100)
                .WithMessage(string.Format(_localizer.GetString("SIZE_RULE"), "DELIVERED_TO", 100));

            RuleFor(x => x.ArrivedAt)
                .NotNull()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "ARRIVED_AT"))
                .NotEmpty()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "ARRIVED_AT"))
                .Must(x => x != DateTime.MinValue && x != DateTime.MaxValue)
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "ARRIVED_AT"));
        }
    }
}
