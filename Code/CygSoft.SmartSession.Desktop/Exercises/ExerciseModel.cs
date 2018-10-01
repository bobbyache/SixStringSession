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
    public class ExerciseModel : ObservableObject, INotifyDataErrorInfo
    {
        public Exercise Exercise { get; }

        private ValidatableObject validator;

        private bool isDirty;
        public bool IsDirty
        {
            get { return isDirty; }
            set { Set(() => IsDirty, ref isDirty, value); }
        }

        public int Id { get; set; }

        private string title;
        [Required]
        public string Title
        {
            get { return title; }
            set
            {
                if (Set(() => Title, ref title, value))
                {
                    validator.Validate(() => Title, value);
                    isDirty = true;
                }
            }
        }

        private int optimalDuration;
        [Range(0, 10000, ErrorMessage = "Value must be between 0 and 5.")]
        public int OptimalDuration
        {
            get { return optimalDuration; }
            set
            {
                if (Set(() => OptimalDuration, ref optimalDuration, value))
                {
                    validator.Validate(() => OptimalDuration, value);
                    isDirty = true;
                }
            }
        }

        private int difficultyRating;
        [Range(0, 5, ErrorMessage = "Value must be between 0 and 5.")]
        public int DifficultyRating
        {
            get { return difficultyRating; }
            set
            {
                if (Set(() => DifficultyRating, ref difficultyRating, value))
                {
                    validator.Validate(() => DifficultyRating, value);
                    isDirty = true;
                }
            }
        }

        private int practicalityRating;
        [Range(0, 5, ErrorMessage = "Value must be between 0 and 5.")]
        public int PracticalityRating
        {
            get { return practicalityRating; }
            set
            {
                if (Set(() => PracticalityRating, ref practicalityRating, value))
                {
                    validator.Validate(() => PracticalityRating, value);
                    isDirty = true;
                }
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

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public string Notes
        {
            get { return notes; }
            set
            {
                if (Set(() => Notes, ref notes, value))
                    isDirty = true;
            }
        }

        public bool HasErrors => validator.HasErrors;

        public ExerciseModel(Exercise exercise)
        {
            validator = new ValidatableObject(this);
            validator.ErrorsChanged += Validator_ErrorsChanged;
            this.Exercise = exercise;

            Revert();
        }

        private void Validator_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            if (this.ErrorsChanged != null)
                ErrorsChanged(this, e);
        }

        public void Commit()
        {
            Mapper.Map(this, Exercise);
        }

        public void Revert()
        {
            Mapper.Map(Exercise, this);
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return validator.GetErrors(propertyName);
        }
    }
}
