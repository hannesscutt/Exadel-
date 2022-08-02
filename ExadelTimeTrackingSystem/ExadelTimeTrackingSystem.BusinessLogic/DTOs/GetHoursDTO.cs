namespace ExadelTimeTrackingSystem.BusinessLogic.DTOs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GetHoursDTO
    {
        public DateTime Date { get; set; }

        public Guid EmployeeId { get; set; }
    }
}
