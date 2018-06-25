using magicmanam.RemoteManagement.EventViewer;
using magicmanam.RemoteManagement.Services;
using System;

namespace magicmanam.RemoteManagement
{
    public interface IRemoteShell : IDisposable
    {
        IServicesShell Services { get; }

        IEventViewerShell Events { get; }
    }
}
