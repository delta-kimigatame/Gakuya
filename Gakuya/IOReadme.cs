using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gakuya
{
    class IOReadme
    {
        private String dirPath;
        const String fileName = "readme.txt";
        private String data;

        public string Data { get => data; set => data = value; }

        public IOReadme(String path)
        {
            dirPath = path;
            Console.WriteLine(Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.ANSICodePage).WebName);
            if (!HasFile())
            {
                File.WriteAllText(Path.Combine(dirPath, fileName), "");
            }
            Read();
        }

        private bool HasFile()
        {
            return File.Exists(Path.Combine(dirPath, fileName));
        }
        private void Read()
        {
            StreamReader file = new StreamReader(Path.Combine(dirPath, fileName), Encoding.GetEncoding(Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.ANSICodePage).WebName));

            data = file.ReadToEnd();
            file.Close();
        }

        public void Write()
        {
            File.WriteAllText(Path.Combine(dirPath, fileName), data, Encoding.GetEncoding("Shift_JIS"));
        }


    }
}
