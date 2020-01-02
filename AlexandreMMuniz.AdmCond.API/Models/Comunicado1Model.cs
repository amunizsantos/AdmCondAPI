using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlexandreMMuniz.AdmCond.API.Models
{
    public class Comunicado1Model
    {
        [Required(ErrorMessage = "É obrigatório informar o campo IdUsuarioRemetente.")]
        [Range(1, int.MaxValue, ErrorMessage = "O campo IdUsuarioRemetente precisa ser maior que 0 (zero).")]
        public int IdUsuarioRemetente { get; set; }        

        [Required(ErrorMessage = "É obrigatório informar o campo Assunto.")]
        [EnumDataType(typeof(ComunicadoAssuntoEnum), ErrorMessage = "O valor do campo Assunto é inválido. Utilize um dos valores permitidos pelo campo.")]
        public ComunicadoAssuntoEnum Assunto { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o campo Mensagem.")]
        [StringLength(500, ErrorMessage = "O campo Mensagem não pode conter mais de 500 caracteres.")]
        public string Mensagem { get; set; }

        /// <summary>
        /// Tipo do assunto.
        /// Legenda dos valores: 1 = Administrativo e 2 = Condominial.
        /// </summary>
        public enum ComunicadoAssuntoEnum
        {
            Administrativo = 1,
            Condominial = 2
        }
    }
}
