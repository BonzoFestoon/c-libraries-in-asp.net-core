using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;

namespace ImageConvertService
{
    public class Program
    {
        private static void UnlockLeadtools()
        {
            // TODO: Update with your LEADTOOLS developer key
            const string key = @"Your LEADTOOLS Developer Key Here";
           
            var assembly = Assembly.GetEntryAssembly();
            
            // TODO: Make sure the namespace and resource name matches. Reminder: Case Sensitive - even the license file name!!!!
            var licStream = assembly.GetManifestResourceStream(@"ImageConvertService.LEADTOOLS.LIC");
            if (licStream == null)
                throw new FileNotFoundException("Could not load license file from resource stream.");

            using (var ms = new MemoryStream())
            {
                licStream.CopyTo(ms);
                var ret = LTInterop.L_SetLicenseBuffer(ms.ToArray(), new IntPtr(ms.Length), key);
                if (ret != 1)
                    throw new Exception("Error setting LEADTOOLS License : " + ret);
            }
        }

        public static void Main(string[] args)
        {
            UnlockLeadtools();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
        
    }
}
