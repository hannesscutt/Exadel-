using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExadelTimeTrackingSystem.Data.Models
{
    public class Project
    {
        public string _id { get; set; }

        public int[] Activities { get; set; }

        public string Name { get; set; }

        public string ApproverID { get; set; }

        public string[] EmployeeIDs { get; set; }
    }
}