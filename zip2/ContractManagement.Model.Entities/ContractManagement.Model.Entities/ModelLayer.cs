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

    //enum, joka kuvaa blokin tyypin (teksti, kuva tai muu)
    public enum BlockType
    {
        Text,
        Image,
        Other
    }

    //admin-käyttäjää kuvaava tietomalli
    public class Administrator
    {
        //admin ID
        public int Administrator_ID { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    //sisästä käyttäjää kuvaava tietomalli
    public class InternalUser
    {
        public int Int_User_ID { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    //ulkosta käyttäjää kuvaava tietomalli
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

    //sopimusblokin kategorian tietomalli
    public class ContractBlockCategory
    {
        public string Category_name { get; set; }
        public string Description { get; set; }
    }

    //alkuperänen (mallipohjanen) sopimusblokki
    public class OriginalContractBlock
    {
        public int Org_Cont_ID { get; set; }
        public string Category_name { get; set; }
        public string Contract_text { get; set; }

        //blokin luonut käyttäjä
        public int Created_by { get; set; }
        public DateTime Created_date { get; set; }
    }

    //sopimukseen lisätty yksittäinen blokki
    public class ContractBlock
    {
        //sopimuksen blokin ID
        public int Contract_Block_NR { get; set; }

        //viittaus alkuperäseen blokkiin (jos käytetty)
        public int Org_Cont_ID { get; set; }

        //blokin sisältö
        public string Contract_text { get; set; }

        //onko blokki uusi verrattuna alkuperäseen
        public bool New { get; set; }

        //muokkauspäivämäärä
        public DateTime Modified_date { get; set; }

        //luontipäivämäärä
        public DateTime Created_date { get; set; }

        //kuka loi blokin
        public int Created_by { get; set; }

        //blokin tyyppi (teksti, kuva,...)
        public BlockType Type { get; set; }

        //media (esim. kuvat) binääridatana
        public byte[] MediaContent { get; set; }

        //mahdollinen parent-blokin ID (hierarkiaa varten)
        public int? Parent_Block_NR { get; set; }

        //blokin aliblokit
        public List<ContractBlock> ChildBlocks { get; set; } = new List<ContractBlock>();

        //lista muista blokeista joihin tämä viittaa
        public List<int> References { get; set; } = new List<int>();
    }

    //blokkien välisiä viittauksii kuvaava malli
    public class ContractBlockReference
    {
        public int Reference_ID { get; set; }

        //blokki josta viitataan
        public int Contract_Block_NR { get; set; }

        //viitattu blokki
        public int Referenced_Block_NR { get; set; }

        //viittauksen järjestysnumero
        public int Reference_Order { get; set; }
    }

    //sopimusta kuvaava tietomalli
    public class Contract
    {
        //sopimuksen yksilöivä ID
        public int Contract_NR { get; set; }

        //yrityksen nimi
        public string Company_name { get; set; }

        //sopimuksen luonu käyttäjä
        public int The_Creator { get; set; }

        //luontipäivämäärä
        public DateTime Created_date { get; set; }

        //onko sopimus hyväksytty
        public bool Approved { get; set; }

        //onko sopimus lähetetty ulkosille käyttäjille
        public bool Sent_to_external { get; set; }
    }

    //sisäsen käyttäjän roolit ja oikeudet sopimuksessa
    public class ContractStakeholder
    {
        //sopimuksen ID
        public int Contract_NR { get; set; }

        //sisäsen käyttäjän ID
        public int Int_User_ID { get; set; }

        //onko käyttäjällä hyväksyntäoikeus
        public bool Has_approval_rights { get; set; }

        //onko käyttäjä hyväksyny sopimuksen
        public bool Approved { get; set; }

        //hyväksymisen päivämäärä
        public DateTime? Approved_date { get; set; }
    }

    //ulkosten käyttäjien osallistuminen sopimukseen
    public class ContractExternalUser
    {
        //sopimuksen ID
        public int Contract_NR { get; set; }

        //ulkosen käyttäjän ID
        public int Ext_User_ID { get; set; }

        //kutsuajan päivämäärä
        public DateTime Invited_date { get; set; }
    }

    //kommentteja kuvaava tietomalli
    public class Comment
    {
        public int Comment_ID { get; set; }

        //sopimus johon kommentti liittyy
        public int Contract_NR { get; set; }

        //mahdollinen sopimusblokki johon kommentti kohdistuu
        public int? Contract_Block_NR { get; set; }

        //kommentoijan käyttäjä-ID
        public int User_ID { get; set; }

        //käyttäjätyyppi (internal/external)
        public string User_type { get; set; } // 'internal' or 'external'

        //kommentin teksti
        public string Comment_text { get; set; }

        //kommentin päivämäärä ja aika
        public DateTime Comment_date { get; set; }
    }
}

// ==================== DATABASE ACCESS CLASSES ====================

namespace ContractManagement.Model.DAL
{
    using System.Data.SqlClient;
    using System.Reflection;
    using ContractManagement.Model.Entities;

    //vastaa MySQL-tietokantayhteydestä
    //tarjoaa vaan yhen tehtävän -> palauttaa valmiin MySQL-olion
    public class DatabaseConnection
    {
        //tietokantayhteyden connection string
        private string connectionString = "server=127.0.0.1;user=root;database=kettera;password=";

        //luo ja palauttaa uuden MySQL-yhteysolion
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }

    // ==================== ADMINISTRATOR DAL ====================

    //data access layer admin-käyttäjille
    //sisältää CRUD-toiminnot ja adminien poistoon liittyvän äänestyslogiikan
    public class AdministratorDAL
    {
        //tietokantayhteysolio
        private DatabaseConnection dbConn = new DatabaseConnection();

        //hakee adminin käyttäjänimen perusteella
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

        //hakee kaikki järjestelmän adminit
        public List<Administrator> GetAllAdministrators()
        {
            List<Administrator> admins = new List<Administrator>();

            using (var conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM administrator";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        admins.Add(new Administrator
                        {
                            Administrator_ID = reader.GetInt32("Administrator_ID"),
                            First_name = reader.GetString("First_name"),
                            Last_name = reader.GetString("Last_name"),
                            Username = reader.GetString("Username"),
                            Password = reader.GetString("Password")
                        });
                    }
                }
            }

            return admins;
        }


        //luo uuden adminin käyttäen generoitua SQL-querya ja DbHelperiä
        public bool CreateAdministrator(Administrator admin) =>
            DbHelper.ExecuteNonQuery(
            SqlBuilder.Insert("administrator", "First_name", "Last_name", "Username", "Password", "Email"),
            admin
         );

        //lisää äänestyksen adminin poistamiseen
        //INSERT IGNORE estää tuplasti äänestämisen saman käyttäjän toimesta
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

        //tarkistaa kuinka moni admin on äänestänyt poistamisen puolesta
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

        //poistaa adminin jos ääniä tarpeeks
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
                    //tyhjentää äänestysdatan
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

    //data access layer sisäsille käyttäjille
    //hoitaa kirjautumisen, uusien käyttäjien luonnin ja käyttäjätietojen lukemisen
    public class InternalUserDAL
    {
        //yhdistää tietokantaan
        private DatabaseConnection dbConn = new DatabaseConnection();

        //hakee kaikki sisäset käyttäjät listana
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

        //hakee sisäsen käyttäjän ID:n perusteella
        //palauttaa InternalUser-objektin tai null jos ei löydy
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

        //hakee sisäsen käyttäjän käyttäjänimen perusteella
        //palauttaa InternalUser-objektin tai null jos ei löydy
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

        //luo uuden sisäsen käyttäjän tietokantaan
        //palauttaa true jos onnistu, false jos epäonnistu
        public bool CreateInternalUser(InternalUser user) =>
            DbHelper.ExecuteNonQuery(
                SqlBuilder.Insert("internal_user", "First_name", "Last_name", "Username", "Password", "Email"),
                user
            );

        //päivittää olemassaolevan sisäsen käyttäjän tiedot
        //palauttaa true jos onnistu
        public bool UpdateInternalUser(InternalUser user) =>
            DbHelper.ExecuteNonQuery(
                SqlBuilder.Update("internal_user", "Int_User_ID", "First_name", "Last_name", "Username", "Password", "Email"),
                user
            );

        //poistaa sisäsen käyttäjän ID:n perusteella
        //palauttaa true jos rivi poistettiin
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

    //data access layer ulkosille käyttäjille
    //sisältää kirjautumisen, haun, luonnin, päivityksen ja poistamisen
    public class ExternalUserDAL
    {
        //yhteydenhallinnan apuluokka
        private DatabaseConnection dbConn = new DatabaseConnection();

        //hakee kaikki ulkoset käyttäjät
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

        //hakee ulkosen käyttäjän ID:n perusteella
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

        //hakee ulkosen käyttäjän käyttäjänimellä
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

        //luo uuden ulkosen käyttäjän
        public bool CreateExternalUser(ExternalUser user) =>
            DbHelper.ExecuteNonQuery(
                SqlBuilder.Insert("external_user", "First_name", "Last_name", "Company_name", "Email", "Username", "Password"),
                user
            );

        //päivittää ulkosen käyttäjän tiedot
        public bool UpdateExternalUser(ExternalUser user) =>
            DbHelper.ExecuteNonQuery(
                SqlBuilder.Update("external_user", "Int_User_ID", "First_name", "Last_name", "Company_name", "Email", "Username", "Password"),
                 user
            );

        //poistaa ulkosen käyttäjän
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

    //data access layer sopimusblokkien kategorioille
    //kategoriat kuvaa blokkien tyyppiä, kuten yleiset ehdot, maksuehdot jne.
    public class CategoryDAL
    {
        private DatabaseConnection dbConn = new DatabaseConnection();

        //hakee kaikki kategoriat tietokannasta
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

        //hakee yksittäisen kategorian nimen perusteella
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

        //luo uuden kategorian tietokantaan
        public bool CreateCategory(ContractBlockCategory category) =>
        DbHelper.ExecuteNonQuery(
            SqlBuilder.Insert("contract_block_category", "Category_name", "Description"),
            category
        );

        //päivittää kategorian tiedot
        public bool UpdateCategory(ContractBlockCategory category) =>
        DbHelper.ExecuteNonQuery(
            SqlBuilder.Update("contract_block_category", "Category_name", "Description"),
            category
    );

        //poistaa kategorian
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

    //DAL alkuperäisille sopimusblokeille
    //tämä huolehtii tietokantakyselyistä alkuperäisten blokkien osalta
    public class OriginalContractBlockDAL
    {
        private DatabaseConnection dbConn = new DatabaseConnection();

        //hakee kaikki alkuperäiset sopimusblokit
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

        //hakee kaikki blokit tietystä kategoriasta
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

        //hakee yksittäisen blokin ID:n perusteella
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
            try
            {
                using (MySqlConnection conn = dbConn.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO original_contract_block 
                (Category_name, Contract_text, Created_by, Created_date, Type, MediaContent) 
                VALUES (@category, @text, @creator, @created, @type, @media);
                SELECT LAST_INSERT_ID();";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@category", block.Category_name);
                    cmd.Parameters.AddWithValue("@text", block.Contract_text);
                    cmd.Parameters.AddWithValue("@creator", block.Created_by);
                    cmd.Parameters.AddWithValue("@created", block.Created_date);
                    cmd.Parameters.AddWithValue("@type", 0); // Default to Text type
                    cmd.Parameters.AddWithValue("@media", DBNull.Value);

                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
            catch (Exception)
            {
                return 0; //palautetaan 0 virheen tapahtuessa
            }
        }

        //päivittää olemassa olevan blokin tiedot
        public bool UpdateOriginalBlock(OriginalContractBlock block)
        {
            try
            {
                using (MySqlConnection conn = dbConn.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE original_contract_block 
                           SET Category_name = @category, 
                               Contract_text = @text 
                           WHERE Org_Cont_ID = @id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@category", block.Category_name);
                    cmd.Parameters.AddWithValue("@text", block.Contract_text);
                    cmd.Parameters.AddWithValue("@id", block.Org_Cont_ID);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        //poistaa blokin ID:n perusteella
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

    //DAL sopimusblokeille
    //tämä huolehtii tietokannan käsittelystä yksittäisten sopimusblokkien osalta
    public class ContractBlockDAL
    {

        private OriginalContractBlockDAL originalDAL;


        private DatabaseConnection dbConn = new DatabaseConnection();

        //hakee sopimusblokin ID:n perusteella
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

        //hakee kaikki blokit tietystä sopimuksesta
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

        //luo uuden sopimusblokin ja palauttaa sen ID:n
        public int CreateContractBlock(ContractBlock block)
        {
            try
            {
                using (MySqlConnection conn = dbConn.GetConnection())
                {
                    conn.Open();

                    string query = @"INSERT INTO contract_block 
                (Org_Cont_ID, Contract_text, New, Modified_date, Type, MediaContent) 
                VALUES (@orgId, @text, @new, @modified, @type, @media);
                SELECT LAST_INSERT_ID();";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@orgId", block.Org_Cont_ID);
                    cmd.Parameters.AddWithValue("@text", block.Contract_text ?? "");
                    cmd.Parameters.AddWithValue("@new", block.New);
                    cmd.Parameters.AddWithValue("@modified", block.Modified_date);
                    cmd.Parameters.AddWithValue("@type", (int)block.Type);
                    cmd.Parameters.AddWithValue("@media", block.MediaContent ?? (object)DBNull.Value);

                    object result = cmd.ExecuteScalar();
                    int newId = result != null ? Convert.ToInt32(result) : 0;

                    Console.WriteLine("DEBUG: CreateContractBlock returned ID: {0}", newId);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DEBUG: Exception in CreateContractBlock: {0}", ex.Message);
                return 0;
            }
        }



        //päivittää olemassa olevan sopimusblokin
        public bool UpdateContractBlock(ContractBlock block)
        {
            try
            {
                using (MySqlConnection conn = dbConn.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE contract_block SET 
                Contract_text = @text,
                New = @new,
                Modified_date = @modified,
                Type = @type,
                MediaContent = @media
                WHERE Contract_Block_NR = @id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", block.Contract_Block_NR);
                    cmd.Parameters.AddWithValue("@text", block.Contract_text);
                    cmd.Parameters.AddWithValue("@new", block.New);
                    cmd.Parameters.AddWithValue("@modified", block.Modified_date);
                    cmd.Parameters.AddWithValue("@type", (int)block.Type);
                    cmd.Parameters.AddWithValue("@media", block.MediaContent ?? (object)DBNull.Value);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        //poistaa sopimusblokin
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

        //päivittää blokkien esiintymisen (blokit A ja B esiintyvät yhdessä)
        private void IncrementCoOccurrence(int blockA, int blockB, MySqlConnection conn)
        {
            if (blockA > blockB) //järjestää aina A < B
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


        //lisää blokin sopimukseen tietyllä järjestyksellä
        public bool AddBlockToContract(int contractNr, int blockNr, int blockOrder)
        {
            try
            {
                using (MySqlConnection conn = dbConn.GetConnection())
                {
                    conn.Open();
                    using (var tx = conn.BeginTransaction()) //käyttää transaktiota monivaiheiseen operaatioon
                    {
                        try
                        {
                            // 1)tarkistaa onko blokki jo sopimuksessa
                            using (var checkCmd = new MySqlCommand(
                                "SELECT COUNT(*) FROM contract_blocks WHERE Contract_NR=@contractNr AND Contract_Block_NR=@blockNr",
                                conn, tx))
                            {
                                checkCmd.Parameters.AddWithValue("@contractNr", contractNr);
                                checkCmd.Parameters.AddWithValue("@blockNr", blockNr);
                                var exists = Convert.ToInt64(checkCmd.ExecuteScalar());
                                if (exists > 0)
                                {
                                    Console.WriteLine("DEBUG: Block {0} already in contract {1}", blockNr, contractNr);
                                    tx.Rollback();
                                    return false;
                                }
                            }

                            // 2)lisää blokin contract_blocks-tauluun
                            using (var insertCmd = new MySqlCommand(
                                "INSERT INTO contract_blocks (Contract_NR, Contract_Block_NR, Block_order) VALUES (@contractNr, @blockNr, @order)",
                                conn, tx))
                            {
                                insertCmd.Parameters.AddWithValue("@contractNr", contractNr);
                                insertCmd.Parameters.AddWithValue("@blockNr", blockNr);
                                insertCmd.Parameters.AddWithValue("@order", blockOrder);
                                int inserted = insertCmd.ExecuteNonQuery();

                                Console.WriteLine("DEBUG: Inserted {0} rows into contract_blocks", inserted);

                                if (inserted == 0)
                                {
                                    tx.Rollback();
                                    return false;
                                }
                            }

                            // 3)hakee muut sopimuksen blokit jotta voidaan päivittää co-occurrence
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

                            // 4)päivittää esiintymäyhteydet (co-occurrence) jokaselle parille
                            if (otherBlocks.Count > 0)
                            {
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
                            }

                            tx.Commit(); //committaa kaikki muutokset
                            Console.WriteLine("DEBUG: Successfully added block {0} to contract {1}", blockNr, contractNr);
                            return true;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("DEBUG: Transaction exception: {0}", ex.Message);
                            try { tx.Rollback(); } catch { }
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DEBUG: Exception in AddBlockToContract DAL: {0}", ex.Message);
                return false;
            }
        }


        //hakee suositeltavat blokit sopimukselle co-occurrence-logiikan perusteella
        public List<BlockRecommendation> GetRecommendationsForContract(int contractNr, int take = 5)
        {
            try
            {
                using (MySqlConnection conn = dbConn.GetConnection())
                {
                    conn.Open();

                    //hakee kaikki nykyset blokit sopimuksesta
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

                    string blockList = string.Join(",", currentBlocks);

                    //rakentaa SQL, joka laskee co-occurrence pisteet ja suositukset
                    string sql = string.Format(@"
                SELECT 
                    CASE 
                        WHEN bc.Block_A_ID IN ({0}) THEN bc.Block_B_ID 
                        ELSE bc.Block_A_ID 
                    END AS RecommendedBlockId,
                    SUM(bc.Times_Used_Together) AS Score,
                    ocb.Contract_text,
                    ocb.Category_name
                FROM block_cooccurrence bc
                INNER JOIN contract_block cb ON 
                    (CASE 
                        WHEN bc.Block_A_ID IN ({0}) THEN bc.Block_B_ID 
                        ELSE bc.Block_A_ID 
                    END) = cb.Contract_Block_NR
                INNER JOIN original_contract_block ocb ON cb.Org_Cont_ID = ocb.Org_Cont_ID
                WHERE (bc.Block_A_ID IN ({0}) OR bc.Block_B_ID IN ({0}))
                GROUP BY RecommendedBlockId, ocb.Contract_text, ocb.Category_name
                HAVING RecommendedBlockId NOT IN ({0})
                ORDER BY Score DESC
                LIMIT @take", blockList);

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
            catch (Exception)
            {
                return new List<BlockRecommendation>();
            }
        }

        //DTO-luokka suosituksii varten
        public class BlockRecommendation
        {
            public int BlockId { get; set; }
            public int Score { get; set; }
            public string BlockText { get; set; }
            public string Category { get; set; }
        }


        //poistaa blokin sopimuksesta
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


        //hakee lapsiblokit tietylle parent-blokille
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

        //hakee koko blokkipuun (parent, lapset ja lasten lapset)
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

        //mappaa MySqlDataReader-olion ContractBlock-olioksi
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

        //lisää referenssin sopimusblokkiin tiettyyn järjestykseen
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

        //hakee kaikki referenssit tietylle sopimusblokille, järjestettynä Reference_Order mukaan
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

        //hakee kaikki sopimukset
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

        //hakee yksittäisen sopimuksen ID:n perusteella
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

        //hakee kaikki sopimukset tietyn luojan mukaan
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

        //luo uuden sopimuksen ja palauttaa sen ID:n
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

        //päivittää olemassa olevan sopimuksen tietoja
        public bool UpdateContract(Contract contract)
        {
            try
            {
                using (MySqlConnection conn = dbConn.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE contract SET 
                Company_name = @name,
                Approved = @approved,
                Sent_to_external = @sent
                WHERE Contract_NR = @id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", contract.Contract_NR);
                    cmd.Parameters.AddWithValue("@name", contract.Company_name);
                    cmd.Parameters.AddWithValue("@approved", contract.Approved);
                    cmd.Parameters.AddWithValue("@sent", contract.Sent_to_external);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        //poistaa sopimuksen
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

        //hakee kaikki sidosryhmät tietylle sopimukselle
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

        //hakee kaikki sopimukset joissa tietty käyttäjä on sidosryhmä
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

        //lisää uuden sidosryhmän
        public bool AddStakeholder(ContractStakeholder stakeholder) =>
            DbHelper.ExecuteNonQuery(
                SqlBuilder.Insert("contract_stakeholders", "Contract_NR", "Int_User_ID", "Has_approval_rights", "Approved", "Approved_date"),
                stakeholder
            );

        //päivittää olemassa olevan sidosryhmän
        public bool UpdateStakeholder(ContractStakeholder stakeholder) =>
            DbHelper.ExecuteNonQuery(
                SqlBuilder.Update("contract_stakeholders", "Contract_NR", "Int_User_ID", "Has_approval_rights", "Approved", "Approved_date"),
            stakeholder
            );

        //poistaa sidosryhmän sopimuksesta
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

        //tarkistaa onko sopimus hyväksytty kaikilla sidosryhmillä joilla on oikeus hyväksyä
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

        //hakee kaikki ulkoset käyttäjät jotka liittyy tiettyyn sopimukseen
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

        //hakee kaikki sopimukset joissa tietty ulkonen käyttäjä mukana
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

        //kutsuu ulkosen käyttäjän sopimukseen
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

        //poistaa ulkosen käyttäjän sopimuksesta
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

        //tarkistaa onko ulkonen käyttäjä kutsuttu tiettyyn sopimukseen
        public bool IsExternalUserInvitedToContract(int contractNr, int extUserId)
        {
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = @"SELECT COUNT(*) FROM contract_external_users 
                        WHERE Contract_NR = @contractNr AND Ext_User_ID = @userId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@contractNr", contractNr);
                cmd.Parameters.AddWithValue("@userId", extUserId);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }
    }

    // ==================== COMMENT DAL ====================
    public class CommentDAL
    {
        private DatabaseConnection dbConn = new DatabaseConnection();

        //hakee kaikki kommentit tietylle sopimukselle, järjestettynä päivämäärän mukaan
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

        //hakee kaikki kommentit tietylle sopimusblokille
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

        //hakee vaan sisäset kommentit sopimuksesta
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

        //hakee ulkosen käyttäjän kommentit sopimuksesta
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

        //luo uuden kommentin ja palauttaa sen ID:n
        public int CreateComment(Comment comment)
        {
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();

                //tarkistaa ensin että sopimus on olemassa
                string checkQuery = "SELECT COUNT(*) FROM contract WHERE Contract_NR = @contractNr";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@contractNr", comment.Contract_NR);

                int contractExists = Convert.ToInt32(checkCmd.ExecuteScalar());
                if (contractExists == 0)
                {
                    return 0; //sopimusta ei oo
                }

                //sopimus on olemassa, lisää kommentin
                string query = "INSERT INTO comments (Contract_NR, Contract_Block_NR, User_ID, User_type, Comment_text, Comment_date) VALUES (@contractNr, @blockNr, @userId, @userType, @text, @date); SELECT LAST_INSERT_ID();";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@contractNr", comment.Contract_NR);
                cmd.Parameters.AddWithValue("@blockNr", comment.Contract_Block_NR.HasValue ? (object)comment.Contract_Block_NR.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@userId", comment.User_ID);
                cmd.Parameters.AddWithValue("@userType", comment.User_type);
                cmd.Parameters.AddWithValue("@text", comment.Comment_text);
                cmd.Parameters.AddWithValue("@date", comment.Comment_date);

                try
                {
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        //poistaa kommentin
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

        //tarkistaa onko ulkonen käyttäjä kutsuttu tiettyyn sopimukseen
        public bool IsExternalUserInvitedToContract(int contractNr, int extUserId)
        {
            using (MySqlConnection conn = dbConn.GetConnection())
            {
                conn.Open();
                string query = @"SELECT COUNT(*) FROM contract_external_users 
                        WHERE Contract_NR = @contractNr AND Ext_User_ID = @userId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@contractNr", contractNr);
                cmd.Parameters.AddWithValue("@userId", extUserId);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

    }

}