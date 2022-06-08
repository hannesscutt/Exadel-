namespace ExadelTimeTrackingSystem.BusinessLogic.DTOs
{
    using System;

    public class TaskDTO : CreateTaskDTO
    {
        public Guid Id { get; set; }

        public string ProjectName { get; set; }

        public StatusDTO Status { get; set; }
    }
}