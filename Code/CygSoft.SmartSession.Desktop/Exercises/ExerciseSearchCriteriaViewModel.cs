using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Exercises;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    public class ExerciseSearchCriteriaViewModel : ViewModelBase
    {
        private IExerciseService exerciseService;
        private IDialogService dialogService;

        public ExerciseSearchCriteriaViewModel(IExerciseService exerciseService, IDialogService dialogService)
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
            Duration = null;
            Difficulty = null;
            Practicality = null;
            OptimalDurationOperator = Domain.Common.ComparisonOperators.Undefined;
            DifficultyRatingOperator = Domain.Common.ComparisonOperators.Undefined;
            PracticalityRatingOperator = Domain.Common.ComparisonOperators.Undefined;
            IsScribed = null;
            HasNotes = null;
            DateCreatedBefore = null;
            DateCreatedAfter = null;
            DateModifiedAfter = null;
            DateModifiedBefore = null;
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
        public DateTime? DateCreatedBefore
        {
            get
            {
                return dateCreatedBefore;
            }
            set
            {
                Set(() => DateCreatedBefore, ref dateCreatedBefore, value);
            }
        }


        private DateTime? dateCreatedAfter;
        public DateTime? DateCreatedAfter
        {
            get
            {
                return dateCreatedAfter;
            }
            set
            {
                Set(() => DateCreatedAfter, ref dateCreatedAfter, value);
            }
        }


        private DateTime? dateModifiedAfter;
        public DateTime? DateModifiedAfter
        {
            get
            {
                return dateModifiedAfter;
            }
            set
            {
                Set(() => DateModifiedAfter, ref dateModifiedAfter, value);
            }
        }


        private DateTime? dateModifiedBefore;
        public DateTime? DateModifiedBefore
        {
            get
            {
                return dateModifiedBefore;
            }
            set
            {
                Set(() => DateModifiedBefore, ref dateModifiedBefore, value);
            }
        }


        private int? duration;
        public int? Duration
        {
            get
            {
                return duration;
            }
            set
            {
                Set(() => Duration, ref duration, value);
            }
        }

        private ComparisonOperators optimalDurationOperator;
        public ComparisonOperators OptimalDurationOperator
        {
            get
            {
                return optimalDurationOperator;
            }
            set
            {
                Set(() => OptimalDurationOperator, ref optimalDurationOperator, value);
            }
        }

        private int? difficulty;
        public int? Difficulty
        {
            get
            {
                return difficulty;
            }
            set
            {
                Set(() => Difficulty, ref difficulty, value);
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
        public int? Practicality
        {
            get
            {
                return practicality;
            }
            set
            {
                Set(() => Practicality, ref practicality, value);
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

        private bool? isScribed;
        public bool? IsScribed
        {
            get
            {
                return isScribed;
            }
            set
            {
                Set(() => IsScribed, ref isScribed, value);
            }
        }

        private bool? hasNotes;
        public bool? HasNotes
        {
            get
            {
                return hasNotes;
            }
            set
            {
                Set(() => HasNotes, ref hasNotes, value);
            }
        }

        public RelayCommand ResetCommand { get; private set; }
        public RelayCommand FindCommand { get; private set; }
        
    }
}
