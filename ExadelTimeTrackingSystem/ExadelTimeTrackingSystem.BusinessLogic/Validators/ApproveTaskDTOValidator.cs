namespace ExadelTimeTrackingSystem.BusinessLogic.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
    using FluentValidation;

    public class ApproveTaskDTOValidator : AbstractValidator<ApproveTaskDTO>
    {
        private readonly IUserService _userService;
        private readonly IProjectService _projectService;

        public ApproveTaskDTOValidator(IUserService userService, IProjectService projectService)
        {
            _userService = userService;
            _projectService = projectService;
            ConfigureRules();
        }

        private void ConfigureRules()
        {
            RuleFor(t => t.Date)
                .NotEmpty();

            RuleFor(t => t.EmployeeId)
                .NotEmpty()
                .MustAsync(_userService.ExistsAsync)
                .WithMessage(Constants.Validation.EMPLOYEE_ID_DOES_NOT_EXIST);

            RuleFor(t => t.ProjectId)
                .NotEmpty()
                .MustAsync(_projectService.ExistsAsync)
                .WithMessage(Constants.Validation.PROJECT_ID_DOES_NOT_EXIST);
        }
    }
}
