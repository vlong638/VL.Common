using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using VL.Common.DAS.Objects;
using VL.Common.ORM.Utilities.QueryBuilders;

namespace VL.Common.ORM.Utilities.QueryOperators
{
    /// <summary>
    /// 针对MSSQL数据库的操作类
    /// </summary>
    public class MSSQLQueryOperator : IDbQueryOperator
    {
        #region Insert
        /// <summary>
        /// 返回 是否成功新增
        /// </summary>
        public override bool Insert<T>(DbSession session, IDbQueryBuilder queryBuilder)
        {
            var insertBuilder = queryBuilder.InsertBuilders.First();
            return Insert<T>(session, insertBuilder);
        }
        /// <summary>
        /// 返回 是否成功新增
        /// </summary>
        public override bool Insert<T>(DbSession session, InsertBuilder insertBuilder)
        {
            if (insertBuilder == null)
            {
                throw new NotImplementedException("缺少有效的" + nameof(InsertBuilder));
            }
            DbCommand command = session.CreateCommand();
            command.CommandText = insertBuilder.ToQueryString(session, new T().GetTableName());
            insertBuilder.AppendQueryParameter(ref command, session);
            WriteLog(command.CommandText);
            return session.ExecuteNonQuery(command) > 0;
        }
        #endregion
        #region InsertAll
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
                WriteLog(command.CommandText);
                result = result && session.ExecuteNonQuery(command) > 0;
            }
            return result;
        }
        #endregion
        #region Delete
        /// <summary>
        /// 失败表示影响数据为0
        /// </summary>
        public override bool Delete<T>(DbSession session, IDbQueryBuilder queryBuilder)
        {
            var deleteBuilder = queryBuilder.DeleteBuilder;
            return Delete<T>(session, deleteBuilder);
        }
        /// <summary>
        /// 失败表示影响数据为0
        /// </summary>
        public override bool Delete<T>(DbSession session, DeleteBuilder deleteBuilder)
        {
            if (deleteBuilder == null)
            {
                throw new NotImplementedException("缺少有效的" + nameof(DeleteBuilder));
            }
            DbCommand command = session.CreateCommand();
            command.CommandText = deleteBuilder.ToQueryString(session, new T().GetTableName());
            deleteBuilder.AppendQueryParameter(ref command, session);
            WriteLog(command.CommandText);
            return session.ExecuteNonQuery(command) > 0;
        }
        #endregion
        #region Update
        /// <summary>
        /// 失败表示存在更新影响条数为0的update
        /// </summary>
        public override bool Update<T>(DbSession session, IDbQueryBuilder queryBuilder)
        {
            var updateBuilder = queryBuilder.UpdateBuilders.First();
            return Update<T>(session, updateBuilder);
        }
        /// <summary>
        /// 失败表示存在更新影响条数为0的update
        /// </summary>
        public override bool Update<T>(DbSession session, UpdateBuilder updateBuilder)
        {
            if (updateBuilder == null)
            {
                throw new NotImplementedException("缺少有效的" + nameof(UpdateBuilder));
            }
            DbCommand command = session.CreateCommand();
            command.CommandText = updateBuilder.ToQueryString(session, new T().GetTableName());
            updateBuilder.AppendQueryParameter(ref command, session);
            WriteLog(command.CommandText);
            return session.ExecuteNonQuery(command) > 0;
        }
        #endregion
        #region UpdateAll
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
                WriteLog(command.CommandText);
                result = result && session.ExecuteNonQuery(command) > 0;
            }
            return result;
        }
        #endregion
        #region Select
        /// <summary>
        /// 失败返回null
        /// </summary>
        public override T Select<T>(DbSession session, IDbQueryBuilder queryBuilder)
        {
            SelectBuilder selectBuilder = queryBuilder.SelectBuilders.FirstOrDefault();
            return Select<T>(session, selectBuilder);
        }
        /// <summary>
        /// 失败返回null
        /// </summary>
        public override T Select<T>(DbSession session, SelectBuilder selectBuilder)
        {
            if (selectBuilder == null)
            {
                throw new NotImplementedException("缺少有效的" + nameof(SelectBuilder));
            }
            T result = new T();
            DbCommand command = session.CreateCommand();
            command.CommandText = selectBuilder.ToQueryString(session, new T().GetTableName());
            selectBuilder.AppendQueryParameter(ref command, session);
            WriteLog(command.CommandText);
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
        }
        #endregion
        #region SelectAll
        /// <summary>
        /// 失败返回 new List<T>()
        /// </summary>
        public override List<T> SelectAll<T>(DbSession session)
        {
            List<T> results = new List<T>();
            var command = session.CreateCommand("select * from {0}", new T().GetTableName());
            WriteLog(command.CommandText);
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
        /// 失败返回 new List<T>()
        /// </summary>
        public override List<T> SelectAll<T>(DbSession session, IDbQueryBuilder queryBuilder)
        {
            SelectBuilder selectBuilder = queryBuilder.SelectBuilders.First();
            return SelectAll<T>(session, selectBuilder);
        }
        /// <summary>
        /// 失败返回 new List<T>()
        /// </summary>
        public override List<T> SelectAll<T>(DbSession session, SelectBuilder selectBuilder)
        {
            if (selectBuilder == null)
            {
                throw new NotImplementedException("缺少有效的" + nameof(SelectBuilder));
            }
            List<T> results = new List<T>();
            DbCommand command = session.CreateCommand();
            command.CommandText = selectBuilder.ToQueryString(session, new T().GetTableName());
            selectBuilder.AppendQueryParameter(ref command, session);
            WriteLog(command.CommandText);
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
        #endregion        /// <summary>
        #region SelectUnion
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
        #region SelectAs
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public override int? SelectAsInt(DbSession session, SelectBuilder selectBuilder)
        {
            if (selectBuilder == null)
            {
                throw new NotImplementedException("缺少有效的" + nameof(SelectBuilder));
            }
            DbCommand command = session.CreateCommand();
            command.CommandText = selectBuilder.ToQueryString(session, selectBuilder.TableName);
            selectBuilder.AppendQueryParameter(ref command, session);
            WriteLog(command.CommandText);
            var data = session.ExecuteScalar(command);
            int result;
            if (data!=null&&int.TryParse(data.ToString(), out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public override long? SelectAsLong(DbSession session, SelectBuilder selectBuilder)
        {
            if (selectBuilder == null)
            {
                throw new NotImplementedException("缺少有效的" + nameof(SelectBuilder));
            }
            DbCommand command = session.CreateCommand();
            command.CommandText = selectBuilder.ToQueryString(session, selectBuilder.TableName);
            selectBuilder.AppendQueryParameter(ref command, session);
            WriteLog(command.CommandText);
            var data = session.ExecuteScalar(command);
            long result;
            if (data != null && long.TryParse(data.ToString(), out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public override string SelectAsString(DbSession session, SelectBuilder selectBuilder)
        {
            if (selectBuilder == null)
            {
                throw new NotImplementedException("缺少有效的" + nameof(SelectBuilder));
            }
            DbCommand command = session.CreateCommand();
            command.CommandText = selectBuilder.ToQueryString(session, selectBuilder.TableName);
            selectBuilder.AppendQueryParameter(ref command, session);
            WriteLog(command.CommandText);
            var data = session.ExecuteScalar(command);
            if (data!=null)
            {
                return data.ToString();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public override DateTime? SelectAsDateTime(DbSession session, SelectBuilder selectBuilder)
        {
            if (selectBuilder == null)
            {
                throw new NotImplementedException("缺少有效的" + nameof(SelectBuilder));
            }
            DbCommand command = session.CreateCommand();
            command.CommandText = selectBuilder.ToQueryString(session, selectBuilder.TableName);
            selectBuilder.AppendQueryParameter(ref command, session);
            WriteLog(command.CommandText);
            var data = session.ExecuteScalar(command);
            DateTime result;
            if (data != null && DateTime.TryParse(data.ToString(), out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
