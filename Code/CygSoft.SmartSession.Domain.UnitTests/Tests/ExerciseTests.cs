using CygSoft.SmartSession.Domain.Exercises;
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
        public void Exercise_GetPercentComplete_When_StrategyOn_MetronomeSpeed_CalcsCorrectly()
        {
            Exercise exercise = new Exercise
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,

                TargetMetronomeSpeed = 100,
                PercentageCompleteCalculationType = PercentCompleteCalculationStrategy.MetronomeSpeed,

                ExerciseActivity = new List<Sessions.SessionExerciseActivity>
                {
                    new Sessions.SessionExerciseActivity {
                        Id = 1,
                        DateCreated = DateTime.Parse("2018/01/02"),
                        DateModified = DateTime.Parse("2018/01/02"),
                        StartMetronomeSpeed = 60,
                        ComfortMetronomeSpeed = 70,
                        AchievedMetronomeSpeed = 80,
                        StartTime = DateTime.Parse("2018/01/02 10:12:00"),
                        EndTime = DateTime.Parse("2018/01/02 10:15:00"),
                        ExerciseId = 1 },

                    new Sessions.SessionExerciseActivity {
                        Id = 1,
                        DateCreated = DateTime.Parse("2018/01/03"),
                        DateModified = DateTime.Parse("2018/01/03"),
                        StartMetronomeSpeed = 65,
                        ComfortMetronomeSpeed = 75,
                        AchievedMetronomeSpeed = 85,
                        StartTime = DateTime.Parse("2018/01/03 10:12:00"),
                        EndTime = DateTime.Parse("2018/01/03 10:15:00"),
                        ExerciseId = 1 },

                    new Sessions.SessionExerciseActivity {
                        Id = 1,
                        DateCreated = DateTime.Parse("2018/01/04"),
                        DateModified = DateTime.Parse("2018/01/04"),
                        StartMetronomeSpeed = 70,
                        ComfortMetronomeSpeed = 80,
                        AchievedMetronomeSpeed = 85,
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

                ExerciseActivity = new List<Sessions.SessionExerciseActivity>
                {
                    new Sessions.SessionExerciseActivity {
                        Id = 1,
                        DateCreated = DateTime.Parse("2018/01/02"),
                        DateModified = DateTime.Parse("2018/01/02"),
                        StartMetronomeSpeed = 60,
                        ComfortMetronomeSpeed = 70,
                        AchievedMetronomeSpeed = 80,
                        StartTime = DateTime.Parse("2018/01/02 10:00:00"),
                        EndTime = DateTime.Parse("2018/01/02 10:10:00"),
                        ExerciseId = 1 },

                    new Sessions.SessionExerciseActivity {
                        Id = 1,
                        DateCreated = DateTime.Parse("2018/01/03"),
                        DateModified = DateTime.Parse("2018/01/03"),
                        StartMetronomeSpeed = 65,
                        ComfortMetronomeSpeed = 75,
                        AchievedMetronomeSpeed = 85,
                        StartTime = DateTime.Parse("2018/01/03 10:00:00"),
                        EndTime = DateTime.Parse("2018/01/03 10:10:00"),
                        ExerciseId = 1 },

                    new Sessions.SessionExerciseActivity {
                        Id = 1,
                        DateCreated = DateTime.Parse("2018/01/04"),
                        DateModified = DateTime.Parse("2018/01/04"),
                        StartMetronomeSpeed = 70,
                        ComfortMetronomeSpeed = 80,
                        AchievedMetronomeSpeed = 85,
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
