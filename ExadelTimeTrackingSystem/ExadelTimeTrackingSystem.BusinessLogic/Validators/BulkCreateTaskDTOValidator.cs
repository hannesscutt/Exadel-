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

    public class BulkCreateTaskDTOValidator : AbstractValidator<BulkCreateTaskDTO>
    {
        private readonly IValidator<CreateTaskDTO> _createTaskDtoValidator;

        public BulkCreateTaskDTOValidator(IValidator<CreateTaskDTO> createTaskDtoValidator)
        {
            _createTaskDtoValidator = createTaskDtoValidator;
            ConfigureRules();
        }

        private void ConfigureRules()
        {
            RuleFor(t => t.Task)
                .NotEmpty()
                .SetValidator(_createTaskDtoValidator);

            RuleFor(t => t.Dates)
                .NotEmpty();

            RuleForEach(t => t.Dates)
                .NotEmpty();
        }
    }
}
