using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExadelTimeTrackingSystem.Data.Models
{
    public class Task
    {
        public string _id { get; set; }

        public int TimeSpent { get; set; }

        public string Status { get; set; }

        public string Activity { get; set; }

        public string ApproverID { get; set; }

        public DateTime Date { get; set; }

        public string EmployeeID { get; set; }

        public bool IsOvertime { get; set; }

        public string ProjectName { get; set; }

    }
}
