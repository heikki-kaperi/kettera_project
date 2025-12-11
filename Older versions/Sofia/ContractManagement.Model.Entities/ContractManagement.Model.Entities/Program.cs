using System;
using System.Collections.Generic;
using System.Linq;
using ContractManagement.Controller;
using ContractManagement.Model.Entities;

namespace ContractManagement.View
{
    // ==================== MAIN PROGRAM ====================
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleUI ui = new ConsoleUI();
            ui.Start();
        }
    }

    // ==================== CONSOLE UI ====================
    public class ConsoleUI
    {
        private UserController userController = new UserController();
        private BlockController blockController = new BlockController();
        private ContractController contractController = new ContractController();
        private CommentController commentController = new CommentController();
        private ApprovalController approvalController = new ApprovalController();
        private AdministratorController adminController = new AdministratorController();

        // Current session data
        private string currentUserType = null; // "admin", "internal", "external"
        private int currentUserId = 0;
        private string currentUsername = null;

        public void Start()
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("   CONTRACT MANAGEMENT SYSTEM");
            Console.WriteLine("===========================================\n");

            // Login
            if (!Login())
            {
                Console.WriteLine("Login failed. Exiting...");
                return;
            }

            // Main menu based on user type
            if (currentUserType == "admin")
            {
                AdminMenu();
            }
            else if (currentUserType == "internal")
            {
                InternalUserMenu();
            }
            else if (currentUserType == "external")
            {
                ExternalUserMenu();
            }
        }

        // ==================== LOGIN ====================
        private bool Login()
        {
            Console.WriteLine("LOGIN");
            Console.WriteLine("1. Administrator Login");
            Console.WriteLine("2. Internal User Login");
            Console.WriteLine("3. External User Login");
            Console.Write("\nSelect login type: ");

            string choice = Console.ReadLine();

            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            bool success = false;

            switch (choice)
            {
                case "1":
                    var admin = userController.LoginAdministrator(username, password);
                    if (admin != null)
                    {
                        currentUserType = "admin";
                        currentUserId = admin.Administrator_ID;
                        currentUsername = admin.Username;
                        success = true;
                        Console.WriteLine($"\nWelcome, Administrator {admin.First_name} {admin.Last_name}!");
                    }
                    break;
                case "2":
                    var intUser = userController.LoginInternalUser(username, password);
                    if (intUser != null)
                    {
                        currentUserType = "internal";
                        currentUserId = intUser.Int_User_ID;
                        currentUsername = intUser.Username;
                        success = true;
                        Console.WriteLine($"\nWelcome, {intUser.First_name} {intUser.Last_name}!");
                    }
                    break;
                case "3":
                    var extUser = userController.LoginExternalUser(username, password);
                    if (extUser != null)
                    {
                        currentUserType = "external";
                        currentUserId = extUser.Ext_User_ID;
                        currentUsername = extUser.Username;
                        success = true;
                        Console.WriteLine($"\nWelcome, {extUser.First_name} {extUser.Last_name} from {extUser.Company_name}!");
                    }
                    break;
            }

            if (!success)
            {
                Console.WriteLine("\nInvalid username or password.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();

            return success;
        }

        // ==================== ADMINISTRATOR MENU ====================
        private void AdminMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===========================================");
                Console.WriteLine($"   ADMINISTRATOR MENU - {currentUsername}");
                Console.WriteLine("===========================================");
                Console.WriteLine("1. Create Internal User");
                Console.WriteLine("2. Create External User");
                Console.WriteLine("3. View All Internal Users");
                Console.WriteLine("4. View All External Users");
                Console.WriteLine("5. Delete Internal User");
                Console.WriteLine("6. Delete External User");
                Console.WriteLine("7. Delete Administrator (3 votes)");
                Console.WriteLine("0. Logout");
                Console.Write("\nSelect option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateInternalUser();
                        break;
                    case "2":
                        CreateExternalUser();
                        break;
                    case "3":
                        ViewAllInternalUsers();
                        break;
                    case "4":
                        ViewAllExternalUsers();
                        break;
                    case "5":
                        DeleteInternalUser();
                        break;
                    case "6":
                        DeleteExternalUser();
                        break;
                    case "7":
                        DeleteAdministratorVoting();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        Pause();
                        break;
                }
            }
        }

        // ==================== INTERNAL USER MENU ====================
        private void InternalUserMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===========================================");
                Console.WriteLine($"   INTERNAL USER MENU - {currentUsername}");
                Console.WriteLine("===========================================");
                Console.WriteLine("1. Block & Category Management");
                Console.WriteLine("2. Contract Management");
                Console.WriteLine("3. My Contracts (as Reviewer)");
                Console.WriteLine("4. View Contract Details");
                Console.WriteLine("5. Comment on Contract");
                Console.WriteLine("6. Approve Contract");
                Console.WriteLine("0. Logout");
                Console.Write("\nSelect option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        BlockManagementMenu();
                        break;
                    case "2":
                        ContractManagementMenu();
                        break;
                    case "3":
                        ViewMyContractsAsReviewer();
                        break;
                    case "4":
                        ViewContractDetails();
                        break;
                    case "5":
                        AddCommentToContract();
                        break;
                    case "6":
                        ApproveContractMenu();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        Pause();
                        break;
                }
            }
        }

        // ==================== EXTERNAL USER MENU ====================
        private void ExternalUserMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===========================================");
                Console.WriteLine($"   EXTERNAL USER MENU - {currentUsername}");
                Console.WriteLine("===========================================");
                Console.WriteLine("1. View My Contracts");
                Console.WriteLine("2. View Contract Details");
                Console.WriteLine("3. Comment on Contract");
                Console.WriteLine("0. Logout");
                Console.Write("\nSelect option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewMyContractsAsExternal();
                        break;
                    case "2":
                        ViewContractDetailsExternal();
                        break;
                    case "3":
                        AddCommentToContractExternal();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        Pause();
                        break;
                }
            }
        }

        // ==================== ADMIN FUNCTIONS ====================
        private void CreateInternalUser()
        {
            Console.Clear();
            Console.WriteLine("=== CREATE INTERNAL USER ===\n");

            Console.Write("First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            bool success = userController.CreateInternalUser(firstName, lastName, email, username, password);
            Console.WriteLine(success ? "\n✓ Internal user created successfully!" : "\n✗ Failed to create user.");
            Pause();
        }

        private void CreateExternalUser()
        {
            Console.Clear();
            Console.WriteLine("=== CREATE EXTERNAL USER ===\n");

            Console.Write("First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Company Name: ");
            string company = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            bool success = userController.CreateExternalUser(firstName, lastName, company, email, username, password);
            Console.WriteLine(success ? "\n✓ External user created successfully!" : "\n✗ Failed to create user.");
            Pause();
        }

        private void ViewAllInternalUsers()
        {
            Console.Clear();
            Console.WriteLine("=== ALL INTERNAL USERS ===\n");

            List<InternalUser> users = userController.GetAllInternalUsers();
            if (users.Count == 0)
            {
                Console.WriteLine("No internal users found.");
            }
            else
            {
                foreach (var user in users)
                {
                    Console.WriteLine($"[{user.Int_User_ID}] {user.First_name} {user.Last_name} - {user.Username} ({user.Email})");
                }
            }
            Pause();
        }

        private void ViewAllExternalUsers()
        {
            Console.Clear();
            Console.WriteLine("=== ALL EXTERNAL USERS ===\n");

            List<ExternalUser> users = userController.GetAllExternalUsers();
            if (users.Count == 0)
            {
                Console.WriteLine("No external users found.");
            }
            else
            {
                foreach (var user in users)
                {
                    Console.WriteLine($"[{user.Ext_User_ID}] {user.First_name} {user.Last_name} - {user.Company_name} ({user.Email})");
                }
            }
            Pause();
        }

        private void DeleteInternalUser()
        {
            Console.Clear();
            Console.WriteLine("=== DELETE INTERNAL USER ===\n");
            ViewAllInternalUsers();

            Console.Write("\nEnter User ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int userId))
            {
                bool success = userController.DeleteInternalUser(userId);
                Console.WriteLine(success ? "\n✓ User deleted successfully!" : "\n✗ Failed to delete user.");
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
            Pause();
        }

        private void DeleteExternalUser()
        {
            Console.Clear();
            Console.WriteLine("=== DELETE EXTERNAL USER ===\n");
            ViewAllExternalUsers();

            Console.Write("\nEnter User ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int userId))
            {
                bool success = userController.DeleteExternalUser(userId);
                Console.WriteLine(success ? "\n✓ User deleted successfully!" : "\n✗ Failed to delete user.");
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
            Pause();
        }

        private void DeleteAdministratorVoting()
        {
            Console.Clear();
            Console.WriteLine("=== ADMINISTRATOR DELETION VOTING ===\n");

            // List all admins except current
            var admins = userController.GetAllAdministrators();
            Console.WriteLine("Administrators:");
            foreach (var admin in admins)
            {
                if (admin.Administrator_ID != currentUserId)
                    Console.WriteLine($"[{admin.Administrator_ID}] {admin.First_name} {admin.Last_name}");
            }

            Console.Write("\nEnter Administrator ID to vote for deletion: ");
            if (!int.TryParse(Console.ReadLine(), out int targetAdminId))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            if (targetAdminId == currentUserId)
            {
                Console.WriteLine("You cannot vote to delete yourself.");
                Pause();
                return;
            }

            bool voted = adminController.VoteToDeleteAdmin(targetAdminId, currentUserId);
            if (voted)
            {
                Console.WriteLine("✓ Vote recorded!");

                // Check if enough votes to delete
                bool deleted = adminController.TryDeleteAdmin(targetAdminId);
                if (deleted)
                    Console.WriteLine("\n*** Administrator REMOVED from system! ***");
            }
            else
            {
                Console.WriteLine("✗ You have already voted for this deletion.");
            }

            Pause();
        }

        // ==================== BLOCK MANAGEMENT ====================
        private void BlockManagementMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== BLOCK & CATEGORY MANAGEMENT ===");
                Console.WriteLine("1. Create Category");
                Console.WriteLine("2. View All Categories");
                Console.WriteLine("3. Create Original Block");
                Console.WriteLine("4. View All Original Blocks");
                Console.WriteLine("5. View Blocks by Category");
                Console.WriteLine("6. Copy Block");
                Console.WriteLine("7. Add Reference to Block");
                Console.WriteLine("8. Create Composite Block");
                Console.WriteLine("0. Back");
                Console.Write("\nSelect option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateCategory();
                        break;
                    case "2":
                        ViewAllCategories();
                        break;
                    case "3":
                        CreateOriginalBlock();
                        break;
                    case "4":
                        ViewAllOriginalBlocks();
                        break;
                    case "5":
                        ViewBlocksByCategory();
                        break;
                    case "6":
                        CopyBlock();
                        break;
                    case "7":
                        AddReferenceToBlockUI();
                        break;
                    case "8":
                        CreateCompositeBlockUI();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        Pause();
                        break;
                }
            }
        }

        private void CreateCategory()
        {
            Console.Clear();
            Console.WriteLine("=== CREATE CATEGORY ===\n");

            Console.Write("Category Name: ");
            string name = Console.ReadLine();
            Console.Write("Description: ");
            string description = Console.ReadLine();

            bool success = blockController.CreateCategory(name, description);
            Console.WriteLine(success ? "\n✓ Category created successfully!" : "\n✗ Failed to create category.");
            Pause();
        }

        private void ViewAllCategories()
        {
            Console.Clear();
            Console.WriteLine("=== ALL CATEGORIES ===\n");

            List<ContractBlockCategory> categories = blockController.GetAllCategories();
            if (categories.Count == 0)
            {
                Console.WriteLine("No categories found.");
            }
            else
            {
                foreach (var cat in categories)
                {
                    Console.WriteLine($"[{cat.Category_name}] - {cat.Description}");
                }
            }
            Pause();
        }

        private void CreateOriginalBlock()
        {
            Console.Clear();
            Console.WriteLine("=== CREATE ORIGINAL BLOCK ===\n");

            ViewAllCategories();

            Console.Write("\nCategory Name: ");
            string category = Console.ReadLine();
            Console.Write("Block Text: ");
            string text = Console.ReadLine();

            // References
            Console.Write("Add references to other blocks? (y/n): ");
            List<int> referenceIds = new List<int>();
            if (Console.ReadLine().ToLower() == "y")
            {
                ViewAllOriginalBlocks();
                Console.Write("\nEnter reference block IDs (comma-separated): ");
                try
                {
                    referenceIds = Console.ReadLine().Split(',')
                        .Select(id => int.Parse(id.Trim())).ToList();
                }
                catch
                {
                    Console.WriteLine("Invalid input. Skipping references.");
                }
            }

            // Composite children
            Console.Write("Add child blocks (composite)? (y/n): ");
            List<int> childIds = new List<int>();
            if (Console.ReadLine().ToLower() == "y")
            {
                ViewAllOriginalBlocks();
                Console.Write("\nEnter child block IDs (comma-separated): ");
                try
                {
                    childIds = Console.ReadLine().Split(',')
                        .Select(id => int.Parse(id.Trim())).ToList();
                }
                catch
                {
                    Console.WriteLine("Invalid input. Skipping children.");
                }
            }

            int newBlockId = blockController.CreateCompositeBlock(childIds, text, currentUserId);

            // Add references
            foreach (var refId in referenceIds)
            {
                blockController.AddReferenceToBlock(newBlockId, refId);
            }

            Console.WriteLine(newBlockId > 0 ? "\n✓ Block created successfully!" : "\n✗ Failed to create block.");
            Pause();
        }

        private void ViewAllOriginalBlocks()
        {
            Console.Clear();
            Console.WriteLine("=== ALL ORIGINAL BLOCKS ===\n");

            List<OriginalContractBlock> blocks = blockController.GetAllOriginalBlocks();
            if (blocks.Count == 0)
            {
                Console.WriteLine("No blocks found.");
            }
            else
            {
                foreach (var block in blocks)
                {
                    Console.WriteLine($"\n[{block.Org_Cont_ID}] Category: {block.Category_name}");
                    Console.WriteLine($"Text: {block.Contract_text}");
                    Console.WriteLine($"Created: {block.Created_date:yyyy-MM-dd}");
                }
            }
            Pause();
        }

        private void ViewBlocksByCategory()
        {
            Console.Clear();
            Console.WriteLine("=== VIEW BLOCKS BY CATEGORY ===\n");

            ViewAllCategories();

            Console.Write("\nEnter Category Name: ");
            string category = Console.ReadLine();

            List<OriginalContractBlock> blocks = blockController.GetOriginalBlocksByCategory(category);
            if (blocks.Count == 0)
            {
                Console.WriteLine("\nNo blocks found in this category.");
            }
            else
            {
                foreach (var block in blocks)
                {
                    Console.WriteLine($"\n[{block.Org_Cont_ID}] {block.Contract_text}");
                }
            }
            Pause();
        }

        private void CopyBlock()
        {
            Console.Clear();
            Console.WriteLine("=== COPY BLOCK ===\n");

            ViewAllOriginalBlocks();

            Console.Write("\nEnter Original Block ID to copy: ");
            if (int.TryParse(Console.ReadLine(), out int blockId))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            bool success = blockController.CopyOriginalBlock(blockId, currentUserId);

            if (success)
            {
                var original = blockController.GetAllOriginalBlocks().FirstOrDefault(b => b.Org_Cont_ID == blockId);
                var newBlockId = blockController.GetAllOriginalBlocks().Max(b => b.Org_Cont_ID);
            }

            Console.WriteLine(success ? "\n✓ Block copied successfully!" : "\n✗ Failed to copy block.");
            Pause();
        }

        private void AddReferenceToBlockUI()
        {
            Console.Clear();
            Console.WriteLine("=== ADD REFERENCE TO BLOCK ===\n");
            ViewAllOriginalBlocks();

            Console.Write("\nEnter Block ID to add reference to: ");
            if (!int.TryParse(Console.ReadLine(), out int blockId)) return;

            Console.Write("Enter Reference Block ID: ");
            if (!int.TryParse(Console.ReadLine(), out int refId)) return;

            bool success = blockController.AddReferenceToBlock(blockId, refId);
            Console.WriteLine(success ? "\n✓ Reference added!" : "\n✗ Failed to add reference.");
            Pause();
        }

        private void CreateCompositeBlockUI()
        {
            Console.Clear();
            Console.WriteLine("=== CREATE COMPOSITE BLOCK ===\n");
            ViewAllOriginalBlocks();

            Console.Write("\nEnter IDs of child blocks (comma-separated): ");
            var idsInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(idsInput)) return;

            List<int> childIds;
            try
            {
                childIds = idsInput.Split(',').Select(id => int.Parse(id.Trim())).ToList();
            }
            catch
            {
                Console.WriteLine("Invalid input.");
                Pause();
                return;
            }

            Console.Write("Enter text for composite block: ");
            string text = Console.ReadLine();

            int newId = blockController.CreateCompositeBlock(childIds, text, currentUserId);
            Console.WriteLine(newId > 0 ? $"\n✓ Composite block created! ID: {newId}" : "\n✗ Failed to create composite block.");
            Pause();
        }

        // ==================== CONTRACT MANAGEMENT ====================
        private void ContractManagementMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== CONTRACT MANAGEMENT ===");
                Console.WriteLine("1. Create New Contract");
                Console.WriteLine("2. View My Contracts");
                Console.WriteLine("3. Add Block to Contract");
                Console.WriteLine("4. Remove Block from Contract");
                Console.WriteLine("5. Edit Block in Contract");
                Console.WriteLine("6. Invite Internal Reviewers");
                Console.WriteLine("7. Invite External Users");
                Console.WriteLine("0. Back");
                Console.Write("\nSelect option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateContract();
                        break;
                    case "2":
                        ViewMyContracts();
                        break;
                    case "3":
                        AddBlockToContract();
                        break;
                    case "4":
                        RemoveBlockFromContract();
                        break;
                    case "5":
                        EditBlockInContract();
                        break;
                    case "6":
                        InviteInternalReviewers();
                        break;
                    case "7":
                        InviteExternalUsers();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        Pause();
                        break;
                }
            }
        }

        private void CreateContract()
        {
            Console.Clear();
            Console.WriteLine("=== CREATE NEW CONTRACT ===\n");

            Console.Write("Company Name: ");
            string company = Console.ReadLine();

            int contractId = contractController.CreateContract(company, currentUserId);
            if (contractId > 0)
            {
                Console.WriteLine($"\n✓ Contract created successfully! Contract ID: {contractId}");
            }
            else
            {
                Console.WriteLine("\n✗ Failed to create contract.");
            }
            Pause();
        }

        private void ViewMyContracts()
        {
            Console.Clear();
            Console.WriteLine("=== MY CONTRACTS ===\n");

            List<Contract> contracts = contractController.GetContractsByCreator(currentUserId);
            if (contracts.Count == 0)
            {
                Console.WriteLine("No contracts found.");
            }
            else
            {
                foreach (var contract in contracts)
                {
                    string status = contract.Approved ? "✓ Approved" : "Pending";
                    string sent = contract.Sent_to_external ? "Sent to Client" : "Internal";
                    Console.WriteLine($"\n[{contract.Contract_NR}] {contract.Company_name}");
                    Console.WriteLine($"    Status: {status} | {sent}");
                    Console.WriteLine($"    Created: {contract.Created_date:yyyy-MM-dd}");
                }
            }
            Pause();
        }

        private void AddBlockToContract()
        {
            Console.Clear();
            Console.WriteLine("=== ADD BLOCK TO CONTRACT ===\n");

            Console.Write("Enter Contract ID: ");
            if (!int.TryParse(Console.ReadLine(), out int contractId)) return;

            ViewAllOriginalBlocks();

            Console.Write("\nEnter Original Block ID to add: ");
            if (!int.TryParse(Console.ReadLine(), out int blockId)) return;

            bool success = contractController.AddBlockToContract(contractId, blockId);
            Console.WriteLine(success ? "\n✓ Block added to contract!" : "\n✗ Failed to add block.");
            Pause();
        }

        private void RemoveBlockFromContract()
        {
            Console.Clear();
            Console.WriteLine("=== REMOVE BLOCK FROM CONTRACT ===\n");

            Console.Write("Enter Contract ID: ");
            if (!int.TryParse(Console.ReadLine(), out int contractId)) return;

            var blocks = contractController.GetContractBlocks(contractId);
            foreach (var block in blocks)
            {
                Console.WriteLine($"[{block.Contract_Block_NR}] {block.Contract_text.Substring(0, Math.Min(50, block.Contract_text.Length))}...");
            }

            Console.Write("\nEnter Block ID to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int blockId)) return;

            bool success = contractController.RemoveBlockFromContract(contractId, blockId);
            Console.WriteLine(success ? "\n✓ Block removed!" : "\n✗ Failed to remove block.");
            Pause();
        }

        private void EditBlockInContract()
        {
            Console.Clear();
            Console.WriteLine("=== EDIT BLOCK IN CONTRACT ===\n");

            Console.Write("Enter Contract ID: ");
            if (!int.TryParse(Console.ReadLine(), out int contractId)) return;

            var blocks = contractController.GetContractBlocks(contractId);
            foreach (var block in blocks)
            {
                Console.WriteLine($"\n[{block.Contract_Block_NR}] {block.Contract_text}");
            }

            Console.Write("\nEnter Block ID to edit: ");
            if (!int.TryParse(Console.ReadLine(), out int blockId)) return;

            Console.Write("New Text: ");
            string newText = Console.ReadLine();

            bool success = contractController.EditBlockInContract(blockId, newText);
            Console.WriteLine(success ? "\n✓ Block updated!" : "\n✗ Failed to update block.");
            Pause();
        }

        private void InviteInternalReviewers()
        {
            Console.Clear();
            Console.WriteLine("=== INVITE INTERNAL REVIEWERS ===\n");

            Console.Write("Enter Contract ID: ");
            if (!int.TryParse(Console.ReadLine(), out int contractId)) return;

            ViewAllInternalUsers();

            Console.Write("\nEnter User ID to invite: ");
            if (!int.TryParse(Console.ReadLine(), out int userId)) return;

            Console.Write("Grant approval rights? (y/n): ");
            bool approvalRights = Console.ReadLine().ToLower() == "y";

            bool success = contractController.InviteInternalReviewer(contractId, userId, approvalRights);
            Console.WriteLine(success ? "\n✓ Reviewer invited!" : "\n✗ Failed to invite reviewer.");
            Pause();
        }

        private void InviteExternalUsers()
        {
            Console.Clear();
            Console.WriteLine("=== INVITE EXTERNAL USERS ===\n");

            Console.Write("Enter Contract ID: ");
            if (!int.TryParse(Console.ReadLine(), out int contractId)) return;

            // Check if contract is approved
            if (!approvalController.IsContractFullyApproved(contractId))
            {
                Console.WriteLine("\n✗ Contract must be fully approved before inviting external users.");
                Pause();
                return;
            }

            ViewAllExternalUsers();

            Console.Write("\nEnter External User ID to invite: ");
            if (!int.TryParse(Console.ReadLine(), out int userId)) return;

            bool success = contractController.InviteExternalUser(contractId, userId);
            Console.WriteLine(success ? "\n✓ External user invited!" : "\n✗ Failed to invite user.");
            Pause();
        }

        // ==================== CONTRACT VIEWING ====================
        private void ViewMyContractsAsReviewer()
        {
            Console.Clear();
            Console.WriteLine("=== MY CONTRACTS (AS REVIEWER) ===\n");

            List<Contract> contracts = contractController.GetContractsByStakeholder(currentUserId);
            if (contracts.Count == 0)
            {
                Console.WriteLine("No contracts assigned for review.");
            }
            else
            {
                foreach (var contract in contracts)
                {
                    Console.WriteLine($"\n[{contract.Contract_NR}] {contract.Company_name}");
                    Console.WriteLine($"    Status: {(contract.Approved ? "✓ Approved" : "Pending")}");
                    Console.WriteLine($"    Created: {contract.Created_date:yyyy-MM-dd}");
                }
            }
            Pause();
        }

        private void ViewContractDetails()
        {
            Console.Clear();
            Console.WriteLine("=== VIEW CONTRACT DETAILS ===\n");

            Console.Write("Enter Contract ID: ");
            if (!int.TryParse(Console.ReadLine(), out int contractId)) return;

            var contract = contractController.GetContractById(contractId);
            if (contract == null)
            {
                Console.WriteLine("Contract not found.");
                Pause();
                return;
            }

            Console.WriteLine($"\nContract: {contract.Company_name}");
            Console.WriteLine($"Status: {(contract.Approved ? "✓ Approved" : "Pending")}");
            Console.WriteLine($"Created: {contract.Created_date:yyyy-MM-dd}\n");

            var blocks = contractController.GetContractBlocks(contractId);
            Console.WriteLine("=== CONTRACT CONTENT ===");
            int sectionNum = 1;
            foreach (var block in blocks)
            {
                Console.WriteLine($"\n{sectionNum}. {block.Contract_text}");
                sectionNum++;
            }

            // Show internal comments
            var comments = commentController.GetInternalComments(contractId);
            if (comments.Count > 0)
            {
                Console.WriteLine("\n=== INTERNAL COMMENTS ===");
                foreach (var comment in comments)
                {
                    Console.WriteLine($"[{comment.Comment_date:yyyy-MM-dd HH:mm}] User {comment.User_ID}: {comment.Comment_text}");
                }
            }

            Pause();
        }

        private void ViewMyContractsAsExternal()
        {
            Console.Clear();
            Console.WriteLine("=== MY CONTRACTS ===\n");

            List<Contract> contracts = contractController.GetContractsByExternalUser(currentUserId);
            if (contracts.Count == 0)
            {
                Console.WriteLine("No contracts available.");
            }
            else
            {
                foreach (var contract in contracts)
                {
                    Console.WriteLine($"\n[{contract.Contract_NR}] {contract.Company_name}");
                    Console.WriteLine($"    Created: {contract.Created_date:yyyy-MM-dd}");
                }
            }
            Pause();
        }

        private void ViewContractDetailsExternal()
        {
            Console.Clear();
            Console.WriteLine("=== VIEW CONTRACT DETAILS ===\n");

            Console.Write("Enter Contract ID: ");
            if (!int.TryParse(Console.ReadLine(), out int contractId)) return;

            var contract = contractController.GetContractById(contractId);
            if (contract == null)
            {
                Console.WriteLine("Contract not found.");
                Pause();
                return;
            }

            Console.WriteLine($"\nContract: {contract.Company_name}");
            Console.WriteLine($"Created: {contract.Created_date:yyyy-MM-dd}\n");

            var blocks = contractController.GetContractBlocks(contractId);
            Console.WriteLine("=== CONTRACT CONTENT ===");
            int sectionNum = 1;
            foreach (var block in blocks)
            {
                Console.WriteLine($"\n{sectionNum}. {block.Contract_text}");
                sectionNum++;
            }

            // Show only own comments
            var comments = commentController.GetExternalCommentsByUser(contractId, currentUserId);
            if (comments.Count > 0)
            {
                Console.WriteLine("\n=== MY COMMENTS ===");
                foreach (var comment in comments)
                {
                    Console.WriteLine($"[{comment.Comment_date:yyyy-MM-dd HH:mm}] {comment.Comment_text}");
                }
            }

            Pause();
        }

        // ==================== COMMENTS ====================
        private void AddCommentToContract()
        {
            Console.Clear();
            Console.WriteLine("=== ADD COMMENT ===\n");

            Console.Write("Enter Contract ID: ");
            if (!int.TryParse(Console.ReadLine(), out int contractId)) return;

            Console.Write("Comment on specific block? (y/n): ");
            int? blockId = null;
            if (Console.ReadLine().ToLower() == "y")
            {
                Console.Write("Enter Block ID: ");
                if (int.TryParse(Console.ReadLine(), out int bid))
                    blockId = bid;
            }

            Console.Write("Comment: ");
            string text = Console.ReadLine();

            bool success = commentController.AddComment(contractId, blockId, currentUserId, "internal", text);
            Console.WriteLine(success ? "\n✓ Comment added!" : "\n✗ Failed to add comment.");
            Pause();
        }

        private void AddCommentToContractExternal()
        {
            Console.Clear();
            Console.WriteLine("=== ADD COMMENT ===\n");

            Console.Write("Enter Contract ID: ");
            if (!int.TryParse(Console.ReadLine(), out int contractId)) return;

            Console.Write("Comment on specific section? (y/n): ");
            int? blockId = null;
            if (Console.ReadLine().ToLower() == "y")
            {
                Console.Write("Enter Section Number: ");
                if (int.TryParse(Console.ReadLine(), out int bid))
                    blockId = bid;
            }

            Console.Write("Comment: ");
            string text = Console.ReadLine();

            bool success = commentController.AddComment(contractId, blockId, currentUserId, "external", text);
            Console.WriteLine(success ? "\n✓ Comment added!" : "\n✗ Failed to add comment.");
            Pause();
        }

        // ==================== APPROVAL ====================
        private void ApproveContractMenu()
        {
            Console.Clear();
            Console.WriteLine("=== APPROVE CONTRACT ===\n");

            List<Contract> contracts = contractController.GetContractsByStakeholder(currentUserId);
            if (contracts.Count == 0)
            {
                Console.WriteLine("No contracts pending your approval.");
                Pause();
                return;
            }

            foreach (var contract in contracts)
            {
                Console.WriteLine($"[{contract.Contract_NR}] {contract.Company_name} - {(contract.Approved ? "Approved" : "Pending")}");
            }

            Console.Write("\nEnter Contract ID to approve: ");
            if (!int.TryParse(Console.ReadLine(), out int contractId)) return;

            bool success = approvalController.ApproveContract(contractId, currentUserId);
            Console.WriteLine(success ? "\n✓ Contract approved!" : "\n✗ Failed to approve contract.");
            Pause();
        }

        // ==================== HELPER METHODS ====================
        private void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}