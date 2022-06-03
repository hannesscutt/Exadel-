namespace ExadelTimeTrackingSystem.BusinessLogic.DTOs
{
    using System;
    using System.Collections.Generic;

    public class CreateBulkTaskDTO
    {
        public CreateTaskDTO Task { get; set; }

        public List<DateTime> Dates { get; set; }
    }
}
