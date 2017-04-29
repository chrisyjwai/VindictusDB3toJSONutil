using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Hfs;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace DB3_To_JSON_Utility {
    class StreamLocalSourceZip : ICSharpCode.SharpZipLib.Zip.IStaticDataSource {
        private Stream stream;
        public StreamLocalSourceZip(Stream x) {
            this.stream = x;
        }

        public Stream GetSource() {
            return this.stream;
        }
    };

    public class VZipFlip {
        public static void ExtractHfs(string filename) {
            string basep = Path.GetDirectoryName(filename);
            string plain = Path.GetFileNameWithoutExtension(filename);

            using (HfsFile hfs = new HfsFile(filename))
            using (ZipFile zip = ZipFile.Create(basep + @"\" + plain + ".zip")) {

                zip.BeginUpdate();

                foreach (HfsEntry hfsEntry in hfs) {
                    Console.WriteLine("Processing " + hfsEntry.Name);


                    try {
                        Stream read = hfs.GetInputStream(hfsEntry);

                        zip.Add(new StreamLocalSourceZip(read), hfsEntry.Name);
                    }
                    catch (Exception e) {
                        Console.WriteLine("Couldn't process " + hfsEntry.Name + ": " + e.Message);
                    }
                }

                if (hfs.ObfuscationKey != 0) {
                    zip.SetComment("extra_obscure");
                }

                //Console.WriteLine("Compressing..");
                zip.CommitUpdate();
            }

            //Console.WriteLine("Wrote to " + basep + @"\" + plain + "_.zip");
        }
    }
}
