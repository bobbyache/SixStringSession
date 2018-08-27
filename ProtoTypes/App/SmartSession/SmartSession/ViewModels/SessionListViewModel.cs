using CygSoft.SmartSession.Application;
using CygSoft.SmartSession.Application.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SmartSession.ViewModels
{
    public class SessionListViewModel
    {
        private IEnumerable<ISession> _sessions;
        private SmartSessionApplication _application = new SmartSessionApplication();

        public ObservableCollection<SessionViewModel> Sessions
        {
            get
            {
                Load(null);
                return new ObservableCollection<SessionViewModel>(_sessions.Select(
                    s => new SessionViewModel() { Id = s.Id, Title = s.Title }
                    ));
            }
        }

        public void Load(string path)
        {
            _sessions = _application.LoadSessions();
        }
    }
}
