using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using VL.Common.Core.DAS;

namespace VL.Common.Core.ORM//.Utilities.QueryOperators
{
    /// <summary>
    /// 针对MSSQL数据库的操作类
    /// </summary>
    public class MSSQLQueryOperator : IDbQueryOperator
    {
        public MSSQLQueryOperator(DbSession session)
        {
            Session = session;
        }

        #region Insert
        /// <summary>
        /// 返回 是否成功新增
        /// </summary>
        public override bool Insert<T>(DbQueryBuilder queryBuilder)
        {
            var insertBuilder = queryBuilder.InsertBuilders.First();
            return Insert<T>(insertBuilder);
        }
        /// <summary>
        /// 返回 是否成功新增
        /// </summary>
        public override bool Insert<T>(InsertBuilder insertBuilder)
        {
            if (insertBuilder == null)
            {
                throw new NotImplementedException("缺少有效的" + nameof(InsertBuilder));
            }
            DbCommand command = Session.CreateCommand();
            command.CommandText = insertBuilder.ToQueryString(Session, new T().TableName);
            insertBuilder.AddParameter(command, Session);
            WriteQueryLog(command, Session);
            return Session.ExecuteNonQuery(command) > 0;
        }
        #endregion
        #region InsertAll
        /// <summary>
        /// 返回 全部新增都成功
        /// </summary>
        public override bool InsertAll<T>(DbQueryBuilder queryBuilder)
        {
            bool result = true;
            DbCommand command = Session.CreateCommand();
            string tableName = new T().TableName;
            foreach (var insertBuilder in queryBuilder.InsertBuilders)
            {
                command.CommandText = insertBuilder.ToQueryString(Session, tableName);
                insertBuilder.AddParameter(command, Session);
                WriteQueryLog(command, Session);
                result = result && Session.ExecuteNonQuery(command) > 0;
            }
            return result;
        }
        #endregion
        #region Delete
        /// <summary>
        /// 失败表示影响数据为0
        /// </summary>
        public override bool Delete<T>(DbQueryBuilder queryBuilder)
        {
            var deleteBuilder = queryBuilder.DeleteBuilder;
            return Delete<T>(deleteBuilder);
        }
        /// <summary>
        /// 失败表示影响数据为0
        /// </summary>
        public override bool Delete<T>(DeleteBuilder deleteBuilder)
        {
            if (deleteBuilder == null)
            {
                throw new NotImplementedException("缺少有效的" + nameof(DeleteBuilder));
            }
            DbCommand command = Session.CreateCommand();
            command.CommandText = deleteBuilder.ToQueryString(Session, new T().TableName);
            deleteBuilder.AddParameter(command, Session);
            WriteQueryLog(command, Session);
            return Session.ExecuteNonQuery(command) > 0;
        }
        #endregion
        #region Update
        /// <summary>
        /// 失败表示存在更新影响条数为0的update
        /// </summary>
        public override bool Update<T>(DbQueryBuilder queryBuilder)
        {
            var updateBuilder = queryBuilder.UpdateBuilders.First();
            return Update<T>(updateBuilder);
        }
        /// <summary>
        /// 失败表示存在更新影响条数为0的update
        /// </summary>
        public override bool Update<T>(UpdateBuilder updateBuilder)
        {
            if (updateBuilder == null)
            {
                throw new NotImplementedException("缺少有效的" + nameof(UpdateBuilder));
            }
            DbCommand command = Session.CreateCommand();
            command.CommandText = updateBuilder.ToQueryString(Session, new T().TableName);
            updateBuilder.AddParameter(command, Session);
            WriteQueryLog(command, Session);
            return Session.ExecuteNonQuery(command) > 0;
        }
        #endregion
        #region UpdateAll
        /// <summary>
        /// 失败表示存在更新影响条数为0的update
        /// </summary>
        public override bool UpdateAll<T>(DbQueryBuilder queryBuilder)
        {
            bool result = true;
            DbCommand command = Session.CreateCommand();
            foreach (var updateBuilder in queryBuilder.UpdateBuilders)
            {
                command.CommandText = updateBuilder.ToQueryString(Session, new T().TableName);
                updateBuilder.AddParameter(command, Session);
                WriteQueryLog(command, Session);
                result = result && Session.ExecuteNonQuery(command) > 0;
            }
            return result;
        }
        #endregion
        #region Select
        /// <summary>
        /// 失败返回null
        /// </summary>
        public override T Select<T>(DbQueryBuilder queryBuilder)
        {
            SelectBuilder selectBuilder = queryBuilder.SelectBuilders.FirstOrDefault();
            return Select<T>(selectBuilder);
        }
        /// <summary>
        /// 失败返回null
        /// </summary>
        public override T Select<T>(SelectBuilder selectBuilder)
        {
            if (selectBuilder == null)
            {
                throw new NotImplementedException("缺少有效的" + nameof(SelectBuilder));
            }
            T result = new T();
            DbCommand command = Session.CreateCommand();
            command.CommandText = selectBuilder.ToQueryString(Session, new T().TableName);
            selectBuilder.AddParameter(command, Session);
            WriteQueryLog(command, Session);
            using (var reader = Session.ExecuteDataReader(command))
            {
                if (reader.Read())
                {
                    var fields = selectBuilder.ComponentSelect.GetSelectFields();
                    if (fields.Count() == 0)
                    {
                        result.Init(reader);
                    }
                    else
                    {
                        result.Init(reader, fields);
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
        public override List<T> SelectAll<T>()//DbSession Session
        {
            List<T> results = new List<T>();
            var command = Session.CreateCommand("select * from {0}", new T().TableName);
            WriteQueryLog(command, Session);
            using (var reader = Session.ExecuteDataReader(command))
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
        public override List<T> SelectAll<T>(DbQueryBuilder queryBuilder)
        {
            SelectBuilder selectBuilder = queryBuilder.SelectBuilders.First();
            return SelectAll<T>(selectBuilder);
        }
        /// <summary>
        /// 失败返回 new List<T>()
        /// </summary>
        public override List<T> SelectAll<T>(SelectBuilder selectBuilder)
        {
            if (selectBuilder == null)
            {
                throw new NotImplementedException("缺少有效的" + nameof(SelectBuilder));
            }
            List<T> results = new List<T>();
            DbCommand command = Session.CreateCommand();
            command.CommandText = selectBuilder.ToQueryString(Session, new T().TableName);
            selectBuilder.AddParameter(command, Session);
            WriteQueryLog(command, Session);
            using (var reader = Session.ExecuteDataReader(command))
            {
                var fields = selectBuilder.ComponentSelect.GetSelectFields();
                if (fields.Count() == 0)
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
                    while (reader.Read())
                    {
                        T result = new T();
                        result.Init(reader, fields);
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
        /// <param name="Session"></param>
        /// <param name="queryBuilder"></param>
        /// <returns></returns>
        public override List<T> SelectUnion<T>(DbQueryBuilder queryBuilder)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region SelectAs
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public override bool? SelectAsBool<T>(SelectBuilder selectBuilder)
        {
            if (selectBuilder == null)
            {
                throw new NotImplementedException("缺少有效的" + nameof(SelectBuilder));
            }
            DbCommand command = Session.CreateCommand();
            command.CommandText = selectBuilder.ToQueryString(Session, new T().TableName);
            selectBuilder.AddParameter(command, Session);
            WriteQueryLog(command, Session);
            var data = Session.ExecuteScalar(command);
            if (data != null)
            {
                return Convert.ToBoolean(data);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public override short? SelectAsShort<T>(SelectBuilder selectBuilder)
        {
            if (selectBuilder == null)
            {
                throw new NotImplementedException("缺少有效的" + nameof(SelectBuilder));
            }
            DbCommand command = Session.CreateCommand();
            command.CommandText = selectBuilder.ToQueryString(Session, new T().TableName);
            selectBuilder.AddParameter(command, Session);
            WriteQueryLog(command, Session);
            var data = Session.ExecuteScalar(command);
            short result;
            if (data != null && short.TryParse(data.ToString(), out result))
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
        public override int? SelectAsInt<T>(SelectBuilder selectBuilder)
        {
            if (selectBuilder == null)
            {
                throw new NotImplementedException("缺少有效的" + nameof(SelectBuilder));
            }
            DbCommand command = Session.CreateCommand();
            command.CommandText = selectBuilder.ToQueryString(Session, new T().TableName);
            selectBuilder.AddParameter(command, Session);
            WriteQueryLog(command, Session);
            var data = Session.ExecuteScalar(command);
            int result;
            if (data != null && int.TryParse(data.ToString(), out result))
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
        public override long? SelectAsLong<T>(SelectBuilder selectBuilder)
        {
            if (selectBuilder == null)
            {
                throw new NotImplementedException("缺少有效的" + nameof(SelectBuilder));
            }
            DbCommand command = Session.CreateCommand();
            command.CommandText = selectBuilder.ToQueryString(Session, new T().TableName);
            selectBuilder.AddParameter(command, Session);
            WriteQueryLog(command, Session);
            var data = Session.ExecuteScalar(command);
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
        public override Guid? SelectAsGuid<T>(SelectBuilder selectBuilder)
        {
            if (selectBuilder == null)
            {
                throw new NotImplementedException("缺少有效的" + nameof(SelectBuilder));
            }
            DbCommand command = Session.CreateCommand();
            command.CommandText = selectBuilder.ToQueryString(Session, new T().TableName);
            selectBuilder.AddParameter(command, Session);
            WriteQueryLog(command, Session);
            var data = Session.ExecuteScalar(command);
            Guid result;
            if (data != null && Guid.TryParse(data.ToString(), out result))
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
        public override string SelectAsString<T>(SelectBuilder selectBuilder)
        {
            if (selectBuilder == null)
            {
                throw new NotImplementedException("缺少有效的" + nameof(SelectBuilder));
            }
            DbCommand command = Session.CreateCommand();
            command.CommandText = selectBuilder.ToQueryString(Session, new T().TableName);
            selectBuilder.AddParameter(command, Session);
            WriteQueryLog(command, Session);
            var data = Session.ExecuteScalar(command);
            if (data != null)
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
        public override DateTime? SelectAsDateTime<T>(SelectBuilder selectBuilder)
        {
            if (selectBuilder == null)
            {
                throw new NotImplementedException("缺少有效的" + nameof(SelectBuilder));
            }
            DbCommand command = Session.CreateCommand();
            command.CommandText = selectBuilder.ToQueryString(Session, new T().TableName);
            selectBuilder.AddParameter(command, Session);
            WriteQueryLog(command, Session);
            var data = Session.ExecuteScalar(command);
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

        public override List<string> SelectAsStrings<T>(SelectBuilder selectBuilder)
        {
            throw new NotImplementedException();
        }

        public override DbDataReader SelectAsDataReader<T>(SelectBuilder selectBuilder)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
