using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlexandreMMuniz.AdmCond.API.Models.AlexandreMMunizAdmCondSQLDB
{
    public partial class Usuarios
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        public int IdCondominio { get; set; }
        public byte TipoUsuario { get; set; }

        [ForeignKey(nameof(IdCondominio))]
        [InverseProperty(nameof(Condominios.Usuarios))]
        public virtual Condominios IdCondominioNavigation { get; set; }
    }
}
