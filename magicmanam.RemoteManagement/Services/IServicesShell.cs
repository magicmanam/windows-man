using System.Collections.Generic;

namespace magicmanam.RemoteManagement.Services
{
    public interface IServicesShell
    {
        IEnumerable<ServiceObject> GetServices();
        void RestartService(string serviceName);
        void SetServiceStatus(string serviceName, string status);
    }
}
