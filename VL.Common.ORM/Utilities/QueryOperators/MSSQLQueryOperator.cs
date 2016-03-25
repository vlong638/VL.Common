using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using VL.Common.DAS.Objects;
using VL.Common.ORM.Utilities.QueryBuilders;

namespace VL.Common.ORM.DbOperateLib.Utilities.QueryOperators
{
    public class MSSQLQueryOperator: IDbQueryOperator
    {
        #region Utilities
        //private static string GetWhereCondition(DbSession session, ComponentWhere componentWhere)
        //{
        //    StringBuilder whereCondition = new StringBuilder();
        //    foreach (var where in componentWhere.Wheres)
        //    {
        //        AppendQuery(whereCondition, session, where);
        //    }
        //    return whereCondition.ToString();
        //}
        //private static void AppendQuery(StringBuilder whereCondition, DbSession session, PDMDbPropertyOperateValue where)
        //{
        //    //if (where.IsMultipleProperties)
        //    //{
        //    //    throw new NotImplementedException();
        //    //}
        //    //else if (where.SubSelect != null)
        //    //{
        //    //    whereCondition.Append(where.Property.Title + " " + where.Operator.ToQueryString() + " (");
        //    //    AppendQuery(whereCondition, session, where.SubSelect);
        //    //    whereCondition.Append(")");
        //    //}
        //    //else
        //    //{
        //    //    if (whereCondition.Length > 0)
        //    //    {
        //    //        whereCondition.Append(" and ");
        //    //    }
        //    //    whereCondition.Append(where.Property.Title + where.Operator.ToQueryString() + session.GetParameterPrefix() + where.Property.Title);
        //    //}
        //}
        #endregion

