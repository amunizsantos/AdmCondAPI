using System;
using System.ComponentModel.DataAnnotations;

namespace AlexandreMMuniz.AdmCond.API.Models
{
    public class Condominio1Model
    {
        [Required(ErrorMessage = "É obrigatório informar o campo Nome.")]
        [StringLength(100, ErrorMessage = "O campo Nome não pode conter mais de 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o campo IdAdministradora.")]
        [Range(1, int.MaxValue, ErrorMessage = "O campo IdAdministradora precisa ser maior que 0 (zero).")]
        public int IdAdministradora { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o campo Responsável.")]
        [EnumDataType(typeof(ResponsavelEnum), ErrorMessage = "O valor do campo Responsavel é inválido. Utilize um dos valores permitidos pelo campo.")]
        public ResponsavelEnum Responsavel { get; set; }

        /// <summary>
        /// Tipo do usuário responsável pelo condomínio.
        /// Legenda dos valores: 2 = Síndico e 4 = Zelador.
        /// </summary>
        public enum ResponsavelEnum
        {   
            Sindico = 2,         
            Zelador = 4
        }
    }
}
