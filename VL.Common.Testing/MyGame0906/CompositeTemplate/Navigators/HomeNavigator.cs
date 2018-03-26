using System;
using System.Collections.Generic;
using Test.Helper.CompositeTemplate;
using VL.Common.Core.Object.VL.Account;

namespace VL.Common.Testing.CompositeTemplate
{
    /// <summary>
    /// 游戏主界面
    /// </summary>
    public class HomeNavigator : NavigatorItem
    {
        public HomeNavigator() : base()
        {
            SonList.Add(new FunctionItem(this, () =>
            {

            }, "用户登录"));
            SonList.Add(new FunctionItem(this, () =>
            {
            }, "注册账户"));
        }

        private static bool GetInput(string messageInfo, out string input, params Func<string, bool>[] checks)
        {
            Console.WriteLine(messageInfo);
            input = Console.ReadLine();
            foreach (var check in checks)
            {
                if (!check(input))
                    return false;
            }
            return true;
        }
    }
}
