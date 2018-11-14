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

                TargetMetronomeSpeed = 100,
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

                TargetPracticeTime = null,
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

                TargetMetronomeSpeed = null,
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
                InitialMetronomeSpeed = null,
                TargetMetronomeSpeed = 120,
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

            Assert.That(percentComplete, Is.EqualTo(0), "If no InitialMetronomeSpeed has been specified, and there is no activity comfort speed there is no way to calculate progress when calculating by metronome so always return 0% complete");
        }

        [Test]
        public void Exercise_GetPercentComplete_When_StrategyOn_MetronomeSpeed_And_Initial_Metronome_Speed_Is_Equal_To_Current_ComfortSpeed_Returns_0()
        {
            Exercise exercise = new Exercise
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                InitialMetronomeSpeed = 70,
                TargetMetronomeSpeed = 120,
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

            Assert.That(percentComplete, Is.EqualTo(0), "If InitialMetronomeSpeed has been specified, and is equal to the current comfort speed, but less than the target speed. Progress should be 0%");
        }

        [Test]
        public void Exercise_GetPercentComplete_When_StrategyOn_MetronomeSpeed_And_Initial_Metronome_Speed_Is_More_Than_Current_ComfortSpeed_Returns_0()
        {
            Exercise exercise = new Exercise
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                InitialMetronomeSpeed = 80,
                TargetMetronomeSpeed = 120,
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

            Assert.That(percentComplete, Is.EqualTo(0), "If InitialMetronomeSpeed has been specified, and your current comfort speed is less than the Initially stated speed, your progress must be 0%");
        }


        [Test]
        public void Exercise_GetPercentComplete_When_StrategyOn_MetronomeSpeed_And_Target_Is_0_Always_Return_0()
        {
            Exercise exercise = new Exercise
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                InitialMetronomeSpeed = 0,
                TargetMetronomeSpeed = 0,
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
        public void Exercise_GetPercentComplete_When_StrategyOn_MetronomeSpeed_And_Comfort_Speed_Is_Half_Way_To_Target_Return_50()
        {
            Exercise exercise = new Exercise
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                InitialMetronomeSpeed = 50,
                TargetMetronomeSpeed = 150,
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

            Assert.That(percentComplete, Is.EqualTo(50), "If InitialMetronomeSpeed has been specified, and your current comfort speed is half way between the Initial and Target Speed, return 50%");
        }

        [Test]
        public void Exercise_GetPercentComplete_On_MetronomeSpeed_And_No_InitialMetronomeSpeed_With_Calculates_Progress_Using_Initial_Comfort_As_Lower_Bound()
        {
            Exercise exercise = new Exercise
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                InitialMetronomeSpeed = null,
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
                        Id = 1,
                        DateCreated = DateTime.Parse("2018/01/03"),
                        DateModified = DateTime.Parse("2018/01/03"),
                        MetronomeSpeed = 100,
                        StartTime = DateTime.Parse("2018/01/03 10:12:00"),
                        EndTime = DateTime.Parse("2018/01/03 10:15:00"),
                        ExerciseId = 1 }
                }
            };

            var percentComplete = exercise.GetPercentComplete();

            Assert.That(percentComplete, Is.EqualTo(50), "When no initial metronome speed has been captured. Calculation should consider the first activity comfort speed as the lower bound for the progress calculation.");
        }

        [Test]
        public void Exercise_GetPercentComplete_Metronome_No_InitialMetronomeSpeed_And_Last_Activity_Comfort_Less_Or_Equal_To_First_Returns_0()
        {
            Exercise exercise = new Exercise
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                InitialMetronomeSpeed = null,
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
                        Id = 1,
                        DateCreated = DateTime.Parse("2018/01/03"),
                        DateModified = DateTime.Parse("2018/01/03"),
                        MetronomeSpeed = 50,
                        StartTime = DateTime.Parse("2018/01/03 10:12:00"),
                        EndTime = DateTime.Parse("2018/01/03 10:15:00"),
                        ExerciseId = 1 }
                }
            };

            var percentComplete = exercise.GetPercentComplete();

            Assert.That(percentComplete, Is.EqualTo(0), "When no initial metronome speed has been captured and activity shows that last comfort speed is equal (or less than) the initial comfort speed should always return 0%");
        }

        [Test]
        public void Exercise_GetPercentComplete_When_StrategyOn_MetronomeSpeed_CalcsCorrectly()
        {
            Exercise exercise = new Exercise
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,

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
                        Id = 1,
                        DateCreated = DateTime.Parse("2018/01/03"),
                        DateModified = DateTime.Parse("2018/01/03"),
                        MetronomeSpeed = 75,
                        StartTime = DateTime.Parse("2018/01/03 10:12:00"),
                        EndTime = DateTime.Parse("2018/01/03 10:15:00"),
                        ExerciseId = 1 },

                    new Sessions.ExerciseActivity {
                        Id = 1,
                        DateCreated = DateTime.Parse("2018/01/04"),
                        DateModified = DateTime.Parse("2018/01/04"),
                        MetronomeSpeed = 80,
                        StartTime = DateTime.Parse("2018/01/04 10:12:00"),
                        EndTime = DateTime.Parse("2018/01/04 10:15:00"),
                        ExerciseId = 1 }
                }
            };

            var percentComplete = exercise.GetPercentComplete();

            Assert.That(percentComplete, Is.InRange(66.6, 66.7));
        }

        [Test]
        public void Exercise_GetPercentComplete_When_StrategyOn_PracticeTime_CalcsCorrectly()
        {
            Exercise exercise = new Exercise
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,

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
                        Id = 1,
                        DateCreated = DateTime.Parse("2018/01/03"),
                        DateModified = DateTime.Parse("2018/01/03"),
                        MetronomeSpeed = 75,
                        Seconds = 600, // 10 minutes
                        StartTime = DateTime.Parse("2018/01/03 10:00:00"),
                        EndTime = DateTime.Parse("2018/01/03 10:10:00"),
                        ExerciseId = 1 },

                    new Sessions.ExerciseActivity {
                        Id = 1,
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
    }
}
