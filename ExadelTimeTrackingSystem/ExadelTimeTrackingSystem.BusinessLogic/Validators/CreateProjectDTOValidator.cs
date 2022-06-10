namespace ExadelTimeTrackingSystem.Data.Validators
{
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
    using FluentValidation;

    public class CreateProjectDTOValidator : AbstractValidator<CreateProjectDTO>
    {
        private readonly IUserService _userservice;

        public CreateProjectDTOValidator(IUserService userservice)
        {
            _userservice = userservice;
            this.ConfigureRules();
        }

        private void ConfigureRules()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.ApproverId)
                .NotEmpty()
                .MustAsync(async (id, cancellation) =>
                {
                    bool exists = await _userservice.ExistsAsync(id, cancellation);
                    return exists;
                });

            RuleFor(p => p.Activities)
                .NotEmpty();

            RuleForEach(p => p.Activities)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.EmployeeIds)
                .MustAsync(async (ids, cancellation) =>
                {
                    bool exists = await _userservice.ExistAsync(ids, cancellation);
                    return exists;
                });

            RuleForEach(p => p.EmployeeIds)
                .NotEmpty()
                .MustAsync(async (id, cancellation) =>
                {
                    bool exists = await _userservice.ExistsAsync(id, cancellation);
                    return exists;
                });
        }
    }
}
