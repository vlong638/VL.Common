using System;
using System.Collections.Generic;
using VL.Common.Core.DAS;
using VL.Common.Core.Object.VL.Account;

namespace VL.Account.Business
{
    #region Report
    /// <summary>
    /// ����ָʾ���õ�ִ��״̬
    /// </summary>
    public enum ReportStatus
    {
        Success,
        Failure,
        Error,
    }
    /// <summary>
    /// ���������õ�ִ����Ϣ
    /// </summary>
    public class ReportData
    {
        string _Message;
        Exception Exception;

        public ReportData(Exception exception)
        {
            this.Exception = exception;
        }

        public ReportData(string _Message)
        {
            this._Message = _Message;
        }

        public string Message
        {
            get { return string.IsNullOrEmpty(_Message) ? Exception.ToString() : _Message; }
        }
    }

    /// <summary>
    /// ����ʹ��Exception��ʼ����Report
    /// </summary>
    public interface IExcetionReport
    {
        void Init(Exception ex);
    }

    /// <summary>
    /// ���������õ�ȫ����(����ִ��״̬,ִ����Ϣ)
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

        public ReportStatus ReportStatus { set; get; }
        public ReportData ReportData { set; get; }
        public string ModuleName { get; }
        public string FucntionName { get; }

        public void Init(Exception ex)
        {
            ReportData = new ReportData(ex);
            ReportStatus = ReportStatus.Error;
        }
    }

    /// <summary>
    /// ���������õ�ȫ����(����ִ��״̬,ִ����Ϣ)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Report<T> : Report
    {
        public Report(Func<T, ReportStatus> updateReportStatus)
        {
            UpdateReportStatus = updateReportStatus;
        }
        public Report(Func<T, ReportStatus> updateReportStatus, Exception ex) : base(ex)
        {
            UpdateReportStatus = updateReportStatus;
        }
        public Report(Func<T, ReportStatus> updateReportStatus, T data)
        {
            UpdateReportStatus = updateReportStatus;
            Data = data;
        }

        public T Data { set; get; }

        public bool IsSuccess(T data)
        {
            var result = UpdateReportStatus?.Invoke(Data);
            if (result.HasValue)
            {
                ReportStatus = result.Value;
            }
            return ReportStatus == ReportStatus.Success;
        }

        public Func<T, ReportStatus> UpdateReportStatus;
    }
    #endregion

    /// <summary>
    /// TAccount���Žӿ�
    /// </summary>
    public static class TAccountDomain
    {
        #region Create
        public enum CreateStatus
        {
            Success,//�ɹ�
            Failure,//��������
            Empty_AccountName,//�˻�������Ϊ��
            Empty_Password,//���벻��Ϊ��
            Repeat_UserName,//�û����Ѵ���
        }

        public static Report<CreateStatus> Create(this TAccount tAccount, DbSession session)
        {
            var result = new Report<CreateStatus>((data) => { return data == CreateStatus.Success ? ReportStatus.Success : ReportStatus.Failure; });
            //���
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
            //����
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

        #region SelectAllAccounts
        public static Report<List<TAccount>> SelectAllAccounts(DbSession session)
        {
            var result = new Report<List<TAccount>>((data) => { return data == null ? ReportStatus.Failure : ReportStatus.Success; });
            result.Data = new List<TAccount>().DbSelect(session);
            return result;
        } 
        #endregion
    }
}
