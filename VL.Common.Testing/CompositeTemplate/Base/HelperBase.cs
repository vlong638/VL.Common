using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Test.Helper.CompositeTemplate
{
    //仅使用了Layer SuperType模式(简)
    public abstract class HelperBase
    {
        public static int LENGTH_INDEX = 4;
        public static int LENGTH_DESCRIPTION = 80;
        public static string PADDING_STRING = " ";

        public HelperBase Parent { set; get; }
        public List<HelperBase> SonList = new List<HelperBase>();
        public bool IsExecutable;
        public int Index { set; get; }
        public string Description { set; get; }

        private string _menuStr;
        public string MenuStr
        {
            get
            {
                if (string.IsNullOrEmpty(_menuStr))
                {
                    _menuStr = Index + ":";
                    PadRight(ref _menuStr,LENGTH_INDEX, PADDING_STRING);
                    _menuStr += Description;
                }
                return _menuStr;
            }
        }

        public HelperBase(){
        }

        public HelperBase(HelperBase parent, string description = "")
        {
            Description = description;
            SetParent(parent);
        }

        protected void SetParent(HelperBase parent)
        {
            Parent = parent;
            Index = parent.SonList.Count();
        }

        public virtual void Execute() { }

        protected void WriteLog(string msg)
        {
            using (FileStream stream = File.Open(@"C:\Users\Administrator\Desktop\Export", FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] bytes = System.Text.Encoding.Default.GetBytes(msg);
                stream.Write(bytes, 0, bytes.Length);
            }
        }

        private void PadRight(ref string s, int length, string paddingString)
        {
            if (s.Length >= length)
                return;

            StringBuilder sb = new StringBuilder();
            sb.Append(s);
            for (int i = 0; i < length - s.Length; i++)
            {
                sb.Append(paddingString);
            }
            s = sb.ToString();
        }
    }

    static class ExFunctionBase
    {
        public static void PadRight(this string s, int length, string paddingString)
        {
            if (s.Length >= length)
                return;

            StringBuilder sb = new StringBuilder();
            sb.Append(s);
            for (int i = 0; i < length - s.Length; i++)
            {
                sb.Append(paddingString);
            }
            s = sb.ToString();
        }
    }
}
