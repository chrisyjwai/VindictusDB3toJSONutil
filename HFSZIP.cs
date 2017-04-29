using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB3_To_JSON_Utility {
    public static class HFSZIP {
        public static void HFStoZIP() {
            Form1 form1 = (Form1)Application.OpenForms[0];
            VZipFlip.ExtractHfs(Path.Combine(Program.HFSZIPdir, "database.hfs"));
            form1.AppendLine("Converted database.hfs to ZIP archive");
            VZipFlip.ExtractHfs(Path.Combine(Program.HFSZIPdir, "heroestext.hfs"));
            form1.AppendLine("Converted heroestext.hfs to ZIP archive");
        }
    }
}
