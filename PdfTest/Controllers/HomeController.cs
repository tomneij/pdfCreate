using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PdfTet.Pechkin;
using Pechkin;
using Pechkin.Synchronized;
using Rotativa;
using Rotativa.MVC;

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
            return View();
        }


        public ActionResult Rotativa(string url)
        {        
            return new UrlAsPdf(url) {
                 FileName = "Rotativa.pdf",
                 RotativaOptions = new Rotativa.Core.DriverOptions
                 {
                      PageMargins = new Rotativa.Core.Options.Margins(0,0,0,0),
                 }
                //PageMargins = new Rotativa.Core.Options.Margins(0, 0, 0, 0)
            };
        }


        public ActionResult Pechkin(string url)
        {
            var pdf = GetPdf(url);

            // send the PDF file to browser
            FileResult fileResult = new FileContentResult(pdf, "application/pdf");
            fileResult.FileDownloadName = "Pechkin.pdf";

            return fileResult;

        }

        private byte[] GetPdf(string url)
        {
            // create global configuration object
            GlobalConfig gc = new GlobalConfig();

            // set it up using fluent notation
            gc.SetMargins(new System.Drawing.Printing.Margins(0, 0, 0, 0))
                
              .SetDocumentTitle("Test document")
              .SetPaperSize(System.Drawing.Printing.PaperKind.A4);
            
            //... etc

            // create converter
            IPechkin pechkin = new SynchronizedPechkin(gc);

            // create document configuration object
            ObjectConfig oc = new ObjectConfig();

            // and set it up using fluent notation too
            oc
               .SetCreateExternalLinks(false)
               .SetPrintBackground(true)

               .SetLoadImages(true)
               .SetScreenMediaType(true)
               .SetFallbackEncoding(Encoding.ASCII)
               .SetPageUri(url);
            //... etc

            // convert document
            byte[] pdfBuf = pechkin.Convert(oc);

            return pdfBuf;
        }




    }
}