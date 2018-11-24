using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace deldir
{
    class Program
    {
        static void Main(string[] args)
        {
#if true
            if (args.Length != 2)
            {
                Console.WriteLine(@"deldir c:\root\ dir-name");
                return;
            }

            var root = args[0];
            var dirName = args[1];
#else
            var dirName = "tmp";
            var root = @"c:\testRoot";
#endif
            DelDir(dirName, root);
        }
        private static void DelDir(string dirName, string rootDir)
        {
            var subDirs = Directory.GetDirectories(rootDir);
            foreach (var dir in subDirs)
            {
                if (dir.EndsWith(dirName, StringComparison.CurrentCultureIgnoreCase))
                {
                    Directory.Delete(dir, true);
                    //Directory.CreateDirectory(dir);
                    Console.WriteLine(dir);
                }
                else
                {
                    DelDir(dirName, dir);
                }
            }
        }
    }
}
