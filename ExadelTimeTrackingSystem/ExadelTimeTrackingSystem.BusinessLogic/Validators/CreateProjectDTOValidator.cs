namespace ExadelTimeTrackingSystem.Data.Validators
{
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
    using FluentValidation;
    using System;

    public class CreateProjectDTOValidator : AbstractValidator<CreateProjectDTO>
    {
        private readonly IUserService _service;

        public CreateProjectDTOValidator(IUserService service)
        {
            _service = service;

            RuleFor(p => p.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.ApproverId)
                .NotEmpty()
                .Must((model, cancellation) =>
                {
                    foreach (var id in model.EmployeeIds)
                    {
                        if (_service.ExistsAsync(id).Result == false)
                        {
                            return false;
                        }
                    }

                    return true;
                });

            RuleFor(p => p.Activities)
                .NotEmpty();

            RuleForEach(p => p.Activities)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.EmployeeIds)
                .Must((model, cancellation) =>
                {
                    bool exists = _service.ListExistsAsync(model.EmployeeIds).Result;
                    return exists;
                });

            RuleForEach(p => p.EmployeeIds)
                .NotEmpty()
                .Must((model, cancellation) =>
                {
                    foreach (var id in model.EmployeeIds)
                    {
                        if (_service.ExistsAsync(id).Result == false)
                        {
                            return false;
                        }
                    }

                    return true;
                });
        }
    }
}
