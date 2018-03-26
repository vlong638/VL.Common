using System;
using VL.Account.Business;
using VL.Common.Core.DAS;

namespace VL.Common.Testing.Utilities
{
    class TransactionHelper
    {
        /// <summary>
        /// 封装了事务处理的方法
        /// 只需专注于业务逻辑的实现
        /// </summary>
        public static void HandleTransactionEvent(string sessionName, Action<DbSession> action)
        {
            using (var session1 = DbConfigs.DbConfigsInstance.GetDbConfigItem(sessionName).GetDbSession())
            {
                //业务逻辑处理
                try
                {

                    session1.Open();
                    session1.BeginTransaction();
                    try
                    {
                        action(session1);
                        session1.CommitTransaction();
                    }
                    catch (Exception ex)
                    {
                        session1.RollBackTransaction();
                    }
                    session1.Close();
                }
                catch (Exception ex)
                {
                    if (session1 != null && session1.Connection.State == System.Data.ConnectionState.Open)
                        session1.Close();
                }
            }
        }

        /// <summary>
        /// 封装了事务处理的方法
        /// 只需专注于业务逻辑的实现
        /// </summary>
        public static T HandleTransactionEvent<T>(string sessionName, Func<DbSession, T> func) where T : Report, new()
        {
            using (var session1= DbConfigs.DbConfigsInstance.GetDbConfigItem(sessionName).GetDbSession())
            {
                //业务逻辑处理
                try
                {
                    session1.Open();
                    session1.BeginTransaction();
                    T result;
                    try
                    {
                        result = func(session1);
                    }
                    catch (Exception ex)
                    {
                        var t = new T();
                        t.Init(ex);
                        return t;
                    }
                    if (result.ReportData.ReportStatus == ReportStatus.Success)
                        session1.CommitTransaction();
                    else
                        session1.RollBackTransaction();
                    session1.Close();
                    return result;
                }
                catch (Exception ex)
                {
                    if (session1 != null && session1.Connection.State == System.Data.ConnectionState.Open)
                        session1.Close();
                    var t = new T();
                    t.Init(ex);
                    return t;
                }
            }
        }
    }
}
