using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace pageinfo
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("pageinfo http://www.163.com");
                return;
            }
            var url = args[0];
            if (!(url.StartsWith("http://", StringComparison.CurrentCultureIgnoreCase)
                  || url.StartsWith("https://", StringComparison.CurrentCultureIgnoreCase)))
            {
                Console.WriteLine("pageinfo http://www.163.com");
                return;
            }

            try
            {
                var request = WebRequest.Create(url);
                var response = request.GetResponse();
                Console.WriteLine("Url:{0}", response.ResponseUri);
                Console.WriteLine("ContentType:{0}", response.ContentType);
                Console.WriteLine("ContentLength:{0}", response.ContentLength);

                byte[] buffer = response.Headers.ToByteArray();
                string header = Encoding.UTF8.GetString(buffer, 0, buffer.Length);

                Console.WriteLine("Headers:{0}", header);

                response.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
