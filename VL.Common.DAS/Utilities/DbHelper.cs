using VL.Common.DAS.Objects;

namespace VL.Common.DAS.Utilities
{
    public class DbHelper
    {
        public static DbSession GetDbSession(EDatabaseType dbType, string dbName)
        {
            return new DbSession(dbType, DbConfigHelper.GetDbConnectingString(dbName));
        }
    }
}
