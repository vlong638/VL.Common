using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using VL.Common.DAS.Objects;
using VL.Common.ORM.Objects;
using VL.Common.ORM.Utilities.Interfaces;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    /// <summary>
    /// 属性,操作,值
    /// </summary>
    public class ComponentOfSet : IComponentBuilder, IQueriable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="queryBuilder"></param>
        public ComponentOfSet(IQueryBuilder queryBuilder) : base(queryBuilder)
        {
            Sets = new List<ComponentValueOfSet>();
        }

        List<ComponentValueOfSet> Sets { set; get; }
        /// <summary>
        /// 新增设置项
        /// </summary>
        /// <param name="set"></param>
        public void Add(ComponentValueOfSet set)
        {
            Sets.Add(set);
        }
        /// <summary>
        /// 新增设置项
        /// </summary>
        public void Add(PDMDbProperty property, object value, string nickName = null)
        {
            Sets.Add(new ComponentValueOfSet(property,value,nickName));
        }
        /// <summary>
        /// 新增设置项
        /// </summary>
        public void Add(PDMDbProperty property, object value, UpdateType updateType, string nickName = null)
        {
            Sets.Add(new ComponentValueOfSet(property, value, updateType, nickName));
        }
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="command"></param>
        /// <param name="session"></param>
        public void AddParameter(DbCommand command, DbSession session)
        {
            foreach (var Set in Sets)
            {
                Set.AddParameter(command, session);
            }
        }
        /// <summary>
        /// 转换为Query
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public string ToQueryString(DbSession session)
        {
            return " set " + string.Join(",", Sets.Select(c => c.ToQueryString(session)));
        }
    }
}
