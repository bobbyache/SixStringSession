using System;
using System.Collections.Generic;
using CygSoft.SmartSession.EF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartSession.Domain.Records;

namespace CygSoft.SmartSession.EF.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Insert_A_Goal()
        {
            var goal = new Goal
            {
                Title = "Here is my first goal",
                DateCreated = DateTime.Now,
                TargetCompletionDate = DateTime.Now.AddMonths(6),
                Description = "My Description"
            };

            DataAccess dataAccess = new DataAccess();
            dataAccess.InsertGoal(goal);
        }

        [TestMethod]
        public void Insert_A_Task()
        {
            var task = new PracticeTask()
            {
                Title = "Here is my first goal",
                DateCreated = DateTime.Now,
                GoalTaskType = 1,
                TargetPracticeDuration = 10,
                TargetSpeed = 120,
                Exercise = new Exercise()
                {
                    Title = "Exercise Title",
                    DifficultyRating = 3,
                    RequiredDuration = 520,
                    DateCreated = DateTime.Now
                },
                Description = "My Description"
            };

            DataAccess dataAccess = new DataAccess();
            dataAccess.InsertTask(task);
        }

        [TestMethod]
        public void Insert_A_Session()
        {
            Session session = new Session()
            {
                StartTime = DateTime.Now,
                Notes = "It seemed to go well today. Needs more work I think, especially the bends and the rythm."
            };

            session.SessionTasks.AddRange(
                new List<SessionPracticeTask>
                {
                    new SessionPracticeTask
                    {
                        Duration = new SessionPracticeTaskDuration { Minutes = 5 },
                        Metronome = new SessionPracticeTaskMetronome { StartSpeed = 60, EndSpeed = 120, ComfortableSpeed = 90 },
                        PracticeTask = new PracticeTask
                        {
                            Title = "Practice Task Title",
                            Description = "Here is a description",
                            GoalTaskType = 1,
                            DateCreated = DateTime.Now,
                            TargetPracticeDuration = 4,
                            TargetSpeed = 130,
                            Exercise = new Exercise { Title = "Exercise 234", DateCreated = DateTime.Now, DifficultyRating = 4, RequiredDuration = 5 },                            
                        }
                    }
                });


            DataAccess dataAccess = new DataAccess();
            dataAccess.InsertSession(session);
        }

        [TestMethod]
        public void Get_A_Task()
        {
            DataAccess dataAccess = new DataAccess();
            var task = dataAccess.GetTask(1);
            Assert.IsNotNull(task);
        }

        [TestMethod]
        public void Get_A_Session()
        {
            DataAccess dataAccess = new DataAccess();
            var session = dataAccess.GetSession(1);
            Assert.IsNotNull(session);
        }
    }
}
