using HrServiceContract.Interfaces;
using System;

namespace MonitorService
{
    public class EarningsMonitorService : IEarningsMonitorService
    {
        private IEmployeeService m_EmployeeService;
        private DateTime m_now;

        public EarningsMonitorService(IEmployeeService service, DateTime now)
        {
            m_EmployeeService = service;
            m_now = now;
        }

        public decimal GetOpenSessionEarnings(int employeeId)
        {
            var contract = m_EmployeeService.GetContract(employeeId);
            var incident = m_EmployeeService.GetOpenIncident(employeeId);

            int duration = (int)(m_now - incident.StartTime).TotalMinutes;
            return duration * contract.HourlyWage / 60;
        }
    }
}
