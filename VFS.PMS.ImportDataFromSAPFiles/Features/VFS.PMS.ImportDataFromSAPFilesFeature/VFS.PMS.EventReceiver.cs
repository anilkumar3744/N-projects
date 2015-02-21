using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Administration;
using Microsoft.Practices.SharePoint.Common.Logging;

namespace VFS.PMS.ImportDataFromSAPFiles.Features.VFS.PMS.ImportDataFromSAPFilesFeature
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("376713d0-d1df-461d-b6ab-57c9f80f15ea")]
    public class VFSPMSEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

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

                    SAPDataPortJob tmrJob = new SAPDataPortJob("VFS PMS SAP Data Import Timer job", webApp);
                    //remove the key if already exists
                    bool isKeyExists = tmrJob.Properties.ContainsKey(key);
                    if (isKeyExists)
                    {
                        tmrJob.Properties.Remove(key);
                    }
                    tmrJob.Properties.Add(key, value);
                    SPDailySchedule schedule = new SPDailySchedule();
                    schedule.BeginHour = 0;
                    //SPMinuteSchedule schedule = new SPMinuteSchedule();
                    //schedule.BeginSecond = 0; //to start immediately
                    //// schedule.EndSecond = 59; //use this if timer job is to end after some seconde
                    //schedule.Interval = 60; //number of minutes
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
                        if (job.Name == "VFS PMS SAP Data Import Timer job") job.Delete();
                    web.AllowUnsafeUpdates = false;
                });
            }
            catch (Exception ex)
            {
                //log exception if any
            }
        }


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


    }
}
