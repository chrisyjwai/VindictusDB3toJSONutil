using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using System.Windows.Forms;

namespace DB3_To_JSON_Utility {
    public static class ZIP {
        public static void ExtractZIP() {
            Form1 form1 = (Form1)Application.OpenForms[0];
            string sourceDB = Path.Combine(Program.HFSZIPdir, "database.zip");
            string sourceTXT = Path.Combine(Program.HFSZIPdir, "heroestext.zip");
            if (File.Exists(Path.Combine(Program.DB3TXTdir, "heroes.db3.comp"))) {
                File.Delete(Path.Combine(Program.DB3TXTdir, "heroes.db3.comp"));
            }
            ZipFile.ExtractToDirectory(sourceDB, Program.DB3TXTdir);
            if (File.Exists(Path.Combine(Program.DB3TXTdir,"heroes.db3"))) {
                File.Delete(Path.Combine(Program.DB3TXTdir, "heroes.db3"));
            }
            File.Move(
                Path.Combine(Program.DB3TXTdir, "heroes.db3.comp"),
                Path.Combine(Program.DB3TXTdir, "heroes.db3")
                    );
            form1.AppendLine("database.zip extracted to DB3nHeroesTXT");
            if (File.Exists(Path.Combine(Program.DB3TXTdir, "heroes_text_english.txt"))) {
                File.Delete(Path.Combine(Program.DB3TXTdir, "heroes_text_english.txt"));
            }
            ZipFile.ExtractToDirectory(sourceTXT, Program.DB3TXTdir);
            form1.AppendLine("heroestext.zip extracted to DB3nHeroesTXT");
        }
    }
}
