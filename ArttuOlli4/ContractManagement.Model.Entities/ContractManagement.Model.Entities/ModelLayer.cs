using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ZstdSharp.Unsafe;

// ==================== ENTITY CLASSES ====================

namespace ContractManagement.Model.Entities
{

    public enum BlockType
    {
        Text,
        Image,
        Other
    }

    public class Administrator
    {
        public int Administrator_ID { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
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

    public class ExternalUser
    {
        public int Ext_User_ID { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Company_name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class ContractBlockCategory
    {
        public string Category_name { get; set; }
        public string Description { get; set; }
    }

    public class OriginalContractBlock
    {
        public int Org_Cont_ID { get; set; }
        public string Category_name { get; set; }
        public string Contract_text { get; set; }
        public int Created_by { get; set; }
        public DateTime Created_date { get; set; }
    }

    public class ContractBlock
    {
        public int Contract_Block_NR { get; set; }
        public int Org_Cont_ID { get; set; }
        public string Contract_text { get; set; }
        public bool New { get; set; }
        public DateTime Modified_date { get; set; }
        public BlockType Type { get; set; }
        public byte[] MediaContent { get; set; }

        public int? Parent_Block_NR { get; set; }
        public List<ContractBlock> ChildBlocks { get; set; } = new List<ContractBlock>();
    }

    public class ContractBlockReference
    {
        public int Reference_ID { get; set; }
        public int Contract_Block_NR { get; set; }
        public int Referenced_Block_NR { get; set; }
        public int Reference_Order { get; set; }
    }

    public class Contract
    {
        public int Contract_NR { get; set; }
        public string Company_name { get; set; }
        public int The_Creator { get; set; }
        public DateTime Created_date { get; set; }
        public bool Approved { get; set; }
        public bool Sent_to_external { get; set; }
    }

    public class ContractStakeholder
    {
        public int Contract_NR { get; set; }
        public int Int_User_ID { get; set; }
        public bool Has_approval_rights { get; set; }
        public bool Approved { get; set; }
        public DateTime? Approved_date { get; set; }
    }

    public class ContractExternalUser
    {
        public int Contract_NR { get; set; }
        public int Ext_User_ID { get; set; }
        public DateTime Invited_date { get; set; }
    }

    public class Comment
    {
        public int Comment_ID { get; set; }
        public int Contract_NR { get; set; }
        public int? Contract_Block_NR { get; set; }
        public int User_ID { get; set; }
        public string User_type { get; set; } // 'internal' or 'external'
        public string Comment_text { get; set; }
        public DateTime Comment_date { get; set; }
    }
}

// ==================== DATABASE ACCESS CLASSES ====================

namespace ContractManagement.Model.DAL
{
    using System.Data.SqlClient;
    using System.Reflection;
    using ContractManagement.Model.Entities;

    public class DatabaseConnection
    {
        private string connectionString = "server=127.0.0.1;user=root;database=kettera;password=";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }

    // ==================== ADMINISTRATOR DAL ====================
    public class AdministratorDAL
    {
        private DatabaseConnection dbConn = new DatabaseConnection();

        public Administrator GetAdministratorByUsername(string username)
        {
            Administrator admin = null;
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM administrator WHERE Username = @username";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        admin = new Administrator
                        {
                            Administrator_ID = reader.GetInt32("Administrator_ID"),
                            First_name = reader.GetString("First_name"),
                            Last_name = reader.GetString("Last_name"),
                            Username = reader.GetString("Username"),
                            Password = reader.GetString("Password")
                        };
                    }
                }
            }
            return admin;
        }

        public bool CreateAdministrator(Administrator admin) =>
            DbHelper.ExecuteNonQuery(
            SqlBuilder.Insert("administrator", "First_name", "Last_name", "Username", "Password", "Email"),
            admin
         );

        public bool VoteToDeleteAdmin(int targetAdminId, int votingAdminId)
        {
            using (var conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = @"INSERT IGNORE INTO admin_deletion_votes 
                             (Target_Admin_ID, Voting_Admin_ID) 
                             VALUES (@target, @voter)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@target", targetAdminId);
                cmd.Parameters.AddWithValue("@voter", votingAdminId);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool CanDeleteAdmin(int targetAdminId)
        {
            using (var conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = @"SELECT COUNT(*) FROM admin_deletion_votes 
                             WHERE Target_Admin_ID = @target";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@target", targetAdminId);

                int votes = Convert.ToInt32(cmd.ExecuteScalar());
                return votes >= 3; // tarvitaan kolme hyväksyntää
            }
        }

        public bool DeleteAdminIfApproved(int targetAdminId)
        {
            if (!CanDeleteAdmin(targetAdminId)) return false;

            using (var conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM administrator WHERE Administrator_ID = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", targetAdminId);
                int deleted = cmd.ExecuteNonQuery();

                if (deleted > 0)
                {
                    // Tyhjennä äänestykset
                    query = "DELETE FROM admin_deletion_votes WHERE Target_Admin_ID = @id";
                    cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", targetAdminId);
                    cmd.ExecuteNonQuery();
                }

                return deleted > 0;
            }
        }
    }

    // ==================== INTERNAL USER DAL ====================
    public class InternalUserDAL
    {
        private DatabaseConnection dbConn = new DatabaseConnection();

        public List<InternalUser> GetAllInternalUsers()
        {
            List<InternalUser> users = new List<InternalUser>();
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM internal_user";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new InternalUser
                        {
                            Int_User_ID = reader.GetInt32("Int_User_ID"),
                            First_name = reader.GetString("First_name"),
                            Last_name = reader.GetString("Last_name"),
                            Username = reader.GetString("Username"),
                            Password = reader.GetString("Password"),
                            Email = reader.GetString("Email")
                        });
                    }
                }
            }
            return users;
        }

        public InternalUser GetInternalUserById(int userId)
        {
            InternalUser user = null;
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM internal_user WHERE Int_User_ID = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", userId);

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

        public bool CreateInternalUser(InternalUser user) =>
            DbHelper.ExecuteNonQuery(
                SqlBuilder.Insert("internal_user", "First_name", "Last_name", "Username", "Password", "Email"),
                user
            );

        public bool UpdateInternalUser(InternalUser user) =>
            DbHelper.ExecuteNonQuery(
                SqlBuilder.Update("internal_user", "Int_User_ID", "First_name", "Last_name", "Username", "Password", "Email"),
                user
            );

        public bool DeleteInternalUser(int userId)
        {
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM internal_user WHERE Int_User_ID = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", userId);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }

    // ==================== EXTERNAL USER DAL ====================
    public class ExternalUserDAL
    {
        private DatabaseConnection dbConn = new DatabaseConnection();

        public List<ExternalUser> GetAllExternalUsers()
        {
            List<ExternalUser> users = new List<ExternalUser>();
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM external_user";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new ExternalUser
                        {
                            Ext_User_ID = reader.GetInt32("Ext_User_ID"),
                            First_name = reader.GetString("First_name"),
                            Last_name = reader.GetString("Last_name"),
                            Company_name = reader.GetString("Company_name"),
                            Email = reader.GetString("Email"),
                            Username = reader.GetString("Username"),
                            Password = reader.GetString("Password")
                        });
                    }
                }
            }
            return users;
        }

        public ExternalUser GetExternalUserById(int userId)
        {
            ExternalUser user = null;
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM external_user WHERE Ext_User_ID = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", userId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new ExternalUser
                        {
                            Ext_User_ID = reader.GetInt32("Ext_User_ID"),
                            First_name = reader.GetString("First_name"),
                            Last_name = reader.GetString("Last_name"),
                            Company_name = reader.GetString("Company_name"),
                            Email = reader.GetString("Email"),
                            Username = reader.GetString("Username"),
                            Password = reader.GetString("Password")
                        };
                    }
                }
            }
            return user;
        }

        public ExternalUser GetExternalUserByUsername(string username)
        {
            ExternalUser user = null;
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM external_user WHERE Username = @username";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new ExternalUser
                        {
                            Ext_User_ID = reader.GetInt32("Ext_User_ID"),
                            First_name = reader.GetString("First_name"),
                            Last_name = reader.GetString("Last_name"),
                            Company_name = reader.GetString("Company_name"),
                            Email = reader.GetString("Email"),
                            Username = reader.GetString("Username"),
                            Password = reader.GetString("Password")
                        };
                    }
                }
            }
            return user;
        }

        public bool CreateExternalUser(ExternalUser user) =>
            DbHelper.ExecuteNonQuery(
                SqlBuilder.Insert("external_user", "First_name", "Last_name", "Company_name", "Email", "Username", "Password"),
                user
            );

        public bool UpdateExternalUser(ExternalUser user) =>
            DbHelper.ExecuteNonQuery(
                SqlBuilder.Update("external_user", "Int_User_ID", "First_name", "Last_name", "Company_name", "Email", "Username", "Password"),
                 user
            );

        public bool DeleteExternalUser(int userId)
        {
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM external_user WHERE Ext_User_ID = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", userId);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }

    // ==================== CATEGORY DAL ====================
    public class CategoryDAL
    {
        private DatabaseConnection dbConn = new DatabaseConnection();

        public List<ContractBlockCategory> GetAllCategories()
        {
            List<ContractBlockCategory> categories = new List<ContractBlockCategory>();
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM contract_block_category";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new ContractBlockCategory
                        {
                            Category_name = reader.GetString("Category_name"),
                            Description = reader.GetString("Description")
                        });
                    }
                }
            }
            return categories;
        }

        public ContractBlockCategory GetCategoryByName(string categoryName)
        {
            ContractBlockCategory category = null;
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM contract_block_category WHERE Category_name = @name";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", categoryName);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        category = new ContractBlockCategory
                        {
                            Category_name = reader.GetString("Category_name"),
                            Description = reader.GetString("Description")
                        };
                    }
                }
            }
            return category;
        }

        public bool CreateCategory(ContractBlockCategory category)=>
        DbHelper.ExecuteNonQuery(
            SqlBuilder.Insert("contract_block_category", "Category_name", "Description"),
            category
        );

        public bool UpdateCategory(ContractBlockCategory category) =>
        DbHelper.ExecuteNonQuery(
            SqlBuilder.Update("contract_block_category", "Category_name", "Description"),
            category
    );

        public bool DeleteCategory(string categoryName)
        {
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM contract_block_category WHERE Category_name = @name";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", categoryName);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }

    // ==================== ORIGINAL CONTRACT BLOCK DAL ====================
    public class OriginalContractBlockDAL
    {
        private DatabaseConnection dbConn = new DatabaseConnection();

        public List<OriginalContractBlock> GetAllOriginalBlocks()
        {
            List<OriginalContractBlock> blocks = new List<OriginalContractBlock>();
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM original_contract_block";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        blocks.Add(new OriginalContractBlock
                        {
                            Org_Cont_ID = reader.GetInt32("Org_Cont_ID"),
                            Category_name = reader.GetString("Category_name"),
                            Contract_text = reader.GetString("Contract_text"),
                            Created_by = reader.GetInt32("Created_by"),
                            Created_date = reader.GetDateTime("Created_date")
                        });
                    }
                }
            }
            return blocks;
        }

        public List<OriginalContractBlock> GetOriginalBlocksByCategory(string categoryName)
        {
            List<OriginalContractBlock> blocks = new List<OriginalContractBlock>();
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM original_contract_block WHERE Category_name = @category";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@category", categoryName);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        blocks.Add(new OriginalContractBlock
                        {
                            Org_Cont_ID = reader.GetInt32("Org_Cont_ID"),
                            Category_name = reader.GetString("Category_name"),
                            Contract_text = reader.GetString("Contract_text"),
                            Created_by = reader.GetInt32("Created_by"),
                            Created_date = reader.GetDateTime("Created_date")
                        });
                    }
                }
            }
            return blocks;
        }

        public OriginalContractBlock GetOriginalBlockById(int blockId)
        {
            OriginalContractBlock block = null;
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM original_contract_block WHERE Org_Cont_ID = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", blockId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        block = new OriginalContractBlock
                        {
                            Org_Cont_ID = reader.GetInt32("Org_Cont_ID"),
                            Category_name = reader.GetString("Category_name"),
                            Contract_text = reader.GetString("Contract_text"),
                            Created_by = reader.GetInt32("Created_by"),
                            Created_date = reader.GetDateTime("Created_date")
                        };
                    }
                }
            }
            return block;
        }

        public int CreateOriginalBlock(OriginalContractBlock block)
        {
            object result = DbHelper.ExecuteScalar(
                SqlBuilder.InsertWithId("original_contract_block", "Category_name", "Contract_text", "Created_by", "Created_date"),
                block
            );
            return Convert.ToInt32(result);
        }

        public bool UpdateOriginalBlock(OriginalContractBlock block) =>
            DbHelper.ExecuteNonQuery(
                SqlBuilder.Update("original_contract_block", "Category_name", "Contract_text", "Org_Cont_ID"),
                block
            );

        public bool DeleteOriginalBlock(int blockId)
        {
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM original_contract_block WHERE Org_Cont_ID = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", blockId);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }

    // ==================== CONTRACT BLOCK DAL ====================
    public class ContractBlockDAL
    {

        private OriginalContractBlockDAL originalDAL;


        private DatabaseConnection dbConn = new DatabaseConnection();

        public ContractBlock GetContractBlockById(int blockId)
        {
            ContractBlock block = null;
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM contract_block WHERE Contract_Block_NR = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", blockId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        block = new ContractBlock
                        {
                            Contract_Block_NR = reader.GetInt32("Contract_Block_NR"),
                            Org_Cont_ID = reader.GetInt32("Org_Cont_ID"),
                            Contract_text = reader.GetString("Contract_text"),
                            New = reader.GetBoolean("New"),
                            Modified_date = reader.GetDateTime("Modified_date"),
                            Type = (BlockType)reader.GetInt32("Type"),
                            MediaContent = reader.IsDBNull(reader.GetOrdinal("MediaContent")) ? null : (byte[])reader["MediaContent"]
                        };
                    }
                }
            }
            return block;
        }

        public List<ContractBlock> GetBlocksByContract(int contractNr)
        {
            List<ContractBlock> blocks = new List<ContractBlock>();
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = @"SELECT cb.* FROM contract_block cb
                                INNER JOIN contract_blocks cbs ON cb.Contract_Block_NR = cbs.Contract_Block_NR
                                WHERE cbs.Contract_NR = @contractNr
                                ORDER BY cbs.Block_order";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@contractNr", contractNr);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        blocks.Add(new ContractBlock
                        {
                            Contract_Block_NR = reader.GetInt32("Contract_Block_NR"),
                            Org_Cont_ID = reader.GetInt32("Org_Cont_ID"),
                            Contract_text = reader.GetString("Contract_text"),
                            New = reader.GetBoolean("New"),
                            Modified_date = reader.GetDateTime("Modified_date"),
                            Type = (BlockType)reader.GetInt32("Type"),
                            MediaContent = reader.IsDBNull(reader.GetOrdinal("MediaContent")) ? null : (byte[])reader["MediaContent"]
                        });
                    }
                }
            }
            return blocks;
        }

        public int CreateContractBlock(ContractBlock block)
        {
            object result = DbHelper.ExecuteScalar(
                SqlBuilder.InsertWithId("contract_block", "Org_Cont_ID", "Contract_text", "New", "Modified_date", "Type", "MediaContent"),
                block
            );
            return Convert.ToInt32(result);
        }

        public bool UpdateContractBlock(ContractBlock block) =>
            DbHelper.ExecuteNonQuery(
                SqlBuilder.Update("contract_block", "Org_Cont_ID", "Contract_text", "New", "Modified_date", "Type", "MediaContent"),
                block
            );

        public bool DeleteContractBlock(int blockId)
        {
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM contract_block WHERE Contract_Block_NR = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", blockId);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        private void IncrementCoOccurrence(int blockA, int blockB, MySqlConnection conn)
        {
            if (blockA > blockB) // consistent ordering (A < B) to avoid mirrored duplicates
            {
                int temp = blockA;
                blockA = blockB;
                blockB = temp;
            }
            string updateQuery = @"
                INSERT INTO block_cooccurrence (Block_A_ID, Block_B_ID, Times_Used_Together)
                VALUES (@a, @b, 1)
                ON DUPLICATE KEY UPDATE Times_Used_Together = Times_Used_Together + 1;
            ";
            using (var cmd = new MySqlCommand(updateQuery, conn))
            {
                cmd.Parameters.AddWithValue("@a", blockA);
                cmd.Parameters.AddWithValue("@b", blockB);
                cmd.ExecuteNonQuery();
            }
        }
        //public bool AddBlockToContract(int contractNr, int blockNr, int blockOrder)
        //{

        //    using (MySqlConnection conn = dbConn.GetConnection())
        //    {
        //        conn.Open();
        //        var existingBlocks = new List<int>(); //
        //        string selectQuery = "SELECT Contract_Block_NR FROM contract_blocks WHERE Contract_NR = @contractNr";
        //        using (var getCmd = new MySqlCommand(selectQuery, conn))
        //        {
        //            getCmd.Parameters.AddWithValue("@contractNr", contractNr);
        //            using (var reader = getCmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    existingBlocks.Add(reader.GetInt32(0));
        //                }
        //            }
        //        }

        //        string insertQuery = "INSERT INTO contract_blocks (Contract_NR, Contract_Block_NR, Block_order) VALUES (@contractNr, @blockNr, @order)";

        //        using (var cmd = new MySqlCommand(insertQuery, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@contractNr", contractNr);
        //            cmd.Parameters.AddWithValue("@blockNr", blockNr);
        //            cmd.Parameters.AddWithValue("@order", blockOrder);

        //            if (cmd.ExecuteNonQuery() <= 0)
        //                return false;
        //        }

        //        // 3. Update co-occurrence
        //        foreach (var existingBlock in existingBlocks)
        //        {
        //            IncrementCoOccurrence(existingBlock, blockNr, conn);
        //        }

        //        return true;
        //    }
        //}


        public bool AddBlockToContract(int contractNr, int blockNr, int blockOrder)
        {
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        // 1) Optional: ensure block isn't already present
                        using (var checkCmd = new MySqlCommand(
                            "SELECT COUNT(*) FROM contract_blocks WHERE Contract_NR=@contractNr AND Contract_Block_NR=@blockNr",
                            conn, tx))
                        {
                            checkCmd.Parameters.AddWithValue("@contractNr", contractNr);
                            checkCmd.Parameters.AddWithValue("@blockNr", blockNr);
                            var exists = Convert.ToInt64(checkCmd.ExecuteScalar());
                            if (exists > 0)
                            {
                                tx.Rollback();
                                return false; // already present: change behavior if desired
                            }
                        }

                        // 2) Insert the block into contract_blocks
                        using (var insertCmd = new MySqlCommand(
                            "INSERT INTO contract_blocks (Contract_NR, Contract_Block_NR, Block_order) VALUES (@contractNr, @blockNr, @order)",
                            conn, tx))
                        {
                            insertCmd.Parameters.AddWithValue("@contractNr", contractNr);
                            insertCmd.Parameters.AddWithValue("@blockNr", blockNr);
                            insertCmd.Parameters.AddWithValue("@order", blockOrder);
                            insertCmd.ExecuteNonQuery();
                        }

                        // 3) Read other blocks currently in this contract (excluding the new one)
                        var otherBlocks = new List<int>();
                        using (var selectCmd = new MySqlCommand(
                            "SELECT Contract_Block_NR FROM contract_blocks WHERE Contract_NR=@contractNr AND Contract_Block_NR <> @blockNr",
                            conn, tx))
                        {
                            selectCmd.Parameters.AddWithValue("@contractNr", contractNr);
                            selectCmd.Parameters.AddWithValue("@blockNr", blockNr);
                            using (var reader = selectCmd.ExecuteReader())
                            {
                                while (reader.Read())
                                    otherBlocks.Add(reader.GetInt32(0));
                            }
                        }

                        // 4) For each other block, increment the canonical pair (min,max)
                        using (var upsertCmd = new MySqlCommand(
                            "INSERT INTO block_cooccurrence (Block_A_ID, Block_B_ID, Times_Used_Together) " +
                            "VALUES (@a, @b, 1) " +
                            "ON DUPLICATE KEY UPDATE Times_Used_Together = Times_Used_Together + 1",
                            conn, tx))
                        {
                            var pa = upsertCmd.Parameters.Add("@a", MySqlDbType.Int32);
                            var pb = upsertCmd.Parameters.Add("@b", MySqlDbType.Int32);

                            foreach (var other in otherBlocks)
                            {
                                if (other == blockNr) continue;
                                int a = Math.Min(blockNr, other);
                                int b = Math.Max(blockNr, other);

                                pa.Value = a;
                                pb.Value = b;
                                upsertCmd.ExecuteNonQuery();
                            }
                        }

                        tx.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        try { tx.Rollback(); } catch { /* ignore rollback errors */ }
                        // TODO: log ex
                        return false;
                    }
                }
            }
        }


        public List<BlockRecommendation> GetRecommendationsForContract(int contractNr, int take = 5)
        {
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();

                // Get all blocks currently in the contract
                var currentBlocks = new List<int>();
                using (var cmd = new MySqlCommand(
                    "SELECT Contract_Block_NR FROM contract_blocks WHERE Contract_NR = @contractNr",
                    conn))
                {
                    cmd.Parameters.AddWithValue("@contractNr", contractNr);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            currentBlocks.Add(reader.GetInt32(0));
                    }
                }

                if (currentBlocks.Count == 0)
                    return new List<BlockRecommendation>();

                // Build the SQL query with parameterized IN clause
                string blockList = string.Join(",", currentBlocks);

                // Find blocks that co-occur with current blocks
                // Join with original_contract_block to get text
                string sql = $@"
            SELECT 
                CASE 
                    WHEN bc.Block_A_ID IN ({blockList}) THEN bc.Block_B_ID 
                    ELSE bc.Block_A_ID 
                END AS RecommendedBlockId,
                SUM(bc.Times_Used_Together) AS Score,
                ocb.Contract_text,
                ocb.Category_name
            FROM block_cooccurrence bc
            INNER JOIN contract_block cb ON 
                (CASE 
                    WHEN bc.Block_A_ID IN ({blockList}) THEN bc.Block_B_ID 
                    ELSE bc.Block_A_ID 
                END) = cb.Contract_Block_NR
            INNER JOIN original_contract_block ocb ON cb.Org_Cont_ID = ocb.Org_Cont_ID
            WHERE (bc.Block_A_ID IN ({blockList}) OR bc.Block_B_ID IN ({blockList}))
            GROUP BY RecommendedBlockId, ocb.Contract_text, ocb.Category_name
            HAVING RecommendedBlockId NOT IN ({blockList})
            ORDER BY Score DESC
            LIMIT @take";

                var recommendations = new List<BlockRecommendation>();
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@take", take);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            recommendations.Add(new BlockRecommendation
                            {
                                BlockId = reader.GetInt32("RecommendedBlockId"),
                                Score = reader.GetInt32("Score"),
                                BlockText = reader.GetString("Contract_text"),
                                Category = reader.IsDBNull(reader.GetOrdinal("Category_name"))
                                    ? ""
                                    : reader.GetString("Category_name")
                            });
                        }
                    }
                }

                return recommendations;
            }
        }

        // BlockRecommendation class
        public class BlockRecommendation
        {
            public int BlockId { get; set; }
            public int Score { get; set; }
            public string BlockText { get; set; }
            public string Category { get; set; }
        }


        public bool RemoveBlockFromContract(int contractNr, int blockNr)
        {
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM contract_blocks WHERE Contract_NR = @contractNr AND Contract_Block_NR = @blockNr";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@contractNr", contractNr);
                cmd.Parameters.AddWithValue("@blockNr", blockNr);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<ContractBlock> GetChildBlocks(int parentBlockNr)
        {
            List<ContractBlock> children = new List<ContractBlock>();
            using (var conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM contract_block WHERE Parent_Block_NR = @parent";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@parent", parentBlockNr);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        children.Add(MapReaderToContractBlock(reader));
                    }
                }
            }
            return children;
        }

        public ContractBlock GetBlockTree(int blockNr)
        {
            var block = GetContractBlockById(blockNr);
            if (block != null)
            {
                block.ChildBlocks = GetChildBlocks(block.Contract_Block_NR)
                                    .Select(b => GetBlockTree(b.Contract_Block_NR))
                                    .ToList();
            }
            return block;
        }

        private ContractBlock MapReaderToContractBlock(MySqlDataReader reader)
        {
            return new ContractBlock
            {
                Contract_Block_NR = reader.GetInt32("Contract_Block_NR"),
                Org_Cont_ID = reader.GetInt32("Org_Cont_ID"),
                Contract_text = reader.GetString("Contract_text"),
                New = reader.GetBoolean("New"),
                Modified_date = reader.GetDateTime("Modified_date"),
                Type = (BlockType)Enum.Parse(typeof(BlockType), reader.GetString("Type")),
                MediaContent = reader.IsDBNull(reader.GetOrdinal("MediaContent")) ? null : (byte[])reader["MediaContent"],
                Parent_Block_NR = reader.IsDBNull(reader.GetOrdinal("Parent_Block_NR")) ? null : (int?)reader.GetInt32("Parent_Block_NR"),
                ChildBlocks = new List<ContractBlock>()
            };
        }
    }

    // =========== CONTRACT BLOCK REFERENCE DAL =============

    public class ContractBlockReferenceDAL
    {
        private DatabaseConnection dbConn = new DatabaseConnection();

        public bool AddReference(int contractBlockNr, int referencedBlockNr, int order)
        {
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO contract_block_references (Contract_Block_NR, Referenced_Block_NR, Reference_Order) VALUES (@block, @ref, @order)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@block", contractBlockNr);
                cmd.Parameters.AddWithValue("@ref", referencedBlockNr);
                cmd.Parameters.AddWithValue("@order", order);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<ContractBlockReference> GetReferencesByBlock(int contractBlockNr)
        {
            List<ContractBlockReference> references = new List<ContractBlockReference>();
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM contract_block_references WHERE Contract_Block_NR = @block ORDER BY Reference_Order";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@block", contractBlockNr);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        references.Add(new ContractBlockReference
                        {
                            Reference_ID = reader.GetInt32("Reference_ID"),
                            Contract_Block_NR = reader.GetInt32("Contract_Block_NR"),
                            Referenced_Block_NR = reader.GetInt32("Referenced_Block_NR"),
                            Reference_Order = reader.GetInt32("Reference_Order")
                        });
                    }
                }
            }
            return references;
        }
    }


    // ==================== CONTRACT DAL ====================
    public class ContractDAL
    {
        private DatabaseConnection dbConn = new DatabaseConnection();

        public List<Contract> GetAllContracts()
        {
            List<Contract> contracts = new List<Contract>();
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM contract";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        contracts.Add(new Contract
                        {
                            Contract_NR = reader.GetInt32("Contract_NR"),
                            Company_name = reader.GetString("Company_name"),
                            The_Creator = reader.GetInt32("The_Creator"),
                            Created_date = reader.GetDateTime("Created_date"),
                            Approved = reader.GetBoolean("Approved"),
                            Sent_to_external = reader.GetBoolean("Sent_to_external")
                        });
                    }
                }
            }
            return contracts;
        }

        public Contract GetContractById(int contractId)
        {
            Contract contract = null;
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM contract WHERE Contract_NR = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", contractId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        contract = new Contract
                        {
                            Contract_NR = reader.GetInt32("Contract_NR"),
                            Company_name = reader.GetString("Company_name"),
                            The_Creator = reader.GetInt32("The_Creator"),
                            Created_date = reader.GetDateTime("Created_date"),
                            Approved = reader.GetBoolean("Approved"),
                            Sent_to_external = reader.GetBoolean("Sent_to_external")
                        };
                    }
                }
            }
            return contract;
        }

        public List<Contract> GetContractsByCreator(int creatorId)
        {
            List<Contract> contracts = new List<Contract>();
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM contract WHERE The_Creator = @creator";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@creator", creatorId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        contracts.Add(new Contract
                        {
                            Contract_NR = reader.GetInt32("Contract_NR"),
                            Company_name = reader.GetString("Company_name"),
                            The_Creator = reader.GetInt32("The_Creator"),
                            Created_date = reader.GetDateTime("Created_date"),
                            Approved = reader.GetBoolean("Approved"),
                            Sent_to_external = reader.GetBoolean("Sent_to_external")
                        });
                    }
                }
            }
            return contracts;
        }

        public int CreateContract(Contract contract)
        {
            object result = DbHelper.ExecuteScalar(
                SqlBuilder.InsertWithId("contract", "Company_name", "The_Creator", "Created_date", "Approved", "Sent_to_external"),
                contract
            );
            return Convert.ToInt32(result);
        }

        public bool UpdateContract(Contract contract) =>
            DbHelper.ExecuteNonQuery(
                SqlBuilder.Update("contract", "Contract_NR", "Company_name", "Approved", "Sent_to_external"),
                contract
            );

        public bool DeleteContract(int contractId)
        {
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM contract WHERE Contract_NR = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", contractId);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }

    // ==================== CONTRACT STAKEHOLDER DAL ====================
    public class ContractStakeholderDAL
    {
        private DatabaseConnection dbConn = new DatabaseConnection();

        public List<ContractStakeholder> GetStakeholdersByContract(int contractNr)
        {
            List<ContractStakeholder> stakeholders = new List<ContractStakeholder>();
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM contract_stakeholders WHERE Contract_NR = @contractNr";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@contractNr", contractNr);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        stakeholders.Add(new ContractStakeholder
                        {
                            Contract_NR = reader.GetInt32("Contract_NR"),
                            Int_User_ID = reader.GetInt32("Int_User_ID"),
                            Has_approval_rights = reader.GetBoolean("Has_approval_rights"),
                            Approved = reader.GetBoolean("Approved"),
                            Approved_date = reader.IsDBNull(reader.GetOrdinal("Approved_date")) ? null : (DateTime?)reader.GetDateTime("Approved_date")
                        });
                    }
                }
            }
            return stakeholders;
        }

        public List<Contract> GetContractsByStakeholder(int userId)
        {
            List<Contract> contracts = new List<Contract>();
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = @"SELECT c.* FROM contract c
                                INNER JOIN contract_stakeholders cs ON c.Contract_NR = cs.Contract_NR
                                WHERE cs.Int_User_ID = @userId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        contracts.Add(new Contract
                        {
                            Contract_NR = reader.GetInt32("Contract_NR"),
                            Company_name = reader.GetString("Company_name"),
                            The_Creator = reader.GetInt32("The_Creator"),
                            Created_date = reader.GetDateTime("Created_date"),
                            Approved = reader.GetBoolean("Approved"),
                            Sent_to_external = reader.GetBoolean("Sent_to_external")
                        });
                    }
                }
            }
            return contracts;
        }

        public bool AddStakeholder(ContractStakeholder stakeholder) =>
            DbHelper.ExecuteNonQuery(
                SqlBuilder.Insert("contract_stakeholders", "Contract_NR", "Int_User_ID", "Has_approval_rights", "Approved", "Approved_date"),
                stakeholder
            );

        public bool UpdateStakeholder(ContractStakeholder stakeholder) =>
            DbHelper.ExecuteNonQuery(
                SqlBuilder.Update("contract_stakeholders", "Contract_NR", "Int_User_ID", "Has_approval_rights", "Approved", "Approved_date"),
            stakeholder
            );

        public bool RemoveStakeholder(int contractNr, int userId)
        {
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM contract_stakeholders WHERE Contract_NR = @contractNr AND Int_User_ID = @userId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@contractNr", contractNr);
                cmd.Parameters.AddWithValue("@userId", userId);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool IsContractFullyApproved(int contractNr)
        {
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM contract_stakeholders WHERE Contract_NR = @contractNr AND Has_approval_rights = 1 AND Approved = 0";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@contractNr", contractNr);

                int unapprovedCount = Convert.ToInt32(cmd.ExecuteScalar());
                return unapprovedCount == 0;
            }
        }
    }

    // ==================== CONTRACT EXTERNAL USERS DAL ====================
    public class ContractExternalUserDAL
    {
        private DatabaseConnection dbConn = new DatabaseConnection();

        public List<ExternalUser> GetExternalUsersByContract(int contractNr)
        {
            List<ExternalUser> users = new List<ExternalUser>();
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = @"SELECT eu.* FROM external_user eu
                                INNER JOIN contract_external_users ceu ON eu.Ext_User_ID = ceu.Ext_User_ID
                                WHERE ceu.Contract_NR = @contractNr";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@contractNr", contractNr);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new ExternalUser
                        {
                            Ext_User_ID = reader.GetInt32("Ext_User_ID"),
                            First_name = reader.GetString("First_name"),
                            Last_name = reader.GetString("Last_name"),
                            Company_name = reader.GetString("Company_name"),
                            Email = reader.GetString("Email"),
                            Username = reader.GetString("Username"),
                            Password = reader.GetString("Password")
                        });
                    }
                }
            }
            return users;
        }

        public List<Contract> GetContractsByExternalUser(int extUserId)
        {
            List<Contract> contracts = new List<Contract>();
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = @"SELECT c.* FROM contract c
                                INNER JOIN contract_external_users ceu ON c.Contract_NR = ceu.Contract_NR
                                WHERE ceu.Ext_User_ID = @userId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", extUserId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        contracts.Add(new Contract
                        {
                            Contract_NR = reader.GetInt32("Contract_NR"),
                            Company_name = reader.GetString("Company_name"),
                            The_Creator = reader.GetInt32("The_Creator"),
                            Created_date = reader.GetDateTime("Created_date"),
                            Approved = reader.GetBoolean("Approved"),
                            Sent_to_external = reader.GetBoolean("Sent_to_external")
                        });
                    }
                }
            }
            return contracts;
        }

        public bool InviteExternalUser(int contractNr, int extUserId, DateTime invitedDate)
        {
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO contract_external_users (Contract_NR, Ext_User_ID, Invited_date) VALUES (@contractNr, @userId, @date)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@contractNr", contractNr);
                cmd.Parameters.AddWithValue("@userId", extUserId);
                cmd.Parameters.AddWithValue("@date", invitedDate);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool RemoveExternalUser(int contractNr, int extUserId)
        {
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM contract_external_users WHERE Contract_NR = @contractNr AND Ext_User_ID = @userId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@contractNr", contractNr);
                cmd.Parameters.AddWithValue("@userId", extUserId);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }

    // ==================== COMMENT DAL ====================
    public class CommentDAL
    {
        private DatabaseConnection dbConn = new DatabaseConnection();

        public List<Comment> GetCommentsByContract(int contractNr)
        {
            List<Comment> comments = new List<Comment>();
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM comments WHERE Contract_NR = @contractNr ORDER BY Comment_date";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@contractNr", contractNr);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comments.Add(new Comment
                        {
                            Comment_ID = reader.GetInt32("Comment_ID"),
                            Contract_NR = reader.GetInt32("Contract_NR"),
                            Contract_Block_NR = reader.IsDBNull(reader.GetOrdinal("Contract_Block_NR")) ? null : (int?)reader.GetInt32("Contract_Block_NR"),
                            User_ID = reader.GetInt32("User_ID"),
                            User_type = reader.GetString("User_type"),
                            Comment_text = reader.GetString("Comment_text"),
                            Comment_date = reader.GetDateTime("Comment_date")
                        });
                    }
                }
            }
            return comments;
        }

        public List<Comment> GetCommentsByBlock(int contractNr, int blockNr)
        {
            List<Comment> comments = new List<Comment>();
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM comments WHERE Contract_NR = @contractNr AND Contract_Block_NR = @blockNr ORDER BY Comment_date";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@contractNr", contractNr);
                cmd.Parameters.AddWithValue("@blockNr", blockNr);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comments.Add(new Comment
                        {
                            Comment_ID = reader.GetInt32("Comment_ID"),
                            Contract_NR = reader.GetInt32("Contract_NR"),
                            Contract_Block_NR = reader.IsDBNull(reader.GetOrdinal("Contract_Block_NR")) ? null : (int?)reader.GetInt32("Contract_Block_NR"),
                            User_ID = reader.GetInt32("User_ID"),
                            User_type = reader.GetString("User_type"),
                            Comment_text = reader.GetString("Comment_text"),
                            Comment_date = reader.GetDateTime("Comment_date")
                        });
                    }
                }
            }
            return comments;
        }

        public List<Comment> GetInternalComments(int contractNr)
        {
            List<Comment> comments = new List<Comment>();
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM comments WHERE Contract_NR = @contractNr AND User_type = 'internal' ORDER BY Comment_date";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@contractNr", contractNr);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comments.Add(new Comment
                        {
                            Comment_ID = reader.GetInt32("Comment_ID"),
                            Contract_NR = reader.GetInt32("Contract_NR"),
                            Contract_Block_NR = reader.IsDBNull(reader.GetOrdinal("Contract_Block_NR")) ? null : (int?)reader.GetInt32("Contract_Block_NR"),
                            User_ID = reader.GetInt32("User_ID"),
                            User_type = reader.GetString("User_type"),
                            Comment_text = reader.GetString("Comment_text"),
                            Comment_date = reader.GetDateTime("Comment_date")
                        });
                    }
                }
            }
            return comments;
        }

        public List<Comment> GetExternalCommentsByUser(int contractNr, int extUserId)
        {
            List<Comment> comments = new List<Comment>();
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM comments WHERE Contract_NR = @contractNr AND User_type = 'external' AND User_ID = @userId ORDER BY Comment_date";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@contractNr", contractNr);
                cmd.Parameters.AddWithValue("@userId", extUserId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comments.Add(new Comment
                        {
                            Comment_ID = reader.GetInt32("Comment_ID"),
                            Contract_NR = reader.GetInt32("Contract_NR"),
                            Contract_Block_NR = reader.IsDBNull(reader.GetOrdinal("Contract_Block_NR")) ? null : (int?)reader.GetInt32("Contract_Block_NR"),
                            User_ID = reader.GetInt32("User_ID"),
                            User_type = reader.GetString("User_type"),
                            Comment_text = reader.GetString("Comment_text"),
                            Comment_date = reader.GetDateTime("Comment_date")
                        });
                    }
                }
            }
            return comments;
        }

        public int CreateComment(Comment comment)
        {
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO comments (Contract_NR, Contract_Block_NR, User_ID, User_type, Comment_text, Comment_date) VALUES (@contractNr, @blockNr, @userId, @userType, @text, @date); SELECT LAST_INSERT_ID();";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@contractNr", comment.Contract_NR);
                cmd.Parameters.AddWithValue("@blockNr", comment.Contract_Block_NR.HasValue ? (object)comment.Contract_Block_NR.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@userId", comment.User_ID);
                cmd.Parameters.AddWithValue("@userType", comment.User_type);
                cmd.Parameters.AddWithValue("@text", comment.Comment_text);
                cmd.Parameters.AddWithValue("@date", comment.Comment_date);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public bool DeleteComment(int commentId)
        {
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM comments WHERE Comment_ID = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", commentId);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

    }

}
