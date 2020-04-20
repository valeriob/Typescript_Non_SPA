using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Example.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
             .UseStartup<Startup>()
                .ConfigureServices((ctx, services) =>
                {
                    var node_modules = new OnTmsPhysicalFileProvider(Directory.GetCurrentDirectory(), "node_modules");
                    var areas = new OnTmsPhysicalFileProvider(Directory.GetCurrentDirectory(), "Pages");

                    var compositeFp = new CompositeFileProvider(ctx.HostingEnvironment.WebRootFileProvider, node_modules, areas);

                    ctx.HostingEnvironment.WebRootFileProvider = compositeFp;
                })
                //.ConfigureWebHostDefaults(webBuilder =>
                //{
                //    webBuilder.UseStartup<Startup>();
                //})
            ;
    }
}
