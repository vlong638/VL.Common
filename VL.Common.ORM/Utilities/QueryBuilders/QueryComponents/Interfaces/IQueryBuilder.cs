using System.Data.Common;
using VL.ADO.NET.Objects;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    public abstract class IQueryBuilder
    {
        public string TableName { get; set; }
        public abstract string ToQueryString(DbSession session,string tableName);
        public abstract void AppendQueryParameter(ref DbCommand command, DbSession session);
    }
}
