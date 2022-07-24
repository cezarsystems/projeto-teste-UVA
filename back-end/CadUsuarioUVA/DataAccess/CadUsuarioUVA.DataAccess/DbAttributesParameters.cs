namespace CadUsuarioUVA.DataAccess
{
    public class DbAttributesParameters
    {
        public string NomeStoredProcedure { get; set; }

        public TipoOperacaoEnum TipoOperacao { get; set; }

        public DbAttributesParameters(string _nomeStoredProcedure, TipoOperacaoEnum _tipoOperacao)
        {
            NomeStoredProcedure = _nomeStoredProcedure;
            TipoOperacao = _tipoOperacao;
        }       

        public enum TipoOperacaoEnum
        {
            Insert,
            Update,
            Delete,
            Select
        }        
    }
}