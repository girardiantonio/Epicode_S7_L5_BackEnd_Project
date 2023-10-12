namespace Epicode_S7_L5_BackEnd_Project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("DettaglioOrdine")]
    public partial class DettaglioOrdine
    {
        [Key]
        public int IdDettaglioOrdine { get; set; }

        public int IdOrdine { get; set; }

        public int IdProdotto { get; set; }

        public int Quantita { get; set; }

        public virtual Ordine Ordine { get; set; }

        public virtual Prodotto Prodotto { get; set; }
    }
}
