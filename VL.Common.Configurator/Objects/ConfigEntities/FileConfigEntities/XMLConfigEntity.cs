using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace VL.Common.Configurator.Objects.ConfigEntities
{
    /// <summary>
    /// 采用XML.Linq进行存储的文件类型配置对象
    /// </summary>
    public abstract class XMLConfigEntity : FileConfigEntity
    {
        public XMLConfigEntity(string fileName) : base(fileName)
        {
        }

        public XMLConfigEntity(string fileName, string directoryPath) : base(fileName, directoryPath)
        {
        }

        public override void Load()
        {
            XDocument doc = XDocument.Load(InputFilePath);
            Load(doc);
        }
        public override void Save()
        {
            if (!Directory.Exists(OutputDirectoryPath))
            {
                Directory.CreateDirectory(OutputDirectoryPath);
            }
            XElement root = new XElement("configuration");
            foreach (var element in GetXElements())
            {
                root.Add(element);
            }
            root.Save(OutputFilePath);
        }
        public abstract IEnumerable<XElement> GetXElements();
        protected abstract void Load(XDocument doc);
    }

    public interface XMLConfigItem
    {
        XElement ToXElement();
    }

    public class KeyValueConfigItem<T> : XMLConfigItem where T : struct, IConvertible
    {
        public string Key { set; get; }
        public T Value { set; get; }

        public KeyValueConfigItem(string key, T t = default(T))
        {
            Key = key;
            Value = t;
        }

        public T SetValue(IEnumerable<XElement> elements)
        {
            var element = elements.FirstOrDefault(c => c.Attribute(nameof(Key)).Value == Key);
            if (element == null)
            {
                throw new NotImplementedException("缺少关于" + Key + "的配置");
            }
            return (T)Convert.ChangeType(element.Attribute(nameof(Value)).Value, Value.GetType());
        }
        public XElement ToXElement()
        {
            return new XElement(nameof(XMLConfigItem)
                , new XAttribute(nameof(Key), Key)
                , new XAttribute(nameof(Value), Value.ToString()));
        }
    }

    #region 方案2
    //public class KeyStringConfigItem : KeyValueConfigItem<string>
    //{
    //    public KeyStringConfigItem(XElement element) : base(element)
    //    {
    //    }

    //    public override string GetValueFromElement(XElement element)
    //    {
    //        return element.Attribute(nameof(Value)).Value;
    //    }
    //}
    //public class KeyInt32ConfigItem : KeyValueConfigItem<int>
    //{
    //    public KeyInt32ConfigItem(XElement element) : base(element)
    //    {
    //    }

    //    public override int GetValueFromElement(XElement element)
    //    {
    //        return Convert.ToInt32(element.Attribute(nameof(Value)).Value);
    //    }
    //}
    //public class KeyInt64ConfigItem : KeyValueConfigItem<long>
    //{
    //    public KeyInt64ConfigItem(XElement element) : base(element)
    //    {
    //    }

    //    public override long GetValueFromElement(XElement element)
    //    {
    //        return Convert.ToInt64(element.Attribute(nameof(Value)).Value);
    //    }
    //} 
    #endregion

    #region 方案1
    //public abstract class KeyValueConfigItem
    //{
    //    public string Key { set; get; }

    //    public abstract void Init(XElement element);
    //    public abstract XElement ToXElement();
    //}
    //public class KeyValueConfigItem<T>: KeyValueConfigItem
    //{
    //    public T Value { set; get; }

    //    public override void Init(XElement element)
    //    {
    //        switch (typeof(T).Name)
    //        {
    //            case nameof(String):
    //                Value = element.Attribute(nameof(Value)).Value;
    //            case nameof(Int32):
    //            case nameof(Int64):
    //            case nameof(Boolean):
    //            default:
    //                throw new NotImplementedException("暂不支持该类型的KeyValueConfigItem,请进行扩展后使用,类型:" + typeof(T).Name);
    //        }
    //    }
    //    public override XElement ToXElement()
    //    {
    //        return new XElement(nameof(KeyConfigItem)
    //            , new XAttribute(nameof(Key), Key)
    //            , new XAttribute(nameof(Value), Value));
    //    }
    //} 
    #endregion

    public static class XElementEx
    {
        public static string GetStringFromSubelementValue(this XElement element, string subelementName)
        {
            var subelement = element.Element(subelementName);
            if (subelement != null)
            {
                return subelement.Value;
            }
            else
            {
                return "";
            }
        }
        public static bool GetBoolFromSubelementValue(this XElement element, string subelementName)
        {
            var subelement = element.Element(subelementName);
            if (subelement != null)
            {
                return subelement.Value.ToLower() == "true" || subelement.Value == "1";
            }
            else
            {
                return false;
            }
        }
        public static int GetIntFromSubelementValue(this XElement element, string subelementName)
        {
            var subelement = element.Element(subelementName);
            if (subelement != null)
            {
                int result;
                int.TryParse(subelement.Value, out result);
                return result;
            }
            else
            {
                return 0;
            }
        }
    }
}
