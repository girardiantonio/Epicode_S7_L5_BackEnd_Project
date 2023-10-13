namespace Epicode_S7_L5_BackEnd_Project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ordine")]
    public partial class Ordine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ordine()
        {
            DettaglioOrdine = new HashSet<DettaglioOrdine>();
        }

        [Key]
        public int IdOrdine { get; set; }

        public int IdUtente { get; set; }

        [DisplayName("Data Ordine")]
        public DateTime DataOrdine { get; set; }

        public decimal Importo { get; set; }

        [StringLength(255)]
        [DisplayName("Indirizzo di consegna")]
        public string IndirizzoConsegna { get; set; }

        [StringLength(255)]
        [DisplayName("Note")]
        public string NoteSpeciali { get; set; }

        [DisplayName("Stato Ordine")]
        public bool Evaso { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DettaglioOrdine> DettaglioOrdine { get; set; }

        public virtual Utente Utente { get; set; }
    }
}
