using Caliburn.Micro;
using JsonDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartGoals
{
    public class GoalSummaryModel : PropertyChangedBase
    {
        private GoalListItem goalListItem;
        public GoalSummaryModel(GoalListItem goalListItem)
        {
            this.goalListItem = goalListItem;
            this.Title = goalListItem.Title;
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { 
                title = value;
                goalListItem.Title = title;
                NotifyOfPropertyChange(() => Title);
            }
        }
    }
}
