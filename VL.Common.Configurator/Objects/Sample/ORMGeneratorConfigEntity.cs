using System;
using System.Xml.Linq;

namespace VL.Common.Configurator.Objects
{
    public enum EDatabaseType
    {
        None = 0,
        Oracle,
        MSSQL,
        MySQL,
    }
    public class ORMGeneratorConfigEntity : XConfigEntity
    {
        /// <summary>
        /// PDM文件目录
        /// </summary>
        public string PDMFilePath { set; get; } = "";
        /// <summary>
        /// 目标文件夹目录
        /// </summary>
        public string TargetDirectoryPath { set; get; } = "";
        /// <summary>
        /// 目标命名空间
        /// </summary>
        public string TargetNamespace { set; get; } = "";
        /// <summary>
        /// 目标数据库类型
        /// </summary>
        public EDatabaseType TargetDatabaseType { set; get; } = EDatabaseType.None;
        /// <summary>
        /// 是否支持WCF
        /// </summary>
        public bool IsSupportWCF { set; get; } = false;

        public ORMGeneratorConfigEntity(string fileName, string directoryPath, bool isInitFromFile = false) : base(fileName, directoryPath, isInitFromFile)
        {
        }

        public override XElement ToXElement()
        {
            XElement element = new XElement("ConfigItem");
            element.Add(new XElement(nameof(PDMFilePath), PDMFilePath));
            element.Add(new XElement(nameof(TargetDirectoryPath), TargetDirectoryPath));
            element.Add(new XElement(nameof(TargetNamespace), TargetNamespace));
            element.Add(new XElement(nameof(TargetDatabaseType), (int)TargetDatabaseType));
            element.Add(new XElement(nameof(IsSupportWCF), IsSupportWCF));
            return element;
        }
        protected override void Load(XDocument doc)
        {
            var configItem = doc.Element("ConfigItem");
            PDMFilePath = configItem.GetStringFromSubelementValue(nameof(PDMFilePath));
            TargetDirectoryPath = configItem.GetStringFromSubelementValue(nameof(TargetDirectoryPath));
            TargetNamespace = configItem.GetStringFromSubelementValue(nameof(TargetNamespace));
            TargetDatabaseType = (EDatabaseType)Enum.Parse(typeof(EDatabaseType), configItem.GetStringFromSubelementValue(nameof(TargetDatabaseType)));
            IsSupportWCF = configItem.GetBoolFromSubelementValue(nameof(IsSupportWCF));
        }
    }
}
