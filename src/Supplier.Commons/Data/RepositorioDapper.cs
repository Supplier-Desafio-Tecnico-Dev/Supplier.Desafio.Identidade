using Dapper;
using System.Data;

namespace Supplier.Commons.Data;

public class RepositorioDapper<T> : IRepositorioDapper<T> where T : class
{
    public readonly IDbConnection session;

    public RepositorioDapper(DapperDbContext dapperContext)
    {
        session = dapperContext.CreateConnection();
    }
}
