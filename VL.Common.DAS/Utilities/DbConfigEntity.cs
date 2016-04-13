using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using VL.Common.Configurator.Objects.ConfigEntities;
using VL.Common.DAS.Objects;

namespace VL.Common.DAS.Utilities
{
    public abstract class DbConfigEntity : XMLConfigEntity
    {
        public List<DbConfigItem> DbConfigItems { set; get; }

        public DbConfigEntity(string fileName) : base(fileName)
        {
            DbConfigItems = GetDbConfigItems();
        }

        protected abstract List<DbConfigItem> GetDbConfigItems();
        public override XElement ToXElement()
        {
            XElement root = new XElement("configuration");
            foreach (var dbConfigDetail in DbConfigItems)
            {
                root.Add(dbConfigDetail.ToXElement());
            }
            return root;
        }
        protected override void Load(XDocument doc)
        {
            var elements = doc.Descendants(nameof(DbConfigItem));
            foreach (var dbConfigDetail in DbConfigItems)
            {
                var element = elements.FirstOrDefault(c => c.Attribute(nameof(DbConfigItem.DbName)).Value == dbConfigDetail.DbName);
                if (element == null)
                {
                    throw new NotImplementedException("未配置" + dbConfigDetail.DbName + "数据库配置");
                }
                dbConfigDetail.Init(element);
            }
        }
        public string GetDbConnectingString(string dbName)
        {
            var dbConfigItem = DbConfigItems.FirstOrDefault(c => c.DbName == dbName);
            if (dbConfigItem == null)
            {
                throw new NotImplementedException("未配置对应的数据库连接字符串" + dbName);
            }
            return dbConfigItem.ConnectingString;
        }
        public EDatabaseType GetDbType(string dbName)
        {
            var dbConfigItem = DbConfigItems.FirstOrDefault(c => c.DbName == dbName);
            if (dbConfigItem == null)
            {
                throw new NotImplementedException("未配置对应的数据库连接字符串" + dbName);
            }
            return dbConfigItem.DbType;
        }
    }
}
