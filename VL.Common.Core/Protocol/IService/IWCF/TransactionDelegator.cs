using System;
using VL.Common.Object.Protocol;
using VL.Common.DAS;

namespace VL.Common.Protocol//.IService.IWCF
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

        #region 非事务处理_标准化
        /// <summary>
        /// 封装了非事务处理的方法
        /// 只需专注于业务逻辑的实现
        /// </summary>
        public Report<T> HandleEvent<T>(DbSession session1, Func<DbSession, Report<T>> func)
        {
            //业务逻辑处理
            try
            {
                session1.Open();
                Report<T> result;
                try
                {
                    result = func(session1);
                }
                catch (Exception ex)
                {
                    var report= ReportHelper.GetReport(default(T), nameof(HandleTransactionEvent), CProtocol.CReport.CError, ex.ToString());
                    ServiceContext.ServiceLogger.Error(report.ToString());
                    return report;
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
                var report = ReportHelper.GetReport(default(T), nameof(HandleEvent), CProtocol.CReport.CError, ex.ToString());
                ServiceContext.ServiceLogger.Error(report.ToString());
                return report;
            }
        }
        /// <summary>
        /// 封装了非事务处理的方法
        /// 只需专注于业务逻辑的实现
        /// </summary>HandleTransactionEvent
        public Report<T> HandleEvent<T>(string session1Name, Func<DbSession, Report<T>> func)
        {
            try
            {
                return HandleEvent(ServiceContext.GetDbSession(session1Name), func);
            }
            catch (Exception ex)
            {
                var report= ReportHelper.GetReport(default(T), nameof(HandleEvent), CProtocol.CReport.CError, ex.ToString());
                ServiceContext.ServiceLogger.Error(report.ToString());
                return report;
            }
        }
        #endregion

        #region 事务处理_标准化 无T
        /// <summary>
        /// 封装了事务处理的方法
        /// 只需专注于业务逻辑的实现
        /// </summary>
        public Report HandleTransactionEvent(DbSession session1, Func<DbSession, Report> func)
        {
            //业务逻辑处理
            try
            {
                session1.Open();
                session1.BeginTransaction();
                Report result;
                try
                {
                    result = func(session1);
                }
                catch (Exception ex)
                {
                    var report = ReportHelper.GetReport(nameof(HandleTransactionEvent), CProtocol.CReport.CError, ex.ToString());
                    ServiceContext.ServiceLogger.Error(report.ToString());
                    return report;
                }
                if (result.Code == CProtocol.CReport.CSuccess)
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
                var report = ReportHelper.GetReport(nameof(HandleTransactionEvent), CProtocol.CReport.CError, ex.ToString());
                ServiceContext.ServiceLogger.Error(report.ToString());
                return report;
            }
        }
        /// <summary>
        /// 封装了事务处理的方法
        /// 只需专注于业务逻辑的实现
        /// </summary>
        public Report HandleTransactionEvent(string session1Name, Func<DbSession, Report> func)
        {
            try
            {
                return HandleTransactionEvent(ServiceContext.GetDbSession(session1Name), func);
            }
            catch (Exception ex)
            {
                var report = ReportHelper.GetReport(nameof(HandleTransactionEvent), CProtocol.CReport.CError, ex.ToString());
                ServiceContext.ServiceLogger.Error(report.ToString());
                return report;
            }
        }
        /// <summary>
        /// 封装了事务处理的方法
        /// 只需专注于业务逻辑的实现
        /// </summary>HandleTransactionEvent
        public Report HandleTransactionEvent(DbSession session1, DbSession session2, Func<DbSession, DbSession, Report> func)
        {
            //业务逻辑处理
            try
            {
                session1.Open();
                session2.Open();
                session1.BeginTransaction();
                session2.BeginTransaction();
                Report result;
                try
                {
                    result = func(session1, session2);
                }
                catch (Exception ex)
                {
                    var report = ReportHelper.GetReport(nameof(HandleTransactionEvent), CProtocol.CReport.CError, ex.ToString());
                    ServiceContext.ServiceLogger.Error(report.ToString());
                    return report;
                }
                if (result.Code == CProtocol.CReport.CSuccess)
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
                var report = ReportHelper.GetReport(nameof(HandleTransactionEvent), CProtocol.CReport.CError, ex.ToString());
                ServiceContext.ServiceLogger.Error(report.ToString());
                return report;
            }
        }
        /// <summary>
        /// 封装了事务处理的方法
        /// 只需专注于业务逻辑的实现
        /// </summary>HandleTransactionEvent
        public Report HandleTransactionEvent<T>(string session1Name, string session2Name, Func<DbSession, DbSession, Report> func)
        {
            try
            {
                return HandleTransactionEvent(ServiceContext.GetDbSession(session1Name), ServiceContext.GetDbSession(session2Name), func);
            }
            catch (Exception ex)
            {
                var report = ReportHelper.GetReport(nameof(HandleTransactionEvent), CProtocol.CReport.CError, ex.ToString());
                ServiceContext.ServiceLogger.Error(report.ToString());
                return report;
            }
        }
        #endregion

        #region 事务处理_标准化 T
        /// <summary>
        /// 封装了事务处理的方法
        /// 只需专注于业务逻辑的实现
        /// </summary>
        public Report<T> HandleTransactionEvent<T>(DbSession session1, Func<DbSession, Report<T>> func)
        {
            //业务逻辑处理
            try
            {
                session1.Open();
                session1.BeginTransaction();
                Report<T> result;
                try
                {
                    result = func(session1);
                }
                catch (Exception ex)
                {
                    var report = ReportHelper.GetReport(default(T), nameof(HandleTransactionEvent), CProtocol.CReport.CError, ex.ToString());
                    ServiceContext.ServiceLogger.Error(report.ToString());
                    return report;
                }
                if (result.Code == CProtocol.CReport.CSuccess)
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
                var report = ReportHelper.GetReport(default(T), nameof(HandleTransactionEvent), CProtocol.CReport.CError, ex.ToString());
                ServiceContext.ServiceLogger.Error(report.ToString());
                return report;
            }
        }
        /// <summary>
        /// 封装了事务处理的方法
        /// 只需专注于业务逻辑的实现
        /// </summary>
        public Report<T> HandleTransactionEvent<T>(string session1Name, Func<DbSession, Report<T>> func)
        {
            try
            {
                return HandleTransactionEvent(ServiceContext.GetDbSession(session1Name), func);
            }
            catch (Exception ex)
            {
                var report = ReportHelper.GetReport(default(T), nameof(HandleTransactionEvent), CProtocol.CReport.CError, ex.ToString());
                ServiceContext.ServiceLogger.Error(report.ToString());
                return report;
            }
        }
        /// <summary>
        /// 封装了事务处理的方法
        /// 只需专注于业务逻辑的实现
        /// </summary>HandleTransactionEvent
        public Report<T> HandleTransactionEvent<T>(DbSession session1, DbSession session2, Func<DbSession, DbSession, Report<T>> func)
        {
            //业务逻辑处理
            try
            {
                session1.Open();
                session2.Open();
                session1.BeginTransaction();
                session2.BeginTransaction();
                Report<T> result;
                try
                {
                    result = func(session1, session2);
                }
                catch (Exception ex)
                {
                    var report = ReportHelper.GetReport(default(T), nameof(HandleTransactionEvent), CProtocol.CReport.CError, ex.ToString());
                    ServiceContext.ServiceLogger.Error(report.ToString());
                    return report;
                }
                if (result.Code == CProtocol.CReport.CSuccess)
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
                var report = ReportHelper.GetReport(default(T), nameof(HandleTransactionEvent), CProtocol.CReport.CError, ex.ToString());
                ServiceContext.ServiceLogger.Error(report.ToString());
                return report;
            }
        }
        /// <summary>
        /// 封装了事务处理的方法
        /// 只需专注于业务逻辑的实现
        /// </summary>HandleTransactionEvent
        public Report<T> HandleTransactionEvent<T>(string session1Name, string session2Name, Func<DbSession, DbSession, Report<T>> func)
        {
            try
            {
                return HandleTransactionEvent(ServiceContext.GetDbSession(session1Name), ServiceContext.GetDbSession(session2Name), func);
            }
            catch (Exception ex)
            {
                var report = ReportHelper.GetReport(default(T), nameof(HandleTransactionEvent), CProtocol.CReport.CError, ex.ToString());
                ServiceContext.ServiceLogger.Error(report.ToString());
                return report;
            }
        }
        #endregion
    }
}
