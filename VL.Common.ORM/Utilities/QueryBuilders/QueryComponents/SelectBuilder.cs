using System;
using System.Data.Common;
using VL.Common.DAS.Objects;
using VL.Common.ORM.Utilities.Interfaces;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    /// <summary>
    /// Select只能由内部设置进行操作,不能通过外部传递参数
    /// 内部直接拼接字符串
    /// </summary>
    public class SelectBuilder : IQueryBuilder
    {
        private ComponentOfSelect componentSelect;
        private ComponentOfWhere componentWhere;
        private ComponentOfOrder componentOrder;
        ////TODO
        //private ComponentOfPager componentOfPager;
        ///// <summary>
        ///// 属性,操作,值
        ///// </summary>
        //public class ComponentOfPager : IComponentBuilder, IQueriable
        //{
        //    /// <summary>
        //    /// 分页大小
        //    /// </summary>
        //    public int PageSize { set; get; } = 1;
        //    /// <summary>
        //    /// 分页页码,从1开始
        //    /// </summary>
        //    public int PageIndex { set; get; } = 1;

        //    /// <summary>
        //    /// 构造函数,实现上下级链
        //    /// </summary>
        //    /// <param name="queryBuilder"></param>
        //    public ComponentOfPager(IQueryBuilder queryBuilder) : base(queryBuilder)
        //    {
        //    }

        //    /// <summary>
        //    /// 所有的支持IQuerable的元素都可以转换为query
        //    /// </summary>
        //    /// <param name="session"></param>
        //    /// <returns></returns>
        //    public string ToQueryString(DbSession session)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        /// <summary>
        /// Select部分
        /// </summary>
        public ComponentOfSelect ComponentSelect
        {
            get
            {
                if (componentSelect == null)
                {
                    componentSelect = new ComponentOfSelect(this);
                }
                return componentSelect;
            }

            set
            {
                componentSelect = value;
            }
        }
        /// <summary>
        /// Where部分
        /// </summary>
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
        /// <summary>
        /// Order部分
        /// </summary>
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

        /// <summary>
        /// 构建query
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public override string ToQueryString(DbSession session)
        {
            return string.Format("{0} from {1}{2}{3}", ComponentSelect.ToQueryString(session), TableName
                , ComponentWhere.ToQueryString(session)
                , ComponentOrder.ToQueryString(session));
        }
        /// <summary>
        /// 添加query语句所对应的参数
        /// </summary>
        /// <param name="command"></param>
        /// <param name="session"></param>
        public override void AddParameter(DbCommand command, DbSession session)
        {
            ComponentWhere.AddParameter(command, session);
        }
    }
}
