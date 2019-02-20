using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proxy10
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        static Icon ico0, ico1;
        static NotifyIcon nf;
        static bool STATE = false;
        static RegistryKey registry;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            /*INITIALIZES*/
            registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);

            ico0 = Properties.Resources.ico0;
            ico1 = Properties.Resources.ico1;

            nf = new NotifyIcon();

            nf.Icon = ico0;
            nf.Visible = true;
            /*END INITIALIZES*/

            //CHECH STATUS ON RUN
            if (getProxyStatus() == 1)
            {
                nf.Icon = ico1;
                STATE = true;
            }
            else {
                nf.Icon = ico0;
                STATE = false;
            }
            //DOUBLE CLICK EVENT HANDLER CALL FOR NOTIFYICON
            nf.DoubleClick += Nf_DoubleClick;


            Application.Run();
        }

        private static void Nf_DoubleClick(object sender, EventArgs e)
        {
            switchProxy();
        }
        private static void switchProxy() {
            if (STATE)
            {
                //ha be van kapcsolva akkor ki
                registry.SetValue("ProxyEnable", 0);
                STATE = false;
                nf.Icon = ico0;

            }
            else {
                //ki volt kapcsolva és most bekopcsolás
                registry.SetValue("ProxyEnable", 1);
                STATE = true;
                nf.Icon = ico1;
            }
        }
        private static int getProxyStatus() {
            return (int)registry.GetValue("ProxyEnable");
        }
    }
}
