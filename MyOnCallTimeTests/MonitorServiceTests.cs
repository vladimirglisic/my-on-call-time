using HrServiceContract.Interfaces;
using HrServiceContract.Model;
using MonitorService;
using Moq;
using NUnit.Framework;
using System;

namespace MyOnCallTimeTests
{
    public class MonitorServiceTests
    {
        private const decimal HourlyWage = 500;
        private readonly DateTime StartTime = new DateTime(2023, 1, 1, 9, 0, 0);

        private Mock<IEmployeeService> m_employeeServiceMock;

        [SetUp]
        public void Setup()
        {
            m_employeeServiceMock = new Mock<IEmployeeService>();
            m_employeeServiceMock.Setup(x => x.GetContract(It.IsAny<int>())).Returns(new Contract { HourlyWage = HourlyWage });
        }

        [Test]
        public void GetOpenSessionEarnings_OneHour_OneHourWage()
        {
            var now = StartTime.AddHours(1);
            m_employeeServiceMock.Setup(x => x.GetOpenIncident(It.IsAny<int>())).Returns(new Incident { StartTime = StartTime });

            var service = new EarningsMonitorService(m_employeeServiceMock.Object, now);

            decimal result = service.GetOpenSessionEarnings(It.IsAny<int>());

            Assert.That(HourlyWage, Is.EqualTo(result));
        }
    }
}