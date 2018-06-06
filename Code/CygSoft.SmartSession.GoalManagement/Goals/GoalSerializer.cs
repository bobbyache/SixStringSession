using CygSoft.SmartSession.GoalManagement.Files;
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
                DeserializeFiles(goalNode.Element("Files"))
            );
        }

        private IGoalFile[] DeserializeFiles(XElement filesNode)
        {
            List<IGoalFile> goalFiles = new List<IGoalFile>();

            foreach (XElement fileNode in filesNode.Elements("File"))
            {
                GoalFile goalFile = new GoalFile();
                goalFile.FilePath = fileNode.Attribute("Path").Value;
                goalFiles.Add(goalFile);
            }
            return goalFiles.ToArray();
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
                            GetDurationSessions(taskNode)

                        );
                        task.Weighting = int.Parse(taskNode.Attribute("Weighting").Value);
                        break;
                    case "Percent":
                        task = new PercentGoalTask(
                            taskNode.Attribute("Id").Value,
                            taskNode.Attribute("Title").Value,
                            DateTime.Parse(taskNode.Attribute("DateCreated").Value),
                            GetPercentSessions(taskNode)
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
                             GetMetronomeSessions(taskNode)
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

        private List<MetronomeSessionResult> GetMetronomeSessions(XElement metronomeTask)
        {
            List<MetronomeSessionResult> metronomeSessions = new List<MetronomeSessionResult>();

            foreach (XElement sessionNode in metronomeTask.Element("SessionResults").Elements("SessionResult"))
            {
                MetronomeSessionResult result = new MetronomeSessionResult(
                        sessionNode.Attribute("Id").Value,
                        DateTime.Parse(sessionNode.Attribute("StartTime").Value),
                        DateTime.Parse(sessionNode.Attribute("EndTime").Value),
                        int.Parse(sessionNode.Attribute("Speed").Value)
                    );
                metronomeSessions.Add(result);
            }
            return metronomeSessions;
        }

        private List<DurationSessionResult> GetDurationSessions(XElement durationTask)
        {
            List<DurationSessionResult> durationSessions = new List<DurationSessionResult>();

            foreach (XElement sessionNode in durationTask.Element("SessionResults").Elements("SessionResult"))
            {
                DurationSessionResult result = new DurationSessionResult(
                        sessionNode.Attribute("Id").Value,
                        DateTime.Parse(sessionNode.Attribute("StartTime").Value),
                        DateTime.Parse(sessionNode.Attribute("EndTime").Value)
                    );
                durationSessions.Add(result);
            }
            return durationSessions;
        }

        private List<PercentSessionResult> GetPercentSessions(XElement durationTask)
        {
            List<PercentSessionResult> percentSessions = new List<PercentSessionResult>();

            foreach (XElement sessionNode in durationTask.Element("SessionResults").Elements("SessionResult"))
            {
                PercentSessionResult result = new PercentSessionResult(
                        sessionNode.Attribute("Id").Value,
                        DateTime.Parse(sessionNode.Attribute("StartTime").Value),
                        DateTime.Parse(sessionNode.Attribute("EndTime").Value),
                        int.Parse(sessionNode.Attribute("Percent").Value)
                    );
                percentSessions.Add(result);
            }
            return percentSessions;
        }

        public string Serialize(IGoal goal)
        {
            throw new NotImplementedException();
        }
    }
}
