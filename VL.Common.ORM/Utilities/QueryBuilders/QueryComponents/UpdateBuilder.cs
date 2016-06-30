using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using VL.Common.DAS.Objects;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    public class UpdateBuilder: IQueryBuilder
    {
        private ComponentWhere componentWhere;
        private ComponentValue componentValue;

        public ComponentWhere ComponentWhere
        {
            get
            {
                if (componentWhere==null)
                {
                    componentWhere = new ComponentWhere(this);
                }
                return componentWhere;
            }
            set
            {
                componentWhere = value;
            }
        }
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

        public override string ToQueryString(DbSession session, string tableName)
        {
            return string.Format("update {0} set {1}{2}", tableName, ComponentValue.ToQueryComponentOfSets(session)
                , ComponentWhere.Wheres.Count > 0 ? " where " + ComponentWhere.ToQueryComponentOfWheres(session) : "");
        }

        public override void AppendQueryParameter(ref DbCommand command, DbSession session)
        {
            command.AddParameter(session, ComponentWhere.Wheres);
            command.AddParameter(session, ComponentValue.Values);
        }
    }
}
