using System;
using MySql.Data.MySqlClient;

namespace ContractManagement.Model.Entities
{
    public class Contract
    {
        public int Contract_NR { get; set; }
        public string Company_name { get; set; }
        public int The_Creator { get; set; }
        public DateTime Created_date { get; set; }
        public bool Approved { get; set; }
        public bool Sent_to_external { get; set; }
    }

    public class InternalUser
    {
        public int Int_User_ID { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}

namespace ContractManagement.Model.DAL
{
    using ContractManagement.Model.Entities;

    public class DatabaseConnection
    {
        private string connectionString = "server=127.0.0.1;user=root;database=kettera;password=";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }

    public class InternalUserDAL
    {
        private DatabaseConnection dbConn = new DatabaseConnection();

        public InternalUser GetInternalUserByUsername(string username)
        {
            InternalUser user = null;
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM internal_user WHERE Username = @username";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new InternalUser
                        {
                            Int_User_ID = reader.GetInt32("Int_User_ID"),
                            First_name = reader.GetString("First_name"),
                            Last_name = reader.GetString("Last_name"),
                            Username = reader.GetString("Username"),
                            Password = reader.GetString("Password"),
                            Email = reader.GetString("Email")
                        };
                    }
                }
            }
            return user;
        }
    }

    public class ContractDAL
    {
        private DatabaseConnection dbConn = new DatabaseConnection();

        public int CreateContract(Contract contract)
        {
            try
            {
                using (MySqlConnection conn = dbConn.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO contract 
                (Company_name, The_Creator, Created_date, Approved, Sent_to_external) 
                VALUES (@name, @creator, @created, @approved, @sent);
                SELECT LAST_INSERT_ID();";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", contract.Company_name);
                    cmd.Parameters.AddWithValue("@creator", contract.The_Creator);
                    cmd.Parameters.AddWithValue("@created", contract.Created_date);
                    cmd.Parameters.AddWithValue("@approved", contract.Approved);
                    cmd.Parameters.AddWithValue("@sent", contract.Sent_to_external);

                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}