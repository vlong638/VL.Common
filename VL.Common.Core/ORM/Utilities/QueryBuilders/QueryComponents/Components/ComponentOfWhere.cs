using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using VL.Common.Object.ORM;
using VL.Common.DAS;


namespace VL.Common.ORM//.Utilities.QueryBuilders
{
    /// <summary>
    /// 属性,操作,值
    /// </summary>
    public class ComponentOfWhere : IComponentBuilder, IQueriable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="queryBuilder"></param>
        public ComponentOfWhere(IQueryBuilder queryBuilder) : base(queryBuilder)
        {
            Wheres = new List<ComponentValueOfWhere>();
        }

        List<ComponentValueOfWhere> Wheres { get; set; }
        /// <summary>
        /// 新增条件项
        /// </summary>
        public void Add(ComponentValueOfWhere where)
        {
            Wheres.Add(where);
        }
        /// <summary>
        /// 新增条件项
        /// </summary>
        public void Add(PDMDbProperty property, SelectBuilder subSelect, LocateType operatorType, string nickName = null)
        {
            Wheres.Add(new ComponentValueOfWhere(property, subSelect, operatorType, nickName));
        }
        /// <summary>
        /// 新增条件项
        /// </summary>
        public void Add(PDMDbProperty property, object value, LocateType operatorType, string nickName = null)
        {
            Wheres.Add(new ComponentValueOfWhere(property, value, operatorType, nickName));
        }
        /// <summary>
        /// 新增条件项
        /// </summary>
        public void Add(List<PDMDbProperty> Properties, object value, LocateType operatorType, string nickName)
        {
            Wheres.Add(new ComponentValueOfWhere(Properties, value, operatorType, nickName));
        }
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="command"></param>
        /// <param name="session"></param>
        public void AddParameter(DbCommand command, DbSession session)
        {
            foreach (var Where in Wheres)
            {
                Where.AddParameter(command, session);
            }
        }
        /// <summary>
        /// 转换为Query
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public string ToQueryString(DbSession session)
        {
            if (Wheres.Count()==0)
            {
                return "";
            }

            //TODO 对于Oracle支持(FieldA,FieldB) in ((ValueA1,ValueB1),(ValueA2,ValueB2)) 但是MSSQL不支持 需针对数据库优化
            //TODO 条件过长时应支持Split()操作,将一条语句的处理分散成多个处理 阶次拆分1表示一次二分,2表两次二分,依次类推
            StringBuilder whereCondition = new StringBuilder();
            whereCondition.Append(" where ");
            bool isFirst = true;
            foreach (var where in Wheres)
            {
                if (!isFirst)
                {
                    whereCondition.Append(" and ");
                }
                else
                {
                    isFirst = false;
                }
                //Fields
                if (where.IsMultipleProperties)
                {
                    whereCondition.AppendFormat("({0})", string.Join(",", where.Properties.Select(c => c.Title)));
                }
                else
                {
                    whereCondition.Append(where.Property.Title);
                }
                //Operator
                whereCondition.AppendFormat(" {0} ", where.Operator.ToQueryString());
                //Parameters
                if (where.SubSelect == null)
                {
                    switch (where.Operator)
                    {
                        case LocateType.Equal:
                        case LocateType.NotEqual:
                            whereCondition.Append(where.GetParameterName(session));
                            break;
                        case LocateType.In:
                        case LocateType.NotIn:
                            whereCondition.Append("(");
                            whereCondition.Append(where.GetParameterName(session));
                            //if (where.IsMultipleProperties)
                            //{
                            //    whereCondition.Append(session.GetParameterPrefix() + string.Join("", where.Properties.Select(c => c.Title)));
                            //}
                            //else
                            //{
                            //    whereCondition.Append(session.GetParameterPrefix() + where.Property.Title);
                            //}
                            whereCondition.Append(")");
                            break;
                        default:
                            throw new NotImplementedException("该操作暂不被支持" + where.Operator.ToString());
                    }
                }
                else
                {
                    whereCondition.AppendFormat("({0})", where.SubSelect.ToQueryString(session, Parent.TableName));
                }
            }
            return whereCondition.ToString();
        }
    }
}
