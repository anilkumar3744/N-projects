using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Administration;
using Microsoft.Practices.SharePoint.Common.Logging;
using Microsoft.SharePoint;
using Microsoft.Practices.SharePoint.Common.ServiceLocation;

namespace VFS.PMS.ImportDataFromSAPFiles
{
    /// <summary>
    /// This class defines a timer job that will run on each WFE once.  The timer job will create the event 
    /// sources for the logger if the event sources do not already exist.  Note that the account the timer 
    /// job runs under must have sufficient permissions to write to the registry.  The default account 
    /// is NetworkService, which does not have permission to write to the directory.  This account is 
    /// changed through central admin.  You will first need to create a managed account 
    /// (CentralAdmin->Security->Configure Managed Accounts) with an account that has sufficent permission.
    /// You then assign the account (CentralAdmin->Security->Configure Service Accounts) to the Farm Account, 
    /// which is used by the timer.  At this point this task will work.
    /// </summary>
    public class CreateLoggingEventSourceJob : SPJobDefinition
    {
        public static string JobName { get { return "CreatePMSLoggingEventSources"; } }
        public static string JobTitle { get { return "Create PMS Logging Event Sources"; } }

        /// <summary>
        /// Creates the job instance
        /// </summary>
        public CreateLoggingEventSourceJob()
            : base(JobName, SPFarm.Local.TimerService, null, SPJobLockType.None)
        {
        }

        /// <summary>
        /// A description of the job being ran
        /// </summary>
        public override string Description
        {
            get
            {
                return "Registers the event sources on WFE's for patterns and practices Logger";
            }
        }

        /// <summary>
        /// The name to display for the job.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return "patterns and practices Event Source Registration job";
            }
        }

        /// <summary>
        /// Called by SharePoint when this job is ran.  This is where the work is done, whihc is simply
        /// calling EnsureConfiguredAreasRegistered.  This method reads the areas from configuration, and
        /// creates an event source for each if it doesn't exist.  It will also create the event source
        /// for the default category if it does not exist.
        /// </summary>
        /// <param name="targetInstance">The target instance IDfor the job</param>
        public override void Execute(Guid targetInstance)
        {
            try
            {
                DiagnosticsAreaEventSource.EnsureConfiguredAreasRegistered();
            }
            catch (Exception e)
            {
                var logger = SharePointServiceLocator.GetCurrent().GetInstance<ILogger>();
                logger.TraceToDeveloper(e);
                logger.LogToOperations(e.ToString() + " INNER EXCEPTION: e.InnerException.ToString()",
                    EventSeverity.ErrorCritical);
                throw;
            }
        }

        /// <summary>
        /// This method sets up the timer to fire.  This is the method after the logging categories are 
        /// setup that an application can call to register the event sources.
        /// </summary>
        public static void ScheduleJob()
        {
            CleanUpJobs(SPFarm.Local.TimerService.JobDefinitions);
            var job = new CreateLoggingEventSourceJob();
            job.Schedule = new SPOneTimeSchedule(DateTime.Now);
            job.Update();
        }

        /// <summary>
        /// Cleans up and old versions found of the job.
        /// </summary>
        /// <param name="jobs">The job list.</param>
        private static void CleanUpJobs(SPJobDefinitionCollection jobs)
        {
            foreach (SPJobDefinition job in jobs)
            {
                if (job.Name.Equals(CreateLoggingEventSourceJob.JobName,
                                        StringComparison.OrdinalIgnoreCase))
                {
                    job.Delete();
                }
            }
        }
    }
}
