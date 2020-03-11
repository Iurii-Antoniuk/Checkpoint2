using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Checkpoint2
{
    public class Database
    {
        private static SqlConnection _instance = null;
        public static SqlConnection Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SqlConnection(GetConnectionString());
                }
                return _instance;
            }
        }
        public static string GetConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CheckPoint2"].ConnectionString;
            return connectionString;
        }

        public static List<object[]> GetEntriesFromDB(string name, string procedureName)
        {
            SqlConnection connection = Instance;
            connection.Open();

            SqlCommand command = new SqlCommand(procedureName, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@personName", SqlDbType.VarChar)).Value = name;

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
