using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using VL.Common.Configurator;

namespace VL.Common.DAS
{
    public abstract class DbConfigEntity : XMLConfigEntity
    {
        public List<DbConfigItem> DbConfigItems { set; get; }

        public DbConfigEntity(string fileName) : base(fileName)
        {
            DbConfigItems = GetDbConfigItems();
        }
        public DbConfigEntity(string fileName, string directoryPath) : base(fileName, directoryPath)
        {
            DbConfigItems = GetDbConfigItems();
        }

        protected abstract List<DbConfigItem> GetDbConfigItems();
        public override IEnumerable<XElement> ToXElements()
        {
            return DbConfigItems.Select(c => c.ToXElement());
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
        public DbConfigItem GetDbConfigItem(string dbName)
        {
            var dbConfigItem = DbConfigItems.FirstOrDefault(c => c.DbName == dbName);
            if (dbConfigItem == null)
            {
                throw new NotImplementedException("未配置对应的数据库连接字符串" + dbName);
            }
            return dbConfigItem;
        }
    }
}
