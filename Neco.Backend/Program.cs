using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading;

namespace Igtampe.Neco.Backend {
    public static  class Program {
        
        public static void Main(string[] args) {

            //Start session remover thread
            new Thread(delegate () { SessionManager.SessionRemoverThread(60); }).Start();

            //Start the website
            CreateHostBuilder(args).Build().Run(); 
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args) .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>();});
    }
}
