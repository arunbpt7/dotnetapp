using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aspose.Pdf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDFTestCore.Models;

namespace PDFTestCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
         public IActionResult GeneratePdf(IFormFile  file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");
 string path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot", 
                        file.FileName);

 using (var fileStream = new FileStream(path, FileMode.Create))
    {
         file.CopyTo(fileStream);
    }
             byte[] buffer = System.IO.File.ReadAllBytes(path);
// byte[] buffer = System.IO.File.ReadAllBytes(@"TempFiles/BEV-100E.html");
    using (var inputStream = new MemoryStream(buffer)) 
    {
        using (var document = new Aspose.Pdf.Document(inputStream, new HtmlLoadOptions { InputEncoding = "UTF-8" }))
        {
            // using (var outputStream = new MemoryStream())
            // {
                document.Save(@"TempFiles/temp.pdf", SaveFormat.Pdf);
                // outputStream.Seek(0, SeekOrigin.Begin);

                // byte[] temp = outputStream.ToArray();               
                // base64String = System.Convert.ToBase64String(temp);
            // }

            byte[] filebytes= System.IO.File.ReadAllBytes(@"TempFiles/temp.pdf");
            return File(filebytes,"application/pdf","Result.pdf");
        }
		}
        
        }
        public IActionResult Privacy()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
