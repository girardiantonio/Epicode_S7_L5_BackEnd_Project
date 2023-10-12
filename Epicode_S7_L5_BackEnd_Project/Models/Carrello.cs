using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epicode_S7_L5_BackEnd_Project.Models
{
    public class Carrello
    {
        public List<CarrelloItem> Prodotti { get; set; }
    }

    public class CarrelloItem
    {
        public Prodotto Prodotto { get; set; }
        public int Quantita { get; set; }
        public decimal Totale { get; set; }
    }
}