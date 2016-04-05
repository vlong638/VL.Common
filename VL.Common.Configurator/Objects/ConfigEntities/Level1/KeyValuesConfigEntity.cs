using System.Collections.Generic;
using System.Xml.Linq;

namespace VL.Common.Configurator.Objects.ConfigEntities
{
    /// <summary>
    /// 项目配置对象
    /// 负责记录项目和其路径信息
    /// </summary>
    public class KeyValuesConfigEntity : XConfigEntity
    {
        public string ItemsName { set; get; }
        public string ItemName { set; get; }
        public List<KeyValueConfigEntity> Items { set; get; } = new List<KeyValueConfigEntity>();

        public KeyValuesConfigEntity(string fileName, string directoryPath, string rootName = "Items", string itemName = "Item") : base(fileName, directoryPath)
        {
        }

        public KeyValuesConfigEntity(string fileName, string rootName = "Items", string itemName = "Item") : base(fileName)
        {
        }

        public override XElement ToXElement()
        {
            XElement root = new XElement(ItemsName);
            foreach (var project in Items)
            {
                XElement configItems = new XElement(ItemName
                    , new XAttribute(nameof(KeyValueConfigEntity.Name), project.Name)
                    , new XAttribute(nameof(KeyValueConfigEntity.Value), project.Value));
                root.Add(configItems);
            }
            return root;
        }
        protected override void Load(XDocument doc)
        {
            var configItems = doc.Descendants(ItemName);
            foreach (var configItem in configItems)
            {
                Items.Add(new KeyValueConfigEntity(configItem.Attribute(nameof(KeyValueConfigEntity.Name)).Value
                    , configItem.Attribute(nameof(KeyValueConfigEntity.Value)).Value));
            }
        }
    }
    public class KeyValueConfigEntity
    {
        public string Name { set; get; }
        public string Value { set; get; }

        public KeyValueConfigEntity(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
