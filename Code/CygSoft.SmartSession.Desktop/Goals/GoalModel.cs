using AutoMapper;
using CygSoft.SmartSession.Desktop.Supports.Validators;
using CygSoft.SmartSession.Domain.Goals;
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

namespace CygSoft.SmartSession.Desktop.Goals
{
    public class GoalModel : ValidatableObservableObject, INotifyDataErrorInfo
    {
        public Goal Goal { get; }

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

        public GoalModel(Goal goal)
        {
            this.Goal = goal;
            Revert();
            TrackChanges = true;
        }

        public override void Commit()
        {
            Mapper.Map(this, Goal);
            base.Commit();
        }

        public override void Revert()
        {
            Mapper.Map(Goal, this);
            base.Revert();
        }
    }
}