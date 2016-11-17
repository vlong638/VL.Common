using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using VL.Common.Object.ORM;
using VL.Common.DAS;
using VL.Common.ORM;


namespace VL.Common.ORM//.Utilities.QueryBuilders
{
    /// <summary>
    /// 属性,操作,值
    /// </summary>
    public class ComponentValueOfWhere : IParameterizable
    {
        public SelectBuilder SubSelect { set; get; }
        public bool IsMultipleProperties { set; get; }
        public PDMDbProperty Property { set; get; }
        public List<PDMDbProperty> Properties { set; get; }
        public LocateType Operator { set; get; }
        public object Value { set; get; }
        public string NickName { set; get; }

        public ComponentValueOfWhere(PDMDbProperty property, SelectBuilder subSelect, LocateType operatorType, string nickName = null)
        {
            this.Property = property;
            this.IsMultipleProperties = false;
            this.Operator = operatorType;
            this.SubSelect = subSelect;
            this.NickName = !string.IsNullOrEmpty(nickName) ? nickName : property.Title;
        }
        public ComponentValueOfWhere(PDMDbProperty property, object value, LocateType operatorType, string nickName = null)
        {
            this.Property = property;
            this.IsMultipleProperties = false;
            this.Operator = operatorType;
            this.Value = value;
            this.NickName = !string.IsNullOrEmpty(nickName) ? nickName : property.Title;
        }
        public ComponentValueOfWhere(List<PDMDbProperty> Properties, object value, LocateType operatorType, string nickName)
        {
            this.Properties = Properties;
            this.IsMultipleProperties = true;
            this.Operator = operatorType;
            this.Value = value;
            this.NickName = nickName;
        }

