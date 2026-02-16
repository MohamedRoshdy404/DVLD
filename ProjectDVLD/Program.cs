using ProjectDVLD.Applications;
using ProjectDVLD.Applications.Application_Types;
using ProjectDVLD.Applications.Local_Driving_License;
using ProjectDVLD.Login;
using ProjectDVLD.People;
using ProjectDVLD.Tests.Test_Types;
using ProjectDVLD.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectDVLD
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           //Application.Run(new FormISTest());
           Application.Run(new frmLogin());

           
        }
    }
}
