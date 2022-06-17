namespace ExadelTimeTrackingSystem.Data.Validators
{
    using ExadelTimeTrackingSystem.BusinessLogic;

    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
    using FluentValidation;

    public abstract class AbstractProjectValidator<TProjectDTO> : AbstractValidator<ProjectDTO>
        where TProjectDTO : ProjectDTO
    {
        private readonly IUserService _userservice;

        public AbstractProjectValidator(IUserService userservice)
        {
            _userservice = userservice;
            ConfigureRules();
        }

        public virtual void ConfigureRules()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.ApproverId)
                .NotEmpty()
                .MustAsync(_userservice.ExistsAsync)
                .WithMessage(Constants.Validation.APPROVER_ID_DOES_NOT_EXIST);

            RuleFor(p => p.Activities)
                .NotEmpty();

            RuleForEach(p => p.Activities)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.EmployeeIds)
                .NotEmpty()
                .MustAsync(_userservice.ExistAsync)
                .WithMessage(Constants.Validation.EMPLOYEE_IDS_DO_NOT_EXIST);

            RuleForEach(p => p.EmployeeIds)
                .NotEmpty();
        }
    }
}