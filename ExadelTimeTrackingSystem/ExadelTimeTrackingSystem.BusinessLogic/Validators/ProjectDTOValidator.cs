namespace ExadelTimeTrackingSystem.Data.Validators
{
    using ExadelTimeTrackingSystem.BusinessLogic;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
    using FluentValidation;
    using static ExadelTimeTrackingSystem.BusinessLogic.Constants;

    public class ProjectDTOValidator : AbstractProjectValidator<ProjectDTO>
    {
        private readonly IProjectService _projectService;

        public ProjectDTOValidator(IProjectService projectservice, IUserService userservice)
            : base(userservice)
        {
            _projectService = projectservice;
            ConfigureRules();
            base.ConfigureRules();
        }

        public override void ConfigureRules()
        {
            RuleFor(p => p.Id)
                   .NotEmpty()
                   .MustAsync(_projectService.ExistsAsync)
                   .WithMessage(Validation.ID_DOES_NOT_EXIST);
        }
    }
}