using System.Collections.Generic;
using VL.Common.Core.DAS;

namespace VL.Common.Testing.Utilities
{

    public class DbConfigs : DbConfigEntity
    {
        public static string DbNameOfAccount = "VLAccount";
        static DbConfigs _DbConfigsInstance;
        public static DbConfigs DbConfigsInstance
        {
            get
            {
                if (_DbConfigsInstance == null)
                {
                    _DbConfigsInstance = new DbConfigs(nameof(DbConfigs));
                    _DbConfigsInstance.Load();
                }
                return _DbConfigsInstance;
            }
        }

        public DbConfigs(string fileName) : base(fileName)
        {
        }

        protected override List<DbConfigItem> GetDbConfigItems()
        {
            List<DbConfigItem> result = new List<DbConfigItem>()
            {
                new DbConfigItem(DbNameOfAccount),
            };
            return result;
        }
    }
}