        public string GetParameterName(DbSession session)
        {
            if (IsMultipleProperties)
            {
                return session.GetParameterPrefix() + (!string.IsNullOrEmpty(NickName)?NickName : string.Join("", Properties.Select(c => c.Title)));
            }
            else
            {
                return session.GetParameterPrefix() + (!string.IsNullOrEmpty(NickName)?NickName : Property.Title);
            }
        }
        public void AddParameter(DbCommand command, DbSession session)
        {
            //包含子查询
            if (this.SubSelect != null)// && this.SubSelect.ComponentWhere.Wheres.Count > 0)
            {
                throw new NotImplementedException("未实现子查询处理");
                //foreach (var subWhere in this.SubSelect.ComponentWhere.Wheres)
                //{
                //    subWhere.AddParameter(session, command);
                //}
            }
            else//不包含子查询
            {
                if (this.IsMultipleProperties)
                {
                    throw new NotImplementedException("未实现多属性处理");
                    //TODO 多值化处理
                    //whereCondition.AppendFormat("({0})", this.SubSelect.ToQueryString(session));
                }
                else
                {
                    switch (this.Operator)
                    {
                        case LocateType.Equal:
                        case LocateType.NotEqual:
                            command.Parameters.Add(this.Property.GetDbParameter(session, this.Value, this.NickName));
                            break;
                        case LocateType.In:
                        case LocateType.NotIn:
                            if (this.IsMultipleProperties)
                            {
                                command.Parameters.Add(DbTranslateHelper.GetDbParameter(session, this.GetParameterName(session), this.Value));
                            }
                            else
                            {
                                var parameterPlaceholder = this.GetParameterName(session);
                                switch (this.Property.Type)
                                {
                                    case PDMDataType.varchar:
                                        var varchars = this.Value as IEnumerable<String>;
                                        if (varchars == null)
                                        {
                                            throw new NotImplementedException("参数需为" + typeof(IEnumerable<String>).Name + "规格");
                                        }
                                        command.CommandText = command.CommandText.Replace(parameterPlaceholder, "'" + string.Join("','", varchars) + "'");
                                        break;
                                    case PDMDataType.numeric:
                                        switch (this.Property.GetCSharpDataType())
                                        {
                                            case CSharpDataType.@string:
                                                var stringValues = GetEnumerableValues<string>(this);
                                                command.CommandText = command.CommandText.Replace(parameterPlaceholder, string.Join(",", stringValues));
                                                break;
                                            case CSharpDataType.Decimal:
                                                var decimalValues = GetEnumerableValues<decimal>(this);
                                                command.CommandText = command.CommandText.Replace(parameterPlaceholder, string.Join(",", decimalValues));
                                                break;
                                            case CSharpDataType.Int64:
                                                var int64Values = GetEnumerableValues<long>(this);
                                                command.CommandText = command.CommandText.Replace(parameterPlaceholder, string.Join(",", int64Values));
                                                break;
                                            case CSharpDataType.Int32:
                                                var int32Values = GetEnumerableValues<int>(this);
                                                command.CommandText = command.CommandText.Replace(parameterPlaceholder, string.Join(",", int32Values));
                                                break;
                                            case CSharpDataType.Int16:
                                                var int16Values = GetEnumerableValues<short>(this);
                                                command.CommandText = command.CommandText.Replace(parameterPlaceholder, string.Join(",", int16Values));
                                                break;
                                            case CSharpDataType.Boolean:
                                                var booleanValues = GetEnumerableValues<bool>(this);
                                                command.CommandText = command.CommandText.Replace(parameterPlaceholder, string.Join(",", booleanValues));
                                                break;
                                            case CSharpDataType.DateTime:
                                                var datetimeValues = GetEnumerableValues<DateTime>(this);
                                                command.CommandText = command.CommandText.Replace(parameterPlaceholder, string.Join(",", datetimeValues));
                                                break;
                                            default:
                                                throw new NotImplementedException("未支持该参数规格" + this.Property.Type);
                                        }
                                        break;
                                    case PDMDataType.datetime:
                                        switch (session.DatabaseType)
                                        {
                                            case EDatabaseType.Oracle:
                                                var times = this.Value as IEnumerable<DateTime>;
                                                if (times == null)
                                                {
                                                    throw new NotImplementedException("参数需为" + typeof(IEnumerable<DateTime>).Name + "规格");
                                                }
                                                command.CommandText = command.CommandText.Replace(parameterPlaceholder, string.Join(",", times.Select(c => "to_date('" + c + "','yyyy-mm-dd hh24:mi:ss')")));
                                                break;
                                            case EDatabaseType.MSSQL:
                                            case EDatabaseType.MySQL:
                                                command.CommandText = command.CommandText.Replace(parameterPlaceholder, "'" + string.Join("','", this.Value) + "'");
                                                break;
                                            default:
                                                throw new NotImplementedException("未支持该类型的日期parameter处理" + this.Property.Type.ToString());
                                        }
                                        break;
                                    case PDMDataType.uniqueidentifier:
                                        var guids = this.Value as IEnumerable<Guid>;
                                        if (guids == null)
                                        {
                                            throw new NotImplementedException("参数需为" + typeof(IEnumerable<Guid>).Name + "规格");
                                        }
                                        command.CommandText = command.CommandText.Replace(parameterPlaceholder, "'" + string.Join("','", guids) + "'");
                                        break;
                                    default:
                                        throw new NotImplementedException("未支持该类型的多值parameter处理" + this.Property.Type.ToString());
                                }
                            }
                            break;
                        default:
                            throw new NotImplementedException("未支持该类型的parameter处理" + this.Operator.ToString());
                    }
                }
            }
        }
        private IEnumerable<T> GetEnumerableValues<T>(ComponentValueOfWhere where)
        {
            var values = where.Value as IEnumerable<T>;
            if (values == null)
            {
                throw new NotImplementedException("参数需为IEnumerable<" + typeof(T) + ">规格");
            }
            return values;
        }
    }
    /// <summary>
    /// 属性,操作,值
    /// </summary>
    public static class PDMDbPropertyLocateValueEx
    {
        /// <summary>
        /// 基于(属性,操作,值)的集合添加参数
        /// </summary>
        public static void AddParameter(this List<ComponentValueOfWhere> pdmDbPropertyOperateValues, DbSession session, DbCommand command)
        {
            //增加Parameter
            foreach (var pdmDbPropertyOperateValue in pdmDbPropertyOperateValues)
            {
                pdmDbPropertyOperateValue.AddParameter(command, session);
            }
        }
    }
    /// <summary>
    /// 操作符类别
    /// </summary>
    public enum LocateType
    {
        Equal,
        NotEqual,
        In,
        NotIn,
    }
}
