namespace VL.Common.ORM.Utilities.QueryBuilders
{
    public abstract class ISubQueryBuilder
    {
        public ISubQueryBuilder(IQueryBuilder queryBuilder)
        {
            Parent = queryBuilder;
        }

        public IQueryBuilder Parent { set; get; }
    }
}
