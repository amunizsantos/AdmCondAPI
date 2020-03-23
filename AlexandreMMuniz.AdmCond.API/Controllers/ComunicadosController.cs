using AlexandreMMuniz.AdmCond.API.Models;
using AlexandreMMuniz.AdmCond.API.Models.AlexandreMMunizAdmCondSQLDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexandreMMuniz.AdmCond.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")] 
    [Produces("application/json")]
    public class ComunicadosController : ControllerBase
    {
        private readonly ILogger<CondominiosController> _logger;
        private readonly AlexandreMMunizAdmCondContext _context;

        public ComunicadosController(ILogger<CondominiosController> logger, AlexandreMMunizAdmCondContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Envia um comunicado de um morador para o síndico/zelador ou para a administradora.
        /// </summary>
        /// <remarks>
        /// Envia um comunicado de um morador para o síndico do seu condomínio. Caso não haja síndico no condomínio, 
        /// o mesmo será enviado ao zelador. Quando o assunto for administrativo, o comunicado será enviado 
        /// para a administradora do condominio.
        /// </remarks>
        /// <param name="comunicado">Dados do comunicado.</param>
        /// <returns></returns>
        /// <response code="200">Comunicado enviado com sucesso.</response>
        /// <response code="400">Não foi possível enviar o comunicado. Retorna a mensagem de erro.</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Post(Comunicado1Model comunicado)
        {
            Usuarios usuarioRemetente;
            Usuarios usuarioDestinatario;
            StringBuilder sbMensagem;

            usuarioRemetente = await _context.Usuarios
                .FirstOrDefaultAsync(usr => usr.Id == comunicado.IdUsuarioRemetente && usr.TipoUsuario == 1).ConfigureAwait(false);

            if (usuarioRemetente == null)
            {
                ModelState.AddModelError("IdUsuarioRemetente", $"O usuário remetente informado, Id {comunicado.IdUsuarioRemetente}, não existe ou não é um usuário do tipo Morador.");

                ValidationProblemDetails vp = new ValidationProblemDetails(ModelState);

                return BadRequest(vp);
            }

            if (comunicado.Assunto == Comunicado1Model.ComunicadoAssuntoEnum.Administrativo)
            {
                usuarioDestinatario = await _context.Usuarios
                    .Where(usr => usr.IdCondominio == usuarioRemetente.IdCondominio && usr.TipoUsuario == 3)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                if (usuarioDestinatario == null)
                {
                    ModelState.AddModelError("Destinatario", $"Não existe um usuário do tipo Administradora cadastrado para o condomínio Id {usuarioRemetente.IdCondominio}");

                    ValidationProblemDetails vp = new ValidationProblemDetails(ModelState);

                    return BadRequest(vp);
                }
            }
            else
            {
                usuarioDestinatario = await _context.Usuarios
                    .Where(usr => usr.IdCondominio == usuarioRemetente.IdCondominio && (usr.TipoUsuario == 2 || usr.TipoUsuario == 4))                    
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                if (usuarioDestinatario == null)
                {
                    ModelState.AddModelError("Destinatario", $"Não existe um usuário do tipo Síndico ou do tipo Zelador cadastrado para o condomínio Id {usuarioRemetente.IdCondominio}");

                    ValidationProblemDetails vp = new ValidationProblemDetails(ModelState);

                    return BadRequest(vp);
                }
            }

            sbMensagem = new StringBuilder();

            sbMensagem.AppendLine($"De: {usuarioRemetente.Nome}");
            sbMensagem.AppendLine($"Para: {usuarioDestinatario.Nome}");
            sbMensagem.AppendLine(string.Empty);
            sbMensagem.AppendLine($"Assunto: {comunicado.Assunto}");
            sbMensagem.AppendLine(string.Empty);
            sbMensagem.AppendLine($"Mensagem: {comunicado.Mensagem}");

            _logger.LogWarning(sbMensagem.ToString());

            return Ok();
        }        
    }
}
