using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB3_To_JSON_Utility {
    public static class HFSCopy {
        public static string hfsPath { get; set; }

        public const string db3Hfs = "286FE9924483F382029EF68BA6C260B3C2563BF9.hfs";
        public const string heroesHfs = "0FABE22A68C0451A7EF97F6E8E682A448BB8122A.hfs";

        public static void CopyHFS() {
            Form1 form1 = (Form1)Application.OpenForms[0];
            string db3From = Path.Combine(hfsPath, db3Hfs);
            string heroesFrom = Path.Combine(hfsPath, heroesHfs);
            File.Copy(db3From, Path.Combine(Program.HFSZIPdir, "database.hfs"), true);
            form1.AppendLine("Copied HFS containing database heroes.db3 to HFSnZIP");
            form1.AppendLine("File has been named database.hfs");
            File.Copy(heroesFrom, Path.Combine(Program.HFSZIPdir, "heroestext.hfs"), true);
            form1.AppendLine("Copied HFS containing textfile heroes_text_english.txt to HFSnZIP");
            form1.AppendLine("File has been named heroestext.hfs");
        }
    }
}
