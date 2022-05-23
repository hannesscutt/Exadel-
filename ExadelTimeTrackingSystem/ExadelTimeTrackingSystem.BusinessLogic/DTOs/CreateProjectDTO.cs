namespace ExadelTimeTrackingSystem.BusinessLogic.DTOs
{
    using System;

    public class CreateProjectDTO
    {
        public string[] Activities { get; set; }

        public string Name { get; set; }

        public Guid ApproverId { get; set; }

        public Guid[] EmployeeIds { get; set; }
    }
}
