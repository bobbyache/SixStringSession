using AutoMapper;
using CygSoft.SmartSession.Domain.Exercises;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    public class ExerciseModel : ObservableObject
    {
        public Exercise Exercise { get; }

        private bool isDirty;
        public bool IsDirty
        {
            get { return isDirty; }
            set { Set(() => IsDirty, ref isDirty, value); }
        }

        public int Id { get; set; }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                if (Set(() => Title, ref title, value))
                    isDirty = true;
            }
        }

        private int optimalDuration;
        public int OptimalDuration
        {
            get { return optimalDuration; }
            set
            {
                if (Set(() => OptimalDuration, ref optimalDuration, value))
                    isDirty = true;
            }
        }

        private int difficultyRating;
        public int DifficultyRating
        {
            get { return difficultyRating; }
            set
            {
                if (Set(() => DifficultyRating, ref difficultyRating, value))
                    isDirty = true;
            }
        }

        private int practicalityRating;
        public int PracticalityRating
        {
            get { return practicalityRating; }
            set
            {
                if (Set(() => PracticalityRating, ref practicalityRating, value))
                    isDirty = true;
            }
        }

        private bool scribed;
        public bool Scribed
        {
            get { return scribed; }
            set
            {
                if (Set(() => Scribed, ref scribed, value))
                    isDirty = true;
            }
        }

        private string notes;
        public string Notes
        {
            get { return notes; }
            set
            {
                if (Set(() => Notes, ref notes, value))
                    isDirty = true;
            }
        }


        public ExerciseModel(Exercise exercise)
        {
            this.Exercise = exercise;

            Revert();
        }

        public void Commit()
        {
            Mapper.Map(this, Exercise);
        }

        public void Revert()
        {
            Mapper.Map(Exercise, this);
        }
    }
}
