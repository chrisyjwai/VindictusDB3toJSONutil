using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB3_To_JSON_Utility {
    static class Program {
        public const string HFSZIPdir = "HFSnZIP";
        public const string DB3TXTdir = "DB3nHeroesTXT";
        public const string OpJSONdir = "OutputJSON";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Directory.CreateDirectory(HFSZIPdir);
            Directory.CreateDirectory(DB3TXTdir);
            Directory.CreateDirectory(OpJSONdir);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }


    }
}
