using HrServiceContract.Interfaces;
using HrServiceContract.Model;
using MonitorService;
using Moq;
using NUnit.Framework;
using System;
using System.Net;

namespace MyOnCallTimeTests
{
    public class MonitorServiceTests
    {
        private readonly DateTime StartDate = new DateTime(2023, 1, 1, 9, 0, 0);
        private const decimal HourlyWages = 100;

        private Mock<IEmployeeService> m_mockEmployeeService; 

        [SetUp]
        public void Setup()
        {
            m_mockEmployeeService = new Mock<IEmployeeService>();
            m_mockEmployeeService.Setup(x => x.GetContract(It.IsAny<int>())).Returns(new Contract { HourlyWage = HourlyWages });
            m_mockEmployeeService.Setup(x => x.GetOpenIncident(It.IsAny<int>())).Returns(new Incident { StartTime = StartDate });
        }

        [TestCase("2023-01-01 9:30:00", 50)]
        [TestCase("2023-01-01 10:00:00", 100)]
        [TestCase("2023-01-01 8:00:00", 0)]
        public void GetOpenSessionEarnings_General_General(DateTime currentTime, decimal amount)
        {
            // prepare
            var monitor = new EarningsMonitorService(m_mockEmployeeService.Object, currentTime);

            // execute
            decimal result = monitor.GetOpenSessionEarnings(It.IsAny<int>());

            // assert
            Assert.That(result, Is.EqualTo(amount));
        }

        [Test]
        public void GetOpenSessionEarnings_ThrowsWebException_ThrowsApplicationException()
        {
            // prepare
            m_mockEmployeeService.Setup(x => x.GetContract(It.IsAny<int>())).Throws(new WebException());
            var monitor = new EarningsMonitorService(m_mockEmployeeService.Object, It.IsAny<DateTime>());

            // execute & assert
            Assert.Throws<ApplicationException>(() =>
                monitor.GetOpenSessionEarnings(It.IsAny<int>())
            );
        }
    }
}