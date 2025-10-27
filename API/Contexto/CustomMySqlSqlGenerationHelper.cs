using Microsoft.EntityFrameworkCore.Storage;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure.Internal;
using Pomelo.EntityFrameworkCore.MySql.Storage.Internal;

namespace API.Data.Context;

public class CustomMySqlSqlGenerationHelper : MySqlSqlGenerationHelper
{
    public CustomMySqlSqlGenerationHelper(
        RelationalSqlGenerationHelperDependencies dependencies,
        IMySqlOptions options)
        : base(dependencies, options)
    {
    }

    public override string GetSchemaName(string name, string schema)
        => schema; // <-- this is the first part that is needed to map schemas to databases 
}