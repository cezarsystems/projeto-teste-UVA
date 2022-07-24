using CadUsuarioUVA.Business.Interfaces;
using CadUsuarioUVA.Entities.Model;
using CadUsuarioUVA.Repository.Interfaces;
using System.Collections.Generic;

namespace CadUsuarioUVA.Business.Implementations
{
    public class UsuarioBusiness : IUsuarioBusiness
    {
        private readonly IUsuarioRepository _iUsuarioRepository;

        public UsuarioBusiness(IUsuarioRepository iUsuarioRepository)
        {
            _iUsuarioRepository = iUsuarioRepository;
        }

        public IEnumerable<PessoaEntityModel> GetUsuariosBySearch(PesquisaCadastroPessoaEntityModel filtroPesquisa)
        {
            return _iUsuarioRepository.GetUsuariosBySearch(filtroPesquisa);
        }

        public IEnumerable<PessoaEntityModel> GetAllUsuarios()
        {
            return _iUsuarioRepository.GetAllUsuarios();
        }

        public PessoaEntityModel GetByUsuarioId(long id)
        {
            return _iUsuarioRepository.GetByUsuarioId(id);
        }

        public bool PostUsuario(PessoaEntityModel usuarioModel)
        {
            return _iUsuarioRepository.PostUsuario(usuarioModel) > 0;
        }

        public bool PutUsuario(PessoaEntityModel usuarioModel)
        {
            return _iUsuarioRepository.PutUsuario(usuarioModel) > 0;
        }

        public bool DeleteUsuario(long id)
        {
            return _iUsuarioRepository.DeleteUsuario(id) > 0;
        }
    }
}