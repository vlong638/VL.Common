using VL.Common.DAS.Objects;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    public abstract class IComponentBuilder
    {
        public IComponentBuilder(IQueryBuilder queryBuilder)
        {
            Parent = queryBuilder;
        }

        public IQueryBuilder Parent { set; get; }
    }
    public interface IQueriable
    {
        string ToQueryString(DbSession session);
    }
}
