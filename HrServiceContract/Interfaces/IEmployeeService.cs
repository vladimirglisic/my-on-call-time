using HrServiceContract.Model;

namespace HrServiceContract.Interfaces
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Return a Contract of the employee containing his hourly incident wage
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>Contract</returns>
        Contract GetContract(int employeeId);

        /// <summary>
        /// Returns the open incident with logged moment (timestamp/date and time) when the engineer started to work on it
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>Open incident</returns>
        Incident GetOpenIncident(int employeeId);
    }
}