        #region 通用规格的操作
        /// <summary>
        /// 返回 是否成功新增
        /// </summary>
        public override bool Insert<T>(DbSession session, IDbQueryBuilder queryBuilder)
        {
            #region old
            ////var fieldNames = queryBuilder.InsertBuilder.ComponentValue.Values.Select(c => c.Property.Title);
            ////    var fieldNamesWithPrefix = queryBuilder.InsertBuilder.ComponentValue.Values.Select(c => session.GetParameterPrefix() + c.Property.Title);
            ////    var command = session.CreateCommand("insert into {0}({1}) values({2})", new T().GetTableName(), string.Join(",", fieldNames), string.Join(",", fieldNamesWithPrefix));
            ////    foreach (PDMDbPropertyValue propertyValue in queryBuilder.InsertBuilder.ComponentValue.Values)
            ////    {
            ////        command.Parameters.Add(propertyValue.Property.GetDbParameter(session, propertyValue.Value));
            ////    }
            ////    return session.ExecuteNonQuery(command) == 1;
            ////TODO 批量插入以List<IDbQueryBuilder>的形式传递
            ////if (queryBuilder.InsertBuilder.ComponentValue.Values.Count == 1)
            ////{
            ////}
            ////else
            ////{
            ////    //TODO Later 批量处理请教专业人士进行优化设计 现在是依次单个处理
            ////    var fieldNames = queryBuilder.InsertBuilder.ComponentValue.Values.Select(c => c.Property.Title);
            ////    var fieldNamesWithPrefix = queryBuilder.InsertBuilder.ComponentValue.Values.Select(c => session.GetParameterPrefix() + c.Property.Title);
            ////    var sql = string.Format("insert into {0}({1}) values({2})", new T().GetTableName(), string.Join(",", fieldNames), string.Join(",", fieldNamesWithPrefix));
            ////    int insertCount = 0;
            ////    foreach (var entity in queryBuilder.InsertBuilder.ComponentValue.Values)
            ////    {
            ////        var command = session.CreateCommand(sql);
            ////        foreach (var propertyValue in entity)
            ////        {
            ////            command.Parameters.Add(propertyValue.Property.GetDbParameter(session, propertyValue.Value));
            ////        }
            ////        if (session.ExecuteNonQuery(command) == 1)
            ////        {
            ////            insertCount++;
            ////        }
            ////    }
            ////    return insertCount == queryBuilder.InsertBuilder.Entities.Count();
            ////} 
            #endregion
            DbCommand command = session.CreateCommand();
            var insertBuilder = queryBuilder.InsertBuilders.First();
            command.CommandText = insertBuilder.ToQueryString(session, new T().GetTableName());
            insertBuilder.AppendQueryParameter(ref command, session);
            return session.ExecuteNonQuery(command) > 0;
        }
        /// <summary>
        /// 返回 全部新增都成功
        /// </summary>
        public override bool InsertAll<T>(DbSession session, IDbQueryBuilder queryBuilder)
        {
            bool result = true;
            DbCommand command = session.CreateCommand();
            string tableName = new T().GetTableName();
            foreach (var insertBuilder in queryBuilder.InsertBuilders)
            {
                command.CommandText = insertBuilder.ToQueryString(session, tableName);
                insertBuilder.AppendQueryParameter(ref command, session);
                result = result && session.ExecuteNonQuery(command) > 0;
            }
            return result;
        }
        /// <summary>
        /// 失败表示影响数据为0
        /// </summary>
        public override bool Delete<T>(DbSession session, IDbQueryBuilder queryBuilder)
        {
            DbCommand command = session.CreateCommand();
            var deleteBuilder = queryBuilder.DeleteBuilder;
            command.CommandText = deleteBuilder.ToQueryString(session, new T().GetTableName());
            deleteBuilder.AppendQueryParameter(ref command, session);
            return session.ExecuteNonQuery(command) > 0;
        }
        /// <summary>
        /// 失败表示存在更新影响条数为0的update
        /// </summary>
        public override bool Update<T>(DbSession session, IDbQueryBuilder queryBuilder)
        {
            DbCommand command = session.CreateCommand();
            var updateBuilder = queryBuilder.UpdateBuilders.First();
            command.CommandText = updateBuilder.ToQueryString(session, new T().GetTableName());
            updateBuilder.AppendQueryParameter(ref command, session);
            return session.ExecuteNonQuery(command) > 0;

            #region old
            //bool result = true;
            //string whereCondition = GetWhereCondition(session, queryBuilder.UpdateBuilder.ComponentWhere);
            //var command = session.CreateCommand("update {0} set {1} where {2}", new T().GetTableName()
            //    , string.Join(",", queryBuilder.UpdateBuilder.ComponentValue.Values.Select(c => c.Property.Title + OperatorType.Equal.ToQueryString() + session.GetParameterPrefix() + c.Property.Title))
            //    , whereCondition);
            //foreach (var where in queryBuilder.UpdateBuilder.ComponentWhere.Wheres)
            //{
            //    command.Parameters.Add(where.Property.GetDbParameter(session, where.Value));
            //}
            //foreach (var value in queryBuilder.UpdateBuilder.ComponentValue.Values)
            //{
            //    command.Parameters.Add(value.Property.GetDbParameter(session, value.Value));
            //}
            //result = session.ExecuteNonQuery(command) > 0 && result;
            //return result;

            //TODO 批量更新以List<IDbQueryBuilder>的形式传递
            //foreach (var update in queryBuilder.UpdateBuilder.Updates)
            //{
            //} 
            #endregion
        }
        /// <summary>
        /// 失败表示存在更新影响条数为0的update
        /// </summary>
        public override bool UpdateAll<T>(DbSession session, IDbQueryBuilder queryBuilder)
        {
            bool result = true;
            DbCommand command = session.CreateCommand();
            foreach (var updateBuilder in queryBuilder.UpdateBuilders)
            {
                command.CommandText = updateBuilder.ToQueryString(session, new T().GetTableName());
                updateBuilder.AppendQueryParameter(ref command, session);
                result= result&& session.ExecuteNonQuery(command) > 0;
            }
            return result;
        }
        /// <summary>
        /// 失败返回null
        /// </summary>
        public override T Select<T>(DbSession session, IDbQueryBuilder queryBuilder)
        {
            T result = new T();
            DbCommand command = session.CreateCommand();
            var selectBuilder = queryBuilder.SelectBuilders.First();
            command.CommandText = selectBuilder.ToQueryString(session, new T().GetTableName());
            selectBuilder.AppendQueryParameter(ref command, session);
            using (var reader = session.ExecuteDataReader(command))
            {
                if (reader.Read())
                {
                    if (selectBuilder.ComponentFieldAliases.FieldAliases.Count == 0)
                    {
                        result.Init(reader);
                    }
                    else
                    {
                        result.Init(reader, selectBuilder.ComponentFieldAliases.FieldAliases.Select(c => c.FieldName).ToList());
                    }
                    return result;
                }
                else
                {
                    return null;
                }
            }

            #region old
            //T result = new T();
            //string selectFields = queryBuilder.SelectBuilder.FieldAliases.ToSQLString();
            //string whereCondition = GetWhereCondition(session, queryBuilder.SelectBuilder.ComponentWhere);
            //var command = session.CreateCommand("select {0} from {1} where {2}", selectFields, new T().GetTableName(), whereCondition);
            //using (var reader = session.ExecuteDataReader(command))
            //{
            //    if (reader.Read())
            //    {
            //        result.Init(reader, queryBuilder.SelectBuilder.FieldAliases.Select(c => c.FieldName).ToList());
            //        return result;
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //} 
            #endregion
        }

