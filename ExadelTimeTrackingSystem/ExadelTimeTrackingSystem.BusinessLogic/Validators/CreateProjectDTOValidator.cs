namespace ExadelTimeTrackingSystem.Data.Validators
{
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
    using ExadelTimeTrackingSystem.Data.Constants;
    using FluentValidation;

    public class CreateProjectDTOValidator : AbstractValidator<CreateProjectDTO>
    {
        private readonly IUserService _userservice;

        public CreateProjectDTOValidator(IUserService userservice)
        {
            _userservice = userservice;
            ConfigureRules();
        }

        private void ConfigureRules()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.ApproverId)
                .NotEmpty()
                .MustAsync(_userservice.ExistsAsync)
                .WithMessage(ValidationConstants.ExistsError());

            RuleFor(p => p.Activities)
                .NotEmpty();

            RuleForEach(p => p.Activities)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.EmployeeIds)
                .NotEmpty()
                .MustAsync(_userservice.ExistAsync)
                .WithMessage(ValidationConstants.ExistError());

            RuleForEach(p => p.EmployeeIds)
                .NotEmpty();
        }
    }
}
