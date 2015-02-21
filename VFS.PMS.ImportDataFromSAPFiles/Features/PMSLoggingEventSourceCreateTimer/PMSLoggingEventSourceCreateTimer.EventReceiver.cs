using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.Practices.SharePoint.Common.Logging;
using Microsoft.Practices.SharePoint.Common.ServiceLocation;
using Microsoft.Practices.SharePoint.Common.Configuration;

namespace VFS.PMS.ImportDataFromSAPFiles.Features.PMSLoggingEventSourceCreateTimer
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("3b0c2664-30f8-4f6d-b1d0-24d88213ce54")]
    public class PMSLoggingEventSourceCreateTimerEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            // recreate the area & categories
            LogConfigurationHelper.RemoveArea(ConstValues.AREA_DEFAULT);

            // create the area
            DiagnosticsArea newArea = new DiagnosticsArea(ConstValues.AREA_DEFAULT);

            // for each loglevel, add a category to the area
            newArea.DiagnosticsCategories.Add(
                new DiagnosticsCategory(ConstValues.LOGCATEGORY_DEFAULT_ERROR,
                    Microsoft.SharePoint.Administration.EventSeverity.Error,
                    Microsoft.SharePoint.Administration.TraceSeverity.Unexpected));

            newArea.DiagnosticsCategories.Add(
                new DiagnosticsCategory(ConstValues.LOGCATEGORY_DEFAULT_WARNING,
                    Microsoft.SharePoint.Administration.EventSeverity.Warning,
                    Microsoft.SharePoint.Administration.TraceSeverity.Medium));

            newArea.DiagnosticsCategories.Add(
                new DiagnosticsCategory(ConstValues.LOGCATEGORY_DEFAULT_VERBOSE,
                    Microsoft.SharePoint.Administration.EventSeverity.Verbose,
                    Microsoft.SharePoint.Administration.TraceSeverity.Verbose));


            LogConfigurationHelper.AddArea(newArea);
            
            // Esure the area is registered
            DiagnosticsAreaEventSource.EnsureConfiguredAreasRegistered();

            CreateLoggingEventSourceJob.ScheduleJob();
        }
        // Uncomment the method below to handle the event raised before a feature is deactivated.

        /// <summary>
        /// Occurs when a Feature is deactivated.
        /// </summary>
        /// <param name="properties">An <see cref="T:Microsoft.SharePoint.SPFeatureReceiverProperties" /> object that represents the properties of the event.</param>
        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            LogConfigurationHelper.RemoveArea(ConstValues.AREA_DEFAULT);
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
