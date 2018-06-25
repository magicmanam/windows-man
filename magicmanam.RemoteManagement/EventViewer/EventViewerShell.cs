using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace magicmanam.RemoteManagement.EventViewer
{
    class EventViewerShell : IEventViewerShell
    {
        private readonly string _computerName;

        public EventViewerShell(string computerName)
        {
            this._computerName = computerName;
        }

        public IEnumerable<EventRecord> ReadEvents(string path, int minutes, bool reverseDirection = true)
        {
            string queryString =
                "<QueryList>" +
                $" <Query Id=\"0\" Path=\"{path}\">" +
                " <Select Path=\"Application\">" +
                $" *[System[TimeCreated[timediff(@SystemTime) &lt;= {minutes * 60}000]]]" +
                " </Select>" +
                " </Query>" +
                "</QueryList>";
            
            EventLogSession session = new EventLogSession(this._computerName);
            EventLogQuery query = new EventLogQuery(path, PathType.LogName, queryString)
            {
                Session = session,
                ReverseDirection = reverseDirection
            };

            var result = new List<EventRecord>();

            try
            {
                EventLogReader logReader = new EventLogReader(query);

                for (EventRecord eventInstance = logReader.ReadEvent(); null != eventInstance; eventInstance = logReader.ReadEvent())
                {
                    result.Add(eventInstance);
                }
            }
            catch (EventLogException) { }

            return result;
        }
    }
}
