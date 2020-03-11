using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Checkpoint2
{
    public class Student : Person
    {
        public Student(String name) : base(name)
        {}
        
        public override String ToString()
        {
            return $"{Name}";
        }      
    }
}
