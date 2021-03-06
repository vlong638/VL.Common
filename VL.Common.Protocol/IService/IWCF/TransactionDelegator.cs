﻿using System;
using VL.Common.DAS.Objects;

namespace VL.Common.Protocol.IService//.IWCF
{
    /// <summary>
    /// 用于规范服务的方法外框架(以隔离核心处理逻辑)
    /// </summary>
    public class TransactionDelegator
    {
        public static ClassReportHelper ReportHelper = new ClassReportHelper(nameof(VL.Common.Protocol), nameof(TransactionDelegator));

        public TransactionDelegator(ServiceContext serviceContext)
        {
            ServiceContext = serviceContext;
        }
        public ServiceContext ServiceContext { set; get; }

        #region 事务处理_标准化
        /// <summary>
        /// 封装了事务处理的方法
        /// 只需专注于业务逻辑的实现
        /// </summary>
        public T HandleTransactionEvent<T>(DbSession session1, Func<DbSession, T> func) where T : Report, new()
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
                    return (T)ReportHelper.GetReport(nameof(HandleTransactionEvent), Report.CodeOfError, ex.ToString());
                }
                if (result.Code == Report.CodeOfSuccess)
                {
                    session1.CommitTransaction();
                }
                else
                {
                    session1.RollBackTransaction();
                }
                session1.Close();
                return result;
            }
            catch (Exception ex)
            {
                if (session1 != null && session1.Connection.State == System.Data.ConnectionState.Open)
                {
                    session1.Close();
                }
                return (T)ReportHelper.GetReport(nameof(HandleTransactionEvent), Report.CodeOfError, ex.ToString());
            }
        }
        /// <summary>
        /// 封装了事务处理的方法
        /// 只需专注于业务逻辑的实现
        /// </summary>
        public T HandleTransactionEvent<T>(string session1Name, Func<DbSession, T> func) where T : Report, new()
        {
            try
            {
                return HandleTransactionEvent(ServiceContext.GetDbSession(session1Name), func);
            }
            catch (Exception ex)
            { 
                return (T)ReportHelper.GetReport(nameof(HandleTransactionEvent), Report.CodeOfError, ex.ToString());
            }    
        }
        /// <summary>
        /// 封装了事务处理的方法
        /// 只需专注于业务逻辑的实现
        /// </summary>HandleTransactionEvent
        public T HandleTransactionEvent<T>(DbSession session1, DbSession session2, Func<DbSession, DbSession, T> func) where T : Report, new()
        {
            //业务逻辑处理
            try
            {
                session1.Open();
                session2.Open();
                session1.BeginTransaction();
                session2.BeginTransaction();
                T result;
                try
                {
                    result = func(session1, session2);
                }
                catch (Exception ex)
                {
                    return (T)ReportHelper.GetReport(nameof(HandleTransactionEvent), Report.CodeOfError, ex.ToString());
                }
                if (result.Code == Report.CodeOfSuccess)
                {
                    session1.CommitTransaction();
                    session2.CommitTransaction();
                }
                else
                {
                    session1.RollBackTransaction();
                    session2.RollBackTransaction();
                }
                session1.Close();
                session2.Close();
                return result;
            }
            catch (Exception ex)
            {
                if (session1 != null && session1.Connection.State == System.Data.ConnectionState.Open)
                {
                    session1.Close();
                }
                if (session2 != null && session2.Connection.State == System.Data.ConnectionState.Open)
                {
                    session2.Close();
                }
                return (T)ReportHelper.GetReport(nameof(HandleTransactionEvent), Report.CodeOfError, ex.ToString());
            }
        }
        /// <summary>
        /// 封装了事务处理的方法
        /// 只需专注于业务逻辑的实现
        /// </summary>HandleTransactionEvent
        public T HandleTransactionEvent<T>(string session1Name, string session2Name, Func<DbSession, DbSession, T> func) where T : Report, new()
        {
            try
            {
                return HandleTransactionEvent(ServiceContext.GetDbSession(session1Name), ServiceContext.GetDbSession(session2Name), func);
            }
            catch (Exception ex)
            {
                return (T)ReportHelper.GetReport(nameof(HandleTransactionEvent), Report.CodeOfError, ex.ToString());
            }
        }
        #endregion
    }
}
