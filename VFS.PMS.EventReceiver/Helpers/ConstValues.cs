using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VFS.PMS.EventReceiver.EventReceiverOnEmployeeUpdated
{
    /// <summary>
    /// Constants for use in the current solution. 
    /// </summary>
    public class ConstValues
    {
        // event ID
        public const int EVENTID = 9999;
        public const int PMSTRACEEVENTID = 99998;

        // The log Area to be used
        public const string AREA_DEFAULT = "VFS Performance Management System";

        // Different Categories within the Area for Error, Warning and Verbose
        public const string LOGCATEGORY_DEFAULT_ERROR = "PMS Error";
        public const string LOGCATEGORY_DEFAULT_WARNING = "PMS Warning";
        public const string LOGCATEGORY_DEFAULT_VERBOSE = "PMS Message";
        public const string LOGCATEGORY_IMPORTJOB = "PMS SAP Data Import Job";
        public const string LOGCATEGORY_APPLICATIONPAGES = "PMS Application Pages";
        public const string LOGCATEGORY_EventReceivers = "PMS Event Receivers";

        //Constants for SAP data files
        public const string DATAFILE_AREAS = "Areas_";
        public const string DATAFILE_SUBAREAS = "SubAreas_";
        public const string DATAFILE_POSITIONS = "Positions_";
        public const string DATAFILE_ORGANIZATIONUNITS = "OrganizationUnits_";
        public const string DATAFILE_COMPANYNAMES = "CompanyNames_";
        public const string DATAFILE_REGIONS = "Regions_";
        public const string DATAFILE_EMPLOYEEDATA = "EmployeeData_";
        public const string DATAFILE_EMPLOYEEGROUPS = "EmployeeGroups_";
        public const string DATAFILE_EMPLOYEESUBGROUPS = "EmployeeSubGroups_";
        public const string DATAFILE_EXTENSION = ".csv";

    }
}
