using System;
using System.Xml.Linq;

namespace VL.Common.Core.DAS
{
    public class DbConfigItem
    {
        public string DbName { set; get; }
        public string ConnectingString { set; get; }
        public EDatabaseType DbType { set; get; }

        public DbConfigItem(string dbName)
        {
            DbName = dbName;
            ConnectingString = "";
            DbType = EDatabaseType.None;
        }

        public void Init(XElement element)
        {
            ConnectingString = element.Attribute(nameof(ConnectingString)).Value;
            DbType = (EDatabaseType)Enum.Parse(typeof(EDatabaseType), element.Attribute(nameof(DbType)).Value);
        }
        public XElement ToXElement()
        {
            return new XElement(nameof(DbConfigItem)
                , new XAttribute(nameof(DbName), DbName)
                , new XAttribute(nameof(ConnectingString), ConnectingString)
                , new XAttribute(nameof(DbType), DbType.ToString()));
        }
        public DbSession GetDbSession()
        {
            if (DbType == EDatabaseType.None)
            {
                throw new NotImplementedException("未配置有效的数据库类型");
            }
            if (string.IsNullOrWhiteSpace(ConnectingString))
            {
                throw new NotImplementedException("未配置有效的数据库连接字符串");
            }
            return new DbSession(DbType, ConnectingString);
        }
    }
}
