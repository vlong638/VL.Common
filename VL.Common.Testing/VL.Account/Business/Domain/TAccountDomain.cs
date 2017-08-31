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

        public ReportData ReportData { set; get; }
        public string ModuleName { get; }
        public string FucntionName { get; }

        public void Init(Exception ex)
        {
            ReportData = new ReportData(ReportStatus.Error,ex);
        }
    }

    /// <summary>
    /// ���������õ�ȫ����(����ִ��״̬,ִ����Ϣ)
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
    /// TAccount���Žӿ�
    /// </summary>
    public static class TAccountDomain
    {
        #region ����
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
            var result = new Report<CreateStatus>((data) =>
            {
                var status = data == CreateStatus.Success ? ReportStatus.Success : ReportStatus.Failure;
                string message = "";
                switch (data)
                {
                    case CreateStatus.Success:
                        message = "�����ɹ�";
                        break;
                    case CreateStatus.Failure:
                        message = "����ʧ��";
                        break;
                    case CreateStatus.Empty_AccountName:
                        message = "�û�������Ϊ��";
                        break;
                    case CreateStatus.Empty_Password:
                        message = "���벻��Ϊ��";
                        break;
                    case CreateStatus.Repeat_UserName:
                        message = "�û����Ѵ���";
                        break;
                    default:
                        break;
                }
                return new ReportData(status, message);
            });
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

        #region ����ȫ��
        public static Report<List<TAccount>> SelectAllAccounts(DbSession session)
        {
            var result = new Report<List<TAccount>>((data) => { return new ReportData(data == null ? ReportStatus.Failure : ReportStatus.Success, "");  });
            result.Data = new List<TAccount>().DbSelect(session);
            return result;
        } 
        #endregion
    }
}
