using System;
using System.Collections.Generic;
using System.Linq;
using ContractManagement.Model.DAL;
using ContractManagement.Model.Entities;
using ContractManagement.Utils;
using MySql.Data.MySqlClient;
using static ContractManagement.Model.DAL.ContractBlockDAL;

namespace ContractManagement.Controller
{
    // ==================== USER CONTROLLER ====================
    public class UserController
    {
        private AdministratorDAL adminDAL = new AdministratorDAL();
        private InternalUserDAL internalUserDAL = new InternalUserDAL();
        private ExternalUserDAL externalUserDAL = new ExternalUserDAL();

        //kirjautuminen järjestelmän adminina
        public Administrator LoginAdministrator(string username, string password)
        {
            Administrator admin = adminDAL.GetAdministratorByUsername(username);
            if (admin != null && PasswordHelper.VerifyPassword(password, admin.Password))
                return admin; //palauttaa admin-olion jos salasana oikein
            return null; //palauttaa null jos kirjautuminen epäonnistu
        }

        //kirjautuminen sisäsenä käyttäjänä
        public InternalUser LoginInternalUser(string username, string password)
        {
            InternalUser user = internalUserDAL.GetInternalUserByUsername(username);
            if (user != null && PasswordHelper.VerifyPassword(password, user.Password))
                return user;
            return null;
        }

        //kirjautuminen ulkosena käyttäjänä
        public ExternalUser LoginExternalUser(string username, string password)
        {
            ExternalUser user = externalUserDAL.GetExternalUserByUsername(username);
            if (user != null && PasswordHelper.VerifyPassword(password, user.Password))
                return user;
            return null;
        }

        //getter, hakee kaikki adminit
        public List<Administrator> GetAllAdministrators()
        {
            return adminDAL.GetAllAdministrators();
        }


        //hakee kaikki sisäset käyttäjät
        public List<InternalUser> GetAllInternalUsers() => internalUserDAL.GetAllInternalUsers();

        //luo uuden sisäsen käyttäjän (salasana hashataan)
        public bool CreateInternalUser(string fname, string lname, string email, string username, string password)
        {
            InternalUser user = new InternalUser
            {
                First_name = fname,
                Last_name = lname,
                Email = email,
                Username = username,
                Password = PasswordHelper.HashPassword(password) //hashattu salasana
            };
            return internalUserDAL.CreateInternalUser(user);
        }

        //päivittää olemassa olevan käyttäjän tiedot
        public bool UpdateInternalUser(InternalUser user) => internalUserDAL.UpdateInternalUser(user);

        //poistaa sisäsen käyttäjän
        public bool DeleteInternalUser(int userId) => internalUserDAL.DeleteInternalUser(userId);

        //hakee sisäsen käyttäjän käyttäjätunnuksen perusteella
        public InternalUser GetInternalUserByUsername(string username) => internalUserDAL.GetInternalUserByUsername(username);

        //hakee kaikki ulkoset käyttäjät
        public List<ExternalUser> GetAllExternalUsers() => externalUserDAL.GetAllExternalUsers();

        //luo uuden ulkosen käyttäjän (salasana hashataan)
        public bool CreateExternalUser(string fname, string lname, string company, string email, string username, string password)
        {
            ExternalUser user = new ExternalUser
            {
                First_name = fname,
                Last_name = lname,
                Company_name = company,
                Email = email,
                Username = username,
                Password = PasswordHelper.HashPassword(password) //hashattu salasana
            };
            return externalUserDAL.CreateExternalUser(user);
        }

        //päivittää ulkosen käyttäjän tiedot
        public bool UpdateExternalUser(ExternalUser user) => externalUserDAL.UpdateExternalUser(user);

        //poistaa ulkosen käyttäjän
        public bool DeleteExternalUser(int userId) => externalUserDAL.DeleteExternalUser(userId);

        //hakee ulkosen käyttäjän käyttäjätunnuksen perusteella
        public ExternalUser GetExternalUserByUsername(string username) => externalUserDAL.GetExternalUserByUsername(username);
    }

    // ==================== BLOCK CONTROLLER ====================
    public class BlockController
    {
        private CategoryDAL categoryDAL = new CategoryDAL();
        private OriginalContractBlockDAL originalDAL = new OriginalContractBlockDAL();
        private ContractBlockDAL blockDAL = new ContractBlockDAL();

