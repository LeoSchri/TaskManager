using System;
using static TaskManager.Data.Types;

namespace TaskManager
{
    public class Task
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public State State { get; set; }
        public Importance Importance { get; set; }
        public string Message { get; set; }
        public DateTime? LastModified { get; set; }


        public Task(int id, string name, string description, DateTime dueDate, State state, Importance importance, string message, DateTime? lastModified)
        {
            ID = id;
            Name = name;
            Description = description;
            DueDate = dueDate;
            State = state;
            Importance = importance;
            Message = message;
            LastModified = lastModified;
        }

        public Task()
        {

        }
    }

    
}
