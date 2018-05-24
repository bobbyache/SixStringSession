using CygSoft.SmartSession.Application.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Application
{
    public class SmartSessionApplication
    {
        public IEnumerable<ISession> LoadSessions()
        {
            return new List<Session>
            {
                new Session("10FA82BC-7456-4A8C-BCA0-47CB262F8AA0", "Session 1 - Beginner Lead Guitar, Chapter 1, Example 1a", DateTime.Now, DateTime.Now),
                new Session("2474DE17-4698-43D5-90ED-8F4C894EF6B1", "Session 2", DateTime.Now, DateTime.Now)
            };
        }
    }
}
