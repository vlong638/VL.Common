using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.Helper.CompositeTemplate
{
    public class FunctionItem : HelperBase
    {
        Action MyAction { set; get; }

        public FunctionItem(HelperBase parent, Action action, string description = "未添加功能描述")
            : base(parent, description)
        {
            MyAction = action;
            IsExecutable = true;
        }

        public override void Execute()
        {
            Console.ForegroundColor = ConsoleColor.White;
            MyAction();
            Console.WriteLine();
        }
    }
}
