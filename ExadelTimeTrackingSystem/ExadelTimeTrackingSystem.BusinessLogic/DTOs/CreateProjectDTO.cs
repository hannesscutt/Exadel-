namespace ExadelTimeTrackingSystem.BusinessLogic.DTOs
{
    using System;
    using System.Collections.Generic;

    public class CreateProjectDTO
    {
        public List<string> Activities { get; set; }

        public string Name { get; set; }

        public Guid ApproverId { get; set; }

        public Guid[] EmployeeIds { get; set; }
    }
}
