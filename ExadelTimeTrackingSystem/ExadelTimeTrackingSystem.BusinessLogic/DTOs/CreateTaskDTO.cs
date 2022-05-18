// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
namespace ExadelTimeTrackingSystem.BusinessLogic.DTOs
{
    using System;
    using ExadelTimeTrackingSystem.Data.Models.Enums;

    public class CreateTaskDTO
    {
        public int HoursSpent { get; set; }

        public Status Status { get; set; }

        public string Activity { get; set; }

        public Guid ApproverId { get; set; }

        public DateTime Date { get; set; }

        public Guid EmployeeId { get; set; }

        public bool IsOvertime { get; set; }

        public string ProjectName { get; set; }
    }
}