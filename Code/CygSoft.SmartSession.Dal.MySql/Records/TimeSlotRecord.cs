using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Dal.MySql.Records
{
    public class TimeSlotRecord
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AssignedPracticeTime { get; set; }
    }
}
