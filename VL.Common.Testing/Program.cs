using System;
using VL.Common.DAS.Utilities;
using VL.Common.Logger.Utilities;

namespace VL.Common.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestCreateConfig();
            var log4Logger = LogHelper.GetLog4netLogger("Generator");
            log4Logger.Error("error");
            log4Logger.Info("info");
            var textLogger = LogHelper.GetTextLogger("text.config", System.Environment.CurrentDirectory + "/TextLogs");
            textLogger.Error("error2");
            textLogger.Info("info2");
            Console.ReadLine();

            //List<ConsoloMenuItem> consoloMenuItems = new List<ConsoloMenuItem>();
            //consoloMenuItems.Add(new ConsoloMenuItem("save", "生成ORM配置文件", () =>
            //{
            //    ORMGeneratorConfigEntity configEntity = new ORMGeneratorConfigEntity("ORMGenerate.config", @"D:\GitProjects\VL.Common\VL.Common.Testing\Configs");
            //    configEntity.PDMFilePath = @"E:\WorkSpace\3.系统建模1";
            //    configEntity.TargetDirectoryPath = @"E:\WorkSpace\3.系统建模2";
            //    configEntity.Save();
            //}));
            //consoloMenuItems.Add(new ConsoloMenuItem("load", "加载ORM配置文件", () =>
            //{
            //    ORMGeneratorConfigEntity configEntity = new ORMGeneratorConfigEntity("ORMGenerate.config", @"D:\GitProjects\VL.Common\VL.Common.Testing\Configs");
            //    return configEntity.Load();
            //}));
            //consoloMenuItems.WaitForOperation();
        }

        private static void TestCreateConfig()
        {
            DbConfigHelper.CreateConfigFile();
        }
    }
}
