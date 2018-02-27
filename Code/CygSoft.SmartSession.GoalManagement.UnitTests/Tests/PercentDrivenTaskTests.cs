using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement.UnitTests.Tests
{
    [TestFixture]
    public class PercentDrivenTaskTests
    {
        [Test]
        public void PercentDrivenTask_Existing_Has_Set_State_Via_Constructor()
        {
            int minutesRecorded = 12;
            int weighting = 10;     // in percent.
            int progress = 25;

            PercentDrivenTask speedTask = new PercentDrivenTask(weighting, minutesRecorded, progress);

            Assert.AreEqual(minutesRecorded, speedTask.MinutesRecorded);
            Assert.AreEqual(progress, speedTask.PercentCompleted);
            Assert.AreEqual(weighting, speedTask.Weighting);
        }
    }
}
