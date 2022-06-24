namespace ExadelTimeTrackingSystem.BusinessLogic.DTOs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UpdateTaskDTO : CreateTaskDTO
    {
        public Guid Id { get; set; }
    }
}
