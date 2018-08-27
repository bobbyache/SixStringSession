using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Application.UnitTests
{
    [TestFixture]
    public class SessionRecorderTests
    {
        [Test]
        public void Create_A_Recordable_Instance_Has_Session_Id()
        {
            Session session = new Session("10FA82BC-7456-4A8C-BCA0-47CB262F8AA0", "Session 1", DateTime.Parse("2018/02/23"), DateTime.Parse("2018/02/25"));
            SessionInstance sessionInstance = session.CreateRecordableInstance();

            Assert.AreEqual(session.Id, sessionInstance.SessionId);
        }
    }
}
