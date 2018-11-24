using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace morse
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1 && args.Length != 2)
            {
                Console.Error.WriteLine("umorse \"** *-** --- ***- * -*-- --- **- *** **** *- *** **** *-\"");
                return;
            }

            var codes = args[0].Split(new[] { ' ' });
            foreach (var code in codes)
            {
                switch (code)
                {
                    case "*-":
                        Console.Write("a");
                        break;
                    case "-***":
                        Console.Write("b");
                        break;
                    case "-*-*":
                        Console.Write("c");
                        break;
                    case "-**":
                        Console.Write("d");
                        break;
                    case "*":
                        Console.Write("e");
                        break;
                    case "**-*":
                        Console.Write("f");
                        break;
                    case "--*":
                        Console.Write("g");
                        break;
                    case "****":
                        Console.Write("h");
                        break;
                    case "**":
                        Console.Write("i");
                        break;
                    case "*---":
                        Console.Write("j");
                        break;
                    case "-*-":
                        Console.Write("k");
                        break;
                    case "*-**":
                        Console.Write("l");
                        break;
                    case "--":
                        Console.Write("m");
                        break;
                    case "-*":
                        Console.Write("n");
                        break;
                    case "---":
                        Console.Write("o");
                        break;
                    case "*--*":
                        Console.Write("p");
                        break;
                    case "--*-":
                        Console.Write("q");
                        break;
                    case "*-*":
                        Console.Write("r");
                        break;
                    case "***":
                        Console.Write("s");
                        break;
                    case "-":
                        Console.Write("t");
                        break;
                    case "**-":
                        Console.Write("u");
                        break;
                    case "***-":
                        Console.Write("v");
                        break;
                    case "*--":
                        Console.Write("w");
                        break;
                    case "-**-":
                        Console.Write("x");
                        break;
                    case "-*--":
                        Console.Write("y");
                        break;
                    case "--**":
                        Console.Write("z");
                        break;
                }
            }
        }
    }
}
