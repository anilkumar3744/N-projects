using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.Practices.SharePoint.Common.Logging;

namespace VFS.PMS.ImportDataFromSAPFiles
{
    /// <summary>
    /// Feature receiver registering the categories & area. 
    /// 
    /// </summary>
    /// 
    public class DiagnosticsRegistrationReceiver : SPFeatureReceiver
    {
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
        }

        /// <summary>
        /// Occurs when a Feature is deactivated.
        /// </summary>
        /// <param name="properties">An <see cref="T:Microsoft.SharePoint.SPFeatureReceiverProperties" /> object that represents the properties of the event.</param>
        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            LogConfigurationHelper.RemoveArea(ConstValues.AREA_DEFAULT);
        }
    }
}
