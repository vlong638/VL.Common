using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace VL.Common.Configurator.Objects.BuiltInConfigEntities
{
    /// <summary>
    /// 项目配置对象
    /// 负责记录项目和其路径信息
    /// </summary>
    public class DbConnectionsConfigEntity : XConfigEntity
    {
        public List<DbConnectionConfigEntity> DbConnectionConfigs { set; get; } = new List<DbConnectionConfigEntity>();

        public DbConnectionsConfigEntity(string fileName, string directoryPath) : base(fileName, directoryPath)
        {
        }

        public DbConnectionsConfigEntity(string fileName) : base(fileName)
        {
        }

        public override XElement ToXElement()
        {
            XElement root = new XElement("DbConnectionConfigs");
            //items
            foreach (var project in DbConnectionConfigs)
            {
                XElement configItems = new XElement("DbConnectionConfig"
                    , new XAttribute(nameof(DbConnectionConfigEntity.Name), project.Name)
                    , new XAttribute(nameof(DbConnectionConfigEntity.ConnectingString), project.ConnectingString));
                root.Add(configItems);
            }
            return root;
        }
        protected override void Load(XDocument doc)
        {
            //items
            var configItems = doc.Descendants("DbConnectionConfig");
            foreach (var configItem in configItems)
            {
                DbConnectionConfigs.Add(new DbConnectionConfigEntity(configItem.Attribute(nameof(DbConnectionConfigEntity.Name)).Value
                    , configItem.Attribute(nameof(DbConnectionConfigEntity.ConnectingString)).Value));
            }
        }
    }
    public enum EDatabaseType
    {
        none = 0,
        oracle,
        mssql,
        mysql,
    }
    public class DbConnectionConfigEntity
    {
        public string Name { set; get; }
        public string ConnectingString { set; get; }

        public DbConnectionConfigEntity(string name, string connectingString)//string databaseTypeString,
        {
            Name = name;
            ConnectingString = connectingString;
        }
    }
}
