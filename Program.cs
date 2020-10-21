using System;

namespace artificial_intelligence_8_hlavolam
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            new Algorithm().Handle();

            Console.WriteLine("Failed to find a solution");

            return;
        }
    }
}