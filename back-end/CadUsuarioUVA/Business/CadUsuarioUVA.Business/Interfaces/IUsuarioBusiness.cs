using CadUsuarioUVA.Entities.Model;
using System.Collections.Generic;

namespace CadUsuarioUVA.Business.Interfaces
{
    public interface IUsuarioBusiness
    {
        IEnumerable<PessoaEntityModel> GetUsuariosBySearch(PesquisaCadastroPessoaEntityModel filtroPesquisa);
        IEnumerable<PessoaEntityModel> GetAllUsuarios();
        PessoaEntityModel GetByUsuarioId(long id);
        bool PostUsuario(PessoaEntityModel usuarioModel);
        bool PutUsuario(PessoaEntityModel usuarioModel);
        bool DeleteUsuario(long id);
    }
}