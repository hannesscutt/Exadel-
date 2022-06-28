namespace ExadelTimeTrackingSystem.BusinessLogic.DTOs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ApproveTaskDTO
    {
        public DateTime Date { get; set; }

        public Guid ProjectId { get; set; }

        public Guid EmployeeId { get; set; }
    }
}
