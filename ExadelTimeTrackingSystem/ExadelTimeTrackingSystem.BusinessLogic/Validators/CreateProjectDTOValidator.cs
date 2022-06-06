namespace ExadelTimeTrackingSystem.Data.Validators
{
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using FluentValidation;

    public class CreateProjectDTOValidator : AbstractValidator<CreateProjectDTO>
    {
        public CreateProjectDTOValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.ApproverId).NotEmpty();
            RuleFor(p => p.Activities).NotEmpty();
        }
    }
}
