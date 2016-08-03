namespace VL.Common.ORM.Utilities.QueryBuilders
{
    /// <summary>
    /// 查询构造器
    /// </summary>
    public class MSSQLQueryBuilder : IDbQueryBuilder
    {
        #region old
        //private static string GetWhereCondition(DbSession session, Wheres wheres)
        //{
        //    StringBuilder whereCondition = new StringBuilder();
        //    foreach (var where in wheres)
        //    {
        //        AppendQuery(whereCondition, session, where);
        //    }
        //    return whereCondition.ToString();
        //}
        //private static void AppendQuery(StringBuilder whereCondition, DbSession session, PDMDbPropertyLocateValue where)
        //{
        //    if (where.IsMultipleProperties)
        //    {
        //        throw new NotImplementedException();
        //    }
        //    else if (where.SubSelect != null)
        //    {
        //        whereCondition.Append(where.Property.Title + " " + where.Operator.ToQueryString() + " (");
        //        AppendQuery(whereCondition, session, where.SubSelect);
        //        whereCondition.Append(")");
        //    }
        //    else
        //    {
        //        if (whereCondition.Length > 0)
        //        {
        //            whereCondition.Append(" and ");
        //        }
        //        whereCondition.Append(where.Property.Title + where.Operator.ToQueryString() + session.GetParameterPrefix() + where.Property.Title);
        //    }
        //} 

        //public override DbCommand GetCommand(DbSession session)
        //{
        //    var command = session.CreateCommand();
        //    switch (OperateType)
        //    {
        //        case OperateType.Insert:
        //            command.CommandText = InsertBuilders.ToQueryString(session, TableName);
        //            command.AddParameter(session, InsertBuilders.ComponentValue.Values);
        //            break;
        //        case OperateType.Select:
        //            command.CommandText = SelectBuilders.ToQueryString(session, TableName);
        //            command.AddParameter(session, SelectBuilders.ComponentWhere.Wheres);
        //            break;
        //        case OperateType.Update:
        //            command.CommandText = UpdateBuilders.ToQueryString(session, TableName);
        //            command.AddParameter(session, UpdateBuilders.ComponentWhere.Wheres);
        //            command.AddParameter(session, UpdateBuilders.ComponentValue.Values);
        //            break;
        //        case OperateType.Delete:
        //            command.CommandText = DeleteBuilders.ToQueryString(session, TableName);
        //            command.AddParameter(session, DeleteBuilders.ComponentWhere.Wheres);
        //            break;
        //        default:
        //            throw new NotImplementedException("未支持该类型的命令对象的创建" + this.OperateType.ToString());
        //    }
        //    return command;
        //}
        #endregion
    }
}
