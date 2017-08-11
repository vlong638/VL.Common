using System;
using VL.Common.Core.ORM;

namespace VL.Common.Core.Object.VL.Account
{
    public class TAccountProperties
    {
        #region Properties
        public static PDMDbProperty<Guid> AccountId { get; set; } = new PDMDbProperty<Guid>(nameof(AccountId), "AccountId", "标识符", true, PDMDataType.uniqueidentifier, 0, 0, true);
        public static PDMDbProperty<String> AccountName { get; set; } = new PDMDbProperty<String>(nameof(AccountName), "AccountName", "账户名", false, PDMDataType.nvarchar, 20, 0, false);
        public static PDMDbProperty<Int32> Phone { get; set; } = new PDMDbProperty<Int32>(nameof(Phone), "Phone", "手机号", false, PDMDataType.numeric, 18, 0, false);
        public static PDMDbProperty<Int64> Email { get; set; } = new PDMDbProperty<Int64>(nameof(Email), "Email", "邮箱", false, PDMDataType.numeric, 50, 0, false);
        public static PDMDbProperty<String> Password { get; set; } = new PDMDbProperty<String>(nameof(Password), "Password", "密码", false, PDMDataType.nvarchar, 20, 0, true);
        public static PDMDbProperty<DateTime> CreatedOn { get; set; } = new PDMDbProperty<DateTime>(nameof(CreatedOn), "CreatedOn", "创建时间", false, PDMDataType.datetime, 0, 0, true);
        public static PDMDbProperty<DateTime> LastEditedOn { get; set; } = new PDMDbProperty<DateTime>(nameof(LastEditedOn), "LastEditedOn", "最近编辑时间", false, PDMDataType.datetime, 0, 0, false);
        #endregion
    }
}
