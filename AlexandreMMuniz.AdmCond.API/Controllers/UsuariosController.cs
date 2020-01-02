using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlexandreMMuniz.AdmCond.API.Models.AlexandreMMunizAdmCondSQLDB;
using AlexandreMMuniz.AdmCond.API.Models;
using Microsoft.Extensions.Logging;

namespace AlexandreMMuniz.AdmCond.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class UsuariosController : ControllerBase
    {
        private readonly ILogger<CondominiosController> _logger;
        private readonly AlexandreMMunizAdmCondContext _context;

        public UsuariosController(ILogger<CondominiosController> logger, AlexandreMMunizAdmCondContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Inclui um novo usuário
        /// </summary>
        /// <remarks>Inclui um novo usuário e retorna a chave de identificação do mesmo.</remarks>
        /// <param name="usuario">Informações do usuário.</param>
        /// <returns></returns>
        /// <response code="201">Usuário incluído com sucesso. Retorna a chave de identificação do usuário.</response>
        /// <response code="400">Não foi possível incluir o usuário. Retorna a mensagem de erro.</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Usuario2Model>> Post(Usuario1Model usuario)
        {
            bool condominioExiste = await _context.Condominios
                .AnyAsync(cond => cond.Id == usuario.IdCondominio).ConfigureAwait(false);

            if (!condominioExiste)
            {
                ModelState.AddModelError("IdCondominio", $"O condomínio informado, {usuario.IdCondominio}, não existe.");

                ValidationProblemDetails vp = new ValidationProblemDetails(ModelState);

                return BadRequest(vp);
            }

            Usuarios novoUsuario = new Usuarios();

            novoUsuario.Nome = usuario.Nome;
            novoUsuario.Email = usuario.Email;
            novoUsuario.IdCondominio = usuario.IdCondominio;
            novoUsuario.TipoUsuario = (byte)usuario.TipoUsuario;

            _context.Usuarios.Add(novoUsuario);

            await _context.SaveChangesAsync().ConfigureAwait(false);

            Usuario2Model key = new Usuario2Model() { Id = novoUsuario.Id };

            return Created(string.Empty, key);
        }
    }
}
