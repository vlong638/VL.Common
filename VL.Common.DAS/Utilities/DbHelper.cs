using VL.Common.DAS.Objects;

namespace VL.Common.DAS.Utilities
{
    public class DbHelper
    {
        public static DbSession GetDbSession(string dbName)
        {
            return new DbSession(DbConfigHelper.GetDbType(dbName), DbConfigHelper.GetDbConnectingString(dbName));
        }
        public static void CreateConfigFile()
        {
            DbConfigHelper.CreateConfigFile();
        }
    }
}
