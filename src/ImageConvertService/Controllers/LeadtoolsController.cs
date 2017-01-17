using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ImageConvertService.Controllers
{
    [Route("api/[controller]")]
    public class ImagingController : Controller
    {
        public async Task<IActionResult> Get(Uri url)
        {
            const int FILE_PNG = 75; // From ltfil.h
            const string mimeType = "image/png";

            if (url == null)
                return Content("url cannot be null");
            if (!url.IsAbsoluteUri)
                return Content("url must be absolute. Make sure to include the protocol.");

            string sourceFile = null;
            string targetFile = null;

            try
            {
                // Download image to a temp file.
                sourceFile = Path.GetTempFileName();
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Get, url.ToString()))
                using (Stream contentStream = await (await client.SendAsync(request)).Content.ReadAsStreamAsync())
                using (Stream fileStream = new FileStream(sourceFile, FileMode.Create, FileAccess.Write, FileShare.None, 3145728, true))
                {
                    await contentStream.CopyToAsync(fileStream);
                }

                // Call native L_FileConvert
                // https://www.leadtools.com/help/leadtools/v19/main/api/l_fileconvert.html
                targetFile = Path.GetTempFileName();
                int ret = LTInterop.L_FileConvert(sourceFile, targetFile, FILE_PNG, 0, 0, 0, 0, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
                if (ret != 1)
                    return Content("Error Converting File : " + ret);

                // Load converted file and return.
                byte[] buffer = System.IO.File.ReadAllBytes(targetFile);
                return File(buffer, mimeType);
            }
            finally
            {
                // Clean up
                if (sourceFile != null && System.IO.File.Exists(sourceFile))
                    System.IO.File.Delete(sourceFile);
                if (targetFile != null && System.IO.File.Exists(targetFile))
                    System.IO.File.Delete(targetFile);
            }
        }
    }
}