        //hakee kaikki kategoriat
        public List<ContractBlockCategory> GetAllCategories() => categoryDAL.GetAllCategories();

        //luo uuden kategorian
        public bool CreateCategory(string name, string description)
        {
            return categoryDAL.CreateCategory(new ContractBlockCategory
            {
                Category_name = name,
                Description = description
            });
        }

        public bool UpdateOriginalBlock(OriginalContractBlock block)
        {
            return originalDAL.UpdateOriginalBlock(block);
        }


        //luo alkuperäsen blokin tietyn tyypin ja median kanssa
        public int CreateOriginalBlockWithType(OriginalContractBlock block, BlockType blockType, byte[] mediaContent)
        {
            var originalDAL = new OriginalContractBlockDAL();

            using (var conn = new DatabaseConnection().GetConnection())
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
                cmd.Parameters.AddWithValue("@type", (int)blockType);
                cmd.Parameters.AddWithValue("@media", mediaContent ?? (object)DBNull.Value);

                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0; //palauttaa luodun blokin ID:n
            }
        }

        //hakee kaikki alkuperäset blokit
        public List<OriginalContractBlock> GetAllOriginalBlocks() => originalDAL.GetAllOriginalBlocks();

        //hakee alkuperäset blokit kategorian perusteella
        public List<OriginalContractBlock> GetOriginalBlocksByCategory(string category)
            => originalDAL.GetOriginalBlocksByCategory(category);

        //luo uuden alkuperäsen blokin
        public bool CreateOriginalBlock(string category, string text, int createdBy)
        {
            OriginalContractBlock block = new OriginalContractBlock
            {
                Category_name = category,
                Contract_text = text,
                Created_by = createdBy,
                Created_date = DateTime.Now
            };
            return originalDAL.CreateOriginalBlock(block) > 0;
        }

        //kopioi olemassa olevan alkuperäsen lohkon
        public bool CopyOriginalBlock(int originalBlockId, int createdBy)
        {
            OriginalContractBlock original = null;

            try
            {
                original = originalDAL.GetOriginalBlockById(originalBlockId);

                if (original == null)
                {
                    Console.WriteLine($"ERROR: Block ID {originalBlockId} not found");
                    return false;
                }

                Console.WriteLine($"Found block: Category={original.Category_name}");

                OriginalContractBlock copy = new OriginalContractBlock
                {
                    Category_name = original.Category_name,
                    Contract_text = original.Contract_text + " (Copy)",
                    Created_by = createdBy,
                    Created_date = DateTime.Now
                };

                Console.WriteLine($"About to create copy with Category={copy.Category_name}, Created_by={copy.Created_by}");

                int newId = originalDAL.CreateOriginalBlock(copy);

                Console.WriteLine($"CreateOriginalBlock returned: {newId}");

                return newId > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"========== ERROR in CopyOriginalBlock ==========");
                Console.WriteLine($"Original Block ID: {originalBlockId}");
                Console.WriteLine($"Created By: {createdBy}");
                Console.WriteLine($"Original found: {original != null}");
                Console.WriteLine($"Exception Message: {ex.Message}");
                Console.WriteLine($"Exception Type: {ex.GetType().Name}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                Console.WriteLine($"================================================");
                return false;
            }
        }

        //luo uuden sopimusblokin alkuperäisen blokin perusteella
        public int CreateContractBlock(int orgId, string text, bool isNew, int createdBy)
        {
            ContractBlock block = new ContractBlock
            {
                Org_Cont_ID = orgId,
                Contract_text = text,
                New = isNew,
                Modified_date = DateTime.Now, //muokkauspäivä nykyhetkessä
                Created_date = DateTime.Now, //luontipäivämäärä nykyhetkessä
                Created_by = createdBy //luojan ID
            };
            return blockDAL.CreateContractBlock(block); //tallentaa blokin tietokantaan
        }

        //päivittää olemassa olevan sopimusblokin tekstin
        public bool UpdateContractBlock(int blockId, string newText)
        {
            ContractBlock block = blockDAL.GetContractBlockById(blockId); //hakee blokin ID:llä
            if (block == null) return false;

            block.Contract_text = newText; //päivittää tekstin
            block.Modified_date = DateTime.Now; //päivittää muokkauspäivämäätän
            return blockDAL.UpdateContractBlock(block); //tallentaa muutokset
        }

