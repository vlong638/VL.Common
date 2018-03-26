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

        public NavigatorItem(HelperBase parent, string description = "未添加功能描述", string doorPlate = "")
            : base(parent, description)
        {
            DoorPlate = doorPlate;
        }

        public override void  Execute()
        {
            ShowMenuWithResult();
        }

        public string DoorPlate { set; get; }
        public string GetDoorPlateBoard()
        {
            StringBuilder sb = new StringBuilder();
            string line = "";
            int boardLength = 10;
            PadToRight(ref line, boardLength, "*");
            sb.AppendLine(line);
            string text = DoorPlate;
            PadToRight(ref text, boardLength, "");
            sb.AppendLine(text);
            return sb.ToString();
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
            if (string.IsNullOrEmpty(DoorPlate))
            {
                Console.WriteLine(GetDoorPlateBoard());
            }
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
