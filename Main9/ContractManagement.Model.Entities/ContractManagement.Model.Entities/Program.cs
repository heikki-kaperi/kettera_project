using System;
using System.Collections.Generic;
using System.Linq;
using ContractManagement.Controller;
using ContractManagement.Model.Entities;
using ContractManagement.Utils;

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

    //LISÄSIN TÄMÄN
    // ============= ABSTRACT USER SESSION ================
    public abstract class UserSession
    {
        protected string Username;
        protected int UserId;

        public UserSession(int userId, string username)
        {
            UserId = userId;
            Username = username;
        }

        public abstract void ShowMenu();

        protected void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        protected int ReadInt(string prompt)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int result))
                return result;
            return -1;
        }
    }

    // ================== ADMIN SESSION ===================
    public class AdminSession : UserSession
    {
        private UserController _userController = new UserController();
        private AdministratorController _adminController = new AdministratorController();

        public AdminSession(int userId, string username) : base(userId, username) { }

        public override void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===========================================");
                Console.WriteLine($"   ADMINISTRATOR MENU - {Username}");
                Console.WriteLine("===========================================");
                Console.WriteLine("1. Create Internal User");
                Console.WriteLine("2. Create External User");
                Console.WriteLine("3. View All Internal Users");
                Console.WriteLine("4. View All External Users");
                Console.WriteLine("5. Delete Internal User");
                Console.WriteLine("6. Delete External User");
                Console.WriteLine("7. Delete Administrator (3 votes)");
                Console.WriteLine("8. Migrate Passwords to Hashed Format");
                Console.WriteLine("0. Logout");
                Console.Write("\nSelect option: ");

                switch (Console.ReadLine())
                {
                    case "1": CreateInternalUser(); break;
                    case "2": CreateExternalUser(); break;
                    case "3": ViewAllInternalUsers(); break;
                    case "4": ViewAllExternalUsers(); break;
                    case "5": DeleteInternalUser(); break;
                    case "6": DeleteExternalUser(); break;
                    case "7": DeleteAdministratorVoting(); break;
                    case "8": MigratePasswords(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid option."); Pause(); break;
                }
            }
        }

        private void CreateInternalUser()
        {
            Console.Clear();
            Console.WriteLine("=== CREATE INTERNAL USER ===\n");
            Console.Write("First Name: "); string firstName = Console.ReadLine();
            Console.Write("Last Name: "); string lastName = Console.ReadLine();
            Console.Write("Email: "); string email = Console.ReadLine();
            Console.Write("Username: "); string username = Console.ReadLine();
            Console.Write("Password: "); string password = Console.ReadLine();

            bool success = _userController.CreateInternalUser(firstName, lastName, email, username, password);
            Console.WriteLine(success ? "\n✓ Internal user created successfully!" : "\n✗ Failed to create user.");
            Pause();
        }

        private void CreateExternalUser()
        {
            Console.Clear();
            Console.WriteLine("=== CREATE EXTERNAL USER ===\n");
            Console.Write("First Name: "); string firstName = Console.ReadLine();
            Console.Write("Last Name: "); string lastName = Console.ReadLine();
            Console.Write("Company Name: "); string company = Console.ReadLine();
            Console.Write("Email: "); string email = Console.ReadLine();
            Console.Write("Username: "); string username = Console.ReadLine();
            Console.Write("Password: "); string password = Console.ReadLine();

            bool success = _userController.CreateExternalUser(firstName, lastName, company, email, username, password);
            Console.WriteLine(success ? "\n✓ External user created successfully!" : "\n✗ Failed to create user.");
            Pause();
        }

        private void ViewAllInternalUsers()
        {
            Console.Clear();
            Console.WriteLine("=== ALL INTERNAL USERS ===\n");
            var users = _userController.GetAllInternalUsers();
            if (users.Count == 0) Console.WriteLine("No internal users found.");
            else foreach (var u in users)
                    Console.WriteLine($"[{u.Int_User_ID}] {u.First_name} {u.Last_name} - {u.Username} ({u.Email})");
            Pause();
        }

        private void ViewAllExternalUsers()
        {
            Console.Clear();
            Console.WriteLine("=== ALL EXTERNAL USERS ===\n");
            var users = _userController.GetAllExternalUsers();
            if (users.Count == 0) Console.WriteLine("No external users found.");
            else foreach (var u in users)
                    Console.WriteLine($"[{u.Ext_User_ID}] {u.First_name} {u.Last_name} - {u.Company_name} ({u.Email})");
            Pause();
        }

        private void DeleteInternalUser()
        {
            ViewAllInternalUsers();
            Console.Write("\nEnter User ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                bool success = _userController.DeleteInternalUser(id);
                Console.WriteLine(success ? "\n✓ User deleted successfully!" : "\n✗ Failed to delete user.");
            }
            else Console.WriteLine("Invalid ID.");
            Pause();
        }

        private void DeleteExternalUser()
        {
            ViewAllExternalUsers();
            Console.Write("\nEnter User ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                bool success = _userController.DeleteExternalUser(id);
                Console.WriteLine(success ? "\n✓ User deleted successfully!" : "\n✗ Failed to delete user.");
            }
            else Console.WriteLine("Invalid ID.");
            Pause();
        }

        private void DeleteAdministratorVoting()
        {
            Console.Clear();
            Console.WriteLine("=== ADMINISTRATOR DELETION VOTING ===\n");

            // List all admins except current
            var admins = _userController.GetAllAdministrators();
            Console.WriteLine("Administrators:");
            foreach (var admin in admins)
            {
                if (admin.Administrator_ID != UserId)
                    Console.WriteLine($"[{admin.Administrator_ID}] {admin.First_name} {admin.Last_name}");
            }

            Console.Write("\nEnter Administrator ID to vote for deletion: ");
            if (!int.TryParse(Console.ReadLine(), out int targetAdminId))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            if (targetAdminId == UserId)
            {
                Console.WriteLine("You cannot vote to delete yourself.");
                Pause();
                return;
            }

            bool voted = _adminController.VoteToDeleteAdmin(targetAdminId, UserId);
            if (voted)
            {
                Console.WriteLine("✓ Vote recorded!");

                // Check if enough votes to delete
                bool deleted = _adminController.TryDeleteAdmin(targetAdminId);
                if (deleted)
                    Console.WriteLine("\n*** Administrator REMOVED from system! ***");
            }
            else
            {
                Console.WriteLine("✗ You have already voted for this deletion.");
            }

            Pause();
        }

        private void MigratePasswords()
        {
            Console.Clear();
            PasswordHelper.MigrateAllPasswords();
            Pause();
        }

    }

    // ================ INTERNAL USER SESSION =================
    public class InternalUserSession : UserSession
    {
        private BlockController _blockController = new BlockController();
        private ContractController _contractController = new ContractController();
        private CommentController _commentController = new CommentController();
        private ApprovalController _approvalController = new ApprovalController();

        public InternalUserSession(int userId, string username) : base(userId, username) { }

        public override void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===========================================");
                Console.WriteLine($"   INTERNAL USER MENU - {Username}");
                Console.WriteLine("===========================================");
                Console.WriteLine("1. Block & Category Management");
                Console.WriteLine("2. Contract Management");
                Console.WriteLine("3. My Contracts (as Reviewer)");
                Console.WriteLine("4. View Contract Details");
                Console.WriteLine("5. Comment on Contract");
                Console.WriteLine("6. Approve Contract");
                Console.WriteLine("0. Logout");
                Console.Write("\nSelect option: ");

                switch (Console.ReadLine())
                {
                    case "1": BlockManagementMenu(); break;
                    case "2": ContractManagementMenu(); break;
                    case "3": ViewMyContractsAsReviewer(); break;
                    case "4": ViewContractDetails(); break;
                    case "5": AddCommentToContract(); break;
                    case "6": ApproveContractMenu(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid option."); Pause(); break;
                }
            }
        }

        // ======= BLOCK MANAGEMENT ========
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

                switch (Console.ReadLine())
                {
                    case "1": CreateCategory(); break;
                    case "2": ViewAllCategories(); break;
                    case "3": CreateOriginalBlock(); break;
                    case "4": ViewAllOriginalBlocks(); break;
                    case "5": ViewBlocksByCategory(); break;
                    case "6": CopyBlock(); break;
                    case "7": AddReferenceToBlockUI(); break;
                    case "8": CreateCompositeBlockUI(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid option."); Pause(); break;
                }
            }
        }

        private void CreateCategory()
        {
            Console.Clear();
            Console.WriteLine("=== CREATE CATEGORY ===\n");
            Console.Write("Category Name: "); string name = Console.ReadLine();
            Console.Write("Description: "); string description = Console.ReadLine();
            bool success = _blockController.CreateCategory(name, description);
            Console.WriteLine(success ? "\n✓ Category created successfully!" : "\n✗ Failed to create category.");
            Pause();
        }

        private void ViewAllCategories()
        {
            Console.Clear();
            Console.WriteLine("=== ALL CATEGORIES ===\n");
            var categories = _blockController.GetAllCategories();
            if (categories.Count == 0) Console.WriteLine("No categories found.");
            else foreach (var c in categories)
                    Console.WriteLine($"[{c.Category_name}] - {c.Description}");
            Pause();
        }

        private void CreateOriginalBlock()
        {
            ViewAllCategories();
            Console.Write("\nCategory Name: "); string category = Console.ReadLine();
            // Select block type
            Console.WriteLine("\n=== SELECT BLOCK TYPE ===");
            Console.WriteLine("1. Text Block");
            Console.WriteLine("2. Preformatted Section (e.g., address, signature space)");
            Console.WriteLine("3. Image Block");
            Console.Write("Choice: ");
            string typeChoice = Console.ReadLine();

            BlockType blockType = BlockType.Text;
            string text = "";
            byte[] mediaContent = null;

            switch (typeChoice)
            {
                case "1":
                    blockType = BlockType.Text;
                    Console.Write("\nBlock Text: ");
                    text = Console.ReadLine();
                    break;

                case "2":
                    blockType = BlockType.Text;
                    Console.WriteLine("\n=== PREFORMATTED SECTION TEMPLATES ===");
                    Console.WriteLine("1. Address Section");
                    Console.WriteLine("2. Signature Section");
                    Console.WriteLine("3. Date Section");
                    Console.WriteLine("4. Custom Preformatted");
                    Console.Write("Choice: ");
                    string templateChoice = Console.ReadLine();

                    switch (templateChoice)
                    {
                        case "1":
                            text = "[ADDRESS SECTION]\n" +
                                   "Company: _______________________________\n" +
                                   "Street: ________________________________\n" +
                                   "City: __________________________________\n" +
                                   "Postal Code: ___________________________\n" +
                                   "Country: _______________________________";
                            break;
                        case "2":
                            text = "[SIGNATURE SECTION]\n\n" +
                                   "Signed: ________________________________\n\n" +
                                   "Name: __________________________________\n\n" +
                                   "Title: _________________________________\n\n" +
                                   "Date: __________________________________";
                            break;
                        case "3":
                            text = "[DATE SECTION]\n" +
                                   "Date: __________________________________\n" +
                                   "Place: _________________________________";
                            break;
                        case "4":
                            Console.Write("\nEnter your preformatted text: ");
                            text = Console.ReadLine();
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Using default signature template.");
                            text = "[SIGNATURE SECTION]\n\nSigned: ________________________________";
                            break;
                    }
                    Console.WriteLine("\n✓ Preformatted section created!");
                    break;

                case "3":
                    blockType = BlockType.Image;
                    Console.Write("\nEnter image file path: ");
                    string imagePath = Console.ReadLine();

                    if (System.IO.File.Exists(imagePath))
                    {
                        try
                        {
                            mediaContent = System.IO.File.ReadAllBytes(imagePath);
                            text = $"[IMAGE: {System.IO.Path.GetFileName(imagePath)}]";
                            Console.WriteLine("✓ Image loaded successfully!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"✗ Error loading image: {ex.Message}");
                            Console.WriteLine("Creating text block instead.");
                            blockType = BlockType.Text;
                            Console.Write("Block Text: ");
                            text = Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("✗ File not found. Creating text block instead.");
                        blockType = BlockType.Text;
                        Console.Write("Block Text: ");
                        text = Console.ReadLine();
                    }
                    break;

                default:
                    Console.WriteLine("Invalid choice. Using text block.");
                    Console.Write("\nBlock Text: ");
                    text = Console.ReadLine();
                    break;
            }

            // Create the original block through BlockController
            var originalBlock = new OriginalContractBlock
            {
                Category_name = category,
                Contract_text = text,
                Created_by = UserId,
                Created_date = DateTime.Now
            };

            int newBlockId = _blockController.CreateOriginalBlockWithType(originalBlock, blockType, mediaContent);

            if (newBlockId > 0)
            {
                // References
                Console.Write("\nAdd references to other blocks? (y/n): ");
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

                    // Add references
                    foreach (var refId in referenceIds)
                    {
                        _blockController.AddReferenceToBlock(newBlockId, refId);
                    }
                }
            }

            Console.WriteLine(newBlockId > 0 ? "\n✓ Block created successfully!" : "\n✗ Failed to create block.");
            Pause();
        }

        private void ViewAllOriginalBlocks()
        {
            Console.Clear();
            Console.WriteLine("=== ALL ORIGINAL BLOCKS ===\n");
            var blocks = _blockController.GetAllOriginalBlocks();
            if (blocks.Count == 0) Console.WriteLine("No blocks found.");
            else
            {
                foreach (var b in blocks)
                {
                    string typeIndicator = "";
                    if (b.Contract_text.Contains("[IMAGE:"))
                        typeIndicator = " [IMAGE]";
                    else if (b.Contract_text.Contains("[ADDRESS SECTION]") ||
                             b.Contract_text.Contains("[SIGNATURE SECTION]") ||
                             b.Contract_text.Contains("[DATE SECTION]"))
                        typeIndicator = " [PREFORMATTED]";

                    Console.WriteLine($"\n[{b.Org_Cont_ID}] Category: {b.Category_name}{typeIndicator}");

                    // Truncate long text for display
                    string displayText = b.Contract_text.Length > 100
                        ? b.Contract_text.Substring(0, 100) + "..."
                        : b.Contract_text;
                    Console.WriteLine($"Text: {displayText}");
                    Console.WriteLine($"Created: {b.Created_date:yyyy-MM-dd}");
                }
            }
            Pause();
        }

        private void ViewBlocksByCategory()
        {
            ViewAllCategories();
            Console.Write("\nEnter Category Name: "); string category = Console.ReadLine();
            var blocks = _blockController.GetOriginalBlocksByCategory(category);
            if (blocks.Count == 0) Console.WriteLine("\nNo blocks found in this category.");
            else
            {
                foreach (var b in blocks)
                {
                    string typeIndicator = "";
                    if (b.Contract_text.Contains("[IMAGE:"))
                        typeIndicator = " [IMAGE]";
                    else if (b.Contract_text.Contains("[ADDRESS SECTION]") ||
                             b.Contract_text.Contains("[SIGNATURE SECTION]") ||
                             b.Contract_text.Contains("[DATE SECTION]"))
                        typeIndicator = " [PREFORMATTED]";

                    Console.WriteLine($"\n[{b.Org_Cont_ID}]{typeIndicator} {b.Contract_text}");
                }
            }
            Pause();
        }

        private void CopyBlock()
        {
            ViewAllOriginalBlocks();
            Console.Write("\nEnter Original Block ID to copy: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }
            bool success = _blockController.CopyOriginalBlock(id, UserId);

            if (success)
            {
                var original = _blockController.GetAllOriginalBlocks().FirstOrDefault(b => b.Org_Cont_ID == id);
                var newBlockId = _blockController.GetAllOriginalBlocks().Max(b => b.Org_Cont_ID);
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

            bool success = _blockController.AddReferenceToBlock(blockId, refId);
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

            int newId = _blockController.CreateCompositeBlock(childIds, text, UserId);
            Console.WriteLine(newId > 0 ? $"\n✓ Composite block created! ID: {newId}" : "\n✗ Failed to create composite block.");

            Pause();
        }

        // ========== CONTRACT MANAGEMENT ==========
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

                switch (Console.ReadLine())
                {
                    case "1": CreateContract(); break;
                    case "2": ViewMyContracts(); break;
                    case "3": AddBlockToContract(); break;
                    case "4": RemoveBlockFromContract(); break;
                    case "5": EditBlockInContract(); break;
                    case "6": InviteInternalReviewers(); break;
                    case "7": InviteExternalUsers(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid option."); Pause(); break;
                }
            }
        }

        // ============ CONTRACT METHODS ====================
        private void CreateContract()
        {
            Console.Clear();
            Console.WriteLine("=== CREATE NEW CONTRACT ===");
            Console.Write("Contract Name: "); string name = Console.ReadLine();

            int contractId = _contractController.CreateContract(name, UserId);

            bool success = contractId > 0;
            Console.WriteLine(success ? "\n✓ Contract created successfully!" : "\n✗ Failed to create contract.");
            Pause();
        }

        private void ViewMyContracts()
        {
            Console.Clear();
            Console.WriteLine("=== MY CONTRACTS ===\n");
            var contracts = _contractController.GetContractsByInternalUser(UserId);

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

            bool continueAdding = true;

            while (continueAdding)
            {
                ViewAllOriginalBlocks();

                Console.Write("\nEnter Original Block ID to add (or 0 to finish): ");
                if (!int.TryParse(Console.ReadLine(), out int blockId) || blockId == 0)
                    break;

                bool success = _contractController.AddBlockToContract(contractId, blockId);

                if (success)
                {
                    Console.WriteLine("\n✓ Block added to contract!");
                    ShowRecommendations(contractId);

                    Console.Write("\nAdd another block? (y/n): ");
                    continueAdding = Console.ReadLine()?.ToLower() == "y";
                }
                else
                {
                    Console.WriteLine("\n✗ Failed to add block.");
                    continueAdding = false;
                }
            }

            Pause();
        }

        private void ShowRecommendations(int contractId)
        {
            var recommendations = _contractController.GetContractRecommendations(contractId, 5);

            if (recommendations.Count > 0)
            {
                Console.WriteLine("\n📋 Recommended blocks based on your current selection:");
                Console.WriteLine("".PadRight(70, '='));

                foreach (var rec in recommendations)
                {
                    string typeIndicator = "";
                    if (rec.BlockText.Contains("[IMAGE:"))
                        typeIndicator = " [IMAGE]";
                    else if (rec.BlockText.Contains("[ADDRESS SECTION]") ||
                             rec.BlockText.Contains("[SIGNATURE SECTION]") ||
                             rec.BlockText.Contains("[DATE SECTION]"))
                        typeIndicator = " [PREFORMATTED]";

                    Console.WriteLine($"\nBlock ID: {rec.BlockId} | Category: {rec.Category} | Score: {rec.Score}{typeIndicator}");

                    string preview = rec.BlockText.Length > 80
                        ? rec.BlockText.Substring(0, 80) + "..."
                        : rec.BlockText;
                    Console.WriteLine($"  Text: {preview}");
                }

                Console.WriteLine("".PadRight(70, '='));
            }
            else
            {
                Console.WriteLine("\n(No recommendations available yet - add more contracts to build the recommendation system)");
            }
        }

        private void RemoveBlockFromContract()
        {
            Console.Clear();
            Console.WriteLine("=== REMOVE BLOCK FROM CONTRACT ===\n");

            Console.Write("Enter Contract ID: ");
            if (!int.TryParse(Console.ReadLine(), out int contractId)) return;

            var blocks = _contractController.GetContractBlocks(contractId);
            foreach (var block in blocks)
            {
                string typeIndicator = "";
                if (block.Contract_text.Contains("[IMAGE:"))
                    typeIndicator = " [IMAGE]";
                else if (block.Contract_text.Contains("[ADDRESS SECTION]") ||
                         block.Contract_text.Contains("[SIGNATURE SECTION]") ||
                         block.Contract_text.Contains("[DATE SECTION]"))
                    typeIndicator = " [PREFORMATTED]";

                Console.WriteLine($"[{block.Contract_Block_NR}]{typeIndicator} {block.Contract_text.Substring(0, Math.Min(50, block.Contract_text.Length))}...");
            }

            Console.Write("\nEnter Block ID to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int blockId)) return;

            bool success = _contractController.RemoveBlockFromContract(contractId, blockId);
            Console.WriteLine(success ? "\n✓ Block removed!" : "\n✗ Failed to remove block.");
            Pause();
        }

        private void EditBlockInContract()
        {
            ViewMyContracts();
            int contractId = ReadInt("\nEnter Contract ID: ");
            var blocks = _contractController.GetBlocksByContract(contractId);
            if (blocks.Count == 0) { Console.WriteLine("No blocks in this contract."); Pause(); return; }
            foreach (var b in blocks)
            {
                string typeIndicator = "";
                if (b.Contract_text.Contains("[IMAGE:"))
                    typeIndicator = " [IMAGE]";
                else if (b.Contract_text.Contains("[ADDRESS SECTION]") ||
                         b.Contract_text.Contains("[SIGNATURE SECTION]") ||
                         b.Contract_text.Contains("[DATE SECTION]"))
                    typeIndicator = " [PREFORMATTED]";

                Console.WriteLine($"[{b.Contract_Block_NR}]{typeIndicator} {b.Contract_text}");
            }
            int blockId = ReadInt("\nEnter Block ID to edit: ");
            Console.Write("New Block Text: "); string newText = Console.ReadLine();
            bool success = _contractController.EditBlockInContract(contractId, blockId, newText);
            Console.WriteLine(success ? "\n✓ Block edited!" : "\n✗ Failed to edit block.");
            Pause();
        }

        private void InviteInternalReviewers()
        {
            ViewMyContracts();
            int contractId = ReadInt("\nEnter Contract ID: ");
            var reviewers = new UserController().GetAllInternalUsers();
            foreach (var r in reviewers) Console.WriteLine($"[{r.Int_User_ID}] {r.First_name} {r.Last_name}");
            int reviewerId = ReadInt("\nEnter Reviewer ID to invite: ");
            bool success = _contractController.InviteInternalReviewer(contractId, reviewerId, false);
            Console.WriteLine(success ? "\n✓ Reviewer invited!" : "\n✗ Failed to invite reviewer.");
            Pause();
        }

        private void InviteExternalUsers()
        {
            ViewMyContracts();
            int contractId = ReadInt("\nEnter Contract ID: ");
            var externalUsers = new UserController().GetAllExternalUsers();
            foreach (var u in externalUsers) Console.WriteLine($"[{u.Ext_User_ID}] {u.First_name} {u.Last_name}");
            int userId = ReadInt("\nEnter External User ID to invite: ");
            bool success = _contractController.InviteExternalUser(contractId, userId);
            Console.WriteLine(success ? "\n✓ External user invited!" : "\n✗ Failed to invite.");
            Pause();
        }

        private void ViewMyContractsAsReviewer()
        {
            Console.Clear();
            Console.WriteLine("=== CONTRACTS TO REVIEW ===");
            var contracts = _contractController.GetContractsToReviewByInternalUser(UserId);
            if (contracts.Count == 0) Console.WriteLine("No contracts assigned for review.");
            else foreach (var c in contracts)
                    Console.WriteLine($"[{c.Contract_NR}] {c.Company_name}");
            Pause();
        }

        private void ViewContractDetails()
        {
            int contractId = ReadInt("\nEnter Contract ID to view details: ");

            var contract = _contractController.GetContractById(contractId);
            if (contract == null)
            {
                Console.WriteLine("Contract not found.");
                Pause();
                return;
            }

            Console.WriteLine($"\nContract: {contract.Company_name}\nCreated: {contract.Created_date:yyyy-MM-dd}");
            var blocks = _contractController.GetBlocksByContract(contractId);
            foreach (var b in blocks)
            {
                string typeIndicator = "";
                if (b.Contract_text.Contains("[IMAGE:"))
                    typeIndicator = " [IMAGE]";
                else if (b.Contract_text.Contains("[ADDRESS SECTION]") ||
                         b.Contract_text.Contains("[SIGNATURE SECTION]") ||
                         b.Contract_text.Contains("[DATE SECTION]"))
                    typeIndicator = " [PREFORMATTED]";

                Console.WriteLine($"[{b.Org_Cont_ID}]{typeIndicator} {b.Contract_text}");
            }
            Pause();
        }

        private void AddCommentToContract()
        {
            int contractId = ReadInt("\nEnter Contract ID to comment: ");
            Console.Write("Comment Text: "); string text = Console.ReadLine();
            bool success = _commentController.AddComment(contractId, null, UserId, "Internal", text);
            Console.WriteLine(success ? "\n✓ Comment added!" : "\n✗ Failed to add comment.");
            Pause();
        }

        private void ApproveContractMenu()
        {
            int contractId = ReadInt("\nEnter Contract ID to approve: ");
            bool success = _approvalController.ApproveContract(contractId, UserId);
            Console.WriteLine(success ? "\n✓ Contract approved!" : "\n✗ Failed to approve contract.");
            Pause();
        }

    }

    // ============= EXTERNAL USER SESSION ===================
    public class ExternalUserSession : UserSession
    {
        private ContractController _contractController = new ContractController();
        private CommentController _commentController = new CommentController();

        public ExternalUserSession(int userId, string username) : base(userId, username) { }

        public override void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===========================================");
                Console.WriteLine($"   EXTERNAL USER MENU - {Username}");
                Console.WriteLine("===========================================");
                Console.WriteLine("1. View My Contracts");
                Console.WriteLine("2. View Contract Details");
                Console.WriteLine("3. Comment on Contract");
                Console.WriteLine("0. Logout");
                Console.Write("\nSelect option: ");

                switch (Console.ReadLine())
                {
                    case "1": ViewMyContracts(); break;
                    case "2": ViewContractDetails(); break;
                    case "3": AddCommentToContract(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid option."); Pause(); break;
                }
            }
        }

        private void ViewMyContracts()
        {
            Console.Clear();
            Console.WriteLine("=== MY CONTRACTS (INVITED) ===");
            var contracts = _contractController.GetContractsByExternalUser(UserId);
            if (contracts.Count == 0)
                Console.WriteLine("No contracts found. You haven't been invited to any contracts yet.");
            else
            {
                foreach (var c in contracts)
                {
                    Console.WriteLine($"[{c.Contract_NR}] {c.Company_name} (Created: {c.Created_date:yyyy-MM-dd})");
                }
            }
            Pause();
        }

        private void ViewContractDetails()
        {
            int contractId = ReadInt("\nEnter Contract ID to view details: ");

            // Check if user is invited to this contract
            if (!_contractController.IsExternalUserInvitedToContract(contractId, UserId))
            {
                Console.WriteLine("\n✗ Access denied. You are not invited to this contract.");
                Pause();
                return;
            }

            var contract = _contractController.GetContractById(contractId);
            if (contract == null)
            {
                Console.WriteLine("Contract not found.");
                Pause();
                return;
            }

            Console.WriteLine($"\nContract: {contract.Company_name}\nCreated: {contract.Created_date:yyyy-MM-dd}");
            var blocks = _contractController.GetBlocksByContract(contractId);
            foreach (var b in blocks)
            {
                string typeIndicator = "";
                if (b.Contract_text.Contains("[IMAGE:"))
                    typeIndicator = " [IMAGE]";
                else if (b.Contract_text.Contains("[ADDRESS SECTION]") ||
                         b.Contract_text.Contains("[SIGNATURE SECTION]") ||
                         b.Contract_text.Contains("[DATE SECTION]"))
                    typeIndicator = " [PREFORMATTED]";

                Console.WriteLine($"[{b.Org_Cont_ID}]{typeIndicator} {b.Contract_text}");
            }
            Pause();
        }

        private void AddCommentToContract()
        {
            int contractId = ReadInt("\nEnter Contract ID to comment: ");

            // Check if user is invited to this contract
            if (!_contractController.IsExternalUserInvitedToContract(contractId, UserId))
            {
                Console.WriteLine("\n✗ Access denied. You can only comment on contracts you've been invited to.");
                Pause();
                return;
            }

            Console.Write("Comment Text: ");
            string text = Console.ReadLine();
            bool success = _commentController.AddComment(contractId, null, UserId, "External", text);
            Console.WriteLine(success ? "\n✓ Comment added!" : "\n✗ Failed to add comment.");
            Pause();
        }
    }

    // ==================== CONSOLE UI ====================
    public class ConsoleUI
    {
        private UserController _userController = new UserController();

        public void Start()
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("   CONTRACT MANAGEMENT SYSTEM");
            Console.WriteLine("===========================================\n");

            while (true)
            {
                UserSession session = Login();
                if (session != null) session.ShowMenu();
            }
        }

        private UserSession Login()
        {
            Console.WriteLine("LOGIN");
            Console.WriteLine("1. Administrator Login");
            Console.WriteLine("2. Internal User Login");
            Console.WriteLine("3. External User Login");
            Console.WriteLine("0. Exit");
            Console.Write("\nSelect login type: ");

            string choice = Console.ReadLine();

            if (choice == "0")
            {
                Console.WriteLine("\nExiting application...");
                Environment.Exit(0);
            }

            Console.Write("Username: "); string username = Console.ReadLine();
            Console.Write("Password: "); string password = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var admin = _userController.LoginAdministrator(username, password);
                    if (admin != null)
                    {
                        Console.WriteLine($"\nWelcome, Administrator {admin.First_name} {admin.Last_name}!");
                        Pause();
                        return new AdminSession(admin.Administrator_ID, admin.Username);
                    }
                    break;
                case "2":
                    var intUser = _userController.LoginInternalUser(username, password);
                    if (intUser != null)
                    {
                        Console.WriteLine($"\nWelcome, {intUser.First_name} {intUser.Last_name}!");
                        Pause();
                        return new InternalUserSession(intUser.Int_User_ID, intUser.Username);
                    }
                    break;
                case "3":
                    var extUser = _userController.LoginExternalUser(username, password);
                    if (extUser != null)
                    {
                        Console.WriteLine($"\nWelcome, {extUser.First_name} {extUser.Last_name} from {extUser.Company_name}!");
                        Pause();
                        return new ExternalUserSession(extUser.Ext_User_ID, extUser.Username);
                    }
                    break;
            }
            Console.WriteLine("\nInvalid username or password.");
            Pause();
            return null;
        }

        private void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}