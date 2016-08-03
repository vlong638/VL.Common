using System;
using System.Data.Common;
using VL.Common.DAS.Objects;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    /// <summary>
    /// Select只能由内部设置进行操作,不能通过外部传递参数
    /// 内部直接拼接字符串
    /// </summary>
    public class SelectBuilder : IQueryBuilder
    {
        private ComponentOfSelect componentSelectField;
        private ComponentOfWhere componentWhere;
        private ComponentOfOrder componentOrder;

        public ComponentOfSelect ComponentSelect
        {
            get
            {
                if (componentSelectField == null)
                {
                    componentSelectField = new ComponentOfSelect(this);
                }
                return componentSelectField;
            }

            set
            {
                componentSelectField = value;
            }
        }
        public ComponentOfWhere ComponentWhere
        {
            get
            {
                if (componentWhere == null)
                {
                    componentWhere = new ComponentOfWhere(this);
                }
                return componentWhere;
            }
            set
            {
                componentWhere = value;
            }
        }
        public ComponentOfOrder ComponentOrder
        {
            get
            {
                if (componentOrder == null)
                {
                    componentOrder = new ComponentOfOrder(this);
                }
                return componentOrder;
            }

            set
            {
                componentOrder = value;
            }
        }

        public override string ToQueryString(DbSession session)
        {
            return string.Format("{0} from {1}{2}{3}", ComponentSelect.ToQueryString(session), TableName
                , ComponentWhere.Wheres.Count > 0 ? ComponentWhere.ToQueryString(session) : ""
                , ComponentOrder.Orders.Count > 0 ?  ComponentOrder.ToQueryString(session) : "");
        }

        public override void AppendQueryParameter(ref DbCommand command, DbSession session)
        {
            ComponentWhere.Wheres.AddParameter(session, command);
        }
    }
}
