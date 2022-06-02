namespace ExadelTimeTrackingSystem.BusinessLogic.DTOs
{
    using System;
    using System.Collections.Generic;

    public class BulkTaskDTO
    {
        public CreateTaskDTO Task { get; set; }

        public List<DateTime> Dates { get; set; }

        //public List<TaskDTO> Tasks { get; set; }
    }
}
