using System.IO;

namespace VL.Common.Configurator.Objects
{
    /// <summary>
    /// 文件类型配置对象
    /// </summary>
    public abstract class FileConfigEntity
    {
        protected string FileName { set; get; }
        protected string DirectoryPath { set; get; }
        public string FilePath { get { return Path.Combine(DirectoryPath, FileName + ".config"); } }

        public FileConfigEntity(string fileName, string directoryPath, bool isInitFromFile = false)
        {
            FileName = fileName;
            DirectoryPath = directoryPath;
            if (isInitFromFile)
            {
                Load();
            }
        }

        public abstract bool Load();
        public abstract void Save();
    }
}
