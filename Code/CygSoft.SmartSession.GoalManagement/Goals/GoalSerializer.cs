using CygSoft.SmartSession.GoalManagement.Infrastructure;
using CygSoft.SmartSession.GoalManagement.Sessions;
using CygSoft.SmartSession.GoalManagement.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CygSoft.SmartSession.GoalManagement.Goals
{
    public class GoalSerializer : IGoalSerializer
    {
        public IGoal Deserialize(string serializedGoal)
        {
            XElement goalNode = XElement.Parse(serializedGoal);


            return new Goal(
                goalNode.Attribute("Id").Value,
                goalNode.Attribute("Title").Value,
                int.Parse(goalNode.Attribute("MaxTaskWeighting").Value),
                DeserializeTasks(goalNode.Element("Tasks")), 
                null
            );
        }

        private IGoalTask[] DeserializeTasks(XElement tasksNode)
        {
            List<IGoalTask> goalTasks = new List<IGoalTask>();

            foreach (XElement taskNode in tasksNode.Elements("Task"))
            {
                GoalTask task;

                switch (taskNode.Attribute("Type").Value)
                {
                    case "Duration":
                        task = new DurationGoalTask(
                            taskNode.Attribute("Id").Value,
                            taskNode.Attribute("Title").Value,
                            DateTime.Parse(taskNode.Attribute("DateCreated").Value),
                            int.Parse(taskNode.Attribute("Minutes").Value),
                            new List<DurationSessionResult>()

                        );
                        task.Weighting = int.Parse(taskNode.Attribute("Weighting").Value);
                        break;
                    case "Percent":
                        task = new PercentGoalTask(
                            taskNode.Attribute("Id").Value,
                            taskNode.Attribute("Title").Value,
                            DateTime.Parse(taskNode.Attribute("DateCreated").Value),
                            new List<PercentSessionResult>()
                        );
                        task.Weighting = int.Parse(taskNode.Attribute("Weighting").Value);
                        break;
                    case "Metronome":
                            task = new MetronomeGoalTask(
                             taskNode.Attribute("Id").Value,
                             taskNode.Attribute("Title").Value,
                             DateTime.Parse(taskNode.Attribute("DateCreated").Value),
                             int.Parse(taskNode.Attribute("InitialSpeed").Value),
                             int.Parse(taskNode.Attribute("TargetSpeed").Value),
                             new List<MetronomeSessionResult>()
                         );
                        task.Weighting = int.Parse(taskNode.Attribute("Weighting").Value);
                        break;
                    default:
                        throw new NotSupportedException("The task type being deserialized is not supported by this version.");
                }
                goalTasks.Add(task);
            }
            return goalTasks.ToArray();
        }

        public string Serialize(IGoal goal)
        {
            throw new NotImplementedException();
        }
    }
}
