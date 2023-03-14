namespace MonitorService
{
    interface IEarningsMonitorService
    {
        /// <summary>
        /// Calculates real-time earnings of the open on-call incident session for the employee.
        /// An engineer should earn extra for working on on-call incident during non-working hours.
        /// </summary>
        /// <param name="employeeId">Employee Id</param>
        /// <returns></returns>
        decimal GetOpenSessionEarnings(int employeeId);
    }
}
