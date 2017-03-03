//using System.Collections.Generic;
//using VL.Common.DAS.Objects;

//namespace VL.Common.ORM.CodeGenerateLib.Samples.Utilities
//{
//    public interface EntityOperator<T>
//    {
//        #region 单例
//        void Insert(DbSession session, T t);
//        void Delete(DbSession session, T t);
//        void Update(DbSession session, T t, List<string> fields);
//        T Select(DbSession session, T t, List<string> fields);
//        #endregion

//        #region 批量处理
//        void Insert(DbSession session, List<T> ts);
//        void Delete(DbSession session, List<T> ts);
//        void Update(DbSession session, List<T> ts, List<string> fields);
//        List<T> Select(DbSession session, List<T> ts, List<string> fields);
//        #endregion

//        #region 基于QueryBuilder的查询方法
//        void Insert(DbSession session, QueryBuilder queryBuilder);
//        void Delete(DbSession session, QueryBuilder queryBuilder);
//        void Update(DbSession session, QueryBuilder queryBuilder);
//        T Select(DbSession session, QueryBuilder queryBuilder);
//        List<T> SelectAll(DbSession session, QueryBuilder queryBuilder); 
//        #endregion
//    }
//}
