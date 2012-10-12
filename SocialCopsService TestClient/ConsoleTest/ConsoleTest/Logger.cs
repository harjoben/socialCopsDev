using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest
{
    class Logger
    {

        public void error(string message)
        {
            //
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message); // <-- see note
            //
            // Reset the color.
            //
            Console.ResetColor();
        }

        public void warning(string message)
        {
            //
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message); // <-- see note
            //
            // Reset the color.
            //
            Console.ResetColor();
        }


        public void start(string message)
        {
            //
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message.PadRight(Console.WindowWidth - 1)); // <-- see note
            //
            // Reset the color.
            //
            Console.ResetColor();
        }
    }
}
