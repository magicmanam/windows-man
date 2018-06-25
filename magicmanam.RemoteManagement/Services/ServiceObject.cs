using System.ServiceProcess;

namespace magicmanam.RemoteManagement.Services
{
    public class ServiceObject
    {
        internal ServiceObject(ServiceController service)
        {
            Status = service.Status.ToString();
            Name = service.ServiceName;
        }

        public string Status { get; private set; }
        public string Name { get; private set; }
    }
}
