using System;
using System.Management.Automation;
using magicmanam.RemoteManagement.EventViewer;
using magicmanam.RemoteManagement.Services;

namespace magicmanam.RemoteManagement
{
    public class RemoteShell : IRemoteShell
    {
        private readonly PowerShell _ps;

        private RemoteShell(PowerShell ps, string computerName)
        {
            this._ps = ps;

            Services = new ServicesShell(ps, computerName);
            Events = new EventViewerShell(computerName);
        }

        public IServicesShell Services { get; private set; }

        public IEventViewerShell Events { get; private set; }

        public static IRemoteShell Create(string computerName)
        {
            return new RemoteShell(PowerShell.Create(), computerName);
        }

        public void Dispose()
        {
            this._ps.Dispose();
        }
    }
}
