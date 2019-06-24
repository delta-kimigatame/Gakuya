using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Text;
using utauPlugin;

namespace gakuya
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {

        private FileCheck FC;
        private IOCharacter Character;
        private IOReadme Readme;
        private IOInstall Install;
        private String dirPath;
        private bool checkFile;
        private bool checkCharacter;
        private bool checkReadme;
        private bool checkInstall;
        private bool needUar;
        private UtauPlugin utauPlugin;
        string[] args = Environment.GetCommandLineArgs();

        public MainWindow()
        {
            InitializeComponent();
            dirPath = "";
            FC = new FileCheck();
            if (args.Length >= 2)
            {
                utauPlugin = new UtauPlugin(args[1]);
                utauPlugin.Input();
                SetDirPath(utauPlugin.VoiceDir);
                voiceDirBox.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CheckOto_Checked(object sender, RoutedEventArgs e)
        {
            FC.CheckOto = true;
        }
        private void CheckOto_Unchecked(object sender, RoutedEventArgs e)
        {
            FC.CheckOto = false;
        }

        private void FileCheck_Checked(object sender, RoutedEventArgs e)
        {
            checkFile = true;
            checkFileGroup.IsEnabled = true;
        }

        private void FileCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            checkFile = false;
            checkFileGroup.IsEnabled = false;
        }

        private void CheckRead_Checked(object sender, RoutedEventArgs e)
        {
            FC.CheckRead = true;
        }
        private void CheckRead_Unchecked(object sender, RoutedEventArgs e)
        {
            FC.CheckRead = false;
        }

        private void CheckUspec_Checked(object sender, RoutedEventArgs e)
        {
            FC.CheckUspec = true;
        }
        private void CheckUspec_Unchecked(object sender, RoutedEventArgs e)
        {
            FC.CheckUspec = false;
        }

        private void CheckFrq_Checked(object sender, RoutedEventArgs e)
        {
            FC.CheckFrq = true;
        }
        private void CheckFrq_Unchecked(object sender, RoutedEventArgs e)
        {
            FC.CheckFrq = false;
        }

        private void CheckPmk_Checked(object sender, RoutedEventArgs e)
        {
            FC.CheckPmk = true;
        }

        private void CheckPmk_Unchecked(object sender, RoutedEventArgs e)
        {
            FC.CheckPmk = false;
        }

        private void CheckVs4ufrq_Checked(object sender, RoutedEventArgs e)
        {
            FC.CheckVs4ufrq = true;
        }
        private void CheckVs4ufrq_Unchecked(object sender, RoutedEventArgs e)
        {
            FC.CheckVs4ufrq = false;
        }

        private void CheckFrc_Checked(object sender, RoutedEventArgs e)
        {
            FC.CheckFrc = true;
        }
        private void CheckFrc_Unchecked(object sender, RoutedEventArgs e)
        {
            FC.CheckFrc = false;
        }

        private void CheckDio_Checked(object sender, RoutedEventArgs e)
        {
            FC.CheckDio = true;
        }
        private void CheckDio_Unchecked(object sender, RoutedEventArgs e)
        {
            FC.CheckDio = false;
        }

        private void CheckLlsm_Checked(object sender, RoutedEventArgs e)
        {
            FC.CheckLlsm = true;
        }
        private void CheckLlsm_Unchecked(object sender, RoutedEventArgs e)
        {
            FC.CheckLlsm = false;
        }

        private void CheckMrq_Checked(object sender, RoutedEventArgs e)
        {
            FC.CheckMrq = true;
        }
        private void CheckMrq_Unchecked(object sender, RoutedEventArgs e)
        {
            FC.CheckMrq = false;
        }

        private void FrqAuto_Click(object sender, RoutedEventArgs e)
        {
            checkFrq.IsChecked=true;
            checkPmk.IsChecked = true;
            checkFrc.IsChecked = true;
            checkDio.IsChecked = true;
            checkLlsm.IsChecked = true;
            checkVs4ufrq.IsChecked = true;
            checkMrq.IsChecked = true;
        }

        private void CheckWavStereo_Checked(object sender, RoutedEventArgs e)
        {
            FC.CheckWavStereo = true;
        }
        private void CheckWavStereo_UnChecked(object sender, RoutedEventArgs e)
        {
            FC.CheckWavStereo = false;

        }

        private void CheckWavSample_Checked(object sender, RoutedEventArgs e)
        {
            FC.CheckWavSample = true;
        }
        private void CheckWavSample_UnChecked(object sender, RoutedEventArgs e)
        {
            FC.CheckWavSample = false;
        }

        private void CharacterCheck_Checked(object sender, RoutedEventArgs e)
        {
            checkCharacter = true;
            characterBox.IsEnabled = true;
        }
        private void CharacterCheck_UnChecked(object sender, RoutedEventArgs e)
        {
            checkCharacter = false;
            characterBox.IsEnabled = false;
        }

        private void ReadMeCheck_Checked(object sender, RoutedEventArgs e)
        {
            checkReadme = true;
            readme.IsEnabled = true;
        }
        private void ReadMeCheck_UnChecked(object sender, RoutedEventArgs e)
        {
            checkReadme = false;
            readme.IsEnabled = false;
        }

        private void IsntallCheck_Checked(object sender, RoutedEventArgs e)
        {
            checkInstall = true;
            installBox.IsEnabled = true;
        }
        private void IsntallCheck_UnChecked(object sender, RoutedEventArgs e)
        {
            checkInstall = false;
            installBox.IsEnabled = false;
        }

        private void MakeUarCheck_Checked(object sender, RoutedEventArgs e)
        {
            needUar = true;
        }
        private void MakeUarCheck_UnChecked(object sender, RoutedEventArgs e)
        {
            needUar = false;
        }

        private void CharacterImageButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "画像を選択";
            dialog.Filter = "画像(*.bmp)|*.bmp";
            dialog.InitialDirectory = dirPath;
            if(dialog.ShowDialog() == true)
            {
                characterImage.Text = dialog.FileName;
            }
        }

        private void CharacterSampleButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "サンプルを選択";
            dialog.Filter = "音声ファイル(*.wav)|*.wav";
            dialog.InitialDirectory = dirPath;
            if (dialog.ShowDialog() == true)
            {
                characterSample.Text = dialog.FileName;
            }

        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (dirPath == "")
            {
                MessageBox.Show("音源フォルダを指定してください。");
                return;
            }
            if (checkFile)
            {
                var reports = FC.GetReport();
                report.Text = "";
                for (int i = 0; i < reports.Count(); i++)
                {
                    report.Text += reports[i] + "\r\n";
                }
                if (FC.Alert)
                {
                    MessageBox.Show("不具合があります。\nreportタブを確認してください。");
                    reportTab.IsSelected=true;
                }
            }
            if (checkCharacter)
            {
                Character.Name = characterName.Text;
                Character.Version = characterVersion.Text;
                Character.Image = characterImage.Text;
                Character.Sample = characterSample.Text;
                Character.Author = characterAuthor.Text;
                Character.Web = characterWeb.Text;
                Character.Write();
                MessageBox.Show("character.txtを更新しました");
            }
            if (checkReadme)
            {
                Readme.Data = readme.Text;
                Readme.Write();
                MessageBox.Show("readme.txtを更新しました");
            }
            if (checkInstall)
            {
                Install.Folder = installDir.Text;
                Install.Contentsdir = installContentsDir.Text;
                Install.Description = installDescription.Text;
                Install.Write();
                MessageBox.Show("install.txtを更新しました");
            }
            MakeZip();

        }
        private void VoiceDirButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog("音源フォルダを選択");
            dialog.IsFolderPicker = true;
            if(dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                SetDirPath(dialog.FileName);
            }
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            var dropFiles = e.Data.GetData(System.Windows.DataFormats.FileDrop) as string[];
            if (dropFiles == null) return;
            SetDirPath(dropFiles[0]);
        }

        private void SetDirPath(String path)
        {
            dirPath = path;
            voiceDir.Text = path;
            FC.SetDirPath(dirPath);
            InitCharacter(path);
            InitReadme(path);
            InitInstall(path);
            submitButton.IsEnabled = true;
        }

        private void InitReadme(string path)
        {
            Readme = new IOReadme(path);
            readme.Text = Readme.Data;
        }

        private void InitCharacter(String path)
        {
            Character = new IOCharacter(path);
            characterName.Text = Character.Name;
            characterVersion.Text = Character.Version;
            characterImage.Text = Character.Image;
            characterSample.Text = Character.Sample;
            characterAuthor.Text = Character.Author;
            characterWeb.Text = Character.Web;
        }

        private void InitInstall(string path)
        {
            Install = new IOInstall(path);
            Install.Type = "voiceset";
            installDir.Text = Install.Folder;
            installContentsDir.Text = Install.Contentsdir;
            installDescription.Text = Install.Description;

        }

        private void MakeZip()
        {
            String outputPath = "";
            List<String> files = FC.GetNeedFiles();
            var dialog = new SaveFileDialog();
            dialog.Title = "保存";
            dialog.Filter = "圧縮ファイル(*.zip)|*.zip";
            dialog.FileName = dirPath.Replace(Directory.GetParent(dirPath).ToString() + "\\", "");
            dialog.InitialDirectory = dirPath;
            if (dialog.ShowDialog() == true)
            {
                outputPath = dialog.FileName;
            }
            else
            {
                MessageBox.Show("保存を中止しました");
                return;
            }
            File.Delete(outputPath);
            using (ZipArchive zip = ZipFile.Open(outputPath, ZipArchiveMode.Update, Encoding.GetEncoding("Shift_JIS")))
            {
                for (int i = 0; i < files.Count(); i++)
                {
                    if (files[i].Contains("install.txt")){
                        ZipArchiveEntry entry = zip.CreateEntryFromFile(files[i], "install.txt");
                    }
                    else
                    {
                        ZipArchiveEntry entry = zip.CreateEntryFromFile(files[i], files[i].Replace(Directory.GetParent(dirPath).ToString() + "\\", "").Replace("\\","/"));
                    }
                }


            }
            MessageBox.Show("zipファイルを作成しました。");
            if (needUar)
            {
                File.Delete(outputPath.Replace(".zip",".uar"));
                File.Copy(outputPath, outputPath.Replace(".zip", ".uar"));
                MessageBox.Show("uarファイルを作成しました。");

            }
        }
    }
}
