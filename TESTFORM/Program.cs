using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TESTFORM
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!HslCommunication.Authorization.SetAuthorizationCode("4e2462e1-d762-4b8f-8df3-ae94f8f4e798"))
            {
                Console.WriteLine("active failed");
                Console.ReadLine();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
