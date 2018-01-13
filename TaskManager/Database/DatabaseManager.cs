using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TaskManager.Data.Types;

namespace TaskManager
{
    class DatabaseManager
    {
        public static List<Task> GetAllTasks()
        {
            var TaskDict = DatabaseConnector.Select("Tasks", new List<string>() { "ID", "Name", "Description", "DueDate", "State", "Importance", "Message","LastModified"});
            var taskList = new List<Task>();
            if(TaskDict != null && TaskDict.Any())
            {
                for (int i = 0; i < TaskDict.Count; i++)
                {
                    var values = TaskDict[i];

                    State state = State.New;
                    switch (values[4])
                    {
                        case "InProgress": state = State.InProgress; break;
                        case "Paused": state = State.Paused; break;
                        case "Done": state = State.Done; break;
                    }

                    Importance importance = Importance.Low;
                    switch (values[5])
                    {
                        case "Medium": importance = Importance.Medium; break;
                        case "High": importance = Importance.High; break;
                    }

                    var lastMod = new DateTime();
                    DateTime.TryParse(values[7], out lastMod);

                    var Task = new Task(Convert.ToInt32(values[0]), values[1], values[2], DateTime.Parse(values[3]), state, importance, values[6], lastMod);
                    taskList.Add(Task);
                }
            }

            return taskList;
        }

        public static Task GetTask(int ID)
        {
            var TaskDict = DatabaseConnector.Select("Tasks", new List<string>() { "ID", "Name", "Description", "DueDate", "State", "Importance", "Message", "LastModified" },ID);
            if(TaskDict != null && TaskDict.Any())
            {
                var values = TaskDict[0];

                State state = State.New;
                switch (values[4])
                {
                    case "InProgress": state = State.InProgress; break;
                    case "Paused": state = State.Paused; break;
                    case "Done": state = State.Done; break;
                }

                Importance importance = Importance.Low;
                switch (values[5])
                {
                    case "Medium": importance = Importance.Medium; break;
                    case "High": importance = Importance.High; break;
                }

                var Task = new Task(Convert.ToInt32(values[0]), values[1], values[2], DateTime.Parse(values[3]), state, importance, values[6], DateTime.Parse(values[7]));
                return Task;
            }

            return null;
        }

        public static void InsertTask(Task task)
        {
            var Values = new List<string>()
            {
                $"'{task.Name}'",
                $"'{task.Description}'",
                $"'{task.DueDate.ToString("yyyy-MM-dd").Split(' ')[0]}'",
                $"'{task.State}'",
                $"'{task.Importance}'"
            };

            DatabaseConnector.Insert("Tasks", new List<string>() {"Name", "Description", "DueDate", "State", "Importance"}, Values);
        }

        public static void UpdateTask(Task task)
        {
            var Values = new List<string>()
            {
                $"'{task.Name}'",
                $"'{task.Description}'",
                $"'{task.DueDate.ToString("yyyy-MM-dd").Split(' ')[0]}'",
                $"'{task.State}'",
                $"'{task.Importance}'"
            };

            DatabaseConnector.Update("Tasks", new List<string>() { "Name", "Description", "DueDate", "State", "Importance"}, Values, task.ID);
        }

        public static void DeleteTask(int ID)
        {
            DatabaseConnector.Delete("Tasks", ID);
        }

        public static void UpdateData()
        {
            DatabaseConnector.UpdateData();
        }
    }
}