        //lisää referenssin blokkiin
        public bool AddReferenceToBlock(int blockId, int referenceBlockId)
        {
            ContractBlock block = blockDAL.GetContractBlockById(blockId); //hakee blokin
            if (block == null) return false;

            if (block.References == null) //jos referenssilista on null, luo uuden listan
                block.References = new List<int>();
            block.References.Add(referenceBlockId); //lisää referenssin
            return blockDAL.UpdateContractBlock(block); //päivittää blokin tietokantaan
        }

        //hakee kaikki blokit joihin referoitu
        public List<ContractBlock> GetReferencedBlocks(int blockId)
        {
            ContractBlock block = blockDAL.GetContractBlockById(blockId);
            if (block == null || block.References == null) return new List<ContractBlock>();

            List<ContractBlock> referencedBlocks = new List<ContractBlock>();
            foreach (var refId in block.References)
            {
                var refBlock = blockDAL.GetContractBlockById(refId); //hakee viitatun blokin
                if (refBlock != null) referencedBlocks.Add(refBlock);
            }
            return referencedBlocks;
        }

        //luo koostetun blokin joka sisältää useita aliblokkei
        public int CreateCompositeBlock(List<int> childBlockIds, string compositeText, int createdBy)
        {
            List<ContractBlock> childBlocks = childBlockIds
            .Select(id => blockDAL.GetContractBlockById(id)) //hakee jokasen aliblokin ID:llä
            .Where(b => b != null)
            .ToList();

            ContractBlock compositeBlock = new ContractBlock
            {
                Contract_text = compositeText, //koosteen teksti
                ChildBlocks = childBlocks, //aliblokit
                Created_date = DateTime.Now, //luontipäivä
                Created_by = createdBy, //luojan ID
                New = true, //merkitään uutena
                Modified_date = DateTime.Now //muokkauspäivä
            };
            return blockDAL.CreateContractBlock(compositeBlock); //tallentaa tietokantaan
        }

    }

    // ==================== CONTRACT CONTROLLER ====================
    public class ContractController
    {
        private ContractDAL contractDAL = new ContractDAL();
        private ContractBlockDAL blockDAL = new ContractBlockDAL();
        private OriginalContractBlockDAL originalDAL = new OriginalContractBlockDAL();
        private ContractStakeholderDAL stakeholderDAL = new ContractStakeholderDAL();
        private ContractExternalUserDAL externalDAL = new ContractExternalUserDAL();

        //luo uuden sopimuksen
        public int CreateContract(string companyName, int creatorId)
        {
            Contract contract = new Contract
            {
                Company_name = companyName,
                The_Creator = creatorId,
                Created_date = DateTime.Now, //luontipäivä
                Approved = false, //aluksi ei hyväksytty
                Sent_to_external = false //aluksi ei lähetetty ulkopuolisille
            };
            return contractDAL.CreateContract(contract); //tallentaa tietokantaan
        }

        //hakee sopimuksen ID:llä
        public Contract GetContractById(int contractId) => contractDAL.GetContractById(contractId);

        //hakee kaikki sopimukset
        public List<Contract> GetAllContracts() => contractDAL.GetAllContracts();

        //hakee sopimukset luojan perusteella
        public List<Contract> GetContractsByCreator(int creatorId) => contractDAL.GetContractsByCreator(creatorId);

        //hakee sopimukset sisäsen käyttäjän perusteella
        public List<Contract> GetContractsByInternalUser(int userId)
        {
            return GetContractsByCreator(userId);
        }

        //hakee sopimukset sidosryhmän perusteella
        public List<Contract> GetContractsByStakeholder(int userId) => stakeholderDAL.GetContractsByStakeholder(userId);

        //hakee sopimukset joita sisänen käyttäjä voi tarkastella
        public List<Contract> GetContractsToReviewByInternalUser(int userId)
        {
            return GetContractsByStakeholder(userId);
        }

        //hakee sopimukset ulkosen käyttäjän perusteella
        public List<Contract> GetContractsByExternalUser(int extUserId) => externalDAL.GetContractsByExternalUser(extUserId);

        //poistaa sopimuksen
        public bool DeleteContract(int contractNr) => contractDAL.DeleteContract(contractNr);

        //hakee kaikki blokit sopimuksesta
        public List<ContractBlock> GetContractBlocks(int contractNr) => blockDAL.GetBlocksByContract(contractNr);

        public List<ContractBlock> GetBlocksByContract(int contractId)
        {
            return GetContractBlocks(contractId);
        }

        //lisää alkuperäsen blokin sopimukseen
        public bool AddBlockToContract(int contractNr, int originalBlockId)
        {
            try
            {
                //vaihe 1: tarkistaa että sopimus on olemassa
                Contract contract = contractDAL.GetContractById(contractNr);
                if (contract == null)
                {
                    Console.WriteLine("DEBUG: Contract {0} not found", contractNr);
                    return false;
                }

                //vaihe 2: hakee alkuperäsen blokin
                OriginalContractBlock original = originalDAL.GetOriginalBlockById(originalBlockId);
                if (original == null)
                {
                    Console.WriteLine("DEBUG: Original block {0} not found", originalBlockId);
                    return false;
                }

                //vaihe 3: luo sopimusblokin alkuperäsestä
                ContractBlock contractBlock = new ContractBlock
                {
                    Org_Cont_ID = originalBlockId,
                    Contract_text = original.Contract_text,
                    New = true,
                    Modified_date = DateTime.Now,
                    Type = BlockType.Text,
                    MediaContent = null
                };

                int newBlockId = blockDAL.CreateContractBlock(contractBlock);
                if (newBlockId == 0)
                {
                    Console.WriteLine("DEBUG: Failed to create contract block");
                    return false;
                }

                Console.WriteLine("DEBUG: Created contract block with ID: {0}", newBlockId);

                //vaihe 4: hakee nykysen suurimman järjestysnumeron
                var existingBlocks = blockDAL.GetBlocksByContract(contractNr);
                int maxOrder = existingBlocks.Count > 0 ? existingBlocks.Count : 0;

                //vaihe 5: lisää block junction-tauluun
                bool addResult = blockDAL.AddBlockToContract(contractNr, newBlockId, maxOrder + 1);

                Console.WriteLine("DEBUG: AddBlockToContract result: {0}", addResult);

                return addResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DEBUG: Exception in AddBlockToContract: {0}", ex.Message);
                Console.WriteLine("DEBUG: Stack trace: {0}", ex.StackTrace);
                return false;
            }
        }

        //hakee suositellut blokit sopimukseen
        public List<BlockRecommendation> GetContractRecommendations(int contractNr, int take = 5)
        {
            try
            {
                return blockDAL.GetRecommendationsForContract(contractNr, take);
            }
            catch (Exception)
            {
                return new List<BlockRecommendation>();
            }
        }

        //poistaa blokin sopimuksesta
        public bool RemoveBlockFromContract(int contractNr, int blockId)
        {
            try
            {
                return blockDAL.RemoveBlockFromContract(contractNr, blockId);
            }
            catch (Exception)
            {
                return false;
            }
        }

        //muokkaa blokkia sopimuksessa
        public bool EditBlockInContract(int blockId, string newText)
        {
            try
            {
                ContractBlock block = blockDAL.GetContractBlockById(blockId);
                if (block == null) return false;

                block.Contract_text = newText;
                block.Modified_date = DateTime.Now;
                return blockDAL.UpdateContractBlock(block);
            }
            catch (Exception)
            {
                return false;
            }
        }

        //sama kuin EditBlockInContract mutta sisältää myös sopimusID:n
        public bool EditBlockInContract(int contractId, int blockId, string newText)
        {
            return EditBlockInContract(blockId, newText);
        }

        // Stakeholder management
        //kutsuu sisäsen käyttäjän tarkastajaksi
        public bool InviteInternalReviewer(int contractNr, int userId, bool hasApprovalRights)
        {
            ContractStakeholder stakeholder = new ContractStakeholder
            {
                Contract_NR = contractNr,
                Int_User_ID = userId,
                Has_approval_rights = hasApprovalRights, //hyväksymisoikeudet
                Approved = false, //aluksi ei hyväksytty
                Approved_date = null //hyväksymispäivä tyhjä
            };
            return stakeholderDAL.AddStakeholder(stakeholder); //lisää tietokantaan
        }

        //kutsuu ulkosen käyttäjän sopimukseen
        public bool InviteExternalUser(int contractNr, int extUserId)
        {
            bool success = externalDAL.InviteExternalUser(contractNr, extUserId, DateTime.Now);
            if (success)
            {
                //merkitsee sopimuksen lähetetyksi ulkopuoliselle
                Contract contract = contractDAL.GetContractById(contractNr);
                if (contract != null)
                {
                    contract.Sent_to_external = true;
                    contractDAL.UpdateContract(contract);
                }
            }
            return success;
        }

        //tarkistaa onko ulkonen käyttäjä kutsuttu sopimukseen
        public bool IsExternalUserInvitedToContract(int contractNr, int extUserId)
        {
            return externalDAL.IsExternalUserInvitedToContract(contractNr, extUserId);
        }

        //hakee kaikki alkuperäset blokit
        public List<OriginalContractBlock> GetAllOriginalBlocks()
        {
            return originalDAL.GetAllOriginalBlocks();
        }
    }



    // ==================== COMMENT CONTROLLER ====================
    public class CommentController
    {
        private CommentDAL commentDAL = new CommentDAL();

        //lisää uuden kommentin
        public bool AddComment(int contractNr, int? blockNr, int userId, string userType, string text)
        {
            Comment comment = new Comment
            {
                Contract_NR = contractNr,
                Contract_Block_NR = blockNr, //null jos ei blokkiin liittyvä
                User_ID = userId,
                User_type = userType, //internal tai external
                Comment_text = text,
                Comment_date = DateTime.Now //asetetaan ajankohtaan
            };
            return commentDAL.CreateComment(comment) > 0;
        }

        //hakee kaikki kommentit sopimuksesta
        public List<Comment> GetCommentsForContract(int contractNr)
            => commentDAL.GetCommentsByContract(contractNr);

        //hakee kommentit tietyltä blokilta
        public List<Comment> GetCommentsByBlock(int contractNr, int blockNr)
            => commentDAL.GetCommentsByBlock(contractNr, blockNr);

        //hakee sisäiset kommentit
        public List<Comment> GetInternalComments(int contractNr)
            => commentDAL.GetInternalComments(contractNr);

        //hakee ulkosen käyttäjän kommentit
        public List<Comment> GetExternalCommentsByUser(int contractNr, int extUserId)
            => commentDAL.GetExternalCommentsByUser(contractNr, extUserId);
    }

    // ==================== APPROVAL CONTROLLER ====================
    public class ApprovalController
    {
        private ContractStakeholderDAL stakeholderDAL = new ContractStakeholderDAL();
        private ContractDAL contractDAL = new ContractDAL();

        //hyväksyy sopimuksen tietyn käyttäjän puolesta
        public bool ApproveContract(int contractNr, int userId)
        {
            List<ContractStakeholder> stakeholders = stakeholderDAL.GetStakeholdersByContract(contractNr);
            ContractStakeholder stakeholder = stakeholders.Find(s => s.Int_User_ID == userId);

            if (stakeholder == null || !stakeholder.Has_approval_rights)
                return false;

            //merkitsee hyväksyjän hyväksyneeksi
            stakeholder.Approved = true;
            stakeholder.Approved_date = DateTime.Now;
            stakeholderDAL.UpdateStakeholder(stakeholder);

            //tarkistaa onko kaikki hyväksytty
            if (stakeholderDAL.IsContractFullyApproved(contractNr))
            {
                Contract contract = contractDAL.GetContractById(contractNr);
                if (contract != null)
                {
                    contract.Approved = true;
                    return contractDAL.UpdateContract(contract); //päivittää sopimuksen
                }
            }

            return true;
        }

        //tarkistaa onko sopimus täysin hyväksytty
        public bool IsContractFullyApproved(int contractNr)
            => stakeholderDAL.IsContractFullyApproved(contractNr);

        //hakee kaikki sidosryhmät sopimukseen
        public List<ContractStakeholder> GetStakeholders(int contractNr)
            => stakeholderDAL.GetStakeholdersByContract(contractNr);
    }

    //================== ADMINSISTRATOR CONTROLLER ======================

    public class AdministratorController
    {
        private AdministratorDAL adminDAL = new AdministratorDAL();

        //hakee kaikki adminit
        public List<Administrator> GetAllAdministrators()
        {
            return adminDAL.GetAllAdministrators();
        }

        //äänestää adminin poistosta
        public bool VoteToDeleteAdmin(int targetAdminId, int voterAdminId)
        {
            return adminDAL.VoteToDeleteAdmin(targetAdminId, voterAdminId);
        }

        //yrittää poistaa adminin jos tarvittavat äänet saavutettu
        public bool TryDeleteAdmin(int targetAdminId)
        {
            return adminDAL.DeleteAdminIfApproved(targetAdminId);
        }
    }

}