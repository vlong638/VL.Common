using System.IO;

namespace VL.Common.Configurator.Objects
{
    /// <summary>
    /// 文件类型配置对象
    /// </summary>
    public abstract class FileConfigEntity
    {
        protected string InputFileName { set; get; }
        protected string InputDirectoryPath { set; get; }
        public string InputFilePath { get { return Path.Combine(InputDirectoryPath, InputFileName); } }
        protected string OutputFileName { set; get; }
        protected string OutputDirectoryPath { set; get; }
        public string OutputFilePath { get { return Path.Combine(OutputDirectoryPath, OutputFileName); } }

        public FileConfigEntity(string fileName)
        {
            InputFileName = fileName;
            InputDirectoryPath = Path.Combine(System.Environment.CurrentDirectory, "Configs");
            OutputFileName = fileName;
            OutputDirectoryPath = Path.Combine(System.Environment.CurrentDirectory, "Configs");
        }
        public FileConfigEntity(string fileName, string directoryPath)
        {
            InputFileName = fileName;
            InputDirectoryPath = directoryPath;
            OutputFileName = fileName;
            OutputDirectoryPath = directoryPath;
        }
        public FileConfigEntity(string inputFileName, string inputDirectoryPath
            , string outputFileName, string outputDirectoryPath)
        {
            InputFileName = inputFileName;
            InputDirectoryPath = inputDirectoryPath;
            OutputFileName = outputFileName;
            OutputDirectoryPath = outputDirectoryPath;
        }

        public abstract bool Load();
        public abstract void Save();
        public string ToDisplayFormat()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
