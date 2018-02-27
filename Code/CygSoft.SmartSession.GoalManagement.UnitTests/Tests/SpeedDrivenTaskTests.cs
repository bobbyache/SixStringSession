using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement.UnitTests.Tests
{
    [TestFixture]
    public class SpeedDrivenTaskTests
    {
        [Test]
        public void SpeedDrivenTask_Has_SetState_Via_Constructor()
        {
            int minutesRecorded = 12;
            int currentSpeed = 60;
            int targetSpeed = 80;
            int speedIncrement = 5; // how much to increment toward target speed when the current speed is mastered.
            int weighting = 10;     // in percent.
            SpeedDrivenTask speedTask = new SpeedDrivenTask(minutesRecorded, currentSpeed, targetSpeed, speedIncrement, weighting);

            Assert.AreEqual(minutesRecorded, speedTask.MinutesRecorded);
            Assert.AreEqual(currentSpeed, speedTask.CurrentSpeed);
            Assert.AreEqual(targetSpeed, speedTask.TargetSpeed);
            Assert.AreEqual(speedIncrement, speedTask.SpeedIncrement);
            Assert.AreEqual(weighting, speedTask.Weighting);
        }
    }
}
