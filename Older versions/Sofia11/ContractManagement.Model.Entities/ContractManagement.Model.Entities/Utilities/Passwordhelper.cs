using System;
using System.Security.Cryptography;
using System.Text;

namespace ContractManagement.Utils
{
    /// <summary>
    /// Utility class for secure password hashing using PBKDF2 (Password-Based Key Derivation Function 2)
    /// This is a secure, industry-standard approach recommended by OWASP
    /// </summary>
    public static class PasswordHelper
    {
        // Number of iterations for PBKDF2 (higher = more secure but slower)
        private const int Iterations = 10000;

        // Size of the salt in bytes
        private const int SaltSize = 16;

        // Size of the hash in bytes
        private const int HashSize = 20;

        /// <summary>
        /// Hashes a password using PBKDF2 with a randomly generated salt
        /// </summary>
        /// <param name="password">Plain text password to hash</param>
        /// <returns>Hashed password in format: iterations.salt.hash (all base64 encoded)</returns>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty", nameof(password));

            // Generate a random salt
            byte[] salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            // Hash the password with the salt
            byte[] hash = HashPasswordWithSalt(password, salt, Iterations, HashSize);

            // Combine iterations, salt, and hash into a single string
            // Format: iterations.salt.hash
            return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        /// <summary>
        /// Verifies a password against a hashed password
        /// </summary>
        /// <param name="password">Plain text password to verify</param>
        /// <param name="hashedPassword">Hashed password from database</param>
        /// <returns>True if password matches, false otherwise</returns>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            if (string.IsNullOrWhiteSpace(hashedPassword))
                return false;

            // Check if this is a legacy (unhashed) password
            // If the hashedPassword doesn't contain dots, it's probably plain text
            if (!hashedPassword.Contains("."))
            {
                // Legacy password - compare directly (for backward compatibility)
                return password == hashedPassword;
            }

            try
            {
                // Split the hashed password into its components
                string[] parts = hashedPassword.Split('.');
                if (parts.Length != 3)
                    return false;

                int iterations = int.Parse(parts[0]);
                byte[] salt = Convert.FromBase64String(parts[1]);
                byte[] hash = Convert.FromBase64String(parts[2]);

                // Hash the input password with the same salt and iterations
                byte[] testHash = HashPasswordWithSalt(password, salt, iterations, hash.Length);

                // Compare the hashes (use constant-time comparison to prevent timing attacks)
                return SlowEquals(hash, testHash);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if a password is hashed or plain text
        /// </summary>
        /// <param name="password">Password string from database</param>
        /// <returns>True if password is hashed, false if plain text</returns>
        public static bool IsPasswordHashed(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            // Hashed passwords have the format: iterations.salt.hash
            if (!password.Contains("."))
                return false;

            string[] parts = password.Split('.');
            return parts.Length == 3;
        }

        /// <summary>
        /// Helper method to hash a password with a given salt
        /// </summary>
        private static byte[] HashPasswordWithSalt(string password, byte[] salt, int iterations, int hashSize)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return pbkdf2.GetBytes(hashSize);
            }
        }

        /// <summary>
        /// Constant-time comparison to prevent timing attacks
        /// </summary>
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }

        /// <summary>
        /// Migrates all plain text passwords to hashed passwords in the database
        /// Should be run once after implementing password hashing
        /// </summary>
        public static void MigrateAllPasswords()
        {
            Console.WriteLine("=== PASSWORD MIGRATION UTILITY ===");
            Console.WriteLine("This will hash all plain text passwords in the database.");
            Console.WriteLine("WARNING: This operation cannot be undone!");
            Console.Write("\nAre you sure you want to continue? (yes/no): ");

            string response = Console.ReadLine();
            if (response?.ToLower() != "yes")
            {
                Console.WriteLine("Migration cancelled.");
                return;
            }

            int migratedCount = 0;

            try
            {
                // Migrate administrators
                var adminDAL = new ContractManagement.Model.DAL.AdministratorDAL();
                var admins = adminDAL.GetAllAdministrators();
                foreach (var admin in admins)
                {
                    if (!IsPasswordHashed(admin.Password))
                    {
                        string originalPassword = admin.Password;
                        admin.Password = HashPassword(originalPassword);
                        // Update in database using DbHelper
                        bool success = ContractManagement.Model.DAL.DbHelper.ExecuteNonQuery(
                            "UPDATE administrator SET Password = @Password WHERE Administrator_ID = @Administrator_ID",
                            admin
                        );
                        if (success)
                        {
                            Console.WriteLine($"✓ Migrated admin: {admin.Username}");
                            migratedCount++;
                        }
                    }
                }

                // Migrate internal users
                var internalDAL = new ContractManagement.Model.DAL.InternalUserDAL();
                var internalUsers = internalDAL.GetAllInternalUsers();
                foreach (var user in internalUsers)
                {
                    if (!IsPasswordHashed(user.Password))
                    {
                        string originalPassword = user.Password;
                        user.Password = HashPassword(originalPassword);
                        bool success = ContractManagement.Model.DAL.DbHelper.ExecuteNonQuery(
                            "UPDATE internal_user SET Password = @Password WHERE Int_User_ID = @Int_User_ID",
                            user
                        );
                        if (success)
                        {
                            Console.WriteLine($"✓ Migrated internal user: {user.Username}");
                            migratedCount++;
                        }
                    }
                }

                // Migrate external users
                var externalDAL = new ContractManagement.Model.DAL.ExternalUserDAL();
                var externalUsers = externalDAL.GetAllExternalUsers();
                foreach (var user in externalUsers)
                {
                    if (!IsPasswordHashed(user.Password))
                    {
                        string originalPassword = user.Password;
                        user.Password = HashPassword(originalPassword);
                        bool success = ContractManagement.Model.DAL.DbHelper.ExecuteNonQuery(
                            "UPDATE external_user SET Password = @Password WHERE Ext_User_ID = @Ext_User_ID",
                            user
                        );
                        if (success)
                        {
                            Console.WriteLine($"✓ Migrated external user: {user.Username}");
                            migratedCount++;
                        }
                    }
                }

                Console.WriteLine($"\n=== MIGRATION COMPLETE ===");
                Console.WriteLine($"Total passwords migrated: {migratedCount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ ERROR during migration: {ex.Message}");
                Console.WriteLine("Some passwords may not have been migrated.");
            }
        }
    }
}