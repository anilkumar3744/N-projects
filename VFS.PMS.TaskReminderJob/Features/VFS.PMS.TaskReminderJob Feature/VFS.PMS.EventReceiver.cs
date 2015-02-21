using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Administration;
using Microsoft.Practices.SharePoint.Common.Logging;

namespace VFS.PMS.TaskReminderJob.Features.VFS.PMS.TaskReminderJob_Feature
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("86e385d8-3d40-40fe-adda-c1622f79991f")]
    public class VFSPMSEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        //public override void FeatureActivated(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            try
            {

                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    SPWeb web = properties.Feature.Parent as SPWeb;
                    web.AllowUnsafeUpdates = true;
                    SPWebApplication webApp = web.Site.WebApplication;
                    foreach (SPJobDefinition job in webApp.JobDefinitions)
                        if (job.Name == "VFS PMS SAP Data Import Timer job") job.Delete();

                    string key = "mySiteUrl";
                    string value = web.Url;

                    TaskReminderJob tmrJob = new TaskReminderJob("VFS PMS Task Reminder Timer Job", webApp);
                    //remove the key if already exists
                    bool isKeyExists = tmrJob.Properties.ContainsKey(key);
                    if (isKeyExists)
                    {
                        tmrJob.Properties.Remove(key);
                    }
                    tmrJob.Properties.Add(key, value);
                    SPDailySchedule schedule = new SPDailySchedule();
                    schedule.BeginHour = 1;
                    tmrJob.Schedule = schedule;
                    tmrJob.Update();

                    web.AllowUnsafeUpdates = false;
                });
                ///
            }
            catch (Exception)
            {
                //log exception if any
            }
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.
        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            try
            {
                //remove the scheduled job
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    SPWeb web = properties.Feature.Parent as SPWeb;
                    web.AllowUnsafeUpdates = true;
                    SPWebApplication webApp = web.Site.WebApplication;
                    foreach (SPJobDefinition job in webApp.JobDefinitions)
                        if (job.Name == "VFS PMS Task Reminder Timer Job") job.Delete();
                    web.AllowUnsafeUpdates = false;
                });
            }
            catch (Exception ex)
            {
                //log exception if any
            }
        }

    }
}
