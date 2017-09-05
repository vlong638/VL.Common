using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.Helper.CompositeTemplate
{
    public class NavigatorItem
        : HelperBase
    {
        public NavigatorItem()
        {
        }

        public NavigatorItem(HelperBase parent, string description = "未添加功能描述")
            : base(parent, description) { }

        public override void  Execute()
        {
            ShowMenuWithResult();
        }

        public void ShowMenuWithResult()
        {
            ShowMenu();

            string input;
            while (!string.Equals(input = Console.ReadLine().ToLower(), "b"))
            {
                if (Parent == null && string.Equals(input, "q"))
                {
                    //退出程序
                    break;
                }

                int index = -1;
                if (int.TryParse(input, out index))
                {
                    HelperBase son = SonList[index];
                    if (son != null)
                    {
                        son.Execute();
                    }
                }
                ShowMenu();
            }
        }
        protected void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (SonList.Count() > 0)
            {
                Console.WriteLine("请选择功能:");
                SonList.ForEach((c) => { Console.WriteLine(c.MenuStr); });
            }
            else
            {
                Console.WriteLine("该节点无下属功能");
            }
            if (Parent != null)
            {
                Console.WriteLine("输入b返回上级");
            }
            else
            {
                Console.WriteLine("输入q退出程序");
            }
            Console.WriteLine();
        }
    }
}
