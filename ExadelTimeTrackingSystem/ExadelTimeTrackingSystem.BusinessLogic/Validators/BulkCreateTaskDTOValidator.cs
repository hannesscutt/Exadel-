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

    public class BulkCreateTaskDTOValidator : AbstractValidator<BulkCreateTaskDTO>
    {
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;

        public BulkCreateTaskDTOValidator(IProjectService projectService, IUserService userService)
        {
            _projectService = projectService;
            _userService = userService;
            ConfigureRules();
        }

        private void ConfigureRules()
        {
            RuleFor(t => t.Task)
                .SetValidator(new CreateTaskDTOValidator(_userService, _projectService));

            RuleFor(t => t.Dates)
                .NotEmpty();

            RuleForEach(t => t.Dates)
                .NotEmpty();
        }
    }
}
