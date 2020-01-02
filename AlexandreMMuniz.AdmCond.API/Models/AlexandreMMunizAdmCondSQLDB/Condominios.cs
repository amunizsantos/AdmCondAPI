using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlexandreMMuniz.AdmCond.API.Models.AlexandreMMunizAdmCondSQLDB
{
    public partial class Condominios
    {
        public Condominios()
        {
            Usuarios = new HashSet<Usuarios>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }
        public int IdAdministradora { get; set; }
        public byte Responsavel { get; set; }

        [ForeignKey(nameof(IdAdministradora))]
        [InverseProperty(nameof(Administradoras.Condominios))]
        public virtual Administradoras IdAdministradoraNavigation { get; set; }
        [InverseProperty("IdCondominioNavigation")]
        public virtual ICollection<Usuarios> Usuarios { get; set; }
    }
}
