using System;

namespace CadUsuarioUVA.Entities.Model
{
    public class PessoaEntityModel
    {
        public long CodigoUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string CPFUsuario { get; set; }
        public string TelefoneUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}