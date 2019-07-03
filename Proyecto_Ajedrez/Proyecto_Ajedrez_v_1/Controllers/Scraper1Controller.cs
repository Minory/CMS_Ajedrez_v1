using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_Ajedrez_v_1.Models;
using System.Text;


namespace Proyecto_Ajedrez_v_1.Controllers
{

    //Scraper scraper;

    public class Scraper1Controller : Controller
    {
        //ScraperdbEntities db = new ScraperdbEntities();


        // GET: 
        Scraper scraper = new Scraper();


        string w;// que es la variable global que va guardar la cadena de todos las url
        string u;//remove
        string[] separ;
        int comas;
        string web1 = null;
        string web2 = null;


        //long a = "holaaa".LongCount(letra => letra.ToString() == "a");

        //int b = (int)a;

        //EntryModel m
        //DataContext = scraper;





        public ActionResult Index()
        {
            Scraper scraper = new Scraper();
            EntryModel m = new EntryModel();

            /* empieza codigo de checkbox-*/
            List<Webs> ws = new List<Webs>();
            ws.Add(new Webs() { id = 1, urlWeb = "http://ajedrezenperu.org/", IsCheck = false });
            ws.Add(new Webs() { id = 2, urlWeb = "https://www.chess.com/es", IsCheck = false });

            urlLista frlist = new urlLista();

            frlist.webs = ws;
            //scraper.ScrapeData
            // DataContext = scraper;
            return View(frlist);
        }

        [HttpPost]
        public ActionResult Index(urlLista fr1)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in fr1.webs)
            {
                if (item.IsCheck)
                {
                    sb.Append(item.urlWeb + ",");
                }
            }
            ViewBag.selectFruit = "Los sitios web seleccionados son " + sb.ToString();

            //u= sb.ToString();
            w = sb.Remove(sb.ToString().LastIndexOf(","), 1).ToString();
            separ = w.Split(',');
            //separ[0]=primera web ejmplis y asi sucesivamente
            comas = w.Split(',').Length - 1;

            if (comas == 1)
            {
                web1 = separ[0];
                web2 = separ[1];
            }

            else if (comas == 0)
            {
                web1 = separ[0];
            }
            //ViewBag.selectFruit = web1;

            Consultaweb2(web1, web2);

            return View(fr1);



        }






        //public ActionResult Consultaweb1(string uri)
        public ActionResult Consultaweb1(string uri)
        {

            uri = "http://ajedrezenperu.org/"; //variable locales para comapararlas con resultado anterior 
                                               //string uri2 = "https://mmohuts.com/news/";


            //uri = "https://www.chess.com/es";
            scraper.ScrapeData(uri);
            //List<EntryModel> m = new List<EntryModel>();
            return View(scraper.Entries.ToList());
        }


        public ActionResult Consultaweb2(string pag1, string pag2)
        {
            string uri1 = "http://ajedrezenperu.org/"; //variable locales para comapararlas con resultado anterior 

            string uri2 = "https://www.chess.com/es";

            //********************empieza codigo+************************

            //pag1 = uri1;//web1;//w1
            //pag2 = uri2; //web2;//w2

            pag1 = web1;//w1
            pag2 = web2;//w2

            if (pag1 == uri1 && pag2 == null)
            {
                scraper.ScrapeData(pag1);
            }

            if (pag1 == uri2 && pag2 == null)
            {
                scraper.ScrapeData2(pag1);
            }


            if (pag1 == uri1 && pag2 == uri2)
            {
                scraper.ScrapeData(pag1);
                scraper.ScrapeData2(pag2);
            }


            //string uri = "http://ajedrezenperu.org/";
            //uri = "https://mmohuts.com/news/";
            //uri = "https://www.chess.com/es";
            //scraper2.ScrapeData(uri);
            //List<EntryModel> m = new List<EntryModel>();
            return View(scraper.Entries.ToList());
        }




        //probando que funcione segunda web

        public ActionResult Consultaweb3(string uri)
        {

            // string  uri1 = "http://ajedrezenperu.org/"; //variable locales para comapararlas con resultado anterior 
            //string uri2 = "https://mmohuts.com/news/";


            uri = "https://www.chess.com/es";
            scraper.ScrapeData2(uri);
            //List<EntryModel> m = new List<EntryModel>();
            return View(scraper.Entries.ToList());
        }

    }
}