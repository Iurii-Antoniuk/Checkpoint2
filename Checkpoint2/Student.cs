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
        
        public override List<object[]> GetEventsFromDB()
        {
            SqlConnection connection = Database.Instance;
            connection.Open();
            string queryString = "sp_eventsStudent";

            SqlCommand command = new SqlCommand(queryString, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@studentName", SqlDbType.VarChar)).Value = Name;

            SqlDataReader dataread =  command.ExecuteReader();
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
