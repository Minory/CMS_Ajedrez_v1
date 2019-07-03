using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Ajedrez_v_1.Models
{
    public class Webs
    {

        public int id { get; set; }
        public string urlWeb { get; set; }
        public bool IsCheck { get; set; }
    }

    public class urlLista
    {
        public List<Webs> webs { get; set; }
    }
}