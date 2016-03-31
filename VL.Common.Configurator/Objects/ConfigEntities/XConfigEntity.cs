using System.IO;
using System.Xml.Linq;

namespace VL.Common.Configurator.Objects
{
    /// <summary>
    /// 采用XML.Linq进行存储的文件类型配置对象
    /// </summary>
    public abstract class XConfigEntity : FileConfigEntity
    {
        public XConfigEntity(string fileName, string directoryPath, bool isInitFromFile = false) : base(fileName, directoryPath, isInitFromFile)
        {
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        /// <returns>true:存在文件并已执行加载</returns>
        /// <returns>false:文件不存在</returns>
        public override bool Load()
        {
            if (File.Exists(InputFilePath))
            {
                XDocument doc = XDocument.Load(InputFilePath);
                Load(doc);
                return true;
            }
            return false;
        }
        public override void Save()
        {
            if (!Directory.Exists(OutputDirectoryPath))
            {
                Directory.CreateDirectory(OutputDirectoryPath);
            }
            this.ToXElement().Save(OutputFilePath);
        }
        public abstract XElement ToXElement();
        protected abstract void Load(XDocument doc);
    }
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