        public override List<T> SelectAll<T>(DbSession session, IDbQueryBuilder queryBuilder)
        {
            List<T> results = new List<T>();
            DbCommand command = session.CreateCommand();
            var selectBuilder = queryBuilder.SelectBuilders.First();
            command.CommandText = selectBuilder.ToQueryString(session, new T().GetTableName());
            selectBuilder.AppendQueryParameter(ref command, session);
            using (var reader = session.ExecuteDataReader(command))
            {
                if (selectBuilder.ComponentFieldAliases.FieldAliases.Count == 0)
                {
                    while (reader.Read())
                    {
                        T result = new T();
                        result.Init(reader);
                        results.Add(result);
                    }
                }
                else
                {
                    var initFields = selectBuilder.ComponentFieldAliases.FieldAliases.Select(c => c.FieldName).ToList();
                    while (reader.Read())
                    {
                        T result = new T();
                        result.Init(reader, initFields);
                        results.Add(result);
                    }
                }
            }
            return results;
        }
        //public override List<T> SelectAll<T>(DbSession session, IDbQueryBuilder queryBuilder)
        //{
        //    throw new NotImplementedException();
        //    //TODO queryBuilder.SelectBuilders() 多个同时查询 Union, Union All
        //    //List<T> results = new List<T>();
        //    //DbCommand command = session.CreateCommand();
        //    //var selectBuilder = queryBuilder.SelectBuilders.First();
        //    //command.CommandText = selectBuilder.ToQueryString(session, new T().GetTableName());
        //    //selectBuilder.AppendQueryParameter(ref command, session);
        //    //using (var reader = session.ExecuteDataReader(command))
        //    //{
        //    //    var initFields = selectBuilder.ComponentFieldAliases.FieldAliases.Select(c => c.FieldName).ToList();
        //    //    while (reader.Read())
        //    //    {
        //    //        T result = new T();
        //    //        result.Init(reader, initFields);
        //    //        results.Add(result);
        //    //    }
        //    //}
        //    //return results;

        //    #region old
        //    //List<T> results = new List<T>();
        //    //string selectFields = queryBuilder.SelectBuilder.ComponentFieldAliases.ToSQLString();
        //    //string whereCondition = GetWhereCondition(session, queryBuilder.SelectBuilder.ComponentWhere);
        //    //var command = session.CreateCommand("select {0} from {1} where {2}", selectFields, new T().GetTableName(), whereCondition);
        //    //using (var reader = session.ExecuteDataReader(command))
        //    //{
        //    //    var initFields = queryBuilder.SelectBuilder.ComponentFieldAliases.Select(c => c.FieldName).ToList();
        //    //    while (reader.Read())
        //    //    {
        //    //        T result = new T();
        //    //        result.Init(reader, initFields);
        //    //        results.Add(result);
        //    //    }
        //    //}
        //    //return results; 
        //    #endregion
        //}
        public override List<T> SelectAll<T>(DbSession session)
        {
            List<T> results = new List<T>();
            var command = session.CreateCommand("select * from {0}", new T().GetTableName());
            using (var reader = session.ExecuteDataReader(command))
            {
                while (reader.Read())
                {
                    T result = new T();
                    result.Init(reader);
                    results.Add(result);
                }
            }
            return results;
        }
        /// <summary>
        /// 组合查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="queryBuilder"></param>
        /// <returns></returns>
        public override List<T> SelectUnion<T>(DbSession session, IDbQueryBuilder queryBuilder)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
