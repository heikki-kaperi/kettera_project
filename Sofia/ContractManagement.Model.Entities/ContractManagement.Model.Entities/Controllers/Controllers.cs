using System;
using System.Collections.Generic;
using System.Linq;
using ContractManagement.Model.Entities;
using ContractManagement.Model.DAL;

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
            OriginalContractBlock original = originalDAL.GetOriginalBlockById(originalBlockId);
            if (original == null) return false;

            OriginalContractBlock copy = new OriginalContractBlock
            {
                Category_name = original.Category_name,
                Contract_text = original.Contract_text + " (Copy)",
                Created_by = createdBy,
                Created_date = DateTime.Now
            };
            return originalDAL.CreateOriginalBlock(copy) > 0;
        }

        // Contract blocks
        public int CreateContractBlock(int orgId, string text, bool isNew)
        {
            ContractBlock block = new ContractBlock
            {
                Org_Cont_ID = orgId,
                Contract_text = text,
                New = isNew,
                Modified_date = DateTime.Now
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

        public List<Contract> GetContractsByStakeholder(int userId) => stakeholderDAL.GetContractsByStakeholder(userId);

        public List<Contract> GetContractsByExternalUser(int extUserId) => externalDAL.GetContractsByExternalUser(extUserId);

        public bool DeleteContract(int contractNr) => contractDAL.DeleteContract(contractNr);

        // Block management in contract
        public List<ContractBlock> GetContractBlocks(int contractNr) => blockDAL.GetBlocksByContract(contractNr);

        public bool AddBlockToContract(int contractNr, int originalBlockId)
        {
            // Get original block
            OriginalContractBlock original = originalDAL.GetOriginalBlockById(originalBlockId);
            if (original == null) return false;

            // Create contract block from original
            ContractBlock contractBlock = new ContractBlock
            {
                Org_Cont_ID = originalBlockId,
                Contract_text = original.Contract_text,
                New = true,
                Modified_date = DateTime.Now
            };

            int newBlockId = blockDAL.CreateContractBlock(contractBlock);
            if (newBlockId == 0) return false;

            // Get current max order
            var existingBlocks = blockDAL.GetBlocksByContract(contractNr);
            int maxOrder = existingBlocks.Count > 0 ? existingBlocks.Count : 0;

            // Add to contract
            return blockDAL.AddBlockToContract(contractNr, newBlockId, maxOrder + 1);
        }

        public bool RemoveBlockFromContract(int contractNr, int blockId)
            => blockDAL.RemoveBlockFromContract(contractNr, blockId);

        public bool EditBlockInContract(int blockId, string newText)
        {
            ContractBlock block = blockDAL.GetContractBlockById(blockId);
            if (block == null) return false;

            block.Contract_text = newText;
            block.Modified_date = DateTime.Now;
            return blockDAL.UpdateContractBlock(block);
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
                contract.Sent_to_external = true;
                contractDAL.UpdateContract(contract);
            }
            return success;
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
                contract.Approved = true;
                return contractDAL.UpdateContract(contract);
            }

            return true;
        }

        public bool IsContractFullyApproved(int contractNr)
            => stakeholderDAL.IsContractFullyApproved(contractNr);

        public List<ContractStakeholder> GetStakeholders(int contractNr)
            => stakeholderDAL.GetStakeholdersByContract(contractNr);
    }
}