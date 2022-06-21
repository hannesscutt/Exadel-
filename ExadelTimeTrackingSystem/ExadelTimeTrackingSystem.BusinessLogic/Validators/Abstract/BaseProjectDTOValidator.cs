namespace ExadelTimeTrackingSystem.Data.Validators
{
    using ExadelTimeTrackingSystem.BusinessLogic;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
    using FluentValidation;

    public abstract class BaseProjectDTOValidator<TDTO> : AbstractValidator<TDTO>
        where TDTO : CreateProjectDTO
    {
        private readonly IUserService _userService;

        public BaseProjectDTOValidator(IUserService userService)
        {
            _userService = userService;
            ConfigureRules();
        }

        private void ConfigureRules()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.ApproverId)
                .NotEmpty()
                .MustAsync(_userService.ExistsAsync)
                .WithMessage(Constants.Validation.APPROVER_ID_DOES_NOT_EXIST);

            RuleFor(p => p.Activities)
                .NotEmpty();

            RuleForEach(p => p.Activities)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.EmployeeIds)
                .NotEmpty()
                .MustAsync(_userService.ExistAsync)
                .WithMessage(Constants.Validation.EMPLOYEE_IDS_DO_NOT_EXIST);

            RuleForEach(p => p.EmployeeIds)
                .NotEmpty();
        }
    }
}