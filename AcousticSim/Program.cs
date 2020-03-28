using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Envision.Windows
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                SplashScreen.SplashScreen.ShowSplashScreen();
                Application.DoEvents();
                SplashScreen.SplashScreen.SetStatus("loading python modules...");
                AppGlobals.PyInit();
                AppGlobals.args = args;
                SplashScreen.SplashScreen.SetStatus("loading blockset...");
                string dir = (new System.IO.FileInfo(Application.ExecutablePath).Directory.FullName);
                AppGlobals.LoadBlockset(dir);
                AppGlobals.LoadBlockset();
            } catch (Exception ex)
            {
                MessageBox.Show("Critical error occured (Err: " + ex.Message + ")");
            }
            Application.Run(new mainForm());
        }
    }
}
