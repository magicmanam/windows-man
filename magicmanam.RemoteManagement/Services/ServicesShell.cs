using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace magicmanam.RemoteManagement.Services
{
    class ServicesShell : IServicesShell
    {
        private PowerShell _ps;
        private string _computerName;

        internal ServicesShell(PowerShell ps, string computerName)
        {
            this._ps = ps;
            this._computerName = computerName;
        }

        public IEnumerable<ServiceObject> GetServices()
        {
            this._ps.AddCommand("Get-Service")
                                  .AddParameter("ComputerName", this._computerName);

            Collection<PSObject> PSOutput = this._ps.Invoke();

            foreach (PSObject outputItem in PSOutput)
            {
                if (outputItem != null)
                {
                    var service = outputItem.BaseObject as ServiceController;
                    yield return new ServiceObject(service);
                }
            }
        }

        public void RestartService(string serviceName)
        {
            this._ps.AddCommand("Get-Service")
                              .AddParameter("ComputerName", this._computerName)
                              .AddParameter("Name", serviceName);

            this._ps.AddCommand("Restart-Service");

            Collection<PSObject> PSOutput = this._ps.Invoke();
        }

        public void SetServiceStatus(string serviceName, string status)
        {
            this._ps.AddCommand("Get-Service")
                              .AddParameter("ComputerName", this._computerName)
                              .AddParameter("Name", serviceName);

            this._ps.AddCommand("Set-Service")
                              .AddParameter("Status", status);

            Collection<PSObject> PSOutput = this._ps.Invoke();
            //PowerShellInstance.AddScript($"Restart-Service -InputObject $(Get-Service -ComputerName tdsptsb{box}.evolution1.local -Name \"{serviceName}\");");
            //PowerShellInstance.AddScript($"Get-Service \"{serviceName}\" -ComputerName tdsptsb{box}.evolution1.local | Start-Service");
        }
    }
}
