namespace ExadelTimeTrackingSystem.BusinessLogic.DTOs
{
    using System;
    using System.Collections.Generic;

    public class BulkCreateTaskDTO
    {
        public CreateTaskDTO Task { get; set; }

        public List<DateTime> Dates { get; set; }
    }
}
