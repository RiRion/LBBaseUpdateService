using System;

namespace TheNewCSVEditorForLB.Services
{
    public static class StatusCode
    {
        public static void StatusOk(string mes)
        {
//            Console.Write("[ ");
//            Console.ForegroundColor = ConsoleColor.Green;
//            Console.Write("OK");
//            Console.ResetColor();
//            Console.WriteLine(" ] " + mes);
            Console.WriteLine("[ OK ] " + mes);
        }
        public static void StatusFalse(string mes)
        {
//            Console.Write("[");
//            Console.ForegroundColor = ConsoleColor.Red;
//            Console.Write("FALSE");
//            Console.ResetColor();
//            Console.WriteLine("] " + mes);
            Console.WriteLine("[FALSE] " + mes);
        }
    }
}