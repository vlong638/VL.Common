using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using VL.Common.DAS.Objects;
using VL.Common.ORM.Objects;
using VL.Common.ORM.Utilities;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    public static class QueryBuilderEx
    {
        public static void AddParameter(this DbCommand command, DbSession session, PDMDbPropertyOperateValue where)
        {
            //包含子查询
            if (where.SubSelect != null && where.SubSelect.ComponentWhere.Wheres.Count > 0)
            {
                foreach (var subWhere in where.SubSelect.ComponentWhere.Wheres)
                {
                    command.AddParameter(session, subWhere);
                }
            }
            else//不包含子查询
            {
                if (where.IsMultipleProperties)
                {
                    //TODO 多值化处理
                    //whereCondition.AppendFormat("({0})", where.SubSelect.ToQueryString(session));
                }
                else
                {
                    switch (where.Operator)
                    {
                        case OperatorType.Equal:
                        case OperatorType.NotEqual:
                            command.Parameters.Add(where.Property.GetDbParameter(session, where.Value));
                            break;
                        case OperatorType.In:
                        case OperatorType.NotIn:
                            if (where.IsMultipleProperties)
                            {
                                command.Parameters.Add(DbTranslateHelper.GetDbParameter(session, session.GetParameterPrefix() + string.Join("", where.Properties.Select(c => c.Title)), where.Value));
                            }
                            else
                            {
                                var parameterPlaceholder = session.GetParameterPrefix() + where.Property.Title;
                                switch (where.Property.Type)
                                {
                                    case PDMDataType.varchar:
                                        var varchars = where.Value as IEnumerable<String>;
                                        if (varchars == null)
                                        {
                                            throw new NotImplementedException("参数需为" + typeof(IEnumerable<String>).Name + "规格");
                                        }
                                        command.CommandText = command.CommandText.Replace(parameterPlaceholder, "'" + string.Join("','", varchars) + "'");
                                        break;
                                    case PDMDataType.numeric:
                                        switch (where.Property.GetCSharpDataType())
                                        {
                                            case CSharpDataType.String:
                                                var stringValues = GetEnumerableValues<String>(where);
                                                command.CommandText = command.CommandText.Replace(parameterPlaceholder, string.Join(",", stringValues));
                                                break;
                                            case CSharpDataType.Decimal:
                                                var decimalValues = GetEnumerableValues<Decimal>(where);
                                                command.CommandText = command.CommandText.Replace(parameterPlaceholder, string.Join(",", decimalValues));
                                                break;
                                            case CSharpDataType.Int64:
                                                var int64Values = GetEnumerableValues<Int64>(where);
                                                command.CommandText = command.CommandText.Replace(parameterPlaceholder, string.Join(",", int64Values));
                                                break;
                                            case CSharpDataType.Int32:
                                                var int32Values = GetEnumerableValues<Int32>(where);
                                                command.CommandText = command.CommandText.Replace(parameterPlaceholder, string.Join(",", int32Values));
                                                break;
                                            case CSharpDataType.Int16:
                                                var int16Values = GetEnumerableValues<Int16>(where);
                                                command.CommandText = command.CommandText.Replace(parameterPlaceholder, string.Join(",", int16Values));
                                                break;
                                            case CSharpDataType.Boolean:
                                                var booleanValues = GetEnumerableValues<Boolean>(where);
                                                command.CommandText = command.CommandText.Replace(parameterPlaceholder, string.Join(",", booleanValues));
                                                break;
                                            case CSharpDataType.DateTime:
                                                var datetimeValues = GetEnumerableValues<DateTime>(where);
                                                command.CommandText = command.CommandText.Replace(parameterPlaceholder, string.Join(",", datetimeValues));
                                                break;
                                            default:
                                                throw new NotImplementedException("未支持该参数规格" + where.Property.Type);
                                        }
                                        break;
                                    case PDMDataType.datetime:
                                        switch (session.DatabaseType)
                                        {
                                            case EDatabaseType.Oracle:
                                                var times = where.Value as IEnumerable<DateTime>;
                                                if (times == null)
                                                {
                                                    throw new NotImplementedException("参数需为" + typeof(IEnumerable<DateTime>).Name + "规格");
                                                }
                                                command.CommandText = command.CommandText.Replace(parameterPlaceholder, string.Join(",", times.Select(c => "to_date('" + c + "','yyyy-mm-dd hh24:mi:ss')")));
                                                break;
                                            case EDatabaseType.MSSQL:
                                            case EDatabaseType.MySQL:
                                                command.CommandText = command.CommandText.Replace(parameterPlaceholder, "'" + string.Join("','", where.Value) + "'");
                                                break;
                                            default:
                                                throw new NotImplementedException("未支持该类型的日期parameter处理" + where.Property.Type.ToString());
                                        }
                                        break;
                                    case PDMDataType.uniqueidentifier:
                                        var guids = where.Value as IEnumerable<Guid>;
                                        if (guids == null)
                                        {
                                            throw new NotImplementedException("参数需为" + typeof(IEnumerable<Guid>).Name + "规格");
                                        }
                                        command.CommandText = command.CommandText.Replace(parameterPlaceholder, "'" + string.Join("','", guids) + "'");
                                        break;
                                    default:
                                        throw new NotImplementedException("未支持该类型的多值parameter处理" + where.Property.Type.ToString());
                                }
                            }
                            break;
                        default:
                            throw new NotImplementedException("未支持该类型的parameter处理" + where.Operator.ToString());
                    }
                }
            }
        }

        private static IEnumerable<T> GetEnumerableValues<T>(PDMDbPropertyOperateValue where)
        {
            var values = where.Value as IEnumerable<T>;
            if (values == null)
            {
                throw new NotImplementedException("参数需为IEnumerable<" + where.Property.Type + ">规格");
            }

            return values;
        }

        public static void AddParameter(this DbCommand command, DbSession session, List<PDMDbPropertyOperateValue> pdmDbPropertyOperateValues)
        {
            //增加Parameter
            foreach (var pdmDbPropertyOperateValue in pdmDbPropertyOperateValues)
            {
                command.AddParameter(session, pdmDbPropertyOperateValue);
            }
        }
        public static void AddParameter(this DbCommand command, DbSession session, PDMDbPropertyValue pdmDbPropertyValue)
        {
            command.Parameters.Add(pdmDbPropertyValue.Property.GetDbParameter(session, pdmDbPropertyValue.Value));
        }
        public static void AddParameter(this DbCommand command, DbSession session, List<PDMDbPropertyValue> pdmDbPropertyValues)
        {
            foreach (var pdmDbPropertyValue in pdmDbPropertyValues)
            {
                command.AddParameter(session, pdmDbPropertyValue);
            }
        }
    }
}
