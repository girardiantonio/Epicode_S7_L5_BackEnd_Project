namespace Epicode_S7_L5_BackEnd_Project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Utente")]
    public partial class Utente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Utente()
        {
            Ordine = new HashSet<Ordine>();
        }

        [Key]
        public int IdUtente { get; set; }

        [StringLength(255)]
        public string Nome { get; set; }

        [StringLength(255)]
        public string Cognome { get; set; }

        [StringLength(255)]
        public string Provincia { get; set; }

        [StringLength(255)]
        public string Citta { get; set; }

        [StringLength(255)]
        public string Indirizzo { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Password { get; set; }

        [DisplayName("Ruolo")]
        public int? IdRuolo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ordine> Ordine { get; set; }

        public virtual Ruolo Ruolo { get; set; }

        public void CreaUtente()
        {
            using (var DbContext = new ModelDbContext())
            {
                var nuovoUtente = new Utente
                {
                    Nome = Nome,
                    Cognome = Cognome,
                    Provincia = Provincia,
                    Citta = Citta,
                    Indirizzo = Indirizzo,
                    Email = Email,
                    Password = Password,
                    IdRuolo = 1
                };

                DbContext.Utente.Add(nuovoUtente);
                DbContext.SaveChanges();
            }
        }
    }
}
