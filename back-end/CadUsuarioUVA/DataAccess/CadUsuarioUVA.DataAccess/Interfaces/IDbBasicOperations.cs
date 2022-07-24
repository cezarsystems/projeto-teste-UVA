using System.Collections.Generic;
using Dapper;

namespace CadUsuarioUVA.DataAccess.Interfaces
{
    public interface IDbBasicOperations
    {
        IEnumerable<T> Selecao<T>(DynamicParameters dynamicParameters, DbAttributesParameters dbAttributesParameters);
        int InUpDe(DynamicParameters dynamicParameters, DbAttributesParameters dbAttributesParameters);
    }
}