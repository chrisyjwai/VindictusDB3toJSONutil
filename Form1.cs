using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using System.Threading;

namespace DB3_To_JSON_Utility {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e) {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button4_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(textBox1.Text) == true) {
                MessageBox.Show("Please enter or select a path");
                return;
            }
            if (Directory.Exists(textBox1.Text) == false) {
                MessageBox.Show("Path does not exist!");
                return;
            }

            //TODO run program
            //TODO disable OK button until done
            button4.Dispose();
            ConvertAndExtract();
            Output();
        }

        private void ConvertAndExtract() {
            HFSCopy.hfsPath = textBox1.Text;
            HFSCopy.CopyHFS();
            HFSZIP.HFStoZIP();
            ZIP.ExtractZIP();
        }

        private void Output() {
            HeroesDB3.CreateJSON();
            HeroesDB3.BuildDictionary();
            HeroesDB3.WriteItemData();
        }

        public void AppendLine(string s) {
            textBox2.AppendText(string.Concat(" ",s,Environment.NewLine));
        }
    }
}
