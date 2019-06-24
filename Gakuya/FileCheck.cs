using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Wave;
using System.Text;
using System.Threading.Tasks;

namespace Gakuya
{
    class FileCheck
    {
        private string dirPath;
        private string[] files;
        private List<string> needFiles;
        private List<String> report;
        private bool checkOto;
        private bool checkRead;
        private bool checkUspec;
        private bool checkFrq;
        private bool checkFrc;
        private bool checkPmk;
        private bool checkVs4ufrq;
        private bool checkLlsm;
        private bool checkMrq;
        private bool checkDio;
        private bool checkWavStereo;
        private bool checkWavSample;
        private bool alert;
        private bool already;
        
        
        public bool CheckFrq { get => checkFrq; set => checkFrq = value; }
        public bool CheckFrc { get => checkFrc; set => checkFrc = value; }
        public bool CheckPmk { get => checkPmk; set => checkPmk = value; }
        public bool CheckVs4ufrq { get => checkVs4ufrq; set => checkVs4ufrq = value; }
        public bool CheckLlsm { get => checkLlsm; set => checkLlsm = value; }
        public bool CheckMrq { get => checkMrq; set => checkMrq = value; }
        public bool CheckDio { get => checkDio; set => checkDio = value; }
        public bool CheckUspec { get => checkUspec; set => checkUspec = value; }
        public bool CheckRead { get => checkRead; set => checkRead = value; }
        public bool CheckOto { get => checkOto; set => checkOto = value; }
        public bool CheckWavStereo { get => checkWavStereo; set => checkWavStereo = value; }
        public bool CheckWavSample { get => checkWavSample; set => checkWavSample = value; }
        public bool Alert { get => alert; set => alert = value; }

        public FileCheck()
        {
        }

        public void SetDirPath(string path)
        {
            dirPath = path;
            alert = false;
            already = false;
            report = new List<String>();
            files = Directory.GetFiles(dirPath, "*", SearchOption.AllDirectories);

        }
        public List<String> GetReport()
        {
            report = new List<String>();
            GetFiles();
            if (checkFrq)
            {
                HasFrq();
            }
            if(checkWavStereo || checkWavSample)
            {
                IsStereo();
            }
            if (checkOto)
            {
                otoSearch(dirPath);
            }
            return report;
        }

        private void otoSearch(String path)
        {
            string[] wavFiles=Directory.GetFiles(path,"*.wav");
            string[] iniFiles= Directory.GetFiles(path, "oto.ini");
            string[] dirs = Directory.GetDirectories(path);
            if (path == dirPath && iniFiles.Length==0)
            {
                report.Add("not exist root oto.ini");
            }
            if(wavFiles.Length>0 && iniFiles.Length == 0)
            {
                report.Add("not exist oto.ini:" + path);
            }
            for (int i = 0; i < dirs.Length; i++)
            {
                otoSearch(dirs[i]);
            }
        }

        private void IsStereo()
        {
            for (int i = 0; i < needFiles.Count(); i++)
            {
                if (Path.GetExtension(needFiles[i]).ToLower() == ".wav")
                {
                    var w=new Wave.Wave(needFiles[i]);
                    w.Read();
                    if(checkWavStereo && w.GetHeader().Channel == 2)
                    {
                        report.Add("Stereo:" + needFiles[i]);
                        alert = true;
                    }
                    if (checkWavSample && w.GetHeader().SampleRate != 44100)
                    {
                        report.Add("Sample Rate not 44100:" + needFiles[i] + w.GetHeader().SampleRate);
                        alert = true;
                    }
                }
            }
        }

        private void HasFrq()
        {
            for (int i = 0; i < needFiles.Count(); i++)
            {
                if(Path.GetExtension(needFiles[i]).ToLower()==".wav" && !needFiles.Contains(needFiles[i].Replace(".wav", "_wav.frq")))
                {
                    report.Add("not exist:" + needFiles[i].Replace(".wav", "_wav.frq"));
                    alert = true;
                }
            }
        }

        private List<String> GetFiles()
        {
            needFiles = new List<string>();
            for (int i = 0; i < files.Length; i++)
            {
                deleteFile(files[i]);
            }
            already = true;
            return needFiles;
        }

        public List<String> GetNeedFiles()
        {
            if (!already)
            {
                GetFiles();
            }
            return needFiles;
        }

        private void deleteFile(string fileName)
        {
            if (checkUspec && Path.GetExtension(fileName).ToLower() == ".uspec")
            {
                report.Add("delete:" + fileName);
            }
            else if (checkDio && (Path.GetExtension(fileName).ToLower() == ".dio" || Path.GetExtension(fileName).ToLower() == ".star" || Path.GetExtension(fileName).ToLower() == ".platinum"))
            {
                report.Add("delete:" + fileName);
            }
            else if (checkMrq && Path.GetExtension(fileName).ToLower() == ".mrq")
            {
                report.Add("delete:" + fileName);
            }
            else if (checkLlsm && Path.GetExtension(fileName).ToLower() == ".llsm")
            {
                report.Add("delete:" + fileName);
            }
            else if (checkVs4ufrq && Path.GetExtension(fileName).ToLower() == ".vs4ufrq")
            {
                report.Add("delete:" + fileName);
            }
            else if (checkPmk && Path.GetExtension(fileName).ToLower() == ".pmk")
            {
                report.Add("delete:" + fileName);
            }
            else if (checkFrc && Path.GetExtension(fileName).ToLower() == ".frc")
            {
                report.Add("delete:" + fileName);
            }else if (checkRead && fileName.Replace(dirPath, "") == "\\$read")
            {
                report.Add("delete:" + fileName);
            }
            else
            {
                needFiles.Add(fileName);
            }
        }
    }
}
