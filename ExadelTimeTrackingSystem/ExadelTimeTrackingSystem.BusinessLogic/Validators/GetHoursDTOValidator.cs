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

    public class GetHoursDTOValidator : AbstractValidator<GetHoursDTO>
    {
        private readonly IUserService _userService;

        public GetHoursDTOValidator(IUserService userService)
        {
            _userService = userService;
            ConfigureRules();
        }

        private void ConfigureRules()
        {
            RuleFor(t => t.EmployeeId)
                .NotEmpty()
                .MustAsync(_userService.ExistsAsync)
                .WithMessage(Constants.Validation.EMPLOYEE_ID_DOES_NOT_EXIST);

            RuleFor(t => t.Date)
                .NotEmpty()
                .Must((t, cancellationToken) => t.Date.DayOfWeek.Equals(DayOfWeek.Sunday));
        }
    }
}
