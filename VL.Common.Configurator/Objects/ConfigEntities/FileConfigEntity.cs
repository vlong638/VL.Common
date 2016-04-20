using System;
using System.IO;

namespace VL.Common.Configurator.Objects.ConfigEntities
{
    /// <summary>
    /// 文件类型配置对象 即Level1 
    /// Level@n表支持几级节点 
    /// 2表根+1级子节点 
    /// 3表根+2级子节点
    /// </summary>
    public abstract class FileConfigEntity: ConfigEntity
    {
        public string InputFileName { set; get; }
        public string InputDirectoryPath { set; get; }
        public string InputFilePath { get { return Path.Combine(InputDirectoryPath, InputFileName); } }
        public string OutputFileName { set; get; }
        public string OutputDirectoryPath { set; get; }
        public string OutputFilePath { get { return Path.Combine(OutputDirectoryPath, OutputFileName); } }

        public FileConfigEntity(string fileName)
        {
            var directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configs");
            Init(fileName, directoryPath, fileName, directoryPath);
        }
        public FileConfigEntity(string fileName, string directoryPath)
        {
            Init(fileName, directoryPath, fileName, directoryPath);
        }
        public FileConfigEntity(string inputFileName, string inputDirectoryPath
            , string outputFileName, string outputDirectoryPath)
        {
            Init(inputFileName, inputDirectoryPath, outputFileName, outputDirectoryPath);
        }

        private void Init(string inputFileName, string inputDirectoryPath, string outputFileName, string outputDirectoryPath)
        {
            if (inputFileName.IndexOf(".") <= 0)
            {
                inputFileName += ".config";
            }
            if (outputFileName.IndexOf(".") <= 0)
            {
                outputFileName += ".config";
            }
            InputFileName = inputFileName;
            InputDirectoryPath = inputDirectoryPath;
            OutputFileName = outputFileName;
            OutputDirectoryPath = outputDirectoryPath;
        }
    }
}
