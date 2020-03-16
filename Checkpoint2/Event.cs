using System;
using System.Globalization;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Concurrent;

namespace Checkpoint2
{
    public class Event
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Event()
        {  
        }
        public override bool Equals(object obj)
        {
            return (this.Name == (obj as Event).Name) && (this.Description == (obj as Event).Description);
        }
        public void Postpone(TimeSpan timeDelta)
        {
            StartTime = StartTime + timeDelta;
            EndTime = EndTime + timeDelta;
        }

        
    }
}