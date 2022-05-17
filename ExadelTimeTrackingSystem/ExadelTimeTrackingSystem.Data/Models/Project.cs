namespace ExadelTimeTrackingSystem.Data.Models
{
    using System;
    using ExadelTimeTrackingSystem.Data.Models.Abstract;

    public class Project : IDocument
    {
        public Guid Id { get; set; }

        public string[] Activities { get; set; }

        public string Name { get; set; }

        public Guid ApproverId { get; set; }

        public Guid[] EmployeeIds { get; set; }
    }
}