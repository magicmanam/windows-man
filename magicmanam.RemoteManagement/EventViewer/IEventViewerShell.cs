using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace magicmanam.RemoteManagement.EventViewer
{
    public interface IEventViewerShell
    {
        IEnumerable<EventRecord> ReadEvents(string path, int minutes, bool reverseDirection = true);
    }
}
