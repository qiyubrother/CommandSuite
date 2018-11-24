/*
 * Created by SharpDevelop.
 * User: zhenhua.liu
 * Date: 2016/5/16
 * Time: 13:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Text;
using System.Net;

namespace dl
{
	class Program
	{
		public static void Main(string[] args)
		{
            if (args.Length != 4)
            {
                Error();
                return;
            }
            var url = args[0];
            var timeout = args[2];
            var itimeout = 3000;
            var path = args[1];
		    var retry = args[3];
            var iretry = 3;
			if (url.StartsWith("-link:", StringComparison.CurrentCulture)) 
            {
				url = url.Substring(6);
				path = args[1];
				if (!path.StartsWith("-path:", StringComparison.CurrentCulture)) {
					Error();
					return;
				}
                path = path.Substring(6);
				timeout = args[2];
				if (!timeout.StartsWith("-timeout:", StringComparison.CurrentCulture)) {
					Error();
					return;
				}
				timeout = timeout.Substring(9);
				if (!int.TryParse(timeout, out itimeout))
				{
					Error();
					return;
				}
				itimeout = Convert.ToInt32(timeout);
				if (itimeout < 1000)
				{
					itimeout = 1000;
				}
                retry = args[3];
                if (!retry.StartsWith("-retry:", StringComparison.CurrentCulture))
                {
                    Error();
                    return;
                }
                retry = retry.Substring(7);
                iretry = Convert.ToInt32(retry);
                if (iretry < 1)
                {
                    iretry = 0;
                }
			    if (iretry > 0)
			    {
			        while (iretry > 0)
			        {
			            if (Download(url, path, itimeout))
			            {
			                break;
			            }
                        iretry--;
			        }
			    }
			    else
			    {
                    Download(url, path, itimeout);
			    }
				
			} else if (url.StartsWith("-file:", StringComparison.CurrentCulture)) {
				path = args[1];
				if (!path.StartsWith("-path:", StringComparison.CurrentCulture)) {
					Error();
					return;
				}
				path = path.Substring(6);
				url = url.Substring(6);
				timeout = args[2];
				if (!timeout.StartsWith("-timeout:", StringComparison.CurrentCulture)) {
					Error();
					return;
				}
				timeout = timeout.Substring(9);
				if (!int.TryParse(timeout, out itimeout))
				{
					Error();
					return;
				}
				itimeout = Convert.ToInt32(timeout);
				if (itimeout < 1000)
				{
					itimeout = 1000;
				}
                retry = args[3];
                if (!retry.StartsWith("-retry:", StringComparison.CurrentCulture))
                {
                    Error();
                    return;
                }
                retry = retry.Substring(7);
                iretry = Convert.ToInt32(retry);
                if (iretry < 1)
                {
                    iretry = 0;
                }
				var sr = new StreamReader(url);
				var lines = sr.ReadToEnd().Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
				foreach (var line in lines)
				{
				    var r = iretry;
                    if (r > 0)
                    {
                        while (r > 0)
                        {
                            if (Download(line, path, itimeout))
                            {
                                break;
                            }
                            r--;

                        }
                    }
                    else
                    {
                        Download(line, path, itimeout);
                    }
				}
			} else {
				Error();
				return;
			}
		}
		
		private static bool Download(string url, string path, int timeout)
		{

		    HttpWebRequest request = null;
		    try
		    {
                request = (HttpWebRequest)WebRequest.Create(url);
		        request.ServicePoint.ConnectionLimit = 5000;
		    }
		    catch (Exception)
		    {
                Console.WriteLine("{0} :: Invalid url.[Falied]", url);
                return false;
		    }
            try
            {
                request.KeepAlive = false;
                request.Timeout = timeout;
                
                var uri = new Uri(url);
                var fileName = string.Empty;
                fileName = uri.Segments[uri.Segments.Length - 1];

                Console.Write("{0} => {1}", url, fileName);

                using (var response = request.GetResponse())
                {
                    var reader = response.GetResponseStream();
                    //可根据实际保存为具体文件
                    var writer = new FileStream(Path.Combine(path, fileName), FileMode.OpenOrCreate, FileAccess.Write);
                    var buff = new byte[512];
                    var c = 0; //实际读取的字节数 
                    while ((c = reader.Read(buff, 0, buff.Length)) > 0)
                    {
                        writer.Write(buff, 0, c);
                    }
                    reader.Close();
                    reader.Dispose();
                    response.Close();
                }
                
                request.Abort();

                Console.WriteLine(" => [OK]");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(" => [Falied]");
                try
                {
                    request.Abort();
                }
                catch (Exception)
                {
                }
                return false;
            }
        }

        private static void Error()
        {
            Console.WriteLine("dl -link:http://www.xxx.com/abc.jpg -timeout:5000 -retry:2");
            Console.WriteLine("dl -file:links.txt -path:. -timeout:5000 -retry:2");
        }
		
	}
}