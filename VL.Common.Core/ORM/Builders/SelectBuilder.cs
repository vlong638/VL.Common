using System.Data.Common;
using VL.Common.Core.DAS;

namespace VL.Common.Core.ORM
{
    /// <summary>
    /// Select 语句构建器
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
        //    public string ToQueryString(IDbSession session)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        /// <summary>
        /// Select 语法段
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
        /// Where 语法段
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
        /// Order 语法段
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
