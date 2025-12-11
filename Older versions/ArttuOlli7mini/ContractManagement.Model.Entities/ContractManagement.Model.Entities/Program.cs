using System;
using ContractManagement.Controller;
using ContractManagement.Model.DAL;

namespace ContractManagement.View
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleUI ui = new ConsoleUI();
            ui.Start();
        }
    }

    public class ConsoleUI
    {
        private InternalUserDAL userDAL = new InternalUserDAL();
        private ContractController contractController = new ContractController();

        public void Start()
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("   CONTRACT MANAGEMENT SYSTEM");
            Console.WriteLine("===========================================\n");

            // Simple login
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            var user = userDAL.GetInternalUserByUsername(username);

            if (user != null && user.Password == password)
            {
                Console.WriteLine($"\nWelcome, {user.First_name} {user.Last_name}!");
                ShowMenu(user.Int_User_ID);
            }
            else
            {
                Console.WriteLine("\nInvalid credentials.");
            }
        }

        private void ShowMenu(int userId)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===========================================");
                Console.WriteLine("   MENU");
                Console.WriteLine("===========================================");
                Console.WriteLine("1. Create Contract");
                Console.WriteLine("0. Exit");
                Console.Write("\nSelect option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        CreateContract(userId);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        private void CreateContract(int userId)
        {
            Console.Clear();
            Console.WriteLine("=== CREATE NEW CONTRACT ===");
            Console.Write("Company Name: ");
            string companyName = Console.ReadLine();

            int contractId = contractController.CreateContract(companyName, userId);

            if (contractId > 0)
            {
                Console.WriteLine($"\n✓ Contract created successfully! Contract ID: {contractId}");
            }
            else
            {
                Console.WriteLine("\n✗ Failed to create contract.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}