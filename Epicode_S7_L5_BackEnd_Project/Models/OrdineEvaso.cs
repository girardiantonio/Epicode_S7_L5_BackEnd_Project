using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epicode_S7_L5_BackEnd_Project.Models
{
    public class OrdineEvaso
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int TotaleOrdiniOggi { get; set; }
        public decimal TotaleIncasso { get; set; }
    }
}