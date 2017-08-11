using System;
using VL.Common.Core.DAS;
using VL.Common.Core.Object.VL.Account;

namespace VL.Account.Business
{
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
    public interface IExcetionReport
    {
        void Init(Exception ex);
    }

    /// <summary>
    /// ���������õ�ȫ����(����ִ��״̬,ִ����Ϣ)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Report: IExcetionReport
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
        public virtual string ModuleName { get; }
        public virtual string FucntionName { get; }

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
    public abstract class Report<T>:Report
    {
        public Report()
        {
        }
        public Report(Exception ex) : base(ex)
        {
        }
        public Report(T data)
        {
            Data = data;
            ReportStatus = IsSuccess(data) ? ReportStatus.Success : ReportStatus.Failure;
        }

        public T Data { set; get; }
        protected abstract bool IsSuccess(T data);
    }

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
        public class AccountCreateReport : Report<CreateStatus>
        {
            public AccountCreateReport() : base()
            {
            }

            public AccountCreateReport(Exception ex) : base(ex)
            {
            }

            public AccountCreateReport(CreateStatus data) : base(data)
            {
            }

            public override string FucntionName
            {
                get
                {
                    return nameof(Create);
                }
            }

            public override string ModuleName
            {
                get
                {
                    return nameof(Business);
                }
            }

            protected override bool IsSuccess(CreateStatus data)
            {
                return data == CreateStatus.Success;
            }
        }
        public static AccountCreateReport Create(this TAccount tAccount, DbSession session)
        {
            //���
            if (string.IsNullOrEmpty(tAccount.AccountName))
                return new AccountCreateReport(CreateStatus.Empty_AccountName);
            if (string.IsNullOrEmpty(tAccount.Password))
                return new AccountCreateReport(CreateStatus.Empty_Password);
            //����
            if (tAccount.DbInsert(session))
                return new AccountCreateReport(CreateStatus.Success);
            else
                return new AccountCreateReport(CreateStatus.Failure);
        } 
        #endregion
    }
}
