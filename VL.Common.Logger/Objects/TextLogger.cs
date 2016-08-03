using System.IO;

namespace VL.Common.Logger.Objects
{
    public class TextLogger : IFileBased
    {
        #region Properties
        static object LogLocker = new object();
        string _fileName;
        public string FileName
        {
            set
            {
                _fileName = value;
            }
            get
            {
                return _fileName;
            }
        }
        string _directoryName;
        public string DirectoryName
        {
            set
            {
                _directoryName = value;
            }
            get
            {
                return _directoryName;
            }
        }
        public string FilePath
        {
            get
            {
                return Path.Combine(DirectoryName, FileName);
            }
        }
        #endregion

        public TextLogger(string fileName, string directoryName)
        {
            FileName = fileName;
            DirectoryName = directoryName;
            if (!Directory.Exists(DirectoryName))
            {
                Directory.CreateDirectory(DirectoryName);
            }
        }

        #region Methods
        protected void writeLog(string message)
        {
            lock (LogLocker)
            {
                using (StreamWriter stream = File.AppendText(FilePath))
                {
                    stream.WriteLine(message);
                }
            }
        }
        public virtual void Error(string message)
        {
            writeLog(message);
        }
        public void Error(string pattern, params object[] args)
        {
            Error(string.Format(pattern, args));
        }
        public void Info(string pattern, params object[] args)
        {
            Info(string.Format(pattern, args));
        }
        public virtual void Info(string message)
        {
            writeLog(message);
        } 
        #endregion
    }
}
