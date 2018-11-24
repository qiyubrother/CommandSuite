using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace umorse
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("umorse \"Morse code test.\"");
                return;
            }
            var isIgnoreOtherCode = args.Length == 2 && args[1] == "-i";

            const char d = '*';
            const char c = '-';
            var code = args[0].ToLower();
            foreach (var ch in code)
            {
                if (ch == 'a') Console.Write("{0}{1} ", d, c);
                else if (ch == 'b') Console.Write("{0}{1}{2}{3} ", c, d, d, d);
                else if (ch == 'c') Console.Write("{0}{1}{2}{3} ", c, d, c, d);
                else if (ch == 'd') Console.Write("{0}{1}{2} ", c, d, d);
                else if (ch == 'e') Console.Write("{0} ", d);
                else if (ch == 'f') Console.Write("{0}{1}{2}{3} ", d, d, c, d);
                else if (ch == 'g') Console.Write("{0}{1}{2} ", c, c, d);
                else if (ch == 'h') Console.Write("{0}{1}{2}{3} ", d, d, d, d);
                else if (ch == 'i') Console.Write("{0}{1} ", d, d);
                else if (ch == 'j') Console.Write("{0}{1}{2}{3} ", d, c, c, c);
                else if (ch == 'k') Console.Write("{0}{1}{2} ", c, d, c);
                else if (ch == 'l') Console.Write("{0}{1}{2}{3} ", d, c, d, d);
                else if (ch == 'm') Console.Write("{0}{1} ", c, c);
                else if (ch == 'n') Console.Write("{0}{1} ", c, d);
                else if (ch == 'o') Console.Write("{0}{1}{2} ", c, c, c);
                else if (ch == 'p') Console.Write("{0}{1}{2}{3} ", d, c, c, d);
                else if (ch == 'q') Console.Write("{0}{1}{2}{3} ", c, c, d, c);
                else if (ch == 'r') Console.Write("{0}{1}{2} ", d, c, d);
                else if (ch == 's') Console.Write("{0}{1}{2} ", d, d, d);
                else if (ch == 't') Console.Write("{0} ", c);
                else if (ch == 'u') Console.Write("{0}{1}{2} ", d, d, c);
                else if (ch == 'v') Console.Write("{0}{1}{2}{3} ", d, d, d, c);
                else if (ch == 'w') Console.Write("{0}{1}{2} ", d, c, c);
                else if (ch == 'x') Console.Write("{0}{1}{2}{3} ", c, d, d, c);
                else if (ch == 'y') Console.Write("{0}{1}{2}{3} ", c, d, c, c);
                else if (ch == 'z') Console.Write("{0}{1}{2}{3} ", c, c, d, d);
                else if (!isIgnoreOtherCode) Console.Write("{0}", ch);
            }
        }
    }
}
