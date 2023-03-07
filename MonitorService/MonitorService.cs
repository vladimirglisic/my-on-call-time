using HrServiceContract.Interfaces;

namespace MonitorService
{
    public class MonitorService : IMonitorService
    {
        private IEmployeeService m_EmployeeService;

        public decimal GetOpenSessionEarnings(int employeeId)
        {
            // todo: exercise
            throw new System.NotImplementedException();
        }
    }
}
