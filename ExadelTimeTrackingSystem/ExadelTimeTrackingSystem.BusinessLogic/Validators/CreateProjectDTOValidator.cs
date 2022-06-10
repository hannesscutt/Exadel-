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
                .MustAsync(async (id, cancellation) =>
                {
                    bool exists = await _service.ExistsAsync(id);
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
                    bool exists = await _service.ListExistsAsync(ids);
                    return exists;
                });

            RuleForEach(p => p.EmployeeIds)
                .NotEmpty()
                .MustAsync(async (id, cancellation) =>
                {
                    bool exists = await _service.ExistsAsync(id);
                    return exists;
                });
        }
    }
}
