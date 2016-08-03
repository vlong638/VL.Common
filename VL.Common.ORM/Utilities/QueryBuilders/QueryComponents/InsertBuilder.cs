using System.Data.Common;
using VL.Common.DAS.Objects;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    /// <summary>
    /// Insert Values
    /// </summary>
    public class InsertBuilder : IQueryBuilder
    {
        private ComponentOfInsert componentInsert;

        public ComponentOfInsert ComponentInsert
        {
            get
            {
                if (componentInsert == null)
                {
                    componentInsert = new ComponentOfInsert(this);
                }
                return componentInsert;
            }

            set
            {
                componentInsert = value;
            }
        }

        public override void AppendQueryParameter(ref DbCommand command, DbSession session)
        {
            ComponentInsert.Values.AddParameter(session, command);
        }

        public override string ToQueryString(DbSession session)
        {
            return string.Format("insert into {0}{1}", TableName , ComponentInsert.ToQueryString(session));
        }
    }
}
