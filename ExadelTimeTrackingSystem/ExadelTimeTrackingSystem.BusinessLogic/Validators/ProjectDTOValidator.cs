namespace ExadelTimeTrackingSystem.Data.Validators
{
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using FluentValidation;

    public class ProjectDTOValidator : AbstractValidator<ProjectDTO>
    {
        private readonly IUserService _userService;
        private CreateProjectDTOValidator createProjectDtoValidator;

        public ProjectDTOValidator(IUserService userService)
        {
            _userService = userService;
            createProjectDtoValidator = new CreateProjectDTOValidator(_userService);
            createProjectDtoValidator.ConfigureRules();
            ConfigureRules();
        }

        public void ConfigureRules()
        {
            RuleFor(p => p.Id)
                   .NotEmpty()
                   .MustAsync(_userService.ExistAsync)
                   .WithMessage(Constants.Validation.ID_DOES_NOT_EXIST);
        }
    }
}