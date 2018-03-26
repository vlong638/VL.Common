using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using VL.Common.Core.ORM;

namespace VL.Common.Core.Object.VL.Account
{
    public partial class TAccount : VLModel_DB
    {
        #region Properties
        /// <summary>
        /// 标识符
        /// </summary>
        public Guid AccountId { get; set; }
        /// <summary>
        /// 账户名
        /// </summary>
        public String AccountName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public Int32? Phone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public Int64? Email { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public String Password { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// 最近编辑时间
        /// </summary>
        public DateTime? LastEditedOn { get; set; }
        #endregion

        #region Constructors
        public TAccount()
        {
        }
        public TAccount(Guid accountId)
        {
            AccountId = accountId;
        }
        public TAccount(IDataReader reader) : base(reader)
        {
        }
        #endregion

        #region Methods
        public override void Init(IDataReader reader)
        {
            this.AccountId = new Guid(reader[nameof(this.AccountId)].ToString());
            this.AccountName = Convert.ToString(reader[nameof(this.AccountName)]);
            if (reader[nameof(this.Phone)] != DBNull.Value)
            {
                this.Phone = Convert.ToInt32(reader[nameof(this.Phone)]);
            }
            if (reader[nameof(this.Email)] != DBNull.Value)
            {
                this.Email = Convert.ToInt64(reader[nameof(this.Email)]);
            }
            this.Password = Convert.ToString(reader[nameof(this.Password)]);
            this.CreatedOn = Convert.ToDateTime(reader[nameof(this.CreatedOn)]);
            if (reader[nameof(this.LastEditedOn)] != DBNull.Value)
            {
                this.LastEditedOn = Convert.ToDateTime(reader[nameof(this.LastEditedOn)]);
            }
        }
        public override void Init(IDataReader reader, List<string> fields)
        {
            if (fields.Contains(nameof(AccountId)))
            {
                this.AccountId = new Guid(reader[nameof(this.AccountId)].ToString());
            }
            if (fields.Contains(nameof(AccountName)))
            {
                this.AccountName = Convert.ToString(reader[nameof(this.AccountName)]);
            }
            if (fields.Contains(nameof(Phone)))
            {
                if (reader[nameof(this.Phone)] != DBNull.Value)
                {
                    this.Phone = Convert.ToInt32(reader[nameof(this.Phone)]);
                }
            }
            if (fields.Contains(nameof(Email)))
            {
                if (reader[nameof(this.Email)] != DBNull.Value)
                {
                    this.Email = Convert.ToInt64(reader[nameof(this.Email)]);
                }
            }
            if (fields.Contains(nameof(Password)))
            {
                this.Password = Convert.ToString(reader[nameof(this.Password)]);
            }
            if (fields.Contains(nameof(CreatedOn)))
            {
                this.CreatedOn = Convert.ToDateTime(reader[nameof(this.CreatedOn)]);
            }
            if (fields.Contains(nameof(LastEditedOn)))
            {
                if (reader[nameof(this.LastEditedOn)] != DBNull.Value)
                {
                    this.LastEditedOn = Convert.ToDateTime(reader[nameof(this.LastEditedOn)]);
                }
            }
        }
        [DataMember]
        public override string TableName
        {
            get
            {
                return nameof(TAccount);
            }
        }
        #endregion

        #region Manual
        #endregion
    }
}
