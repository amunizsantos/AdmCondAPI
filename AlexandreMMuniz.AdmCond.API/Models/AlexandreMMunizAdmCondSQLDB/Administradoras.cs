using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlexandreMMuniz.AdmCond.API.Models.AlexandreMMunizAdmCondSQLDB
{
    public partial class Administradoras
    {
        public Administradoras()
        {
            Condominios = new HashSet<Condominios>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [InverseProperty("IdAdministradoraNavigation")]
        public virtual ICollection<Condominios> Condominios { get; set; }
    }
}
