using System;
using System.Collections.Generic;
using System.Linq;
using VL.Common.Core.DAS;
using VL.Common.Core.ORM;
using VL.Common.Core.Object.VL.Account;

namespace VL.Account.Business
{
    public static partial class EntityOperator
    {
        #region Methods
        #region 写
        public static bool DbDelete(this TAccount entity, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            query.DeleteBuilder.ComponentWhere.Add(new ComponentValueOfWhere(TAccountProperties.AccountId, entity.AccountId, LocateType.Equal));
            return session.GetQueryOperator().Delete<TAccount>(query);
        }
        public static bool DbDelete(this List<TAccount> entities, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            var Ids = entities.Select(c =>c.AccountId );
            query.DeleteBuilder.ComponentWhere.Add(new ComponentValueOfWhere(TAccountProperties.AccountId, Ids, LocateType.In));
            return session.GetQueryOperator().Delete<TAccount>(query);
        }
        public static bool DbInsert(this TAccount entity, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            InsertBuilder builder = new InsertBuilder();
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TAccountProperties.AccountId, entity.AccountId));
            if (entity.AccountName == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.AccountName));
            }
            if (entity.AccountName.Length > 20)
            {
                throw new NotImplementedException(string.Format("参数项:{0}长度:{1}超过额定限制:{2}", nameof(entity.AccountName), entity.AccountName.Length, 20));
            }
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TAccountProperties.AccountName, entity.AccountName));
            if (entity.Phone.HasValue)
            {
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TAccountProperties.Phone, entity.Phone.Value));
            }
            if (entity.Email.HasValue)
            {
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TAccountProperties.Email, entity.Email.Value));
            }
            if (entity.Password == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Password));
            }
            if (entity.Password.Length > 20)
            {
                throw new NotImplementedException(string.Format("参数项:{0}长度:{1}超过额定限制:{2}", nameof(entity.Password), entity.Password.Length, 20));
            }
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TAccountProperties.Password, entity.Password));
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TAccountProperties.CreatedOn, entity.CreatedOn));
            if (entity.LastEditedOn.HasValue)
            {
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TAccountProperties.LastEditedOn, entity.LastEditedOn.Value));
            }
            query.InsertBuilders.Add(builder);
            return session.GetQueryOperator().Insert<TAccount>(query);
        }
        public static bool DbInsert(this List<TAccount> entities, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            foreach (var entity in entities)
            {
                InsertBuilder builder = new InsertBuilder();
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TAccountProperties.AccountId, entity.AccountId));
            if (entity.AccountName == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.AccountName));
            }
            if (entity.AccountName.Length > 20)
            {
                throw new NotImplementedException(string.Format("参数项:{0}长度:{1}超过额定限制:{2}", nameof(entity.AccountName), entity.AccountName.Length, 20));
            }
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TAccountProperties.AccountName, entity.AccountName));
                if (entity.Phone.HasValue)
                {
                    builder.ComponentInsert.Add(new ComponentValueOfInsert(TAccountProperties.Phone, entity.Phone.Value));
                }
                if (entity.Email.HasValue)
                {
                    builder.ComponentInsert.Add(new ComponentValueOfInsert(TAccountProperties.Email, entity.Email.Value));
                }
            if (entity.Password == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Password));
            }
            if (entity.Password.Length > 20)
            {
                throw new NotImplementedException(string.Format("参数项:{0}长度:{1}超过额定限制:{2}", nameof(entity.Password), entity.Password.Length, 20));
            }
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TAccountProperties.Password, entity.Password));
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TAccountProperties.CreatedOn, entity.CreatedOn));
                if (entity.LastEditedOn.HasValue)
                {
                    builder.ComponentInsert.Add(new ComponentValueOfInsert(TAccountProperties.LastEditedOn, entity.LastEditedOn.Value));
                }
                query.InsertBuilders.Add(builder);
            }
            return session.GetQueryOperator().InsertAll<TAccount>(query);
        }
        public static bool DbUpdate(this TAccount entity, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            UpdateBuilder builder = new UpdateBuilder();
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TAccountProperties.AccountId, entity.AccountId, LocateType.Equal));
            if (fields==null|| fields.Length==0)
            {
                builder.ComponentSet.Add(new ComponentValueOfSet(TAccountProperties.AccountName, entity.AccountName));
                builder.ComponentSet.Add(new ComponentValueOfSet(TAccountProperties.Phone, entity.Phone));
                builder.ComponentSet.Add(new ComponentValueOfSet(TAccountProperties.Email, entity.Email));
                builder.ComponentSet.Add(new ComponentValueOfSet(TAccountProperties.Password, entity.Password));
                builder.ComponentSet.Add(new ComponentValueOfSet(TAccountProperties.CreatedOn, entity.CreatedOn));
                builder.ComponentSet.Add(new ComponentValueOfSet(TAccountProperties.LastEditedOn, entity.LastEditedOn));
            }
            else
            {
                if (fields.Contains(TAccountProperties.AccountName))
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TAccountProperties.AccountName, entity.AccountName));
                }
                if (fields.Contains(TAccountProperties.Phone))
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TAccountProperties.Phone, entity.Phone));
                }
                if (fields.Contains(TAccountProperties.Email))
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TAccountProperties.Email, entity.Email));
                }
                if (fields.Contains(TAccountProperties.Password))
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TAccountProperties.Password, entity.Password));
                }
                if (fields.Contains(TAccountProperties.CreatedOn))
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TAccountProperties.CreatedOn, entity.CreatedOn));
                }
                if (fields.Contains(TAccountProperties.LastEditedOn))
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TAccountProperties.LastEditedOn, entity.LastEditedOn));
                }
            }
            query.UpdateBuilders.Add(builder);
            return session.GetQueryOperator().Update<TAccount>(query);
        }
        public static bool DbUpdate(this List<TAccount> entities, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            foreach (var entity in entities)
            {
                UpdateBuilder builder = new UpdateBuilder();
                builder.ComponentWhere.Add(new ComponentValueOfWhere(TAccountProperties.AccountId, entity.AccountId, LocateType.Equal));
                if (fields==null|| fields.Length==0)
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TAccountProperties.AccountName, entity.AccountName));
                    builder.ComponentSet.Add(new ComponentValueOfSet(TAccountProperties.Phone, entity.Phone));
                    builder.ComponentSet.Add(new ComponentValueOfSet(TAccountProperties.Email, entity.Email));
                    builder.ComponentSet.Add(new ComponentValueOfSet(TAccountProperties.Password, entity.Password));
                    builder.ComponentSet.Add(new ComponentValueOfSet(TAccountProperties.CreatedOn, entity.CreatedOn));
                    builder.ComponentSet.Add(new ComponentValueOfSet(TAccountProperties.LastEditedOn, entity.LastEditedOn));
                }
                else
                {
                    if (fields.Contains(TAccountProperties.AccountName))
                    {
                        builder.ComponentSet.Add(new ComponentValueOfSet(TAccountProperties.AccountName, entity.AccountName));
                    }
                    if (fields.Contains(TAccountProperties.Phone))
                    {
                        builder.ComponentSet.Add(new ComponentValueOfSet(TAccountProperties.Phone, entity.Phone));
                    }
                    if (fields.Contains(TAccountProperties.Email))
                    {
                        builder.ComponentSet.Add(new ComponentValueOfSet(TAccountProperties.Email, entity.Email));
                    }
                    if (fields.Contains(TAccountProperties.Password))
                    {
                        builder.ComponentSet.Add(new ComponentValueOfSet(TAccountProperties.Password, entity.Password));
                    }
                    if (fields.Contains(TAccountProperties.CreatedOn))
                    {
                        builder.ComponentSet.Add(new ComponentValueOfSet(TAccountProperties.CreatedOn, entity.CreatedOn));
                    }
                    if (fields.Contains(TAccountProperties.LastEditedOn))
                    {
                        builder.ComponentSet.Add(new ComponentValueOfSet(TAccountProperties.LastEditedOn, entity.LastEditedOn));
                    }
                }
                query.UpdateBuilders.Add(builder);
            }
            return session.GetQueryOperator().UpdateAll<TAccount>(query);
        }
        #endregion
        #region 读
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static TAccount DbSelect(this TAccount entity, DbSession session, SelectBuilder select)
        {
            var query = session.GetDbQueryBuilder();
            query.SelectBuilder = select;
            return session.GetQueryOperator().Select<TAccount>(query);
        }
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static TAccount DbSelect(this TAccount entity, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            SelectBuilder builder = new SelectBuilder();
            if (fields.Count() == 0)
            {
                builder.ComponentSelect.Add(TAccountProperties.AccountId);
                builder.ComponentSelect.Add(TAccountProperties.AccountName);
                builder.ComponentSelect.Add(TAccountProperties.Phone);
                builder.ComponentSelect.Add(TAccountProperties.Email);
                builder.ComponentSelect.Add(TAccountProperties.Password);
                builder.ComponentSelect.Add(TAccountProperties.CreatedOn);
                builder.ComponentSelect.Add(TAccountProperties.LastEditedOn);
            }
            else
            {
                builder.ComponentSelect.Add(TAccountProperties.AccountId);
                foreach (var field in fields)
                {
                    builder.ComponentSelect.Add(field);
                }
            }
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TAccountProperties.AccountId, entity.AccountId, LocateType.Equal));
            query.SelectBuilders.Add(builder);
            return session.GetQueryOperator().Select<TAccount>(query);
        }
        /// <summary>
        /// 未查询到数据时返回 new List<T>()
        /// </summary>
        public static List<TAccount> DbSelect(this List<TAccount> entities, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            SelectBuilder builder = new SelectBuilder();
            if (fields.Count() == 0)
            {
                builder.ComponentSelect.Add(TAccountProperties.AccountId);
                builder.ComponentSelect.Add(TAccountProperties.AccountName);
                builder.ComponentSelect.Add(TAccountProperties.Phone);
                builder.ComponentSelect.Add(TAccountProperties.Email);
                builder.ComponentSelect.Add(TAccountProperties.Password);
                builder.ComponentSelect.Add(TAccountProperties.CreatedOn);
                builder.ComponentSelect.Add(TAccountProperties.LastEditedOn);
            }
            else
            {
                builder.ComponentSelect.Add(TAccountProperties.AccountId);
                foreach (var field in fields)
                {
                    builder.ComponentSelect.Add(field);
                }
            }
            var Ids = entities.Select(c =>c.AccountId );
            if (Ids.Count() != 0)
            {
                builder.ComponentWhere.Add(new ComponentValueOfWhere(TAccountProperties.AccountId, Ids, LocateType.In));
            }
            query.SelectBuilders.Add(builder);
            return session.GetQueryOperator().SelectAll<TAccount>(query);
        }
        /// <summary>
        /// 存在相应对象时返回true,缺少对象时返回false
        /// </summary>
        public static bool DbLoad(this TAccount entity, DbSession session, params PDMDbProperty[] fields)
        {
            var result = entity.DbSelect(session, fields);
            if (result == null)
            {
                return false;
            }
            if (fields.Count() == 0)
            {
                entity.AccountName = result.AccountName;
                entity.Phone = result.Phone;
                entity.Email = result.Email;
                entity.Password = result.Password;
                entity.CreatedOn = result.CreatedOn;
                entity.LastEditedOn = result.LastEditedOn;
            }
            else
            {
                if (fields.Contains(TAccountProperties.AccountName))
                {
                    entity.AccountName = result.AccountName;
                }
                if (fields.Contains(TAccountProperties.Phone))
                {
                    entity.Phone = result.Phone;
                }
                if (fields.Contains(TAccountProperties.Email))
                {
                    entity.Email = result.Email;
                }
                if (fields.Contains(TAccountProperties.Password))
                {
                    entity.Password = result.Password;
                }
                if (fields.Contains(TAccountProperties.CreatedOn))
                {
                    entity.CreatedOn = result.CreatedOn;
                }
                if (fields.Contains(TAccountProperties.LastEditedOn))
                {
                    entity.LastEditedOn = result.LastEditedOn;
                }
            }
            return true;
        }
        /// <summary>
        /// 存在相应对象时返回true,缺少对象时返回false
        /// </summary>
        public static bool DbLoad(this List<TAccount> entities, DbSession session, params PDMDbProperty[] fields)
        {
            bool result = true;
            foreach (var entity in entities)
            {
                result = result && entity.DbLoad(session, fields);
            }
            return result;
        }
        #endregion
        #endregion
    }
}
