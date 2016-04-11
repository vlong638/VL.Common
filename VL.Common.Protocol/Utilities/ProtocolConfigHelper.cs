﻿using System;
using System.Linq;
using VL.Common.Configurator.Objects.ConfigEntities;

namespace VL.Common.DAS.Utilities
{
    public static class ProtocolConfig
    {
        /// <summary>
        /// 是否支持模拟
        /// </summary>
        public static  bool IsSimulationAvailable { set; get; } = false;

        static ProtocolConfig()
        {
            KSConfigEntity ConfigEntity = new KSConfigEntity("VLProtocol.config", Environment.CurrentDirectory + "/Configs", "configuration", "configItem", "value");
            if (!ConfigEntity.Load())
            {
                //默认配置
                ConfigEntity.Items.Add(new KeyValueConfigEntity<string>(nameof(IsSimulationAvailable), IsSimulationAvailable.ToString()));
                ConfigEntity.Save();
                throw new NotImplementedException("配置文件未存在,已创建文件,请调整配置后启动");
            }
            else
            {
                var configItem = ConfigEntity.Items.FirstOrDefault(c => c.Key == nameof(IsSimulationAvailable));
                if (configItem != null)
                {
                    IsSimulationAvailable = Convert.ToBoolean(configItem.Value);
                }
            }
        }
    }
}
