using System;
using System.Collections.Generic;
using System.Text;

namespace Checkpoint2
{
    public static class PersonFactory
    {
        public static Person Create(string name)
        {
            string procedure = "sp_getSubordinatesByName";
            List<object[]> subordinates = Database.GetEntriesFromDB(name, procedure);
            if (subordinates != null && subordinates.Count > 0)
            {
                Person prof = new Prof(name);
                return prof;
            }
            else
            {
                Person stud = new Student(name);
                return stud;
            }
        }
    }
}
