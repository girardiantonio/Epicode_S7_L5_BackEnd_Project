using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.WebPages;

namespace Epicode_S7_L5_BackEnd_Project.Models
{
    public class Carrello
    {
        public List<CarrelloItem> Prodotti { get; set; }
        public int IdUtente { get; set; }
        public string IndirizzoConsegna { get; set; }
        public string NoteSpeciali { get; set; }

        public Carrello()
        {
            Prodotti = new List<CarrelloItem>();
        }
    }

    public class CarrelloItem
    {
        public Prodotto Prodotto { get; set; }
        public int Quantita { get; set; }
        public decimal Totale { get; set; }

        [DisplayName("Indirizzo di consegna")]
        public string IndirizzoConsegna { get; set; }
        [DisplayName("Note speciali")]
        public string NoteSpeciali { get; set; }
    }
}
