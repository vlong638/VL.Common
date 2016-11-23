using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using VL.Common.Core.DAS;

namespace VL.Common.Core.ORM
{
    /// <summary>
    /// 属性,操作,值
    /// </summary>
    public class ComponentOfInsert : IComponentBuilder, IQueriableWithParameter
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="queryBuilder"></param>
        public ComponentOfInsert(IQueryBuilder queryBuilder) : base(queryBuilder)
        {
            Values = new List<ComponentValueOfInsert>();
        }

        List<ComponentValueOfInsert> Values { set; get; }
        /// <summary>
        /// 新增插入项
        /// </summary>
        /// <param name="value"></param>
        public void Add(ComponentValueOfInsert value)
        {
            Values.Add(value);
        }
        /// <summary>
        /// 新增插入项
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <param name="nickName"></param>
        public void Add(PDMDbProperty property, object value, string nickName = null)
        {
            Values.Add(new ComponentValueOfInsert(property, value, nickName));
        }
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="command"></param>
        /// <param name="session"></param>
        public void AddParameter(DbCommand command, DbSession session)
        {
            foreach (var Value in Values)
            {
                Value.AddParameter(command, session);
            }
        }
        /// <summary>
        /// 转换为Query
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public string ToQueryString(DbSession session)
        {
            return string.Format("({0}) values({1})", string.Join(",", Values.Select(c => c.Property.Title)), string.Join(",", Values.Select(c => c.GetParameterName(session))));
        }
    }
}
