using System;
using System.Text;

namespace VL.Logger
{

    interface ILogger
    {
        void Info(LogData locator);
        void Error(LogData locator);

        //void Info(Exception ex);
        //void Error(Exception ex);
    }
}
