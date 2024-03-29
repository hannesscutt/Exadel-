﻿namespace ExadelTimeTrackingSystem.BusinessLogic.DTOs
{
    using System;

    public class CreateTaskDTO
    {
        public int HoursSpent { get; set; }

        public string Activity { get; set; }

        public Guid ApproverId { get; set; }

        public DateTime Date { get; set; }

        public Guid EmployeeId { get; set; }

        public bool IsOvertime { get; set; }

        public Guid ProjectId { get; set; }

        public string Description { get; set; }
    }
}