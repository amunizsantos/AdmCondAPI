using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlexandreMMuniz.AdmCond.API.Models.AlexandreMMunizAdmCondSQLDB
{
    public partial class Assuntos
    {
        [Key]
        public byte Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Tipo { get; set; }
    }
}
