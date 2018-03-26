using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VL.Account.Business;
using VL.Common.Core.Object.VL.Account;
using VL.Common.Testing.Utilities;
using static VL.Account.Business.TAccountDomain;

namespace VL.Common.Testing
{
    /// <summary>
    /// Service是服务窗体
    /// </summary>
    class CommonService
    {
        public static Report<CreateStatus> CreateAccount(TAccount account)
        {
            return TransactionHelper.HandleTransactionEvent(DbConfigs.DbNameOfAccount, (session) =>
            {
                return account.Create(session);
            });
        }

        //public static Report<CreateStatus> CreateAccount(TAccount account)
        //{
        //    return TransactionHelper.HandleTransactionEvent(DbConfigs.DbNameOfAccount, (session) =>
        //    {
        //        return account.Create(session);
        //    });
        //}




    }
}
