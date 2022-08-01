namespace ExadelTimeTrackingSystem.BusinessLogic
{
    public static class Constants
    {
        public static class Validation
        {
            public const string APPROVER_ID_DOES_NOT_EXIST = "ApproverId doesn't exist";
            public const string EMPLOYEE_IDS_DO_NOT_EXIST = "At least one EmployeeId not found";
            public const string ID_DOES_NOT_EXIST = "Id doesn't exist";
            public const string REQUEST_WAS_CANCELLED = "Request was cancelled";
            public const string PROJECT_ID_DOES_NOT_EXIST = "ProjectId doesn't exist";
            public const string EMPLOYEE_ID_DOES_NOT_EXIST = "EmployeeId doesn't exist";
            public const string ACTIVITY_NOT_FOUND = "Activity was not found in the specified project";
        }

        public static class MustacheTemplates
        {
            public const string EMAILTABLEHEADER = @"
                                                <html>
                                                <head>
                                                    <style>
                                                        td { width: 300; }
                                                        table { width: 900px; table-layout: fixed; }
                                                    </style>
                                                </head>
                                                <body>
                                                      <table border=""1"">
                                                        <tr text = ""blue"">
                                                            <th bgcolor=""blue"">Employee</th>
                                                            <th bgcolor=""green"">Hours</th>
                                                            <th bgcolor=""pink"">Date</th>
                                                            <th bgcolor=""red"">Project</th>
                                                        </tr>
                                                      </table>
                                                </body>
                                                </html>";

            public const string EMAILTABLEBODY = @"
                                                <html>
                                                <head>
                                                    <style>
                                                        td { width: 300; }
                                                        table { width: 900px; table-layout: fixed; }
                                                    </style>
                                                </head>
                                                <body>
                                                      <table border=""1"">
                                                        <tr>
                                                            <td>{{EmployeeName}}</td>
                                                            <td>{{Hours}}</td>
                                                            <td>{{Date}}</td>
                                                            <td>{{ProjectName}}</td>
                                                        </tr>
                                                      </table>
                                                </body>
                                                </html>";
        }
    }
}
