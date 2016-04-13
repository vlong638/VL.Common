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
        /// 是否支持模拟
        /// </summary>
        public KeyValueConfigItem<bool> IsSimulationAvailable { set; get; } = new KeyValueConfigItem<bool>(nameof(IsSimulationAvailable));

        public ProtocolConfig(string fileName) : base(fileName)
        {
        }
        public ProtocolConfig(string fileName, string directoryPath) : base(fileName, directoryPath)
        {
        }

        protected override void Load(XDocument doc)
        {
            var elements = doc.Descendants(nameof(XMLConfigItem));
            IsSimulationAvailable.SetValue(elements);
        }

        public override IEnumerable<XElement> GetXElements()
        {
            List<XElement> elements = new List<XElement>();
            elements.Add(IsSimulationAvailable.ToXElement());
            return elements;
        }
    }
}
