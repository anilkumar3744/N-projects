// -----------------------------------------------------------------------
// <copyright file="CSVHelperClasses.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace VFS.PMS.EventReceiver.EventReceiverOnEmployeeUpdated.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CsvColumn : Attribute
    {
        public bool Export { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }

        public CsvColumn()
        {
            Export = true;
            Order = int.MaxValue; // so unordered columns are at the end
        }

    }
    public class CSVClassToExport
    {
        [CsvColumn(Name = "Employee Code", Order = 1)]
        public string EmployeeCode { get; set; }

        [CsvColumn(Name = "H1 Score", Order = 2)]
        public string H1Score { get; set; }

        [CsvColumn(Name = "H2 Score", Order = 3)]
        public string H2Score { get; set; }

        [CsvColumn(Name = "Final Score", Order = 4)]
        public string FinalScore { get; set; }

        [CsvColumn(Name = "Final Rating", Order = 5)]
        public string FinalRating { get; set; }

        [CsvColumn(Name = "Performance Cycle Start Date", Order = 6)]
        public string PerformanceCycleStartDate  { get; set; }

        [CsvColumn(Name = "Performance Cycle End Date", Order = 6)]
        public string PerformanceCycleEndDate  { get; set; }
    }
}
