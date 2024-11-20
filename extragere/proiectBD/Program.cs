using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Xml.Linq;
using proiectBD;

namespace SOA_L1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StiriLocaleEntities3 db = new StiriLocaleEntities3(); //nume baza de date

            ChromeOptions options = new ChromeOptions();
            options.AddArguments("start-maximized");
            ChromeDriver driver = new ChromeDriver(options);

            driver.Navigate().GoToUrl(@"https://www.protv.ro/articole");
            Thread.Sleep(5000);

            driver.FindElement(By.Id("onetrust-accept-btn-handler")).Click();
            Thread.Sleep(5000);

            //db.importStiris.RemoveRange(db.importStiris);
            //db.SaveChanges();

            List<string> titluArticol = driver.FindElements(By.CssSelector(".title")).Select(c => c.Text).ToList();
            List<string> categArticol = driver.FindElements(By.CssSelector(".category")).Select(c => c.Text).ToList();
            List<string> autorArticol = driver.FindElements(By.CssSelector(".author-name-full")).Select(c => c.Text).ToList();
            List<string> dataArticol = driver.FindElements(By.CssSelector(".date")).Select(c => c.Text).ToList();

            for (int i = 0; i < titluArticol.Count; i++)
            {
                try
                {
                    Console.WriteLine("Titlul articolululi este:" + titluArticol.ElementAt(i));
                    Console.WriteLine("Categoria articolului este:" + categArticol.ElementAt(i));
                    Console.WriteLine("Autorul articolului este:" + autorArticol.ElementAt(i)+"\n");

                            importStiri isl = new importStiri();
                            isl.is_titlu = titluArticol.ElementAt(i);
                            isl.is_categorie = categArticol.ElementAt(i);
                            isl.is_autor = autorArticol.ElementAt(i);
                            isl.is_data = dataArticol.ElementAt(i);
                            db.importStiris.Add(isl);
                            db.SaveChanges();
                }
                        catch
                        {
                            //nimic
                        }

            }



                    driver.Quit();
                   // ExportPDF();
        }
        //static void ExportPDF()
        //{
        //    SOA_L1Entities db = new SOA_L1Entities();
        //    string caleFisier = "D:\\10213\\fisier.pdf";
        //    List<importSuperBet> lista = db.importSuperBet.ToList();

        //    using (var fileStream = new FileStream(caleFisier, FileMode.Create))
        //    {
        //        using (var writer = new PdfWriter(fileStream))
        //        {
        //            using (var pdf = new PdfDocument(writer))
        //            {
        //                var document = new Document(pdf);

        //                foreach (importSuperBet isb in lista)
        //                {
        //                    document.Add(new Paragraph(isb.isb_jucator_1 + " " + isb.isb_punctaj_2 + "=>" + isb.isb_punctaj_1 + " - " + isb.isb_punctaj_2));
        //                }
        //            }
        //        }
        //    }

        //    Console.WriteLine("Fisierul PDF a fost generat cu succes la adresa: " + caleFisier);
        //}
    }
}
