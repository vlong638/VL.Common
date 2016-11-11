//using System;
//using System.Data.Common;
//using VL.Common.DAS.Objects;
//using VL.Common.ORM.Utilities.Interfaces;

//namespace VL.Common.ORM.Utilities.QueryBuilders
//{
//    public abstract class IQueryBuilder: IQueriable
//    {
//        public string TableName { get; set; }
//        public string ToQueryString(DbSession session, string tableName)
//        {
//            TableName = tableName;
//            return ToQueryString(session);
//        }
//        public abstract void AppendQueryParameter(ref DbCommand command, DbSession session);

//        public abstract string ToQueryString(DbSession session);
//    }
//}
