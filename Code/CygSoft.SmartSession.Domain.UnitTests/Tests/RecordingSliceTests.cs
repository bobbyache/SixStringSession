using CygSoft.SmartSession.Domain.Sessions;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.UnitTests.Tests
{
    [TestFixture]
    public class RecordingSliceTests
    {
        [Test]
        public void RecordingSlice_Seconds_Calculated_Correctly()
        {
            var recordingSlice = new Recorder.RecordingSlice(DateTime.Parse("2018/10/19 10:00:00"), DateTime.Parse("2018/10/19 10:30:00"));
            Assert.That(recordingSlice.Seconds, Is.EqualTo(1800));
        }

        [Test]
        public void RecordingSlice_If_StartTime_Later_Than_EndTime_Throws_Exception()
        {
            object testDelegate()
                => new Recorder.RecordingSlice(DateTime.Parse("2018/10/19 10:00:00"), DateTime.Parse("2018/10/19 09:59:59"));

            Assert.That(testDelegate, Throws.TypeOf<ArgumentOutOfRangeException>());
        }
    }
}
