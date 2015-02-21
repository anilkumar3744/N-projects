using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace VFS.PMS.EventReceiver
{
    public class DisableAppraisalTaskEvents : Microsoft.SharePoint.SPItemEventReceiver
    {

        public DisableAppraisalTaskEvents() { }
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
