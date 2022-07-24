using System;
using System.Configuration;
using System.Data.SqlClient;

namespace CadUsuarioUVA.DataAccess
{
    internal class DbConnectionFactory
    {
        private SqlConnection _sqlConnection;

        private static string _connectionString = string.Empty;

        private SqlConnection GetConfigConnection
        {
            get {
                if (_sqlConnection != null)
                    return _sqlConnection;
                else
                {
                    _sqlConnection = new SqlConnection();
                    return _sqlConnection;
                }
            }
        }

        private string GetConnectionString()
        {
            try
            {
                return _connectionString = ConfigurationManager.ConnectionStrings["CSDataBaseUVA"].ConnectionString;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao capturar a connection string da base de dados UVA no arquivo do WebConfig: " + ex.Message);
            }           
        }

        public SqlConnection GetDataBaseConnection()
        {
            try
            {
                GetConnectionString();

                if (string.IsNullOrEmpty(_connectionString))
                    throw new Exception("A connection string está vazia/sem valores no arquivo WebConfig");

                GetConfigConnection.ConnectionString = _connectionString;

                return GetConfigConnection;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao retornar a conexão com o banco de dados: " + ex.Message);
            }
        }
    }
}