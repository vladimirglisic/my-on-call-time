using HrServiceContract.Interfaces;
using System;

namespace MonitorService
{
    public class EarningsMonitorService : IEarningsMonitorService
    {
        private IEmployeeService m_EmployeeService;
        private DateTime m_CurrentTime;

        public EarningsMonitorService(IEmployeeService employeeService, DateTime currentTime)
        {
            m_EmployeeService = employeeService;
            m_CurrentTime = currentTime;
        }

        public decimal GetOpenSessionEarnings(int employeeId)
        {
            try
            {
                var contract = m_EmployeeService.GetContract(employeeId);
                var session = m_EmployeeService.GetOpenIncident(employeeId);
                if (session.StartTime > m_CurrentTime) return 0;
                decimal amount = (decimal)(m_CurrentTime - session.StartTime).TotalMinutes / 60m * contract.HourlyWage;
                return amount;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occured.", ex);
            }
        }
    }
}
