using System.Data;

namespace AppTemplate.Core.Application.Abstractions.Data.Dapper;

public interface ISqlConnectionFactory
{
  IDbConnection CreateConnection();
}
