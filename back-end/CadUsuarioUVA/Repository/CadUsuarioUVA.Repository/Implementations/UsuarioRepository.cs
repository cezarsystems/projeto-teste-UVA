using CadUsuarioUVA.DataAccess.Implementations;
using CadUsuarioUVA.Entities.Model;
using CadUsuarioUVA.Repository.Interfaces;
using System.Collections.Generic;
using Dapper;
using System.Data;
using CadUsuarioUVA.DataAccess;
using System.Linq;

namespace CadUsuarioUVA.Repository.Implementations
{
    public class UsuarioRepository : DbBasicOperations, IUsuarioRepository
    {
        public IEnumerable<PessoaEntityModel> GetUsuariosBySearch(PesquisaCadastroPessoaEntityModel filtroPesquisa)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("NUM_OPERACAO", 3, DbType.Int16, ParameterDirection.Input);
            dynamicParameters.Add("NOME_USUARIO", filtroPesquisa.NomeUsuario, DbType.String, ParameterDirection.Input);
            dynamicParameters.Add("CPF_USUARIO_PESQUISA", filtroPesquisa.CPFUsuario, DbType.String, ParameterDirection.Input);
            dynamicParameters.Add("EMAIL_USUARIO", filtroPesquisa.EmailUsuario, DbType.String, ParameterDirection.Input);
            dynamicParameters.Add("DATA_CRIACAO_INICIO", filtroPesquisa.DataCriacaoInicio, DbType.DateTime, ParameterDirection.Input);
            dynamicParameters.Add("DATA_CRIACAO_FIM", filtroPesquisa.DataCriacaoFim, DbType.Date, ParameterDirection.Input);

            DbAttributesParameters dbAttributesParameters = new DbAttributesParameters("SP_CAD_USUARIO_UVA", DbAttributesParameters.TipoOperacaoEnum.Select);

            return Selecao<PessoaEntityModel>(dynamicParameters, dbAttributesParameters);
        }

        public IEnumerable<PessoaEntityModel> GetAllUsuarios()
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("NUM_OPERACAO", 1, DbType.Int16, ParameterDirection.Input);

            DbAttributesParameters dbAttributesParameters = new DbAttributesParameters("SP_CAD_USUARIO_UVA", DbAttributesParameters.TipoOperacaoEnum.Select);

            return Selecao<PessoaEntityModel>(dynamicParameters, dbAttributesParameters);
        }

        public PessoaEntityModel GetByUsuarioId(long id)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("NUM_OPERACAO", 2, DbType.Int16, ParameterDirection.Input);
            dynamicParameters.Add("COD_USUARIO", id, DbType.Int64, ParameterDirection.Input);


            DbAttributesParameters dbAttributesParameters = new DbAttributesParameters("SP_CAD_USUARIO_UVA", DbAttributesParameters.TipoOperacaoEnum.Select);

            return Selecao<PessoaEntityModel>(dynamicParameters, dbAttributesParameters).FirstOrDefault();
        }

        public int PostUsuario(PessoaEntityModel usuarioModel)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("NUM_OPERACAO", 1, DbType.Int16, ParameterDirection.Input);
            dynamicParameters.Add("NOME_USUARIO", usuarioModel.NomeUsuario, DbType.String, ParameterDirection.Input);
            dynamicParameters.Add("CPF_USUARIO", usuarioModel.CPFUsuario, DbType.String, ParameterDirection.Input);
            dynamicParameters.Add("TELEFONE_USUARIO", usuarioModel.TelefoneUsuario, DbType.String, ParameterDirection.Input);
            dynamicParameters.Add("EMAIL_USUARIO", usuarioModel.EmailUsuario, DbType.String, ParameterDirection.Input);
            dynamicParameters.Add("ATIVO", usuarioModel.Ativo, DbType.Boolean, ParameterDirection.Input);

            DbAttributesParameters dbAttributesParameters = new DbAttributesParameters("SP_CAD_USUARIO_UVA", DbAttributesParameters.TipoOperacaoEnum.Insert);

            return InUpDe(dynamicParameters, dbAttributesParameters);
        }

        public int PutUsuario(PessoaEntityModel usuarioModel)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("NUM_OPERACAO", 1, DbType.Int16, ParameterDirection.Input);
            dynamicParameters.Add("COD_USUARIO", usuarioModel.CodigoUsuario, DbType.Int64, ParameterDirection.Input);
            dynamicParameters.Add("NOME_USUARIO", usuarioModel.NomeUsuario, DbType.String, ParameterDirection.Input);
            dynamicParameters.Add("CPF_USUARIO", usuarioModel.CPFUsuario, DbType.String, ParameterDirection.Input);
            dynamicParameters.Add("TELEFONE_USUARIO", usuarioModel.TelefoneUsuario, DbType.String, ParameterDirection.Input);
            dynamicParameters.Add("EMAIL_USUARIO", usuarioModel.EmailUsuario, DbType.String, ParameterDirection.Input);
            dynamicParameters.Add("ATIVO", usuarioModel.Ativo, DbType.Boolean, ParameterDirection.Input);

            DbAttributesParameters dbAttributesParameters = new DbAttributesParameters("SP_CAD_USUARIO_UVA", DbAttributesParameters.TipoOperacaoEnum.Update);

            return InUpDe(dynamicParameters, dbAttributesParameters);
        }

        public int DeleteUsuario(long id)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("NUM_OPERACAO", 1, DbType.Int16, ParameterDirection.Input);
            dynamicParameters.Add("COD_USUARIO", id, DbType.Int64, ParameterDirection.Input);

            DbAttributesParameters dbAttributesParameters = new DbAttributesParameters("SP_CAD_USUARIO_UVA", DbAttributesParameters.TipoOperacaoEnum.Delete);

            return InUpDe(dynamicParameters, dbAttributesParameters);
        }        
    }
}