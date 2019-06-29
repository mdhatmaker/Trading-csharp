using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TradingTechnologies.TTAPI;

namespace FillsDeluxe
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Attach a UIDispatcher to the current thread
            using (UIDispatcher disp = Dispatcher.AttachUIDispatcher())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }
    } // class
} // namespace
