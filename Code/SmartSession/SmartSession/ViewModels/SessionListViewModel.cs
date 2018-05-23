using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSession.ViewModels
{
    public class SessionListViewModel
    {
        public List<SessionViewModel> Sessions
        {
            get
            {
                return new List<SessionViewModel>
                {
                    new SessionViewModel { Id="1", Title="Session 1" },
                    new SessionViewModel { Id="2", Title="Session 2"  }
                };
            }
        }
    }
}
