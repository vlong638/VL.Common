using System;
using System.Collections.Generic;
using VL.Common.DAS.Objects;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    /// <summary>
    /// 数据库语句构建类
    /// </summary>
    public abstract class IDbQueryBuilder
    {
        ////迁移至Protocol 0630
        //public static IDbQueryBuilder GetDbQueryBuilder(DbSession session)
        //{
        //    switch (session.DatabaseType)
        //    {
        //        case EDatabaseType.MSSQL:
        //            return new MSSQLQueryBuilder();
        //        case EDatabaseType.MySQL:
        //        case EDatabaseType.Oracle:
        //        //TODO 未支持多数据库
        //        default:
        //            throw new NotImplementedException("未实现该类别的QueryBuilder" + session.DatabaseType);
        //    }
        //}

        //public string TableName { set; get; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public OperateType OperateType
        {
            set
            {
                operateType = value;
                switch (operateType)
                {
                    case OperateType.Insert:
                        InsertBuilders = new List<InsertBuilder>();
                        break;
                    case OperateType.Select:
                        SelectBuilders = new List<SelectBuilder>();
                        break;
                    case OperateType.Update:
                        UpdateBuilders = new List<UpdateBuilder>();
                        break;
                    case OperateType.Delete:
                        DeleteBuilder = new DeleteBuilder();
                        break;
                    default:
                        break;
                }
            }
            get
            {
                return operateType;
            }
        }
        private OperateType operateType;
        /// <summary>
        /// Insert语句构建器
        /// </summary>
        public List<InsertBuilder> InsertBuilders
        {
            get
            {
                if (insertBuilders==null)
                {
                    insertBuilders = new List<InsertBuilder>();
                }
                return insertBuilders;
            }
            set
            {
                operateType = OperateType.Insert;
                insertBuilders = value;
            }
        }
        private List<InsertBuilder> insertBuilders;
        /// <summary>
        /// Insert语句构建器
        /// </summary>
        public InsertBuilder InsertBuilder
        {
            get
            {
                if (insertBuilder == null)
                {
                    insertBuilder = new InsertBuilder();
                }
                return insertBuilder;
            }
            set
            {
                operateType = OperateType.Insert;
                insertBuilder = value;
            }
        }
        private InsertBuilder insertBuilder;
        /// <summary>
        /// Select语句构建器
        /// </summary>
        public List<SelectBuilder> SelectBuilders
        {
            get
            {
                if (selectBuilders == null)
                {
                    selectBuilders = new List<SelectBuilder>();
                }
                return selectBuilders;
            }
            set
            {
                operateType = OperateType.Select;
                selectBuilders = value;
            }
        }
        private List<SelectBuilder> selectBuilders;
        /// <summary>
        /// Select语句构建器
        /// </summary>
        public SelectBuilder SelectBuilder
        {
            get
            {
                if (selectBuilder == null)
                {
                    selectBuilder = new SelectBuilder();
                }
                return selectBuilder;
            }
            set
            {
                operateType = OperateType.Select;
                selectBuilder = value;
            }
        }
        private SelectBuilder selectBuilder;
        /// <summary>
        /// Update语句构建器
        /// </summary>
        public List<UpdateBuilder> UpdateBuilders
        {
            get
            {
                if (updateBuilders == null)
                {
                    updateBuilders = new List<UpdateBuilder>();
                }
                return updateBuilders;
            }
            set
            {
                operateType = OperateType.Update;
                updateBuilders = value;
            }
        }
        private List<UpdateBuilder> updateBuilders;
        /// <summary>
        /// Update语句构建器
        /// </summary>
        public UpdateBuilder UpdateBuilder
        {
            get
            {
                if (updateBuilder == null)
                {
                    updateBuilder = new UpdateBuilder();
                }
                return updateBuilder;
            }
            set
            {
                operateType = OperateType.Update;
                updateBuilder = value;
            }
        }
        private UpdateBuilder updateBuilder;
        /// <summary>
        /// Delete语句构建器
        /// </summary>
        public DeleteBuilder DeleteBuilder
        {
            get
            {
                if (deleteBuilder == null)
                {
                    deleteBuilder = new DeleteBuilder();
                }
                return deleteBuilder;
            }
            set
            {
                operateType = OperateType.Delete;
                deleteBuilder = value;
            }
        }
        private DeleteBuilder deleteBuilder;
    }

    /// <summary>
    /// CRUD操作类别
    /// </summary>
    public enum OperateType
    {
        /// <summary>
        /// 增
        /// </summary>
        Insert,
        /// <summary>
        /// 删
        /// </summary>
        Delete,
        /// <summary>
        /// 改
        /// </summary>
        Update,
        /// <summary>
        /// 查
        /// </summary>
        Select,
    }
}
