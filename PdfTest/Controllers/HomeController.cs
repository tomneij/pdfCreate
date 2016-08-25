using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;



namespace PdfTest.Controllers
{
    public class HomeController : Controller
    {
       

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Your index page.";

            return View();
        }


        public ActionResult Html2pdfRocket(string url)
        {
            using (var client = new WebClient())
            {
                // Build the conversion options 
                NameValueCollection options = new NameValueCollection();
                options.Add("apikey", "6c9b0e56-829d-4734-8984-4bb3b39c23e2");
                options.Add("value", url);
                options.Add("ViewPort", "1500x1000");
                //options.Add("UsePrintStylesheet", "true");
                options.Add("JavascriptDelay", "1000");
                //options.Add("UseGrayscale", "true");

                // Call the API convert to a PDF
                MemoryStream ms = new MemoryStream(client.UploadValues("http://api.html2pdfrocket.com/pdf", options));

                // Make the file a downloadable attachment - comment this out to show it directly inside
                HttpContext.Response.AddHeader("content-disposition", "attachment; filename=PDF.pdf");

                // Return the file as a PDF
                return new FileStreamResult(ms, "application/pdf");
            } 
        }
    }
}