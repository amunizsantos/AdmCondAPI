using AlexandreMMuniz.AdmCond.API.Models;
using AlexandreMMuniz.AdmCond.API.Models.AlexandreMMunizAdmCondSQLDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AlexandreMMuniz.AdmCond.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class CondominiosController : ControllerBase
    {
        private readonly ILogger<CondominiosController> _logger;
        private readonly AlexandreMMunizAdmCondContext _context;

        public CondominiosController(ILogger<CondominiosController> logger, AlexandreMMunizAdmCondContext context)
        {
            _logger = logger;
            _context = context;

            
        }

        /// <summary>
        /// Inclui um novo condomínio
        /// </summary>
        /// <remarks>Inclui um novo condomínio e retorna a chave de identificação do mesmo.</remarks>
        /// <param name="condominio">Informações do condomínio.</param>
        /// <returns></returns>
        /// <response code="201">Condomínio incluído com sucesso. Retorna a chave de identificação do condomínio.</response>
        /// <response code="400">Não foi possível incluir o condomínio. Retorna a mensagem de erro.</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Condominio2Model>> Post(Condominio1Model condominio)
        {
            bool administradoraExiste = await _context.Administradoras
                .AnyAsync(adm => adm.Id == condominio.IdAdministradora).ConfigureAwait(false);

            if (!administradoraExiste)
            {
                ModelState.AddModelError("IdAdministradora", $"A administradora informada, {condominio.IdAdministradora}, não existe.");

                ValidationProblemDetails vp = new ValidationProblemDetails(ModelState);

                return BadRequest(vp);
            }

            Condominios novoCondominio = new Condominios();

            novoCondominio.Nome = condominio.Nome;
            novoCondominio.IdAdministradora = condominio.IdAdministradora;
            novoCondominio.Responsavel = (byte)condominio.Responsavel;

            _context.Condominios.Add(novoCondominio);

            await _context.SaveChangesAsync().ConfigureAwait(false);

            Condominio2Model key = new Condominio2Model() { Id = novoCondominio.Id };

            return Created(string.Empty, key);
        }        
    }
}
