using System;
using System.Management.Automation;
using magicmanam.RemoteManagement.EventViewer;
using magicmanam.RemoteManagement.Services;

namespace magicmanam.RemoteManagement
{
    public class RemoteShell : IRemoteShell
    {
        private readonly string _server;

        private PowerShell _powerShell;

        private IServicesShell _services;
        private IEventViewerShell _events;

        /// <summary>
        /// Initializes a new instance of the RemoteShell class.
        /// </summary>
        /// <param name="computerName">The name of the computer on which to connect.</param>
        public RemoteShell(string server)
        {
            this._server = server;
        }

        /// <summary>
        /// Gets the PowerShell instance used in the current RemoteShell instance.
        /// </summary>
        protected PowerShell PS
        {
            get
            {
                if (this._powerShell == null)
                {
                    this._powerShell = PowerShell.Create();
                }

                return this._powerShell;
            }
        }

        /// <summary>
        /// Gets the IServiceShell instance to work with Windows services. Works based on PowerShell .NET wrapper.
        /// </summary>
        public IServicesShell Services
        {
            get
            {
                if (this._services == null)
                {
                    this._services = new ServicesShell(this.PS, this._server);
                }

                return this._services;
            }
        }

        /// <summary>
        /// Gets the IEventViewerShell instance to work with Windows Event Viewer events.
        /// </summary>
        public IEventViewerShell Events {
            get
            {
                if (this._events == null)
                {
                    this._events = new EventViewerShell(this._server);
                }

                return this._events;
            }
        }

        public void Dispose()
        {
            if (this._powerShell != null)
            {
                this._powerShell.Dispose();
            }
        }
    }
}
