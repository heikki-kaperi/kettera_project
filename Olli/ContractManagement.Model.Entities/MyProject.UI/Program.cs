using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyProject.UI
{
    internal static class Program
    {
        [STAThread] // tämä on tärkeä!
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1()); // 👈 tämä avaa Form1-ikkunan
        }
    }
}