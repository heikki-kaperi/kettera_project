using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Reflection;

namespace ContractManagement.Model.DAL
{
    public static class DbHelper
    {
        public static string ConnectionString = "server=127.0.0.1;user=root;database=kettera;password=";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        // Dynamically adds parameters from an anonymous object or dictionary
        public static void AddParams(this MySqlCommand cmd, object paramObj)
        {
            if (paramObj == null) return;

            foreach (PropertyInfo prop in paramObj.GetType().GetProperties())
            {
                var name = "@" + prop.Name;
                var value = prop.GetValue(paramObj) ?? DBNull.Value;
                cmd.Parameters.AddWithValue(name, value);
            }
        }

        // Simplified ExecuteNonQuery
        public static bool ExecuteNonQuery(string sql, object param = null)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.AddParams(param);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Simplified ExecuteScalar
        public static object ExecuteScalar(string sql, object param = null)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.AddParams(param);
                    return cmd.ExecuteScalar();
                }
            }
        }
    }
}

