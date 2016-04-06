using System.Collections.Generic;
using System.Xml.Linq;

namespace VL.Common.Configurator.Objects.ConfigEntities
{
    /// <summary>
    /// Key+String
    /// </summary>
    public class KSConfigEntity : KeyValueCollectionConfigEntity<KeyValueConfigEntity<string>>
    {
        public string ValueName { set; get; }

        public KSConfigEntity(string fileName, string rootName = "configuration", string itemName = "Item", string valueName = "Value") : base(fileName, rootName, itemName)
        {
            Init(valueName);
        }
        public KSConfigEntity(string fileName, string directoryPath, string rootName = "configuration", string itemName = "Item", string valueName = "Value") : base(fileName, directoryPath, rootName, itemName)
        {
            Init(valueName);
        }
        private void Init(string valueName)
        {
            ValueName = valueName;
        }

        public override XElement ToXElement()
        {
            XElement root = new XElement(ItemsName);
            foreach (var project in Items)
            {
                XElement configItems = new XElement(ItemName
                    , new XAttribute(nameof(KeyConfigEntity.Key), project.Key)
                    , new XAttribute(ValueName, project.Value));
                root.Add(configItems);
            }
            return root;
        }
        protected override void Load(XDocument doc)
        {
            var configItems = doc.Descendants(ItemName);
            foreach (var configItem in configItems)
            {
                Items.Add(new KeyValueConfigEntity<string>(configItem.Attribute(nameof(KeyConfigEntity.Key)).Value
                    , configItem.Attribute(ValueName).Value));
            }
        }
    }
    /// <summary>
    /// Key+String+String
    /// </summary>
    public class KSSConfigEntity : KeyValueCollectionConfigEntity<KeyValueConfigEntity<string, string>>
    {
        public string Value1Name { set; get; }
        public string Value2Name { set; get; }

        public KSSConfigEntity(string fileName, string rootName = "Items", string itemName = "Item", string value1Name = "Value1", string value2Name = "Value2")
            : base(fileName, rootName, itemName)
        {
            Init(value1Name, value2Name);
        }
        public KSSConfigEntity(string fileName, string directoryPath, string rootName = "Items", string itemName = "Item", string value1Name = "Value1", string value2Name = "Value2")
            : base(fileName, directoryPath, rootName, itemName)
        {
            Init(value1Name, value2Name);
        }
        private void Init(string value1Name, string value2Name)
        {
            Value1Name = value1Name;
            Value2Name = value2Name;
        }

        public override XElement ToXElement()
        {
            XElement root = new XElement(ItemsName);
            foreach (var project in Items)
            {
                XElement configItems = new XElement(ItemName
                    , new XAttribute(nameof(KeyConfigEntity.Key), project.Key)
                    , new XAttribute(Value1Name, project.Value1)
                    , new XAttribute(Value2Name, project.Value2));
                root.Add(configItems);
            }
            return root;
        }
        protected override void Load(XDocument doc)
        {
            var configItems = doc.Descendants(ItemName);
            foreach (var configItem in configItems)
            {
                Items.Add(new KeyValueConfigEntity<string, string>(configItem.Attribute(nameof(KeyConfigEntity.Key)).Value
                    , configItem.Attribute(Value1Name).Value
                    , configItem.Attribute(Value2Name).Value));
            }
        }
    }

    #region Base
    /// <summary>
    /// 项目配置对象
    /// 负责记录项目和其路径信息
    /// </summary>
    public abstract class KeyValueCollectionConfigEntity<T> : XMLConfigEntity where T : KeyConfigEntity
    {
        public string ItemsName { set; get; }
        public string ItemName { set; get; }
        public List<T> Items { set; get; } = new List<T>();

        public KeyValueCollectionConfigEntity(string fileName, string directoryPath, string rootName = "Items", string itemName = "Item") : base(fileName, directoryPath)
        {
            Init(rootName, itemName);
        }
        public KeyValueCollectionConfigEntity(string fileName, string rootName = "Items", string itemName = "Item") : base(fileName)
        {
            Init(rootName, itemName);
        }
        private void Init(string rootName, string itemName)
        {
            ItemsName = rootName;
            ItemName = itemName;
        }
    }
    public abstract class KeyConfigEntity
    {
        public string Key { set; get; }

        public KeyConfigEntity(string key)
        {
            Key = key;
        }
    }
    public class KeyValueConfigEntity<T> : KeyConfigEntity
    {
        public T Value { set; get; }

        public KeyValueConfigEntity(string key, T value) : base(key)
        {
            Value = value;
        }
    }
    public class KeyValueConfigEntity<T1, T2> : KeyConfigEntity
    {
        public T1 Value1 { set; get; }
        public T2 Value2 { set; get; }

        public KeyValueConfigEntity(string key, T1 value1, T2 value2) : base(key)
        {
            Value1 = value1;
            Value2 = value2;
        }
    }
    #endregion
}
