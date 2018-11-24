using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

namespace wwwstatus
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var r = 100;
                var url = "www.baidu.com";
                var param = new CommandParameterHelper(args);
                foreach (var p in param.Parameters)
                {
                    if (p.Key != "r" && p.Key != "url")
                    {
                        Error();
                        return;
                    }
                }
                if (param.Parameters.ContainsKey("r"))
                {
                    if (!int.TryParse(param.Parameters["r"], out r))
                    {
                        Error();
                        return;
                    }
                }
                if (param.Parameters.ContainsKey("url"))
                {
                    if (url.Length == 0)
                    {
                        Error();
                        return;
                    }
                    url = param.Parameters["url"];
                }


                var pingSender = new Ping();
                var status = false;
                while (!status && r >= 0)
                {
                    try
                    {
                        var reply = pingSender.Send(url, 200);
                        if (reply != null && reply.Status == IPStatus.Success)
                        {
                            Thread.Sleep(400);
                            status = true;
                            Console.Write("OK");
                        }
                        else
                        {
                            Thread.Sleep(400);
                            r--;
                        }
                    }
                    catch
                    {
                    }
                }
                if (!status)
                {
                    Console.Write("Faild");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Error();
            }
        }

        private static void Error()
        {
            Console.Error.WriteLine("wwwstatus");
            Console.Error.WriteLine("wwwstatus -r:100");
            Console.Error.WriteLine("wwwstatus -url:www.baidu.com");
            Console.Error.WriteLine("wwwstatus -r:100 -url:www.baidu.com");
            return;
        }
    }
}
