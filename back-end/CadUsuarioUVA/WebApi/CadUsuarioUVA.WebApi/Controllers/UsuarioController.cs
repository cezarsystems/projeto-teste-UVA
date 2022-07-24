using CadUsuarioUVA.Business.Interfaces;
using CadUsuarioUVA.Entities.Model;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CadUsuarioUVA.WebApi.Controllers
{
    [EnableCors("*", "*", "*")]
    public class UsuarioController : ApiController
    {
        private readonly IUsuarioBusiness _iUsuarioBusiness;

        public UsuarioController(IUsuarioBusiness iUsuarioBusiness)
        {
            _iUsuarioBusiness = iUsuarioBusiness;
        }

        [HttpGet]
        [Route("api/usuarios")]
        public IHttpActionResult GetAllUsuarios()
        {
            return Ok(_iUsuarioBusiness.GetAllUsuarios());
        }

        [HttpGet]
        [Route("api/usuario/{codigoUsuario}")]
        public IHttpActionResult GetByUsuarioId([FromUri] long codigoUsuario)
        {
            if (codigoUsuario < 1)
                return BadRequest("O código do usuário recebido é inválido");

            return Ok(_iUsuarioBusiness.GetByUsuarioId(codigoUsuario));
        }

        [HttpPost]
        [Route("api/usuario/criar")]
        public IHttpActionResult PostUsuario([FromBody] PessoaEntityModel usuarioModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Os dados do usuário são inválidos");

            return Ok(_iUsuarioBusiness.PostUsuario(usuarioModel));
        }

        [HttpPost]
        [Route("api/usuarios-pesquisa")]
        public IHttpActionResult GetUsuariosBySearch([FromBody] PesquisaCadastroPessoaEntityModel filtroPesquisa)
        {
            return Ok(_iUsuarioBusiness.GetUsuariosBySearch(filtroPesquisa));
        }

        [HttpPut]
        [Route("api/usuario/atualizar")]
        public IHttpActionResult PutUsuario([FromBody] PessoaEntityModel usuarioModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Os dados do usuário são inválidos");

            return Ok(_iUsuarioBusiness.PutUsuario(usuarioModel));
        }

        [HttpDelete]
        [Route("api/usuario/deletar/{codigoUsuario}")]
        public IHttpActionResult DeleteUsuario([FromUri] long codigoUsuario)
        {
            if (codigoUsuario < 1)
                return BadRequest("O código do usuário recebido é inválido");

            return Ok(_iUsuarioBusiness.DeleteUsuario(codigoUsuario));
        }
    }
}