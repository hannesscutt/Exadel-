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

    public class CreateTaskDTOValidator : BaseTaskDTOValidator<CreateTaskDTO>
    {
        public CreateTaskDTOValidator(IUserService userService, IProjectService projectService)
             : base(userService, projectService)
        {
        }
    }
}
