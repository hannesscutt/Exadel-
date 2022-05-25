namespace ExadelTimeTrackingSystem.Data.Models
{
    using System;
    using ExadelTimeTrackingSystem.Data.Models.Abstract;
    using ExadelTimeTrackingSystem.Data.Models.Enums;

    public class Task : IDocument
    {
        public Guid Id { get; set; }

        public int HoursSpent { get; set; }

        public Status Status { get; set; }

        public string Activity { get; set; }

        public string Description { get; set; }

        public Guid ApproverId { get; set; }

        public DateTime Date { get; set; }

        public Guid EmployeeId { get; set; }

        public bool IsOvertime { get; set; }

        public string ProjectName { get; set; }
    }
}