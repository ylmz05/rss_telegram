using System;

namespace Rss.TLBotCommunication.TLBotInstructions.Helpers
{
    public class RandomCodeHelper
    {
        private const string CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        public static string Code = string.Empty;
        public static string Produce(int length)
        {
            char[] stringChars = new char[length];
            Random random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
                stringChars[i] = CHARACTERS[random.Next(CHARACTERS.Length)];

            return Code = new string(stringChars);
        }
        public static bool Validate(string code)
        {
            return (code == Code);
        }
    }
}
