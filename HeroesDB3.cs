using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DB3_To_JSON_Utility {
    public static class HeroesDB3 {
        //ItemClassInfo and EquipClassInfo
        #region
        private static string[] Cols = {
                                "ItemClass",
                                "Icon",
                                "Category",
                                "TradeCategory",
                                "TradeCategorySub",
                                "InventoryFilterName",
                                "MaxStack",
                                "QuickSlotMaxStack",
                                "Price",
                                "SellPrice",
                                "ExpireIn",
                                "Rarity",
                                "Level",
                                "RequiredLevel",
                                "LevelUpperBound",
                                "RequiredSkill",
                                "RequiredSkillRank",
                                "ClassRestriction",
                                "Unique",
                                "IsCafeOnly",
                                "Indestructible",
                                "MaxAntiBind",
                                "UseInTown",
                                "UseInQuest",
                                "Bind",
                                "TradeOnBind",
                                "TradeRestirction",
                                "RequiredEnhanceLevel",
                                "RequiredEnchantLevel",
                                "EquipClass",
                                "CostumeType",
                                "MaxDurability",
                                "ArmorHP",
                                "ATK",
                                "ATK_Speed",
                                "Critical",
                                "Balance",
                                "MATK",
                                "DEF",
                                "Res_Critical",
                                "PVP_ATK",
                                "PVP_MATK",
                                "PVP_DEF",
                                "STR",
                                "DEX",
                                "INT",
                                "WILL",
                                "LUCK",
                                "HP",
                                "STAMINA",
                                "TOWN_SPEED",
                                "ATK_LimitOver",
                                "DamageReflect",
                                "Weight",
                                "EnhanceType",
                                "QualityType",
                                "Synthesizable",
                                "Enchantable",
                                "Dyeable"
        };
        #endregion

        private static Dictionary<string, string> names = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        private static List<string> chclass = new List<string>();
        private static List<string> chnames = new List<string>();

        private static int CurrentV;

        public static void CreateJSON() {
            Regex rg = new Regex(@"([0-9]+)");
            string[] files = Directory.GetFiles(Program.OpJSONdir);
            string newfile;
            Match m;
            int matches = 0;
            foreach (string file in files) {
                m = rg.Match(file);
                if (m.Success) {
                    matches++;
                }
            }
            if (matches == 0) {
                newfile = "ItemClassInfoV1.json";
                File.Create(Path.Combine(Program.OpJSONdir, newfile)).Dispose();
                CurrentV = 1;
                return;
            }
            int curver = 0;
            int ver;
            foreach (string file in files) {
                ver = int.Parse(rg.Split(file)[1]);
                if (ver > curver) {
                    curver = ver;
                }
            }
            newfile = string.Concat("ItemClassInfoV", (++curver).ToString(), ".json");
            File.Create(Path.Combine(Program.OpJSONdir, newfile)).Dispose();
            CurrentV = curver;
        }

        public static void BuildDictionary() {
            Regex rg = new Regex(@"""HEROES_ITEM_NAME_([\&\:\[\]0-9A-Z_]+)""\s*""(.*?)""$", RegexOptions.Multiline);
            Regex chrg = new Regex(@"""CHN_HEROES_ITEM_NAME_([\&\:\[\]0-9A-Z_]+)""\s*""(.*?)""$", RegexOptions.Multiline);
            string path = Path.Combine(Program.DB3TXTdir, "heroes_text_english.txt");
            Form1 form1 = (Form1)Application.OpenForms[0];
            form1.AppendLine(string.Concat("Building dictionary from ", path, "..."));
            string line;
            Match match;
            using (StreamReader sr = new StreamReader(path)) {
                while((line = sr.ReadLine()) != null) {
                    if ((match = rg.Match(line)).Success) {
                        names.Add(match.Groups[1].Value, match.Groups[2].Value);
                    }
                }
            }
            form1.AppendLine("Dictionary built");
            form1.AppendLine("Building CHN dictionary");
            using (StreamReader sr = new StreamReader(path)) {
                while ((line = sr.ReadLine()) != null) {
                    if ((match = chrg.Match(line)).Success) {
                        chclass.Add(match.Groups[1].Value);
                        chnames.Add(match.Groups[2].Value);
                    }
                }
            }
            form1.AppendLine("CHN dictionary built");
        }


        public static void WriteItemData() {
            Form1 form1 = (Form1)Application.OpenForms[0];
            string dbpath = Path.Combine(Program.DB3TXTdir, "heroes.db3");
            string connect = string.Concat("Data Source=", dbpath, ";Version=3;");
            SQLiteConnection dbConnection = new SQLiteConnection(connect);
            dbConnection.Open();
            
            string sql = string.Concat("SELECT * FROM ItemClassInfo LEFT JOIN EquipItemInfo on ItemClassInfo.ItemClass = EquipItemInfo.ItemClass");
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            string fname = string.Concat("ItemClassInfoV", CurrentV.ToString(), ".json");
            
            StreamWriter sw = new StreamWriter(Path.Combine(Program.OpJSONdir, fname), true);
            sw.WriteLine("[");
            string line = string.Empty;
            int n = Cols.Length;

            int count = 0;

            while (reader.Read()) {
                line = string.Concat(line, string.Format("{{\"ItemClass\":\"{0}\",", reader["ItemClass"]));
                if (names.ContainsKey((string)reader["ItemClass"])) {
                    line = string.Concat(line, string.Format("\"Name\":\"{0}\",", names[(string)reader["ItemClass"]]));
                }
                else {
                    line = string.Empty;
                    continue;
                }
                for (int i = 1; i < n; i++) {
                    if (i < n - 1) {
                        line = string.Concat(
                            line,
                            string.Format("\"{0}\":\"{1}\",", Cols[i], reader[Cols[i]])
                            );
                    }
                    else {
                        line = string.Concat(
                            line,
                            string.Format("\"{0}\":\"{1}\"", Cols[i], reader[Cols[i]])
                            );
                    }
                }
                sw.Write(line);
                sw.Write("}},{0}", Environment.NewLine);
                line = string.Empty;
                form1.AppendLine(string.Format("Wrote item: {0}", reader["ItemClass"]));
                count++;
            }

            int m = chnames.Count;
            for (int j = 0; j < m; j++) {
                line = string.Concat(line, string.Format("{{\"ItemClass\":\"{0}\",", chclass[j]));
                line = string.Concat(line, string.Format("\"Name\":\"{0}\",", chnames[j]));
                for (int i = 0; i < n; i++) {
                    if (i < n - 1) {
                        line = string.Concat(
                            line,
                            string.Format("\"{0}\":\"\",", Cols[i])
                            );
                    }
                    else {
                        line = string.Concat(
                            line,
                            string.Format("\"{0}\":\"\"", Cols[i])
                            );
                    }
                }
                sw.Write(line);
                if (j < m - 1) {
                    sw.Write("}},{0}", Environment.NewLine);
                }
                else {
                    sw.Write("}}{0}", Environment.NewLine);
                }
                line = string.Empty;
                form1.AppendLine(string.Format("Wrote CHN name: {0}", chclass[j]));
            }
            sw.Write("]");
            sw.Close();
            dbConnection.Close();

            string fullpathdir = Path.Combine(Directory.GetCurrentDirectory(), Program.OpJSONdir);
            form1.AppendLine(string.Concat("Done! Wrote JSON file: ", fname, " to ", fullpathdir));
        }
    }
}
