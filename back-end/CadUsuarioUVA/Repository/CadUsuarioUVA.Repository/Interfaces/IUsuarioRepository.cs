using CadUsuarioUVA.Entities.Model;
using System.Collections.Generic;

namespace CadUsuarioUVA.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        IEnumerable<PessoaEntityModel> GetUsuariosBySearch(PesquisaCadastroPessoaEntityModel filtroPesquisa);
        IEnumerable<PessoaEntityModel> GetAllUsuarios();
        PessoaEntityModel GetByUsuarioId(long id);
        int PostUsuario(PessoaEntityModel usuarioModel);
        int PutUsuario(PessoaEntityModel usuarioModel);
        int DeleteUsuario(long id);
    }
}