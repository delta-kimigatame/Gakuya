using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;

namespace Gakuya
{
    class IOInstall
    {
        private String dirPath;
        const String fileName = "install.txt";
        private String type;
        private String folder;
        private String contentsdir;
        private String description;

        public string Type { get => type; set => type = value; }
        public string Folder { get => folder; set => folder = value; }
        public string Contentsdir { get => contentsdir; set => contentsdir = value; }
        public string Description { get => description; set => description = value; }

        public IOInstall(String path)
        {
            dirPath = path;
            Console.WriteLine(Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.ANSICodePage).WebName);
            if (!HasFile())
            {
                File.WriteAllText(Path.Combine(dirPath, fileName), "");
            }
            folder = dirPath.Replace(Directory.GetParent(dirPath).ToString()+"\\", "");
            Read();
        }

        private void Read()
        {
            var data = new List<String>();
            string line;
            StreamReader file = new StreamReader(Path.Combine(dirPath, fileName), Encoding.GetEncoding(Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.ANSICodePage).WebName));

            while((line=file.ReadLine()) != null)
            {
                data.Add(line);
            }
            file.Close();
            Analyze(data);
        }

        private void Analyze(List<String> data)
        {
            for(int i = 0; i < data.Count(); i++)
            {
                if (data[i].Contains("type="))
                {
                    type = data[i].Replace("type=", "");
                }
                else if (data[i].Contains("folder="))
                {
                    folder = data[i].Replace("folder=", "");
                }
                else if (data[i].Contains("contentsdir="))
                {
                    contentsdir = data[i].Replace("contentsdir=", "");
                }
                else if (data[i].Contains("description="))
                {
                    Description = data[i].Replace("description=", "");
                }
            }
        }

        public void Write()
        {
            List<String> writeData = new List<String>();
            if (type != "")
            {
                writeData.Add("type=" + type);
            }
            if (folder != "")
            {
                writeData.Add("folder=" + folder);
            }
            if (contentsdir != "")
            {
                writeData.Add("contentsdir=" + contentsdir);
            }
            else
            {
                writeData.Add("contentsdir=" + folder);
            }
            writeData.Add("description=" + description);
            File.WriteAllLines(Path.Combine(dirPath, fileName), writeData, Encoding.GetEncoding("Shift_JIS"));
        }

        private bool HasFile()
        {
            return File.Exists(Path.Combine(dirPath, fileName));
        }
    }
}
