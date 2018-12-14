using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Infrastructure.Enums;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.UnitTests.Tests
{
    [TestFixture]
    public class ExerciseTests
    {
        [Test]
        public void Exercise_Create_Has_Default_Progress_Weightings()
        {
            var exercise = new Exercise();
            Assert.That(exercise.PracticeTimeProgressWeighting, Is.EqualTo(50));
            Assert.That(exercise.SpeedProgressWeighting, Is.EqualTo(50));
        }

        [Test]
        public void Exercise_GetSecondsPracticed_With_NoActivity_Recorded_Returns_0_Seconds()
        {
            Exercise exercise = new Exercise
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,

                TargetMetronomeSpeed = 100,
                PercentageCompleteCalculationType = PercentCompleteCalculationStrategy.MetronomeSpeed,
                ExerciseActivity = null
            };

            var percentComplete = exercise.GetSecondsPracticed();

            Assert.That(percentComplete, Is.EqualTo(0));
        }

        [Test]
        public void Exercise_GetPercentComplete_With_NoActivity_Recorded_Returns_0_Seconds()
        {
            Exercise exercise = new Exercise
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                SpeedProgressWeighting = 50,
                TargetMetronomeSpeed = 100,
                PracticeTimeProgressWeighting = 50,
                TargetPracticeTime = 5000,
                PercentageCompleteCalculationType = PercentCompleteCalculationStrategy.MetronomeSpeed,
                ExerciseActivity = null
            };

            var percentComplete = exercise.GetPercentComplete();

            Assert.That(percentComplete, Is.EqualTo(0));
        }

        [Test]
        public void Exercise_GetPercentComplete_When_StrategyOn_PracticeTime_And_Target_Time_Is_Null_Returns_0()
        {
            Exercise exercise = new Exercise
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,

                // -----------------------------------------------------
                SpeedProgressWeighting = 0,
                TargetPracticeTime = null,
                PercentageCompleteCalculationType = PercentCompleteCalculationStrategy.PracticeTime,
                // -----------------------------------------------------

                ExerciseActivity = new List<Sessions.ExerciseActivity>
                {
                    new Sessions.ExerciseActivity {
                        Id = 1,
                        DateCreated = DateTime.Parse("2018/01/02"),
                        DateModified = DateTime.Parse("2018/01/02"),
                        MetronomeSpeed = 70,
                        Seconds = 600, // 10 minutes
                        StartTime = DateTime.Parse("2018/01/02 10:00:00"),
                        EndTime = DateTime.Parse("2018/01/02 10:10:00"),
                        ExerciseId = 1 }
                }
            };

            var percentComplete = exercise.GetPercentComplete();
            Assert.That(percentComplete, Is.EqualTo(0), "If no PracticeTime has been specified, there is no way to calculate progress when calculating by practice time so always return 0% complete");
        }

        [Test]
        public void Exercise_GetPercentComplete_When_StrategyOn_MetronomeSpeed_And_Target_Metronome_Is_Null_Returns_0()
        {
            Exercise exercise = new Exercise
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                // -----------------------------------------------------
                SpeedProgressWeighting = 50,
                PracticeTimeProgressWeighting = 0,
                TargetMetronomeSpeed = null,
                // -----------------------------------------------------
                PercentageCompleteCalculationType = PercentCompleteCalculationStrategy.MetronomeSpeed,

                ExerciseActivity = new List<Sessions.ExerciseActivity>
                {
                    new Sessions.ExerciseActivity {
                        Id = 1,
                        DateCreated = DateTime.Parse("2018/01/02"),
                        DateModified = DateTime.Parse("2018/01/02"),
                        MetronomeSpeed = 70,
                        StartTime = DateTime.Parse("2018/01/02 10:12:00"),
                        EndTime = DateTime.Parse("2018/01/02 10:15:00"),
                        ExerciseId = 1 },
                }
            };

            var percentComplete = exercise.GetPercentComplete();

            Assert.That(percentComplete, Is.EqualTo(0), "If no TargetMetronomeSpeed has been specified, there is no way to calculate progress when calculating by metronome so always return 0% complete");
        }

        [Test]
        public void Exercise_GetPercentComplete_When_StrategyOn_MetronomeSpeed_And_Initial_Metronome_Speed_Is_Null_And_No_Activity_Always_Returns_0()
        {
            Exercise exercise = new Exercise
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                // -----------------------------------------------------
                TargetMetronomeSpeed = 120,
                PracticeTimeProgressWeighting = 0,
                SpeedProgressWeighting = 50,
                // -----------------------------------------------------
                PercentageCompleteCalculationType = PercentCompleteCalculationStrategy.MetronomeSpeed,

                ExerciseActivity = new List<Sessions.ExerciseActivity>
                {
                    new Sessions.ExerciseActivity {
                        Id = 1,
                        DateCreated = DateTime.Parse("2018/01/02"),
                        DateModified = DateTime.Parse("2018/01/02"),
                        MetronomeSpeed = 70,
                        StartTime = DateTime.Parse("2018/01/02 10:12:00"),
                        EndTime = DateTime.Parse("2018/01/02 10:15:00"),
                        ExerciseId = 1 },
                }
            };

            var percentComplete = exercise.GetPercentComplete();

            Assert.That(percentComplete, Is.EqualTo(0), "If no activity speed there is no way to calculate progress when calculating by metronome so always return 0% complete");
        }

        [Test]
        public void Exercise_GetPercentComplete_When_StrategyOn_MetronomeSpeed_And_Initial_Metronome_Speed_Is_Equal_To_Current_Speed_Returns_0()
        {
            Exercise exercise = new Exercise
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                // -----------------------------------------------------
                TargetMetronomeSpeed = 120,
                SpeedProgressWeighting = 50,
                PracticeTimeProgressWeighting = 0,
                // -----------------------------------------------------
                PercentageCompleteCalculationType = PercentCompleteCalculationStrategy.MetronomeSpeed,

                ExerciseActivity = new List<Sessions.ExerciseActivity>
                {
                    new Sessions.ExerciseActivity {
                        Id = 1,
                        DateCreated = DateTime.Parse("2018/01/02"),
                        DateModified = DateTime.Parse("2018/01/02"),
                        MetronomeSpeed = 70,
                        StartTime = DateTime.Parse("2018/01/02 10:12:00"),
                        EndTime = DateTime.Parse("2018/01/02 10:15:00"),
                        ExerciseId = 1 },
                }
            };

            var percentComplete = exercise.GetPercentComplete();

            Assert.That(percentComplete, Is.EqualTo(0), "Single activity creates an initial speed (and hence a progress start point). Progress should therefore be 0%");
        }

        [Test]
        public void Exercise_GetPercentComplete_When_StrategyOn_MetronomeSpeed_And_Target_Is_0_Always_Return_0()
        {
            Exercise exercise = new Exercise
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                // -----------------------------------------------------
                TargetMetronomeSpeed = 0,
                PracticeTimeProgressWeighting = 0,
                SpeedProgressWeighting = 50,
                // -----------------------------------------------------
                PercentageCompleteCalculationType = PercentCompleteCalculationStrategy.MetronomeSpeed,

                ExerciseActivity = new List<Sessions.ExerciseActivity>
                {
                    new Sessions.ExerciseActivity {
                        Id = 1,
                        DateCreated = DateTime.Parse("2018/01/02"),
                        DateModified = DateTime.Parse("2018/01/02"),
                        MetronomeSpeed = 100,
                        StartTime = DateTime.Parse("2018/01/02 10:12:00"),
                        EndTime = DateTime.Parse("2018/01/02 10:15:00"),
                        ExerciseId = 1 },
                }
            };

            var percentComplete = exercise.GetPercentComplete();

            // Avoid divide by zero!
            Assert.That(percentComplete, Is.EqualTo(0), "If TargetMetronomeSpeed is 0, always return 0% when the calculation strategy is MetronomeSpeed.");
        }

        [Test]
        public void Exercise_GetPercentComplete_On_MetronomeSpeed_Calculates_Progress_Using_Initial_Speed_As_Lower_Bound()
        {
            Exercise exercise = new Exercise
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                // -----------------------------------------------------
                PracticeTimeProgressWeighting = 0, // Only calculate progress by metronome speed.
                SpeedProgressWeighting = 50,
                // -----------------------------------------------------
                TargetMetronomeSpeed = 150,
                PercentageCompleteCalculationType = PercentCompleteCalculationStrategy.MetronomeSpeed,

                ExerciseActivity = new List<Sessions.ExerciseActivity>
                {
                    new Sessions.ExerciseActivity {
                        Id = 1,
                        DateCreated = DateTime.Parse("2018/01/02"),
                        DateModified = DateTime.Parse("2018/01/02"),
                        MetronomeSpeed = 50,
                        StartTime = DateTime.Parse("2018/01/02 10:12:00"),
                        EndTime = DateTime.Parse("2018/01/02 10:15:00"),
                        ExerciseId = 1 },

                    new Sessions.ExerciseActivity {
                        Id = 2,
                        DateCreated = DateTime.Parse("2018/01/03"),
                        DateModified = DateTime.Parse("2018/01/03"),
                        MetronomeSpeed = 100,
                        StartTime = DateTime.Parse("2018/01/03 10:12:00"),
                        EndTime = DateTime.Parse("2018/01/03 10:15:00"),
                        ExerciseId = 1 }
                }
            };

            var percentComplete = exercise.GetPercentComplete();

            Assert.That(percentComplete, Is.EqualTo(50), "Calculation should consider the first activity speed as the lower bound for the progress calculation.");
        }

        [Test]
        public void Exercise_GetPercentComplete_Metronome_And_Last_Activity_Speed_Less_Or_Equal_To_First_Returns_0()
        {
            Exercise exercise = new Exercise
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                TargetMetronomeSpeed = 150,
                // -----------------------------------------------------
                PracticeTimeProgressWeighting = 0, // Only calculate progress by metronome speed.
                SpeedProgressWeighting = 50,
                // -----------------------------------------------------
                PercentageCompleteCalculationType = PercentCompleteCalculationStrategy.MetronomeSpeed,

                ExerciseActivity = new List<Sessions.ExerciseActivity>
                {
                    new Sessions.ExerciseActivity {
                        Id = 1,
                        DateCreated = DateTime.Parse("2018/01/02"),
                        DateModified = DateTime.Parse("2018/01/02"),
                        MetronomeSpeed = 50,
                        StartTime = DateTime.Parse("2018/01/02 10:12:00"),
                        EndTime = DateTime.Parse("2018/01/02 10:15:00"),
                        ExerciseId = 1 },

                    new Sessions.ExerciseActivity {
                        Id = 2,
                        DateCreated = DateTime.Parse("2018/01/03"),
                        DateModified = DateTime.Parse("2018/01/03"),
                        MetronomeSpeed = 50,
                        StartTime = DateTime.Parse("2018/01/03 10:12:00"),
                        EndTime = DateTime.Parse("2018/01/03 10:15:00"),
                        ExerciseId = 1 }
                }
            };

            var percentComplete = exercise.GetPercentComplete();

            Assert.That(percentComplete, Is.EqualTo(0), "When activity shows that last speed is equal (or less than) the initial speed should always return 0%");
        }

        [Test]
        public void Exercise_GetPercentComplete_When_StrategyOn_MetronomeSpeed_CalcsCorrectly()
        {
            Exercise exercise = new Exercise
            {
                Id = 1,
                DateCreated = DateTime.Parse("2018/01/01"),
                DateModified = null,
                // -----------------------------------------------------
                PracticeTimeProgressWeighting = 0, // Only calculate progress by metronome speed.
                SpeedProgressWeighting = 50,
                // -----------------------------------------------------
                TargetMetronomeSpeed = 100,
                PercentageCompleteCalculationType = PercentCompleteCalculationStrategy.MetronomeSpeed,

                ExerciseActivity = new List<Sessions.ExerciseActivity>
                {
                    new Sessions.ExerciseActivity {
                        Id = 1,
                        DateCreated = DateTime.Parse("2018/01/02"),
                        DateModified = DateTime.Parse("2018/01/02"),
                        MetronomeSpeed = 70,
                        StartTime = DateTime.Parse("2018/01/02 10:12:00"),
                        EndTime = DateTime.Parse("2018/01/02 10:15:00"),
                        ExerciseId = 1 },

                    new Sessions.ExerciseActivity {
                        Id = 2,
                        DateCreated = DateTime.Parse("2018/01/03"),
                        DateModified = DateTime.Parse("2018/01/03"),
                        MetronomeSpeed = 75,
                        StartTime = DateTime.Parse("2018/01/03 10:12:00"),
                        EndTime = DateTime.Parse("2018/01/03 10:15:00"),
                        ExerciseId = 1 },

                    new Sessions.ExerciseActivity {
                        Id = 3,
                        DateCreated = DateTime.Parse("2018/01/04"),
                        DateModified = DateTime.Parse("2018/01/04"),
                        MetronomeSpeed = 80,
                        StartTime = DateTime.Parse("2018/01/04 10:12:00"),
                        EndTime = DateTime.Parse("2018/01/04 10:15:00"),
                        ExerciseId = 1 }
                }
            };

            var percentComplete = exercise.GetPercentComplete();

            Assert.That(percentComplete, Is.InRange(33.3, 33.4));
        }

        [Test]
        public void Exercise_GetPercentComplete_When_StrategyOn_PracticeTime_CalcsCorrectly()
        {
            Exercise exercise = new Exercise
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                // -----------------------------------------------------
                SpeedProgressWeighting = 0, // calculate by practice time only.
                PracticeTimeProgressWeighting = 50,
                // -----------------------------------------------------
                TargetPracticeTime = (int)TimeSpan.FromHours(1).TotalSeconds,
                PercentageCompleteCalculationType = PercentCompleteCalculationStrategy.PracticeTime,

                ExerciseActivity = new List<Sessions.ExerciseActivity>
                {
                    new Sessions.ExerciseActivity {
                        Id = 1,
                        DateCreated = DateTime.Parse("2018/01/02"),
                        DateModified = DateTime.Parse("2018/01/02"),
                        MetronomeSpeed = 70,
                        Seconds = 600, // 10 minutes
                        StartTime = DateTime.Parse("2018/01/02 10:00:00"),
                        EndTime = DateTime.Parse("2018/01/02 10:10:00"),
                        ExerciseId = 1 },

                    new Sessions.ExerciseActivity {
                        Id = 2,
                        DateCreated = DateTime.Parse("2018/01/03"),
                        DateModified = DateTime.Parse("2018/01/03"),
                        MetronomeSpeed = 75,
                        Seconds = 600, // 10 minutes
                        StartTime = DateTime.Parse("2018/01/03 10:00:00"),
                        EndTime = DateTime.Parse("2018/01/03 10:10:00"),
                        ExerciseId = 1 },

                    new Sessions.ExerciseActivity {
                        Id = 3,
                        DateCreated = DateTime.Parse("2018/01/04"),
                        DateModified = DateTime.Parse("2018/01/04"),
                        MetronomeSpeed = 80,
                        Seconds = 600, // 10 minutes
                        StartTime = DateTime.Parse("2018/01/04 10:00:00"),
                        EndTime = DateTime.Parse("2018/01/04 10:10:00"),
                        ExerciseId = 1 }
                }
            };

            // 30 min in seconds = 1800
            // 30 min is a half hour.
            var percentComplete = exercise.GetPercentComplete();
            Assert.That(percentComplete, Is.EqualTo(50));
        }

        public void Exercise_AddRecording_Successfully_AddsRecording()
        {
            Exercise exercise = new Exercise
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                TargetMetronomeSpeed = 150,
                PercentageCompleteCalculationType = PercentCompleteCalculationStrategy.MetronomeSpeed,
            };

            int initialActivityCount = exercise.ExerciseActivity.Count;

            exercise.AddRecording(60, 3000, DateTime.Parse("2018/01/04 10:00:00"), DateTime.Parse("2018/01/04 10:10:00"));

            Assert.That(initialActivityCount, Is.EqualTo(0));
            Assert.That(exercise.ExerciseActivity.Count, Is.EqualTo(1));
        }

        public void Exercise_RemoveRecording_Successfully_RemovesRecording()
        {
            Exercise exercise = new Exercise
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                TargetMetronomeSpeed = 150,
                PercentageCompleteCalculationType = PercentCompleteCalculationStrategy.MetronomeSpeed,
            };
            int no_activities = exercise.ExerciseActivity.Count;

            var addedRecording = exercise.AddRecording(60, 3000, DateTime.Parse("2018/01/04 10:00:00"), DateTime.Parse("2018/01/04 10:10:00"));
            int one_activity = exercise.ExerciseActivity.Count;

            exercise.RemoveRecording(addedRecording);

            Assert.That(no_activities, Is.EqualTo(0));
            Assert.That(one_activity, Is.EqualTo(1));
            Assert.That(exercise.ExerciseActivity.Count, Is.EqualTo(0));
        }

        [Test]
        public void Exercise_With_Metronome_Speed_CalculatePercentComplete_Progress_Is_Accurately_Measured()
        {
            Exercise exercise = new Exercise
            {
                Id = 57,
                DateCreated = DateTime.Parse("2018-02-02 10:56:41"),
                DateModified = DateTime.Parse("2018-02-02 11:03:59"),

                TargetPracticeTime = null,
                TargetMetronomeSpeed = 150,
                // -----------------------------------------------------
                PracticeTimeProgressWeighting = 0, // Only calculate progress by metronome speed.
                SpeedProgressWeighting = 50,
                // -----------------------------------------------------
                PercentageCompleteCalculationType = PercentCompleteCalculationStrategy.MetronomeSpeed,

                ExerciseActivity = new List<Sessions.ExerciseActivity>
                {
                        new Sessions.ExerciseActivity {
                        Id = 5,
                        DateCreated = DateTime.Parse("2018-02-03 10:56:41"),
                        DateModified = null,
                        MetronomeSpeed = 50,
                        Seconds = 8000,
                        StartTime = DateTime.Parse("2018-02-03 12:15:00"),
                        EndTime = DateTime.Parse("2018-02-03 12:25:00"),
                        ExerciseId = 57 },

                    new Sessions.ExerciseActivity {
                        Id = 5,
                        DateCreated = DateTime.Parse("2018-02-04 10:56:41"),
                        DateModified = null,
                        MetronomeSpeed = 100,
                        Seconds = 8000,
                        StartTime = DateTime.Parse("2018-02-04 12:15:00"),
                        EndTime = DateTime.Parse("2018-02-04 12:25:00"),
                        ExerciseId = 57 },

                    new Sessions.ExerciseActivity {
                        Id = 6,
                        DateCreated = DateTime.Parse("2018-02-05 10:56:41"),
                        DateModified = null,
                        MetronomeSpeed = 100,
                        Seconds = 4000,
                        StartTime = DateTime.Parse("2018-02-05 12:15:00"),
                        EndTime = DateTime.Parse("2018-02-05 12:25:00"),
                        ExerciseId = 57 }
                }
            };

            var percentComplete_50 = exercise.GetPercentComplete();

            exercise.AddRecording(40, 15, DateTime.Parse("2018-12-13 10:03:26"), DateTime.Parse("2018-12-13 10:03:42"));
            var percentComplete_Below_0 = exercise.GetPercentComplete();

            exercise.AddRecording(110, 15, DateTime.Parse("2018-12-13 11:03:26"), DateTime.Parse("2018-12-13 11:03:42"));
            var percentComplete_60 = exercise.GetPercentComplete();

            exercise.AddRecording(120, 15, DateTime.Parse("2018-12-14 11:03:26"), DateTime.Parse("2018-12-14 11:03:42"));
            var percentComplete_70 = exercise.GetPercentComplete();

            exercise.AddRecording(130, 15, DateTime.Parse("2018-12-15 11:03:26"), DateTime.Parse("2018-12-15 11:03:42"));
            var percentComplete_80 = exercise.GetPercentComplete();

            exercise.AddRecording(140, 15, DateTime.Parse("2018-12-16 11:03:26"), DateTime.Parse("2018-12-16 11:03:42"));
            var percentComplete_90 = exercise.GetPercentComplete();

            exercise.AddRecording(150, 15, DateTime.Parse("2018-12-17 11:03:26"), DateTime.Parse("2018-12-17 11:03:42"));
            var percentComplete_100 = exercise.GetPercentComplete();

            exercise.AddRecording(160, 15, DateTime.Parse("2018-12-18 11:03:26"), DateTime.Parse("2018-12-18 11:03:42"));
            var percentComplete_Above_100 = exercise.GetPercentComplete();

            Assert.That(percentComplete_50, Is.EqualTo(50));
            Assert.That(percentComplete_60, Is.Not.EqualTo(40)); // Avoid a historical error.

            Assert.That(percentComplete_Below_0, Is.EqualTo(0));
            Assert.That(percentComplete_60, Is.EqualTo(60));
            Assert.That(percentComplete_70, Is.EqualTo(70));
            Assert.That(percentComplete_80, Is.EqualTo(80));
            Assert.That(percentComplete_90, Is.EqualTo(90));
            Assert.That(percentComplete_100, Is.EqualTo(100));
            Assert.That(percentComplete_Above_100, Is.EqualTo(100));
        }

        [Test]
        public void Exercise_CalculateProgress_100_Percent_Complete_Weighted_Only_By_Speed()
        {
            Exercise exercise = new Exercise
            {
                Id = 57,
                DateCreated = DateTime.Parse("2018-02-02 10:56:41"),
                DateModified = DateTime.Parse("2018-02-02 11:03:59"),

                TargetPracticeTime = 5000,
                TargetMetronomeSpeed = 150,

                // -----------------------------------------------------
                PracticeTimeProgressWeighting = 0, // Only calculate progress by metronome speed.
                SpeedProgressWeighting = 100,
                // -----------------------------------------------------

                ExerciseActivity = new List<Sessions.ExerciseActivity>
                {
                        new Sessions.ExerciseActivity {
                        Id = 5,
                        DateCreated = DateTime.Parse("2018-02-03 10:56:41"),
                        DateModified = null,
                        MetronomeSpeed = 50,
                        Seconds = 8000,
                        StartTime = DateTime.Parse("2018-02-03 12:15:00"),
                        EndTime = DateTime.Parse("2018-02-03 12:25:00"),
                        ExerciseId = 57 },

                    new Sessions.ExerciseActivity {
                        Id = 5,
                        DateCreated = DateTime.Parse("2018-02-04 10:56:41"),
                        DateModified = null,
                        MetronomeSpeed = 100,
                        Seconds = 8000,
                        StartTime = DateTime.Parse("2018-02-04 12:15:00"),
                        EndTime = DateTime.Parse("2018-02-04 12:25:00"),
                        ExerciseId = 57 },

                    new Sessions.ExerciseActivity {
                        Id = 6,
                        DateCreated = DateTime.Parse("2018-02-05 10:56:41"),
                        DateModified = null,
                        MetronomeSpeed = 150,
                        Seconds = 4000,
                        StartTime = DateTime.Parse("2018-02-05 12:15:00"),
                        EndTime = DateTime.Parse("2018-02-05 12:25:00"),
                        ExerciseId = 57 }
                }
            };

            var progress = exercise.GetPercentComplete();
            Assert.That(progress, Is.EqualTo(100));
        }

        [Test]
        public void Exercise_CalculateProgress_100_Percent_Complete_Weighted_Only_By_PracticeTime()
        {
            Exercise exercise = new Exercise
            {
                Id = 57,
                DateCreated = DateTime.Parse("2018-02-02 10:56:41"),
                DateModified = DateTime.Parse("2018-02-02 11:03:59"),

                TargetPracticeTime = 5000,
                TargetMetronomeSpeed = 150,
                // -----------------------------------------------------
                PracticeTimeProgressWeighting = 100, // Only calculate progress by metronome speed.
                SpeedProgressWeighting = 0,
                // -----------------------------------------------------

                ExerciseActivity = new List<Sessions.ExerciseActivity>
                {
                        new Sessions.ExerciseActivity {
                        Id = 5,
                        DateCreated = DateTime.Parse("2018-02-03 10:56:41"),
                        DateModified = null,
                        MetronomeSpeed = 50,
                        Seconds = 1000,
                        StartTime = DateTime.Parse("2018-02-03 12:15:00"),
                        EndTime = DateTime.Parse("2018-02-03 12:25:00"),
                        ExerciseId = 57 },

                    new Sessions.ExerciseActivity {
                        Id = 5,
                        DateCreated = DateTime.Parse("2018-02-04 10:56:41"),
                        DateModified = null,
                        MetronomeSpeed = 100,
                        Seconds = 2000,
                        StartTime = DateTime.Parse("2018-02-04 12:15:00"),
                        EndTime = DateTime.Parse("2018-02-04 12:25:00"),
                        ExerciseId = 57 },

                    new Sessions.ExerciseActivity {
                        Id = 6,
                        DateCreated = DateTime.Parse("2018-02-05 10:56:41"),
                        DateModified = null,
                        MetronomeSpeed = 110,
                        Seconds = 2000,
                        StartTime = DateTime.Parse("2018-02-05 12:15:00"),
                        EndTime = DateTime.Parse("2018-02-05 12:25:00"),
                        ExerciseId = 57 }
                }
            };

            var progress = exercise.GetPercentComplete();
            Assert.That(progress, Is.EqualTo(100));
        }


        [Test]
        public void Exercise_CalculateProgress_75_Percent_When_PracticeTime_Progress_50_Speed_Progress_100_SameWeighting()
        {
            Exercise exercise = new Exercise
            {
                Id = 57,
                DateCreated = DateTime.Parse("2018-02-02 10:56:41"),
                DateModified = DateTime.Parse("2018-02-02 11:03:59"),

                TargetPracticeTime = 5000,
                TargetMetronomeSpeed = 150,
                // -----------------------------------------------------
                PracticeTimeProgressWeighting = 50, // Only calculate progress by metronome speed.
                SpeedProgressWeighting = 50,
                // -----------------------------------------------------

                ExerciseActivity = new List<Sessions.ExerciseActivity>
                {
                        new Sessions.ExerciseActivity {
                        Id = 5,
                        DateCreated = DateTime.Parse("2018-02-03 10:56:41"),
                        DateModified = null,
                        MetronomeSpeed = 50,
                        Seconds = 1000,
                        StartTime = DateTime.Parse("2018-02-03 12:15:00"),
                        EndTime = DateTime.Parse("2018-02-03 12:25:00"),
                        ExerciseId = 57 },

                    new Sessions.ExerciseActivity {
                        Id = 5,
                        DateCreated = DateTime.Parse("2018-02-04 10:56:41"),
                        DateModified = null,
                        MetronomeSpeed = 100,
                        Seconds = 1000,
                        StartTime = DateTime.Parse("2018-02-04 12:15:00"),
                        EndTime = DateTime.Parse("2018-02-04 12:25:00"),
                        ExerciseId = 57 },

                    new Sessions.ExerciseActivity {
                        Id = 6,
                        DateCreated = DateTime.Parse("2018-02-05 10:56:41"),
                        DateModified = null,
                        MetronomeSpeed = 150,
                        Seconds = 500,
                        StartTime = DateTime.Parse("2018-02-05 12:15:00"),
                        EndTime = DateTime.Parse("2018-02-05 12:25:00"),
                        ExerciseId = 57 }
                }
            };

            var progress = exercise.GetPercentComplete();
            Assert.That(progress, Is.EqualTo(75));
        }
    }
}
