using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.SharePoint.Common.Logging;
using Microsoft.Practices.SharePoint.Common.ServiceLocation;
using Microsoft.Practices.SharePoint.Common.Configuration;
using Microsoft.Practices.ServiceLocation;

namespace VFS.PMS.EventReceiver.EventReceiverOnEmployeeUpdated
{
    /// <summary>
    /// 
    /// </summary>
    public static class LogConfigurationHelper
    {

        /// <summary>
        /// Currents the areas.
        /// </summary>
        /// <returns></returns>
        public static DiagnosticsAreaCollection CurrentAreas()
        {
            IServiceLocator serviceLocator = SharePointServiceLocator.GetCurrent();
            IConfigManager config = serviceLocator.GetInstance<IConfigManager>();
            DiagnosticsAreaCollection areaCollection = new DiagnosticsAreaCollection(config);
            return areaCollection;
        }

        /// <summary>
        /// Determines whether [is exist area] [the specified collection].
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="areaName">Name of the area.</param>
        /// <returns>
        ///   <c>true</c> if [is exist area] [the specified collection]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsExistArea(DiagnosticsAreaCollection collection, string areaName)
        {

            foreach (DiagnosticsArea item in collection)
            {
                if (item.Name.Trim().ToUpper() == areaName.Trim().ToUpper())
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether [is exist category in area] [the specified area name].
        /// </summary>
        /// <param name="areaName">Name of the area.</param>
        /// <param name="categoryName">Name of the category.</param>
        /// <returns>
        ///   <c>true</c> if [is exist category in area] [the specified area name]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsExistCategoryInArea(string areaName, string categoryName)
        {

            bool found = false;
            DiagnosticsAreaCollection areas = CurrentAreas();
            if (IsExistArea(areas, areaName))
            {
                DiagnosticsArea foundArea = areas[areaName];
                int index = areas.IndexOf(foundArea);
                foreach (DiagnosticsCategory item in foundArea.DiagnosticsCategories)
                {
                    if (areas[index].DiagnosticsCategories.Contains(item))
                    {
                        found = true;
                        break;
                    }
                }
            }
            return found;
        }
        /// <summary>
        /// Adds the area.
        /// </summary>
        /// <param name="newArea">The new area.</param>
        public static void AddArea(DiagnosticsArea newArea)
        {
            DiagnosticsAreaCollection areas = CurrentAreas();
            if (!IsExistArea(areas, newArea.Name))
            //if (!areas.Contains(newArea))
            {
                areas.Add(newArea);
            }
            else
            {
                int index = areas.IndexOf(newArea);
                foreach (DiagnosticsCategory item in newArea.DiagnosticsCategories)
                {
                    if (!areas[index].DiagnosticsCategories.Contains(item))
                    {
                        areas[index].DiagnosticsCategories.Add(item);
                    }
                }
            }
            areas.SaveConfiguration();
        }
        /// <summary>
        /// Removes the area.
        /// </summary>
        /// <param name="areaName">Name of the area.</param>
        public static void RemoveArea(string areaName)
        {
            DiagnosticsAreaCollection areas = CurrentAreas();
            if (IsExistArea(areas, areaName))
            {
                while (areas[areaName].DiagnosticsCategories.Count != 0)
                {
                    areas[areaName].DiagnosticsCategories.Clear();
                }
                areas.RemoveAt(areas.IndexOf(areas[areaName]));
                areas.SaveConfiguration();
            }
        }
    }
}
