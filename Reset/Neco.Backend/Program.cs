using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading;

namespace Igtampe.Neco.Backend {
    public static  class Program {
        
        public static void Main(string[] args) {

            //Start session remover thread
            if (!OnHeroku) {
                //Only start this thread if we're not on heroku. If we are I'm 90% sure this can consume dyno hours since it's doing *something*
                new Thread(delegate () { SessionManager.SessionRemoverThread(60); }).Start();
            }

            //Start the website
            CreateHostBuilder(args).Build().Run(); 
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args) .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>();});

        public static bool OnHeroku { get {
                return !string.IsNullOrWhiteSpace(System.Environment.GetEnvironmentVariable("HEROKU"));
            } 
        } 
    }
}
