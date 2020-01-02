using System;
using System.ComponentModel.DataAnnotations;

namespace AlexandreMMuniz.AdmCond.API.Models
{
    public class Usuario1Model
    {
        [Required(ErrorMessage = "É obrigatório informar o campo Nome.")]
        [StringLength(100, ErrorMessage = "O campo Nome não pode conter mais de 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o campo Email.")]
        [StringLength(100, ErrorMessage = "O campo Email não pode conter mais de 100 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o campo IdCondominio.")]
        [Range(1, int.MaxValue, ErrorMessage = "O campo IdCondominio precisa ser maior que 0 (zero).")]
        public int IdCondominio { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o campo TipoUsuario.")]
        [EnumDataType(typeof(TipoUsuarioEnum), ErrorMessage = "O valor do campo TipoUsuario é inválido. Utilize um dos valores permitidos pelo campo.")]
        public TipoUsuarioEnum TipoUsuario { get; set; }

        /// <summary>
        /// Tipo do usuário.
        /// Legenda dos valores: 1 = Morador; 2 = Síndico; 3 = Administradora e 4 = Zelador.
        /// </summary>
        public enum TipoUsuarioEnum
        {
            Morador = 1,
            Sindico = 2,
            Administradora = 3,
            Zelador = 4
        }
    }
}
