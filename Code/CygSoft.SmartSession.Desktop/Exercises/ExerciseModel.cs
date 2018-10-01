using AutoMapper;
using CygSoft.SmartSession.Desktop.Supports.Validators;
using CygSoft.SmartSession.Domain.Exercises;
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

namespace CygSoft.SmartSession.Desktop.Exercises
{
    public class ExerciseModel : ValidatableObservableObject, INotifyDataErrorInfo
    {
        public Exercise Exercise { get; }

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

        private int optimalDuration;
        [Range(0, 10000, ErrorMessage = "Value must be between 0 and 10,000.")]
        public int OptimalDuration
        {
            get { return optimalDuration; }
            set
            {
                Set(() => OptimalDuration, ref optimalDuration, value, true, true);
            }
        }

        private int difficultyRating;
        [Range(0, 5, ErrorMessage = "Value must be between 0 and 5.")]
        public int DifficultyRating
        {
            get { return difficultyRating; }
            set
            {
                Set(() => DifficultyRating, ref difficultyRating, value, true, true);
            }
        }

        private int practicalityRating;
        [Range(0, 5, ErrorMessage = "Value must be between 0 and 5.")]
        public int PracticalityRating
        {
            get { return practicalityRating; }
            set
            {
                Set(() => PracticalityRating, ref practicalityRating, value, true, true);
            }
        }

        private bool scribed;
        public bool Scribed
        {
            get { return scribed; }
            set
            {
                Set(() => Scribed, ref scribed, value, true, true);
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

        public ExerciseModel(Exercise exercise)
        {
            this.Exercise = exercise;
            Revert();
            TrackChanges = true;
        }

        public override void Commit()
        {
            Mapper.Map(this, Exercise);
            base.Commit();
        }

        public override void Revert()
        {
            Mapper.Map(Exercise, this);
            base.Revert();
        }
    }
}
