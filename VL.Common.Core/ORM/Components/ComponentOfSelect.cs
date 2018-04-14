using System.Collections.Generic;
using System.Linq;
using VL.Common.Core.DAS;

namespace VL.Common.Core.ORM
{
    /// <summary>
    /// 属性,操作,值
    /// </summary>
    public class ComponentOfSelect : IComponentBuilder, IQueriable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="queryBuilder"></param>
        public ComponentOfSelect(IQueryBuilder queryBuilder) : base(queryBuilder)
        {
            Selects = new List<ComponentValueOfSelect>();
        }

        List<ComponentValueOfSelect> Selects { set; get; }
        /// <summary>
        /// 新增查询项
        /// </summary>
        /// <param name="select"></param>
        public void Add(ComponentValueOfSelect select)
        {
            Selects.Add(select);
        }
        /// <summary>
        /// 新增查询项
        /// </summary>
        public void Add(string fieldName, string alias = null)
        {
            this.Add(new ComponentValueOfSelect(fieldName, alias));
        }
        /// <summary>
        /// 新增查询项
        /// </summary>
        /// <param name="select"></param>
        public void Add(PDMDbProperty property)
        {
            this.Add(property.GetFieldName());
        }
        /// <summary>
        /// 转换为Query
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public string ToQueryString(DbSession session)
        {
            if (Selects.Count == 0)
            {
                return "select *";
            }
            else
            {
                return "select " + string.Join(",", Selects.Select(c => string.IsNullOrEmpty(c.Alias) ? c.FieldName : c.Alias));
            }
        }
        /// <summary>
        /// 实际select的字段列表
        /// 无数据时为空列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetSelectFields()
        {
            if (Selects.Count()==0)
            {
                return new List<string>();
            }
            else
            {
                return Selects.Select(c => string.IsNullOrEmpty(c.Alias) ? c.FieldName : c.Alias).ToList();
            }
        }
    }
}
