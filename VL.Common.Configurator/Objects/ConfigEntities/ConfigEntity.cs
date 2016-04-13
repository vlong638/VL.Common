using System.IO;

namespace VL.Common.Configurator.Objects.ConfigEntities
{
    /// <summary>
    /// 配置对象
    /// </summary>
    public abstract class ConfigEntity
    {
        public abstract void Load();
        public abstract void Save();
        public string ToDisplayFormat()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
