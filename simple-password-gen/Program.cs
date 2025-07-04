using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Program
{
    static int len = 0;
    static string lowerCase = "abcdefghijklmnopqrstuvwxyz";
    static string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    static string nums = "1234567890";
    static string chars = "!#¤%&/()=?\\><:,.;-'^*";
    static Random random = new Random();
    static char[] pwChars;
    private const string vfn = "password_vault.enc";

    static void Main()
    {
        Console.WriteLine("the grand passwordinator");
        Console.WriteLine("pick your poiso- i mean select password strength");
        Console.WriteLine("0 = abysmal dogshit | 1 = ehhh | 2 = somewhat strong | 3 = sir how long until we bruteforce this password | v = enter password vault");
        string choice = Console.ReadLine();
        Enumer(choice);
    }

    private static string XORCipher(string input, string key)
    {
        if (string.IsNullOrEmpty(input)) return input;
        if (string.IsNullOrEmpty(key)) return input;

        StringBuilder result = new StringBuilder();
        for (int i = 0; i < input.Length; i++)
        {
            result.Append((char)(input[i] ^ key[i % key.Length]));
        }
        return result.ToString();
    }

    static List<string> LoadEncryptedVault()
    {
        List<string> vaultContent = new List<string>();
        try
        {
            if (File.Exists(vfn))
            {
                vaultContent.AddRange(File.ReadAllLines(vfn));
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error loading vault: {ex.Message}");
        }
        return vaultContent;
    }

    static void PassVault(bool savemode, string passwordtoprocess)
    {
        Console.WriteLine("\n--- password vault (demo only - not secure) ---");
        Console.WriteLine("this vault uses a simple xor cipher and stores passwords in a file.");
        Console.WriteLine("do not store real passwords here.");

        if (savemode)
        {
            Console.WriteLine("enter encryption key:");
            string encryptionkey = Console.ReadLine();
            if (string.IsNullOrEmpty(encryptionkey))
            {
                Console.WriteLine("encryption key cannot be empty. password not saved.");
                return;
            }

            string encryptedpass = XORCipher(passwordtoprocess, encryptionkey);

            List<string> currentvault = LoadEncryptedVault();
            currentvault.Add(encryptedpass);
            SaveEncryptedVault(currentvault);

            Console.WriteLine($"password saved! encrypted form: {encryptedpass}");
            Console.WriteLine("remember your key to decrypt it.");
        }
        else
        {
            Console.WriteLine("enter decryption key:");
            string decryptionkey = Console.ReadLine();

            if (string.IsNullOrEmpty(decryptionkey))
            {
                Console.WriteLine("decryption key cannot be empty. cannot view vault.");
                return;
            }

            List<string> currentvault = LoadEncryptedVault();

            if (currentvault.Count == 0)
            {
                Console.WriteLine("vault is empty.");
                return;
            }

            Console.WriteLine("\n--- decrypted passwords ---");
            for (int i = 0; i < currentvault.Count; i++)
            {
                string decryptedpass = XORCipher(currentvault[i], decryptionkey);
                Console.WriteLine($"password {i + 1}: {decryptedpass}");
            }
        }
        Console.WriteLine("returning to main menu.");
        Main();
    }

    static void SaveEncryptedVault(List<string> vaultContent)
    {
        try
        {
            File.WriteAllLines(vfn, vaultContent);
            Console.WriteLine($"Vault updated and saved to {vfn}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error saving vault: {ex.Message}");
        }
    }

    static void Enumer(string choice)
    {
        if (choice != null)
        {
            switch (choice)
            {
                case "0": len = 1; break;
                case "1": len = 16; break;
                case "2": len = 32; break;
                case "3": len = 128; break;
                case "v": PassVault(false, ""); return;
                default:
                    Console.WriteLine("invalid choice");
                    Main();
                    return;
            }
        }
        else
        {
            Console.WriteLine("invalid choice");
            Main();
            return;
        }

        string allChars = lowerCase + upperCase + nums + chars;
        pwChars = new char[len];

        for (int i = 0; i < len; i++)
        {
            int randomIndex = random.Next(allChars.Length);
            pwChars[i] = allChars[randomIndex];
        }

        string password = new string(pwChars);
        Console.WriteLine($"final password (yippee): {password}");
        Console.WriteLine("would you like to save this to the password vault? (y/N)");
        string input = Console.ReadLine();
        switch (input)
        {
            case "y":
            case "Y":
                PassVault(true, password);
                break;
            default:
                Environment.Exit(0);
                break;
        }
    }
}
