using System.IO;

namespace VL.Common.Configurator.Objects
{
    /// <summary>
    /// 采用XML.Linq进行存储的文件类型配置对象
    /// </summary>
    public abstract class TextConfigEntity : FileConfigEntity
    {
        public TextConfigEntity(string fileName, string directoryPath, bool isInitFromFile = false) : base(fileName, directoryPath, isInitFromFile)
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
                using (StreamReader steam = File.OpenText(InputFilePath))
                {
                    Load(steam);
                    return true;
                }
            }
            return false;
        }
        public override void Save()
        {
            if (!Directory.Exists(InputDirectoryPath))
            {
                Directory.CreateDirectory(InputDirectoryPath);
            }
            File.WriteAllText(InputFilePath, ToContent());
        }
        public abstract string ToContent();
        protected abstract void Load(StreamReader stream);
    }
}
