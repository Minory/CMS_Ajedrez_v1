using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace Proyecto_Ajedrez_v_1.Controllers
{
    public class BusquedaWEBController : Controller
    {
        // GET: BusquedaWEB
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string GetUrlSource(string url)
        {
            url = url.Substring(0, 4) != "http" ? "http://" + url : url;
            string htmlCode = "";
            using (WebClient client = new WebClient())
            {
                try
                {
                    htmlCode = client.DownloadString(url);
                }
                catch (Exception ex)
                {

                }
            }
            return htmlCode;
        }
    }
}