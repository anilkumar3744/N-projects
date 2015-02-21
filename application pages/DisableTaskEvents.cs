// -----------------------------------------------------------------------
// <copyright file="DisableTaskEvents.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace VFS.PMS.ApplicationPages.Layouts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DisableTaskEvents : Microsoft.SharePoint.SPItemEventReceiver
    {

        public DisableTaskEvents() { }
        new public void DisableEventFiring()
        {

            //throw new NotImplementedException();

            base.DisableEventFiring();
            //EventFiringEnabled = false;
        }
        new public void EnableEventFiring()
        {
            base.EnableEventFiring();
            //EventFiringEnabled = true;
        }

    }
}
