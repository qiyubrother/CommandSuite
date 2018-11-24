using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace touch
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("touch emptyfilename");
                return;
            }
            try
            {
                var sw = new StreamWriter(args[0]);
                sw.Write(string.Empty);
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return;
            }
        }
    }
}
