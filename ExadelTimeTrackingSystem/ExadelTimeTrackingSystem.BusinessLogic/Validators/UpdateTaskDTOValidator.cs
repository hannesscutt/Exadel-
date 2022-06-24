namespace ExadelTimeTrackingSystem.BusinessLogic.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
    using ExadelTimeTrackingSystem.BusinessLogic.Validators.Abstract;
    using FluentValidation;

    public class UpdateTaskDTOValidator : BaseTaskDTOValidator<UpdateTaskDTO>
    {
        private readonly ITaskService _taskService;

        public UpdateTaskDTOValidator(IUserService userService, IProjectService projectService, ITaskService taskService)
            : base(userService, projectService)
        {
            _taskService = taskService;
            ConfigureRules();
        }

        private void ConfigureRules()
        {
            RuleFor(p => p.Id)
                   .NotEmpty()
                   .MustAsync(_taskService.ExistsAsync)
                   .WithMessage(Constants.Validation.ID_DOES_NOT_EXIST);
        }
    }
}
