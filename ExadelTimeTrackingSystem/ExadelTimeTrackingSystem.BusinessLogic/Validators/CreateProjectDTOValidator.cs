namespace ExadelTimeTrackingSystem.Data.Validators
{
    using ExadelTimeTrackingSystem.BusinessLogic;

    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
    using FluentValidation;

    public class CreateProjectDTOValidator : BaseProjectDTOValidator<CreateProjectDTO>
    {
        public CreateProjectDTOValidator(IUserService userService)
             : base(userService)
        {
        }
    }
}
