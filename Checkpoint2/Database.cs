using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;

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

        
    }
}
