using System;
using System.Collections.Generic;
using System.Data.Common;
using VL.ADO.NET.Objects;
using VL.ORM.DbOperateLib.Objects;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    /// <summary>
    /// Select只能由内部设置进行操作,不能通过外部传递参数
    /// 内部直接拼接字符串
    /// </summary>
    public class SelectBuilder: IQueryBuilder
    {
        private ComponentFieldAliases componentFieldAliases;
        private ComponentWhere componentWhere;
        private ComponentOrder componentOrder;

        public ComponentFieldAliases ComponentFieldAliases
        {
            get
            {
                if (componentFieldAliases == null)
                {
                    componentFieldAliases = new ComponentFieldAliases(this);
                }
                return componentFieldAliases;
            }

            set
            {
                componentFieldAliases = value;
            }
        }
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
        public ComponentOrder ComponentOrder
        {
            get
            {
                if (componentOrder == null)
                {
                    componentOrder = new ComponentOrder(this);
                }
                return componentOrder;
            }

            set
            {
                componentOrder = value;
            }
        }

        public override string ToQueryString(DbSession session, string tableName)
        {
            return string.Format("select {0} from {1}{2}{3}", ComponentFieldAliases.ToQueryComponentOfFieldAliases(), tableName
                , ComponentWhere.Wheres.Count > 0 ? " where " + ComponentWhere.ToQueryComponentOfWheres(session) : ""
                , ComponentOrder.Orders.Count > 0 ? " order by " + ComponentOrder.ToQueryComponentOfOrders() : "");
        }

        public override void AppendQueryParameter(ref DbCommand command, DbSession session)
        {
            command.AddParameter(session, ComponentWhere.Wheres);
        }
    }
}
