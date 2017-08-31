using System;
using System.Collections.Generic;
using VL.Common.Core.DAS;
using VL.Common.Core.Object.VL.Account;

namespace VL.Account.Business
{
    #region Report
    /// <summary>
    /// 负责指示调用的执行状态
    /// </summary>
    public enum ReportStatus
    {
        Success,
        Failure,
        Error,
    }

    /// <summary>
    /// 负责反馈调用的执行信息
    /// </summary>
    public class ReportData
    {
        public ReportStatus ReportStatus { set; get; }
        string _Message;
        Exception Exception;

        public ReportData(ReportStatus reportStatus, Exception exception)
        {
            ReportStatus = reportStatus;
            Exception = exception;
        }
        public ReportData(ReportStatus reportStatus, string _Message)
        {
            ReportStatus = reportStatus;
            this._Message = _Message;
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(_Message) ? Exception.ToString() : _Message;
        }
    }

    /// <summary>
    /// 可以使用Exception初始化的Report
    /// </summary>
    public interface IExcetionReport
    {
        void Init(Exception ex);
    }

    /// <summary>
    /// 负责反馈调用的全面结果(包含执行状态,执行信息)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Report : IExcetionReport
    {
        public Report()
        {
        }
        public Report(Exception ex)
        {
            Init(ex);
        }

        public ReportData ReportData { set; get; }
        public string ModuleName { get; }
        public string FucntionName { get; }

        public void Init(Exception ex)
        {
            ReportData = new ReportData(ReportStatus.Error,ex);
        }
    }

    /// <summary>
    /// 负责反馈调用的全面结果(包含执行状态,执行信息)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Report<T> : Report
    {
        public Report(Func<T, ReportData> updateReportStatus)
        {
            UpdateReportData = updateReportStatus;
        }
        public Report(Func<T, ReportData> updateReportStatus, Exception ex) : base(ex)
        {
            UpdateReportData = updateReportStatus;
        }
        public Report(Func<T, ReportData> updateReportStatus, T data)
        {
            UpdateReportData = updateReportStatus;
            Data = data;
        }

        public T Data { set; get; }

        public Func<T, ReportData> UpdateReportData;

        public bool IsSuccess(T data)
        {
            ReportData = UpdateReportData?.Invoke(Data);
            return ReportData.ReportStatus == ReportStatus.Success;
        }

    }
    #endregion

    /// <summary>
    /// TAccount开放接口
    /// </summary>
    public static class TAccountDomain
    {
        #region 创建
        public enum CreateStatus
        {
            Success,//成功
            Failure,//其他错误
            Empty_AccountName,//账户名不可为空
            Empty_Password,//密码不可为空
            Repeat_UserName,//用户名已存在
        }

        public static Report<CreateStatus> Create(this TAccount tAccount, DbSession session)
        {
            var result = new Report<CreateStatus>((data) =>
            {
                var status = data == CreateStatus.Success ? ReportStatus.Success : ReportStatus.Failure;
                string message = "";
                switch (data)
                {
                    case CreateStatus.Success:
                        message = "创建成功";
                        break;
                    case CreateStatus.Failure:
                        message = "创建失败";
                        break;
                    case CreateStatus.Empty_AccountName:
                        message = "用户名不可为空";
                        break;
                    case CreateStatus.Empty_Password:
                        message = "密码不可为空";
                        break;
                    case CreateStatus.Repeat_UserName:
                        message = "用户名已存在";
                        break;
                    default:
                        break;
                }
                return new ReportData(status, message);
            });
            //检测
            if (string.IsNullOrEmpty(tAccount.AccountName))
            {
                result.Data = CreateStatus.Empty_AccountName;
                return result;
            }
            if (string.IsNullOrEmpty(tAccount.Password))
            {
                result.Data = CreateStatus.Empty_Password;
                return result;
            }
            //处理
            if (tAccount.DbInsert(session))
            {
                result.Data = CreateStatus.Success;
                return result;
            }
            else
            {
                result.Data = CreateStatus.Failure;
                return result;
            }
        }
        #endregion

        #region 搜索全部
        public static Report<List<TAccount>> SelectAllAccounts(DbSession session)
        {
            var result = new Report<List<TAccount>>((data) => { return new ReportData(data == null ? ReportStatus.Failure : ReportStatus.Success, "");  });
            result.Data = new List<TAccount>().DbSelect(session);
            return result;
        } 
        #endregion
    }
}
