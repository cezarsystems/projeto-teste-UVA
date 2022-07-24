using System;

namespace CadUsuarioUVA.Entities.Model
{
    public class PesquisaCadastroPessoaEntityModel
    {
        public string NomeUsuario { get; set; }
        public string CPFUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public DateTime? DataCriacaoInicio { get; set; }
        public DateTime? DataCriacaoFim { get; set; }
    }
}