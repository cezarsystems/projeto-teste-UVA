using CadUsuarioUVA.DataAccess.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CadUsuarioUVA.DataAccess.Implementations
{
    public class DbBasicOperations : IDbBasicOperations, IDisposable
    {
        protected SqlConnection _conn;

        DbConnectionFactory dbConnectionFactory = new DbConnectionFactory();

        public IEnumerable<T> Selecao<T>(DynamicParameters dynamicParameters, DbAttributesParameters dbAttributesParameters)
        {
            _conn = dbConnectionFactory.GetDataBaseConnection();

            IEnumerable<T> _resultado = null;

            object lockConnection = new object();

            lock (lockConnection)
            {
                try
                {
                    using (_conn)
                    {
                        if (_conn.State != ConnectionState.Open)
                            _conn.Open();

                        SetaTipoOperacao(ref dynamicParameters, DbAttributesParameters.TipoOperacaoEnum.Select);

                        _resultado = _conn.Query<T>(
                            dbAttributesParameters.NomeStoredProcedure,
                            dynamicParameters,
                            commandType: CommandType.StoredProcedure,
                            commandTimeout: 3600);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Falha ao executar a operação na base de dados UVA - Select cadastro de usuário: " + ex.Message);
                }
                finally
                {
                    Dispose();
                }
            }

            return _resultado;
        }

        public int InUpDe(DynamicParameters dynamicParameters, DbAttributesParameters dbAttributesParameters)
        {
            _conn = dbConnectionFactory.GetDataBaseConnection();

            SqlTransaction sqlTransaction = null;

            int _linhasAfetadas = int.MinValue;

            object lockConnection = new object();

            lock (lockConnection)
            {
                try
                {
                    using (_conn)
                    {
                        if (_conn.State != ConnectionState.Open)
                            _conn.Open();

                        using (sqlTransaction = _conn.BeginTransaction("transacao-cad-usuario-uva"))
                        {
                            SetaTipoOperacao(ref dynamicParameters, dbAttributesParameters.TipoOperacao);

                            _linhasAfetadas = _conn.Execute(
                                dbAttributesParameters.NomeStoredProcedure,
                                dynamicParameters,
                                commandType: CommandType.StoredProcedure,
                                transaction: sqlTransaction,
                                commandTimeout: 3600);

                            sqlTransaction.Commit();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Falha ao executar a operação na base de dados UVA - Insert cadastro de usuário: " + ex.Message);
                }
                finally
                {
                    Dispose();
                }
            }

            return _linhasAfetadas;
        }

        private void SetaTipoOperacao(ref DynamicParameters dynamicParameters, DbAttributesParameters.TipoOperacaoEnum tipoOperacaoEnum)
        {
            switch (tipoOperacaoEnum)
            {
                case DbAttributesParameters.TipoOperacaoEnum.Select:
                    dynamicParameters.Add("OPC_OPERACAO", "SEL", DbType.String, ParameterDirection.Input);
                    break;
                case DbAttributesParameters.TipoOperacaoEnum.Insert:
                    dynamicParameters.Add("OPC_OPERACAO", "INS", DbType.String, ParameterDirection.Input);
                    break;
                case DbAttributesParameters.TipoOperacaoEnum.Update:
                    dynamicParameters.Add("OPC_OPERACAO", "UPD", DbType.String, ParameterDirection.Input);
                    break;
                case DbAttributesParameters.TipoOperacaoEnum.Delete:
                    dynamicParameters.Add("OPC_OPERACAO", "DEL", DbType.String, ParameterDirection.Input);
                    break;
                default:
                    break;
            }
        }

        public void Dispose()
        {
            if (_conn != null)
            {
                if (_conn.State != ConnectionState.Closed)
                    _conn.Close();

                _conn.Dispose();
                _conn = null;
            }
        }
    }
}