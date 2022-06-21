namespace ExadelTimeTrackingSystem.Data.Validators
{
    using ExadelTimeTrackingSystem.BusinessLogic;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
    using FluentValidation;
    using static ExadelTimeTrackingSystem.BusinessLogic.Constants;

    public class ProjectDTOValidator : BaseProjectDTOValidator<ProjectDTO>
    {
        private readonly IProjectService _projectService;

        public ProjectDTOValidator(IProjectService projectService, IUserService userService)
            : base(userService)
        {
            _projectService = projectService;
            ConfigureRules();
        }

        private void ConfigureRules()
        {
            RuleFor(p => p.Id)
                   .NotEmpty()
                   .MustAsync(_projectService.ExistsAsync)
                   .WithMessage(Constants.Validation.ID_DOES_NOT_EXIST);
        }
    }
}