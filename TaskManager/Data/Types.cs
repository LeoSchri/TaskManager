using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Data
{
    public class Types
    {
        public enum State
        {
            New,
            InProgress,
            Paused,
            Done
        }

        public enum Importance
        {
            Low,
            Medium,
            High
        }
    }
}
