using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace DojoActivityCenter
{
    public class Program
    {
     public static void Main(string[] args)
        {
            IWebHost host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup()
                .UseIISIntegration()
                .Build();
            host.Run();
        }
    }
}
