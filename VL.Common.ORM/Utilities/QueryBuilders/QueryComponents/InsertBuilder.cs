using System.Data.Common;
using VL.Common.DAS.Objects;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    /// <summary>
    /// Insert Values
    /// </summary>
    public class InsertBuilder : IQueryBuilder
    {
        private ComponentValue componentValue;

        public ComponentValue ComponentValue
        {
            get
            {
                if (componentValue == null)
                {
                    componentValue = new ComponentValue(this);
                }
                return componentValue;
            }

            set
            {
                componentValue = value;
            }
        }

        public override void AppendQueryParameter(ref DbCommand command, DbSession session)
        {
            command.AddParameter(session, ComponentValue.Values);
        }

        public override string ToQueryString(DbSession session, string tableName)
        {
            return string.Format("insert into {0}({1}) values({2})", tableName, ComponentValue.ToQueryComponentOfFields(), ComponentValue.ToQueryComponentOfValues(session));
        }
    }
}
