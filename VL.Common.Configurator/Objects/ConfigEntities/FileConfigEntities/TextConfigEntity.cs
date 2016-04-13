using System.IO;

namespace VL.Common.Configurator.Objects.ConfigEntities
{
    /// <summary>
    /// 采用XML.Linq进行存储的文件类型配置对象
    /// </summary>
    public abstract class TextConfigEntity : FileConfigEntity
    {
        public TextConfigEntity(string fileName) : base(fileName)
        {
        }

        public TextConfigEntity(string fileName, string directoryPath) : base(fileName, directoryPath)
        {
        }

        public override void Load()
        {
            using (StreamReader steam = File.OpenText(InputFilePath))
            {
                Load(steam);
            }
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
