using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace clearemptydir
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine(@"clearemptydir c:\testRoot\");
                return;
            }

            var dirName = args[0];

            //var dirName = "c:\\testRoot\\ddd";
            DelEmptyDir(dirName, dirName);
            //Console.WriteLine("All empty dir deleted.");
        }

        private static void DelEmptyDir(string dirName, string rootDir)
        {
            var subDirs = Directory.GetDirectories(dirName);
            foreach (var dir in subDirs)
            {
                var subsubDirs = Directory.GetDirectories(dir);
                if (subsubDirs.Length == 0)
                {
                    if (Directory.GetFiles(dir).Length == 0)
                    {
                        try
                        {
                            Directory.Delete(dir);
                            Console.WriteLine("{0}", dir);
                            DirectoryInfo pdir = null;
                            var curDir = Directory.GetParent(dir);

                            while (curDir != null)
                            {
                                if (curDir.FullName == rootDir)
                                {
                                    break;
                                }
                                if (Directory.GetFiles(curDir.FullName).Length == 0
                                    && Directory.GetDirectories(curDir.FullName).Length == 0)
                                {
                                    curDir.Delete();
                                    Console.WriteLine("{0}", curDir.FullName);
                                    curDir = Directory.GetParent(curDir.FullName);
                                }
                                else
                                {
                                    break;
                                }
                            } 
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                    else
                    {
                        DelEmptyDir(dir, rootDir);
                    }
                }
                else
                {
                    DelEmptyDir(dir, rootDir);
                }
            }
        }
    }
}
