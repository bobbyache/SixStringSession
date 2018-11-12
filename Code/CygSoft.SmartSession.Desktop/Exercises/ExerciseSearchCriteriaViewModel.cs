using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Exercises;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    public class ExerciseSearchCriteriaViewModel : ViewModelBase, IExerciseSearchCriteria
    {
        private IExerciseService exerciseService;
        private IDialogViewService dialogService;

        public ExerciseSearchCriteriaViewModel(IExerciseService exerciseService, IDialogViewService dialogService)
        {
            this.exerciseService = exerciseService ?? throw new ArgumentNullException("Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            ResetCommand = new RelayCommand(Reset, true);
            FindCommand = new RelayCommand(Find, true);
        }

        private void Find()
        {
            Messenger.Default.Send(new FindExercisesMessage());
        }

        private void Reset()
        {
            Title = null;
            DifficultyRating = null;
            PracticalityRating = null;
            TargetMetronomeSpeed = null;
            TargetPracticeTime = null;
            TargetMetronomeSpeedOperator = Domain.Common.ComparisonOperators.Undefined;
            TargetPracticeTimeOperator = Domain.Common.ComparisonOperators.Undefined;
            DifficultyRatingOperator = Domain.Common.ComparisonOperators.Undefined;
            PracticalityRatingOperator = Domain.Common.ComparisonOperators.Undefined;
            ToDateCreated = null;
            FromDateCreated = null;
            FromDateModified = null;
            ToDateModified = null;
        }

        public string[] ComparisonOperators
        {
            get => new string[]
            {
                "",
                "<",
                ">",
                ">=",
                "<="
            };
        }

        public string[] RatingValues
        {
            get => new string[]
            {
                "",
                "1",
                "2",
                "3",
                "4",
                "5"
            };
        }


        private DateTime? dateCreatedBefore;
        public DateTime? ToDateCreated
        {
            get
            {
                return dateCreatedBefore;
            }
            set
            {
                Set(() => ToDateCreated, ref dateCreatedBefore, value);
            }
        }


        private DateTime? dateCreatedAfter;
        public DateTime? FromDateCreated
        {
            get
            {
                return dateCreatedAfter;
            }
            set
            {
                Set(() => FromDateCreated, ref dateCreatedAfter, value);
            }
        }


        private DateTime? dateModifiedAfter;
        public DateTime? FromDateModified
        {
            get
            {
                return dateModifiedAfter;
            }
            set
            {
                Set(() => FromDateModified, ref dateModifiedAfter, value);
            }
        }


        private DateTime? dateModifiedBefore;
        public DateTime? ToDateModified
        {
            get
            {
                return dateModifiedBefore;
            }
            set
            {
                Set(() => ToDateModified, ref dateModifiedBefore, value);
            }
        }

        private int? difficulty;
        public int? DifficultyRating
        {
            get
            {
                return difficulty;
            }
            set
            {
                Set(() => DifficultyRating, ref difficulty, value);
            }
        }

        private ComparisonOperators difficultyRatingOperator;
        public ComparisonOperators DifficultyRatingOperator
        {
            get
            {
                return difficultyRatingOperator;
            }
            set
            {
                Set(() => DifficultyRatingOperator, ref difficultyRatingOperator, value);
            }
        }

        private int? practicality;
        public int? PracticalityRating
        {
            get
            {
                return practicality;
            }
            set
            {
                Set(() => PracticalityRating, ref practicality, value);
            }
        }

        private ComparisonOperators practicalityRatingOperator;
        public ComparisonOperators PracticalityRatingOperator
        {
            get
            {
                return practicalityRatingOperator;
            }
            set
            {
                Set(() => PracticalityRatingOperator, ref practicalityRatingOperator, value);
            }
        }


        private int? targetMetronomeSpeed;
        public int? TargetMetronomeSpeed
        {
            get { return targetMetronomeSpeed; }
            set
            {
                Set(() => TargetMetronomeSpeed, ref targetMetronomeSpeed, value);
            }
        }

        private ComparisonOperators targetMetronomeSpeedOperator;
        public ComparisonOperators TargetMetronomeSpeedOperator
        {
            get
            {
                return targetMetronomeSpeedOperator;
            }
            set
            {
                Set(() => TargetMetronomeSpeedOperator, ref targetMetronomeSpeedOperator, value);
            }
        }

        private int? targetPracticeTime;
        public int? TargetPracticeTime
        {
            get { return targetPracticeTime; }
            set
            {
                Set(() => TargetPracticeTime, ref targetPracticeTime, value);
            }
        }

        private ComparisonOperators targetPracticeTimeOperator;
        public ComparisonOperators TargetPracticeTimeOperator
        {
            get
            {
                return targetPracticeTimeOperator;
            }
            set
            {
                Set(() => TargetPracticeTimeOperator, ref targetPracticeTimeOperator, value);
            }
        }

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                Set(() => Title, ref title, value);
            }
        }

        private string keywords;
        public string Keywords
        {
            get
            {
                return keywords;
            }
            set
            {
                Set(() => Keywords, ref keywords, value);
            }
        }

        public RelayCommand ResetCommand { get; private set; }
        public RelayCommand FindCommand { get; private set; }

    }
}
