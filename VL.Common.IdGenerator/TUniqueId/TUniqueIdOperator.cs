using System.Collections.Generic;
using System.Linq;
using VL.Common.DAS.Objects;
using VL.Common.ORM.Utilities.QueryBuilders;
using VL.Common.ORM.Utilities.QueryOperators;

namespace VL.Common.Entities
{
    public static partial class EntityOperator
    {
        #region Methods
        #region 写
        public static bool DbDelete(this TUniqueId entity, DbSession session)
        {
            var query = IDbQueryBuilder.GetDbQueryBuilder(session);
            query.DeleteBuilder.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TUniqueIdProperties.IdentityName, OperatorType.Equal, entity.IdentityName));
            return IDbQueryOperator.GetQueryOperator(session).Delete<TUniqueId>(session, query);
        }
        public static bool DbDelete(this List<TUniqueId> entities, DbSession session)
        {
            var query = IDbQueryBuilder.GetDbQueryBuilder(session);
            var Ids = entities.Select(c =>c.IdentityName );
            query.DeleteBuilder.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TUniqueIdProperties.IdentityName, OperatorType.In, Ids));
            return IDbQueryOperator.GetQueryOperator(session).Delete<TUniqueId>(session, query);
        }
        public static bool DbInsert(this TUniqueId entity, DbSession session)
        {
            var query = IDbQueryBuilder.GetDbQueryBuilder(session);
            InsertBuilder builder = new InsertBuilder();
            builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TUniqueIdProperties.IdentityName, entity.IdentityName));
            builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TUniqueIdProperties.IdentityValue, entity.IdentityValue));
            query.InsertBuilders.Add(builder);
            return IDbQueryOperator.GetQueryOperator(session).Insert<TUniqueId>(session, query);
        }
        public static bool DbInsert(this List<TUniqueId> entities, DbSession session)
        {
            var query = IDbQueryBuilder.GetDbQueryBuilder(session);
            foreach (var entity in entities)
            {
                InsertBuilder builder = new InsertBuilder();
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TUniqueIdProperties.IdentityName, entity.IdentityName));
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TUniqueIdProperties.IdentityValue, entity.IdentityValue));
                query.InsertBuilders.Add(builder);
            }
            return IDbQueryOperator.GetQueryOperator(session).InsertAll<TUniqueId>(session, query);
        }
        public static bool DbUpdate(this TUniqueId entity, DbSession session, List<string> fields)
        {
            var query = IDbQueryBuilder.GetDbQueryBuilder(session);
            UpdateBuilder builder = new UpdateBuilder();
            builder.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TUniqueIdProperties.IdentityName, OperatorType.Equal, entity.IdentityName));
            if (fields.Contains(TUniqueIdProperties.IdentityValue.Title))
            {
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TUniqueIdProperties.IdentityValue, entity.IdentityValue));
            }
            query.UpdateBuilders.Add(builder);
            return IDbQueryOperator.GetQueryOperator(session).Update<TUniqueId>(session, query);
        }
        public static bool DbUpdate(this List<TUniqueId> entities, DbSession session, List<string> fields)
        {
            var query = IDbQueryBuilder.GetDbQueryBuilder(session);
            foreach (var entity in entities)
            {
                UpdateBuilder builder = new UpdateBuilder();
                builder.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TUniqueIdProperties.IdentityName, OperatorType.Equal, entity.IdentityName));
                if (fields.Contains(TUniqueIdProperties.IdentityValue.Title))
                {
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TUniqueIdProperties.IdentityValue, entity.IdentityValue));
                }
                query.UpdateBuilders.Add(builder);
            }
            return IDbQueryOperator.GetQueryOperator(session).UpdateAll<TUniqueId>(session, query);
        }
        #endregion
        #region 读
        public static TUniqueId DbSelect(this TUniqueId entity, DbSession session, List<string> fields)
        {
            var query = IDbQueryBuilder.GetDbQueryBuilder(session);
            SelectBuilder builder = new SelectBuilder();
            foreach (var field in fields)
            {
                builder.ComponentFieldAliases.FieldAliases.Add(new FieldAlias(field));
            }
            builder.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TUniqueIdProperties.IdentityName, OperatorType.Equal, entity.IdentityName));
            query.SelectBuilders.Add(builder);
            return IDbQueryOperator.GetQueryOperator(session).Select<TUniqueId>(session, query);
        }
        public static List<TUniqueId> DbSelect(this List<TUniqueId> entities, DbSession session, List<string> fields)
        {
            var query = IDbQueryBuilder.GetDbQueryBuilder(session);
            SelectBuilder builder = new SelectBuilder();
            foreach (var field in fields)
            {
                builder.ComponentFieldAliases.FieldAliases.Add(new FieldAlias(field));
            }
            var Ids = entities.Select(c =>c.IdentityName );
            builder.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TUniqueIdProperties.IdentityName, OperatorType.In, Ids));
            query.SelectBuilders.Add(builder);
            return IDbQueryOperator.GetQueryOperator(session).SelectAll<TUniqueId>(session, query);
        }
        #endregion
        #endregion
    }
}
