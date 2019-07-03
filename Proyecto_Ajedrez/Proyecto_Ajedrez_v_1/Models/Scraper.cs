using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Proyecto_Ajedrez_v_1.Models
{
    public class Scraper
    {
        private ObservableCollection<EntryModel> _entries = new ObservableCollection<EntryModel>();

        public ObservableCollection<EntryModel> Entries
        {
            get { return _entries; }
            set { _entries = value; }
        }


        public void ScrapeData(string page)
        {
            var web = new HtmlWeb();
            var doc = web.Load(page);


            //var Articles = doc.DocumentNode.SelectNodes("//*[@class= 'article-single']");
            //var Articles = doc.DocumentNode.SelectNodes("//div[@class= 'entry-header-details']"); ESTE CORRE SOLO CON LOS DATOS DE TEXTO
            var Articles = doc.DocumentNode.SelectNodes("//header[@class= 'entry-header']"); // si corre con el div de la imagen dentro T_T


            foreach (var article in Articles)
            {
                /*
                var header = HttpUtility.HtmlDecode(article.SelectSingleNode(".//li[@class = 'article-header']").InnerText);
                var description = HttpUtility.HtmlDecode(article.SelectSingleNode(".//li[@class = 'article-copy']").InnerText);
                var autor = HttpUtility.HtmlDecode(article.SelectSingleNode(".//li[@class = 'article-author']").InnerText);
                //var foto = HttpUtility.HtmlDecode(article.SelectSingleNode(".//img[@class = 'thumb']/@src").InnerText);
                */

                var header = HttpUtility.HtmlDecode(article.SelectSingleNode(".//h2[@class = 'entry-title']").InnerText);
                var description = HttpUtility.HtmlDecode(article.SelectSingleNode(".//div[@class = 'post-excerpt']").InnerText);
                var autor = HttpUtility.HtmlDecode(article.SelectSingleNode(".//div[@class = 'post-item-metadata entry-meta']").InnerText);
                //var imagen = HttpUtility.HtmlDecode(article.SelectSingleNode(".//a").Attributes["href"].Value);
                var imagen = HttpUtility.HtmlDecode(article.SelectSingleNode(".//img").Attributes["src"].Value);

                Debug.Print($"Title: {header}\n" + $"Decription: {description}\n" + $"Autor: {autor}\n" + $"Imagen: {imagen}");
                _entries.Add(new EntryModel { Title = header, Description = description, Autor = autor, Imagen = imagen });
            }
        }



        // probando con web 2 de chezz
        public void ScrapeData2(string page)
        {
            var web = new HtmlWeb();
            var doc = web.Load(page);


            //var Articles = doc.DocumentNode.SelectNodes("//*[@class= 'article-single']");
            //var Articles = doc.DocumentNode.SelectNodes("//div[@class= 'entry-header-details']"); ESTE CORRE SOLO CON LOS DATOS DE TEXTO
            var Articles = doc.DocumentNode.SelectNodes("//article[@class= 'post-preview-component']"); // si corre con el div de la imagen dentro T_T


            foreach (var article in Articles)
            {
                /*
                var header = HttpUtility.HtmlDecode(article.SelectSingleNode(".//li[@class = 'article-header']").InnerText);
                var description = HttpUtility.HtmlDecode(article.SelectSingleNode(".//li[@class = 'article-copy']").InnerText);
                var autor = HttpUtility.HtmlDecode(article.SelectSingleNode(".//li[@class = 'article-author']").InnerText);
                //var foto = HttpUtility.HtmlDecode(article.SelectSingleNode(".//img[@class = 'thumb']/@src").InnerText);
                */

                var header = HttpUtility.HtmlDecode(article.SelectSingleNode(".//h2[@class = 'post-preview-titlecontainer']").InnerText);
                var description = HttpUtility.HtmlDecode(article.SelectSingleNode(".//p[@class = 'post-preview-excerpt post-preview-whole-text']").InnerText);
                //var autor =HttpUtility.HtmlDecode(article.SelectSingleNode(".//div[@class = 'post-item-metadata entry-meta']").InnerText);

                var imagen = HttpUtility.HtmlDecode(article.SelectSingleNode(".//img").Attributes["src"].Value);

                Debug.Print($"Title: {header}\n" + $"Decription: {description}\n" + $"Imagen: {imagen}");
                _entries.Add(new EntryModel { Title = header, Description = description, Imagen = imagen });
            }
        }






    }
}