using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.SharePoint.Common.ServiceLocation;
using Microsoft.Practices.SharePoint.Common.Logging;
using Microsoft.SharePoint;

namespace VFS.PMS.EventReceiver.EventReceiverOnEmployeeUpdated
{
    /// <summary>
    /// Wrapper class to be used for logging. 
    /// </summary>
    public static class LogHandler
    {
        /// <summary>
        /// log an error using the default values for eventId, category and area
        /// </summary>
        /// <param name="ex"></param>
        public static void LogError(Exception ex, string additionalMessage)
        {
            Log(ex, additionalMessage, ConstValues.EVENTID,
                Microsoft.SharePoint.Administration.TraceSeverity.Unexpected,
                ConstValues.LOGCATEGORY_DEFAULT_ERROR, ConstValues.AREA_DEFAULT);
        }

        /// <summary>
        /// Log a warning using the default category/area settings
        /// </summary>
        /// <param name="message"></param>
        public static void LogWarning(string message)
        {
            LogWarning(message, ConstValues.EVENTID, ConstValues.LOGCATEGORY_DEFAULT_WARNING, ConstValues.AREA_DEFAULT);
        }

        /// <summary>
        /// Log a warning using the given category/area settings
        /// </summary>
        /// <param name="message">Message to be logged</param>
        /// <param name="eventId">EventId under which to log</param>
        /// <param name="category">Category to use for logging</param>
        /// <param name="area">Area to use for logging</param>
        public static void LogWarning(string message, int eventId, string category, string area)
        {
            string logToCategory = category;
            string logToArea = area;

            var logger = SharePointServiceLocator.GetCurrent().GetInstance<ILogger>();
            Log(message, eventId,
                Microsoft.SharePoint.Administration.TraceSeverity.Medium,
                category,
                area);
        }

        /// <summary>
        /// Log a verbose message to the log using the default settings for category/area
        /// </summary>
        /// <param name="message"></param>
        public static void LogVerbose(string message)
        {
            LogVerbose(message, ConstValues.PMSTRACEEVENTID, ConstValues.LOGCATEGORY_DEFAULT_VERBOSE, ConstValues.AREA_DEFAULT);
        }

        /// <summary>
        /// Log a verbose message to the log using the given category/area settings
        /// </summary>
        /// <param name="message">The message to be logged</param>
        /// <param name="eventId">The eventId under which to log</param>
        /// <param name="category">Category used for logging</param>
        /// <param name="area">Area to use for logging</param>
        public static void LogVerbose(string message, int eventId, string category, string area)
        {
            var logger = SharePointServiceLocator.GetCurrent().GetInstance<ILogger>();
            Log(message, eventId,
                Microsoft.SharePoint.Administration.TraceSeverity.Verbose,
                category,
                area);
        }

        /// <summary>
        /// Logs the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="additionalMessage">The additional message.</param>
        /// <param name="eventId">The event id.</param>
        /// <param name="severity">The severity.</param>
        /// <param name="category">The category.</param>
        /// <param name="area">The area.</param>
        internal static void Log(Exception ex, string additionalMessage,
            int eventId, Microsoft.SharePoint.Administration.TraceSeverity severity,
            string category, string area)
        {

            try
            {
                var logger = SharePointServiceLocator.GetCurrent().GetInstance<ILogger>();
                logger.TraceToDeveloper(ex, additionalMessage, eventId, severity, string.Format("{0}/{1}", area, category));
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="eventId">The event id.</param>
        /// <param name="severity">The severity.</param>
        /// <param name="category">The category.</param>
        /// <param name="area">The area.</param>
        internal static void Log(string message,
            int eventId, Microsoft.SharePoint.Administration.TraceSeverity severity,
            string category, string area)
        {
            try
            {
                var logger = SharePointServiceLocator.GetCurrent().GetInstance<ILogger>();
                logger.TraceToDeveloper(message, eventId, severity, string.Format("{0}/{1}", area, category));
            }
            catch (Exception)
            {
            }
        }
    }
}
