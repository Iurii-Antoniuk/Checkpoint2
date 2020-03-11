using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Checkpoint2
{
    public class Prof : Person
    {
        public List<Person> Subordinates { get; set; }

        public Prof(String name) : base(name)
        {
            Subordinates = new List<Person>();
        }

        public override String ToString()
        {
            String subordinates = $"{Name}";
            foreach (Person sub in Subordinates)
            {
                subordinates += $"\n\t {sub.Name}";
            }
            return subordinates;
        }
    }
}
