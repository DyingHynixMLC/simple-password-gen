using System;

class Program
{
    static int len = 0;
    static string lowerCase = "abcdefghijklmnopqrstuvwxyz";
    static string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    static string nums = "1234567890";
    static string chars = "!#¤%&/()=?\\><:,.;-'^*";
    static Random random = new Random();
    static char[] pwChars;

    static void Main()
    {
        Console.WriteLine("the grand passwordinator");
        Console.WriteLine("pick your poiso- i mean select password strength");
        Console.WriteLine("0 = abysmal dogshit | 1 = ehhh | 2 = somewhat strong | 3 = sir how long until we bruteforce this password | v = enter password vault");
        string choice = Console.ReadLine();
        Enumer(choice);
    }
    static void passVault()
    {
        
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
                case "v":
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
    }
}
