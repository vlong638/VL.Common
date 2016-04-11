using System;
using VL.Common.DAS.Utilities;
using VL.Common.Logger.Utilities;

namespace VL.Common.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            var isSimulationAvailable = ProtocolConfig.IsSimulationAvailable;
            //TestCreateConfig();
            //TestLongger();
        }

        private static void TestLongger()
        {
            var log4Logger = LoggerProvider.GetLog4netLogger("Generator");
            log4Logger.Error("error");
            log4Logger.Info("info");
            var textLogger = LoggerProvider.GetTextLogger("text.config", System.Environment.CurrentDirectory + "/TextLogs");
            textLogger.Error("error2");
            textLogger.Info("info2");
            Console.ReadLine();
        }

        private static void TestCreateConfig()
        {
            DbConfigHelper.CreateConfigFile();
        }
    }
}
