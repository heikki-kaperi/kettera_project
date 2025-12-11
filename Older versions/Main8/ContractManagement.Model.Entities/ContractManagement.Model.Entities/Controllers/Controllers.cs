using System;
using System.Collections.Generic;
using System.Linq;
using ContractManagement.Model.Entities;
using ContractManagement.Model.DAL;
using static ContractManagement.Model.DAL.ContractBlockDAL;
using MySql.Data.MySqlClient;

namespace ContractManagement.Controller
{
    // ==================== USER CONTROLLER ====================
    public class UserController
    {
        private AdministratorDAL adminDAL = new AdministratorDAL();
        private InternalUserDAL internalUserDAL = new InternalUserDAL();
        private ExternalUserDAL externalUserDAL = new ExternalUserDAL();

        // Login methods
        public Administrator LoginAdministrator(string username, string password)
        {
            Administrator admin = adminDAL.GetAdministratorByUsername(username);
            if (admin != null && admin.Password == password)
                return admin;
            return null;
        }

        public InternalUser LoginInternalUser(string username, string password)
        {
            InternalUser user = internalUserDAL.GetInternalUserByUsername(username);
            if (user != null && user.Password == password)
                return user;
            return null;
        }

        public ExternalUser LoginExternalUser(string username, string password)
        {
            ExternalUser user = externalUserDAL.GetExternalUserByUsername(username);
            if (user != null && user.Password == password)
                return user;
            return null;
        }

        //Get all admins
        public List<Administrator> GetAllAdministrators()
        {
            return adminDAL.GetAllAdministrators();
        }


        // Internal users
        public List<InternalUser> GetAllInternalUsers() => internalUserDAL.GetAllInternalUsers();

        public bool CreateInternalUser(string fname, string lname, string email, string username, string password)
        {
            InternalUser user = new InternalUser
            {
                First_name = fname,
                Last_name = lname,
                Email = email,
                Username = username,
                Password = password
            };
            return internalUserDAL.CreateInternalUser(user);
        }

        public bool UpdateInternalUser(InternalUser user) => internalUserDAL.UpdateInternalUser(user);
        public bool DeleteInternalUser(int userId) => internalUserDAL.DeleteInternalUser(userId);
        public InternalUser GetInternalUserByUsername(string username) => internalUserDAL.GetInternalUserByUsername(username);

        // External users
        public List<ExternalUser> GetAllExternalUsers() => externalUserDAL.GetAllExternalUsers();

        public bool CreateExternalUser(string fname, string lname, string company, string email, string username, string password)
        {
            ExternalUser user = new ExternalUser
            {
                First_name = fname,
                Last_name = lname,
                Company_name = company,
                Email = email,
                Username = username,
                Password = password
            };
            return externalUserDAL.CreateExternalUser(user);
        }

        public bool UpdateExternalUser(ExternalUser user) => externalUserDAL.UpdateExternalUser(user);
        public bool DeleteExternalUser(int userId) => externalUserDAL.DeleteExternalUser(userId);
        public ExternalUser GetExternalUserByUsername(string username) => externalUserDAL.GetExternalUserByUsername(username);
    }

    // ==================== BLOCK CONTROLLER ====================
    public class BlockController
    {
        private CategoryDAL categoryDAL = new CategoryDAL();
        private OriginalContractBlockDAL originalDAL = new OriginalContractBlockDAL();
        private ContractBlockDAL blockDAL = new ContractBlockDAL();

        // Categories
        public List<ContractBlockCategory> GetAllCategories() => categoryDAL.GetAllCategories();

        public bool CreateCategory(string name, string description)
        {
            return categoryDAL.CreateCategory(new ContractBlockCategory
            {
                Category_name = name,
                Description = description
            });
        }

