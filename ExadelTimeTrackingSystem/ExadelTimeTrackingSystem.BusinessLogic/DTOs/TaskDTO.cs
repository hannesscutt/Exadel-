namespace ExadelTimeTrackingSystem.BusinessLogic.DTOs
{
    using System;

    public class TaskDTO : UpdateTaskDTO
    {
        public string ProjectName { get; set; }

        public StatusDTO Status { get; set; }
    }
}