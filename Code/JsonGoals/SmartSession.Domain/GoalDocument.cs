using SmartSession.Domain.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSession.Domain
{
    public class GoalDocument
    {
        public GoalDocument(IGoalRepository goalRepository)
        {
            
        }
        public string Title { get; set; }
    }
}
