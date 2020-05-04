using Caliburn.Micro;
using JsonDb;
using JsonDb.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartGoals
{
    public class GoalSummaryModel : PropertyChangedBase
    {
        private JsonGoalListItem goalListItem;
        public GoalSummaryModel(JsonGoalListItem goalListItem)
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
