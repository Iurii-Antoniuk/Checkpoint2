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

        public override List<object[]> GetEventsFromDB()
        {
            SqlConnection connection = Database.Instance;
            connection.Open();
            string queryString = "sp_eventsProf";

            SqlCommand command = new SqlCommand(queryString, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@profName", SqlDbType.VarChar)).Value = Name;

            SqlDataReader dataread = command.ExecuteReader();
            List<object[]> data = new List<object[]>();

            while (dataread.Read())
            {
                object[] output = new object[dataread.FieldCount];
                dataread.GetValues(output);
                data.Add(output);
            }
            dataread.Close();
            connection.Close();
            return data;
        }
    }
}
