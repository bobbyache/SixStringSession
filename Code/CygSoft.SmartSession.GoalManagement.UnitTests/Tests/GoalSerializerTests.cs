using CygSoft.SmartSession.GoalManagement.Goals;
using CygSoft.SmartSession.GoalManagement.Tasks;
using CygSoft.SmartSession.GoalManagement.UnitTests.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement.UnitTests.Tests
{
    [TestFixture]
    [Category("GoalPersistence")]
    [Category("Integration")]
    class GoalSerializerTests
    {
        [Test]
        public void Goal_When_Deserialized_Populates_Goal_Properties()
        {
            string xml = TxtFile.ReadText("GoalXml.txt");

            GoalSerializer serializer = new GoalSerializer();
            var goal = serializer.Deserialize(xml);

            Assert.IsNotNull(goal);
            Assert.That(goal.Id, Is.EqualTo("0a723447-e366-4e0b-9f00-1590e8fd8adc"));
            Assert.That(goal.MaxTaskWeighting, Is.EqualTo(1000));
            Assert.That(goal.Title, Is.EqualTo("Goal 1 - First Ever Goal"));
        }

        [Test]
        public void Goal_When_Deserialized_Populates_All_Goal_Tasks()
        {
            string xml = TxtFile.ReadText("GoalXml.txt");

            GoalSerializer serializer = new GoalSerializer();
            var goal = serializer.Deserialize(xml);

            Assert.That(goal.Tasks.Length, Is.EqualTo(3));
        }

        [Test]
        public void MetronomeGoalTask_Deserializes_Properties_Correctly()
        {
            string xml = TxtFile.ReadText("GoalXml.txt");

            GoalSerializer serializer = new GoalSerializer();
            var goal = serializer.Deserialize(xml);
            var goalTask = goal.Tasks[0] as MetronomeGoalTask;

            Assert.IsNotNull(goalTask);

            Assert.That(goalTask.Id, Is.EqualTo("0bef8bd6-9fda-4115-bb1a-e21f6a8dbeb7"));
            Assert.That(goalTask.CreateDate, Is.EqualTo(DateTime.Parse("2016-12-16")));
            Assert.That(goalTask.Title, Is.EqualTo("Learn Measure 1 - 4"));
            Assert.That(goalTask.Weighting, Is.EqualTo(25));
            Assert.That(goalTask.InitialSpeed,  Is.EqualTo(60));
            Assert.That(goalTask.TargetSpeed, Is.EqualTo(120));
            Assert.That(goalTask.CurrentSpeed, Is.EqualTo(75));
            Assert.That(goalTask.MinutesPracticed, Is.EqualTo(22));
        }

        [Test]
        public void DurationGoalTask_Deserializes_Properties_Correctly()
        {
            string xml = TxtFile.ReadText("GoalXml.txt");

            GoalSerializer serializer = new GoalSerializer();
            var goal = serializer.Deserialize(xml);
            var goalTask = goal.Tasks[2] as DurationGoalTask;

            Assert.IsNotNull(goalTask);

            Assert.That(goalTask.Id, Is.EqualTo("062b7405-5902-4369-88eb-afa3804f1500"));
            Assert.That(goalTask.CreateDate, Is.EqualTo(DateTime.Parse("2016-12-12")));
            Assert.That(goalTask.Title, Is.EqualTo("Bends and Pulloffs"));
            Assert.That(goalTask.Weighting, Is.EqualTo(50));
            Assert.That(goalTask.MinutesPracticed, Is.EqualTo(40));
            Assert.That(goalTask.PercentCompleted, Is.EqualTo(50));
        }

        [Test]
        public void PercentGoalTask_Deserializes_Properties_Correctly()
        {
            string xml = TxtFile.ReadText("GoalXml.txt");

            GoalSerializer serializer = new GoalSerializer();
            var goal = serializer.Deserialize(xml);
            var goalTask = goal.Tasks[1] as PercentGoalTask;

            Assert.IsNotNull(goalTask);

            Assert.That(goalTask.Id, Is.EqualTo("ca5905cc-3fcd-4e43-a4be-9e5a9d8fd0d6"));
            Assert.That(goalTask.CreateDate, Is.EqualTo(DateTime.Parse("2016-12-16")));
            Assert.That(goalTask.Title, Is.EqualTo("Transcribe Examples 1 - 16 in the book"));
            Assert.That(goalTask.Weighting, Is.EqualTo(25));
            Assert.That(goalTask.MinutesPracticed, Is.EqualTo(22));
            Assert.That(goalTask.PercentCompleted, Is.EqualTo(75));
        }

        [Test]
        public void GoalSerializer_Deserializes_Goal_Files_Correctly()
        {
            string xml = TxtFile.ReadText("GoalXml.txt");

            GoalSerializer serializer = new GoalSerializer();
            var goal = serializer.Deserialize(xml);
            Assert.That(goal.FileCount, Is.EqualTo(2));
        }
    }
}
