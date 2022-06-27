namespace ExadelTimeTrackingSystem.BusinessLogic.Validators.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
    using FluentValidation;

    public abstract class BaseTaskDTOValidator<TDTO> : AbstractValidator<TDTO>
        where TDTO : CreateTaskDTO
    {
        private readonly IUserService _userService;
        private readonly IProjectService _projectService;

        public BaseTaskDTOValidator(IUserService userService, IProjectService projectService)
        {
            _userService = userService;
            _projectService = projectService;
            ConfigureRules();
        }

        private void ConfigureRules()
        {
            RuleFor(t => t.HoursSpent)
                .InclusiveBetween(1, 24);

            RuleFor(t => t.ApproverId)
                .NotEmpty()
                .MustAsync(_userService.ExistsAsync)
                .WithMessage(Constants.Validation.APPROVER_ID_DOES_NOT_EXIST);

            RuleFor(t => t.Date)
                .NotEmpty();

            RuleFor(t => t.ProjectId)
               .NotEmpty()
               .MustAsync(_projectService.ExistsAsync)
               .WithMessage(Constants.Validation.PROJECT_ID_DOES_NOT_EXIST);

            RuleFor(t => t.Activity)
                .NotEmpty()
                .MustAsync((t, activity, cancellationToken) => _projectService.ActivityExistsAsync(activity, t.ProjectId, cancellationToken))
                .WithMessage(Constants.Validation.ACTIVITY_NOT_FOUND);

            RuleFor(t => t.Description)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(t => t.EmployeeId)
                .NotEmpty()
                .MustAsync(_userService.ExistsAsync)
                .WithMessage(Constants.Validation.EMPLOYEE_ID_DOES_NOT_EXIST);
        }
    }
}
