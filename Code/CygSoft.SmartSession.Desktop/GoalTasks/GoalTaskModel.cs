using AutoMapper;
using CygSoft.SmartSession.Desktop.Supports.Validators;
using CygSoft.SmartSession.Domain.GoalTasks;
using GalaSoft.MvvmLight;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.GoalTasks
{
    public class GoalTaskModel : ValidatableObservableObject, INotifyDataErrorInfo
    {
        public GoalTask GoalTask { get; }

        public int Id { get; set; }

        private string title;
        [Required]
        public string Title
        {
            get { return title; }
            set
            {
                Set(() => Title, ref title, value, true, true);
            }
        }

        private string notes;

        public string Notes
        {
            get { return notes; }
            set
            {
                Set(() => Notes, ref notes, value, true, true);
            }
        }

        public GoalTaskModel(GoalTask goalTask)
        {
            this.GoalTask = goalTask;
            Revert();
            TrackChanges = true;
        }

        public override void Commit()
        {
            Mapper.Map(this, GoalTask);
            base.Commit();
        }

        public override void Revert()
        {
            Mapper.Map(GoalTask, this);
            base.Revert();
        }
    }
}
