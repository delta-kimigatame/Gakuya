using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;

namespace Gakuya
{
    class IOCharacter
    {
        private String dirPath;
        const String fileName = "character.txt";
        private String name;
        private String image;
        private String sample;
        private String author;
        private String web;
        private String version;

        public string DirPath { get => dirPath; set => dirPath = value; }
        public string Name { get => name; set => name = value; }
        public string Image { get => image; set => image = value; }
        public string Sample { get => sample; set => sample = value; }
        public string Author { get => author; set => author = value; }
        public string Web { get => web; set => web = value; }
        public string Version { get => version; set => version = value; }

        public IOCharacter(String path)
        {
            dirPath = path;
            Console.WriteLine(Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.ANSICodePage).WebName);
            if (!HasFile())
            {
                File.WriteAllText(Path.Combine(dirPath, fileName), "");
            }
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
                if (data[i].Contains("name="))
                {
                    name = data[i].Replace("name=", "");
                }else if (data[i].Contains("image="))
                {
                    image = data[i].Replace("image=", "");
                }else if (data[i].Contains("author="))
                {
                    author = data[i].Replace("author=", "");
                }else if (data[i].Contains("web="))
                {
                    web = data[i].Replace("web=", "");
                }else if (data[i].Contains("sample="))
                {
                    sample = data[i].Replace("sample=", "");
                }
                else if (data[i].Contains("version="))
                {
                    version = data[i].Replace("version=", "");
                }else if (data[i].Contains("name:"))
                {
                    name = data[i].Replace("name:", "");
                }
                else if (data[i].Contains("image:"))
                {
                    image = data[i].Replace("image:", "");
                }
                else if (data[i].Contains("author:"))
                {
                    author = data[i].Replace("author:", "");
                }
                else if (data[i].Contains("web:"))
                {
                    web = data[i].Replace("web:", "");
                }
                else if (data[i].Contains("sample:"))
                {
                    sample = data[i].Replace("sample:", "");
                }
                else if (data[i].Contains("version:"))
                {
                    version = data[i].Replace("version:", "");
                }
            }
        }

        public void Write()
        {
            List<String> writeData = new List<String>();
            if (name != "")
            {
                writeData.Add("name=" + name);
            }
            if (image != "")
            {
                writeData.Add("image=" + image);
            }
            if (sample != "")
            {
                writeData.Add("sample=" + sample);
            }
            if (web != "")
            {
                writeData.Add("web=" + web);
            }
            if (author != "")
            {
                writeData.Add("author=" + author);
            }
            if (version != "")
            {
                writeData.Add("version=" + version);
            }
            File.WriteAllLines(Path.Combine(dirPath, fileName), writeData, Encoding.GetEncoding("Shift_JIS"));
        }

        private bool HasFile()
        {
            return File.Exists(Path.Combine(dirPath, fileName));
        }
    }
}
