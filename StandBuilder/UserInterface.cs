using System;
using System.Windows.Forms;

namespace StandBuilder
{
    internal static class UserInterface
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StandBuilderForm());
        }
    }
}