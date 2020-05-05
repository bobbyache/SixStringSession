using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartGoals
{
    public class GoalSummaryModel : PropertyChangedBase
    {
        public GoalSummaryModel()
        {
            // this.goalListItem = goalListItem;
            // this.Title = goalListItem.Title;
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { 
                title = value;
                // goalListItem.Title = title;
                NotifyOfPropertyChange(() => Title);
            }
        }
    }
}
