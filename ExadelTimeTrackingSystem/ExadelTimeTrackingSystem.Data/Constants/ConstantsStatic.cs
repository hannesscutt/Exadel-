namespace ExadelTimeTrackingSystem.Data.Constants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ConstantsStatic
    {
    }

    public class ValidationConstants : ConstantsStatic
    {
        private const string EXISTSASYNCERROR = "ERROR: Invalid ApproverId entered";
        private const string EXISTASYNCERROR = "ERROR: Invalid EmployeeIds entered";

        public static string ExistsError()
        {
            return EXISTSASYNCERROR;
        }

        public static string ExistError()
        {
            return EXISTASYNCERROR;
        }
    }
}
