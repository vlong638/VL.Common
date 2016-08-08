using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using VL.Common.Configurator.Objects.ConfigEntities;

namespace VL.Common.Protocol
{
    public class ProtocolConfig : XMLConfigEntity
    {
        /// <summary>
        /// 是否支持SQL输出
        /// </summary>
        public KeyValueConfigItem<bool> IsSQLLogAvailable { set; get; } = new KeyValueConfigItem<bool>(nameof(IsSQLLogAvailable));

        public ProtocolConfig(string fileName=nameof(ProtocolConfig)) : base(fileName)
        {
        }
        public ProtocolConfig(string fileName, string directoryPath) : base(fileName, directoryPath)
        {
        }

        protected override void Load(XDocument doc)
        {
            var elements = doc.Descendants(nameof(XMLConfigItem));
            IsSQLLogAvailable.SetValue(elements);
        }

        public override IEnumerable<XElement> ToXElements()
        {
            List<XElement> elements = new List<XElement>();
            elements.Add(IsSQLLogAvailable.ToXElement());
            return elements;
        }
    }
}