        public int CreateOriginalBlockWithType(OriginalContractBlock block, BlockType blockType, byte[] mediaContent)
        {
            // First create the original block in the original_contract_block table
            var originalDAL = new OriginalContractBlockDAL();

            // Create a temporary block with the type and media
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
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        // Original blocks
        public List<OriginalContractBlock> GetAllOriginalBlocks() => originalDAL.GetAllOriginalBlocks();

        public List<OriginalContractBlock> GetOriginalBlocksByCategory(string category)
            => originalDAL.GetOriginalBlocksByCategory(category);

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

        // Contract blocks
        public int CreateContractBlock(int orgId, string text, bool isNew, int createdBy)
        {
            ContractBlock block = new ContractBlock
            {
                Org_Cont_ID = orgId,
                Contract_text = text,
                New = isNew,
                Modified_date = DateTime.Now,
                Created_date = DateTime.Now,
                Created_by = createdBy
            };
            return blockDAL.CreateContractBlock(block);
        }

        public bool UpdateContractBlock(int blockId, string newText)
        {
            ContractBlock block = blockDAL.GetContractBlockById(blockId);
            if (block == null) return false;

            block.Contract_text = newText;
            block.Modified_date = DateTime.Now;
            return blockDAL.UpdateContractBlock(block);
        }

        public bool AddReferenceToBlock(int blockId, int referenceBlockId)
        {
            ContractBlock block = blockDAL.GetContractBlockById(blockId);
            if (block == null) return false;

            if (block.References == null)
                block.References = new List<int>();
            block.References.Add(referenceBlockId);
            return blockDAL.UpdateContractBlock(block);
        }

        public List<ContractBlock> GetReferencedBlocks(int blockId)
        {
            ContractBlock block = blockDAL.GetContractBlockById(blockId);
            if (block == null || block.References == null) return new List<ContractBlock>();

            List<ContractBlock> referencedBlocks = new List<ContractBlock>();
            foreach (var refId in block.References)
            {
                var refBlock = blockDAL.GetContractBlockById(refId);
                if (refBlock != null) referencedBlocks.Add(refBlock);
            }
            return referencedBlocks;
        }

        public int CreateCompositeBlock(List<int> childBlockIds, string compositeText, int createdBy)
        {
            List<ContractBlock> childBlocks = childBlockIds
            .Select(id => blockDAL.GetContractBlockById(id))
            .Where(b => b != null)
            .ToList();

            ContractBlock compositeBlock = new ContractBlock
            {
                Contract_text = compositeText,
                ChildBlocks = childBlocks,
                Created_date = DateTime.Now,
                Created_by = createdBy,
                New = true,
                Modified_date = DateTime.Now
            };
            return blockDAL.CreateContractBlock(compositeBlock);
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

        public int CreateContract(string companyName, int creatorId)
        {
            Contract contract = new Contract
            {
                Company_name = companyName,
                The_Creator = creatorId,
                Created_date = DateTime.Now,
                Approved = false,
                Sent_to_external = false
            };
            return contractDAL.CreateContract(contract);
        }

        public Contract GetContractById(int contractId) => contractDAL.GetContractById(contractId);

        public List<Contract> GetAllContracts() => contractDAL.GetAllContracts();

        public List<Contract> GetContractsByCreator(int creatorId) => contractDAL.GetContractsByCreator(creatorId);

        public List<Contract> GetContractsByInternalUser(int userId)
        {
            return GetContractsByCreator(userId);
        }

        public List<Contract> GetContractsByStakeholder(int userId) => stakeholderDAL.GetContractsByStakeholder(userId);

        public List<Contract> GetContractsToReviewByInternalUser(int userId)
        {
            return GetContractsByStakeholder(userId);
        }

        public List<Contract> GetContractsByExternalUser(int extUserId) => externalDAL.GetContractsByExternalUser(extUserId);

        public bool DeleteContract(int contractNr) => contractDAL.DeleteContract(contractNr);

        // Block management in contract
        public List<ContractBlock> GetContractBlocks(int contractNr) => blockDAL.GetBlocksByContract(contractNr);

        public List<ContractBlock> GetBlocksByContract(int contractId)
        {
            return GetContractBlocks(contractId);
        }

        public bool AddBlockToContract(int contractNr, int originalBlockId)
        {
            try
            {
                // Step 1: Verify contract exists
                Contract contract = contractDAL.GetContractById(contractNr);
                if (contract == null)
                {
                    Console.WriteLine("DEBUG: Contract {0} not found", contractNr);
                    return false;
                }

                // Step 2: Get original block
                OriginalContractBlock original = originalDAL.GetOriginalBlockById(originalBlockId);
                if (original == null)
                {
                    Console.WriteLine("DEBUG: Original block {0} not found", originalBlockId);
                    return false;
                }

                // Step 3: Create contract block from original
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

                // Step 4: Get current max order
                var existingBlocks = blockDAL.GetBlocksByContract(contractNr);
                int maxOrder = existingBlocks.Count > 0 ? existingBlocks.Count : 0;

                // Step 5: Add to contract_blocks junction table
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

        public bool EditBlockInContract(int contractId, int blockId, string newText)
        {
            return EditBlockInContract(blockId, newText);
        }

        // Stakeholder management
        public bool InviteInternalReviewer(int contractNr, int userId, bool hasApprovalRights)
        {
            ContractStakeholder stakeholder = new ContractStakeholder
            {
                Contract_NR = contractNr,
                Int_User_ID = userId,
                Has_approval_rights = hasApprovalRights,
                Approved = false,
                Approved_date = null
            };
            return stakeholderDAL.AddStakeholder(stakeholder);
        }

        public bool InviteExternalUser(int contractNr, int extUserId)
        {
            bool success = externalDAL.InviteExternalUser(contractNr, extUserId, DateTime.Now);
            if (success)
            {
                // Mark contract as sent to external
                Contract contract = contractDAL.GetContractById(contractNr);
                if (contract != null)
                {
                    contract.Sent_to_external = true;
                    contractDAL.UpdateContract(contract);
                }
            }
            return success;
        }

        public List<OriginalContractBlock> GetAllOriginalBlocks()
        {
            return originalDAL.GetAllOriginalBlocks();
        }
    }



    // ==================== COMMENT CONTROLLER ====================
    public class CommentController
    {
        private CommentDAL commentDAL = new CommentDAL();

        public bool AddComment(int contractNr, int? blockNr, int userId, string userType, string text)
        {
            Comment comment = new Comment
            {
                Contract_NR = contractNr,
                Contract_Block_NR = blockNr,
                User_ID = userId,
                User_type = userType,
                Comment_text = text,
                Comment_date = DateTime.Now
            };
            return commentDAL.CreateComment(comment) > 0;
        }

        public List<Comment> GetCommentsForContract(int contractNr)
            => commentDAL.GetCommentsByContract(contractNr);

        public List<Comment> GetCommentsByBlock(int contractNr, int blockNr)
            => commentDAL.GetCommentsByBlock(contractNr, blockNr);

        public List<Comment> GetInternalComments(int contractNr)
            => commentDAL.GetInternalComments(contractNr);

        public List<Comment> GetExternalCommentsByUser(int contractNr, int extUserId)
            => commentDAL.GetExternalCommentsByUser(contractNr, extUserId);
    }

    // ==================== APPROVAL CONTROLLER ====================
    public class ApprovalController
    {
        private ContractStakeholderDAL stakeholderDAL = new ContractStakeholderDAL();
        private ContractDAL contractDAL = new ContractDAL();

        public bool ApproveContract(int contractNr, int userId)
        {
            List<ContractStakeholder> stakeholders = stakeholderDAL.GetStakeholdersByContract(contractNr);
            ContractStakeholder stakeholder = stakeholders.Find(s => s.Int_User_ID == userId);

            if (stakeholder == null || !stakeholder.Has_approval_rights)
                return false;

            // Mark stakeholder as approved
            stakeholder.Approved = true;
            stakeholder.Approved_date = DateTime.Now;
            stakeholderDAL.UpdateStakeholder(stakeholder);

            // Check if all approvals done
            if (stakeholderDAL.IsContractFullyApproved(contractNr))
            {
                Contract contract = contractDAL.GetContractById(contractNr);
                if (contract != null)
                {
                    contract.Approved = true;
                    return contractDAL.UpdateContract(contract);
                }
            }

            return true;
        }

        public bool IsContractFullyApproved(int contractNr)
            => stakeholderDAL.IsContractFullyApproved(contractNr);

        public List<ContractStakeholder> GetStakeholders(int contractNr)
            => stakeholderDAL.GetStakeholdersByContract(contractNr);
    }

    //================== ADMINSISTRATOR CONTROLLER ======================

    public class AdministratorController
    {
        private AdministratorDAL adminDAL = new AdministratorDAL();

        public List<Administrator> GetAllAdministrators()
        {
            return adminDAL.GetAllAdministrators();
        }

        public bool VoteToDeleteAdmin(int targetAdminId, int voterAdminId)
        {
            return adminDAL.VoteToDeleteAdmin(targetAdminId, voterAdminId);
        }

        public bool TryDeleteAdmin(int targetAdminId)
        {
            return adminDAL.DeleteAdminIfApproved(targetAdminId);
        }
    }

}