using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace VL.Common.Testing.Utilities
{
    public class SQLiteHelper
    {
        static string DbName = "VLAccount";
        public static SQLiteConnection Connect()
        {
            return new SQLiteConnection($"DataSource={DbName};Version=3;");
        }

        #region PrepareTables
        public static void PrepareTables()
        {
            foreach (var key in TableCreateSQLs.Keys)
            {
                if (!IsTableExist(key))
                {
                    CreateTables(key);
                }
            }
        }
        static Dictionary<string, string> TableCreateSQLs = new Dictionary<string, string>()
        {
            {"TAccount", $@"
create table TAccount 
(
   AccountId            uniqueidentifier               not null,
   AccountName          nvarchar(20)                   null,
   Phone                numeric(18)                    null,
   Email                numeric(50)                    null,
   Password             nvarchar(20)                   not null,
   CreatedOn            datetime                       not null,
   LastEditedOn         datetime                       null,
   constraint PK_TACCOUNT primary key (AccountId)
);"
            },
    };
        static bool IsTableExist(string table)
        {
            bool exits = false;
            using (var connection = Connect())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"select 1 from sqlite_master where name='{table}'";
                var result = command.ExecuteScalar();//注意返回的数值为long,不可强制转换为int
                exits = result != null && (long)result == 1;
                connection.Close();
            }
            return exits;
        }
        static void CreateTables(string key)
        {
            CreateTable(TableCreateSQLs.First(c => c.Key == key).Value);
        }
        static void CreateTable(string createSQL)
        {
            using (var connection = Connect())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = createSQL;
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion
    }
}
