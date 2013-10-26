using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace DNote
{
    public partial class textBox1 : Form
    {
        
        public textBox1()
        {
            InitializeComponent();
            FtpPasswordBox.PasswordChar = '*';
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void DNote_Load(object sender, EventArgs e)
        {
            label5.Text = DateTime.Now.ToString("HH:mm:ss tt");
            timer1.Enabled = true;
            RunLocalFromExsistingFile.Checked = false;
            saveFileDialog1.Filter = "Text Files (.txt) | *.txt | HyperText Markup Language (.html) | *.html | Cascading Style Sheet (.css) | *.css | Hypertext Preprocessor (.php) | *.php";
            saveFileDialog1.DefaultExt = "txt";
            textBox2.AcceptsTab = true;
            textBox2.WordWrap = true;
            
            
        }


        private void OpenFilePath_TextChanged(object sender, EventArgs e)
        {

        }

        private void NewFilePathBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void SetFontSizeBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void UploadToFTP_Click(object sender, EventArgs e)
        {
            string OpenFileName = openFileDialog1.FileName;

            try
            {
                Upload(OpenFileName);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "FTP ERROR");
                System.Media.SystemSounds.Beep.Play();
            }
        }

        public void Upload(string fileToUpload)
        {
            try
            {

            string FtpHost = FtpHostBox.Text.ToString();
            string FtpUsername = FtpUsernameBox.Text.ToString();
            string FtpPassword = FtpPasswordBox.Text.ToString();
            FileInfo toUpload = new FileInfo(fileToUpload);
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FtpHost + toUpload.Name);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(FtpUsername, FtpPassword);
            Stream ftpStream = request.GetRequestStream();
            FileStream file = File.OpenRead(fileToUpload);
            int lenght = 1024;
            byte[] buffer = new byte[lenght];
            int bytesRead = 0;
            do{
            
            bytesRead = file.Read(buffer, 0, lenght);
            ftpStream.Write(buffer, 0, bytesRead);


            }

            while(bytesRead != 0);
            MessageBox.Show(toUpload.Name + " has been succesfully been uploaded to " + FtpHost, "FTP");
            file.Close();
            ftpStream.Close();
            System.Media.SystemSounds.Beep.Play();


            }
            catch(Exception ex)
            {
                MessageBox.Show("Well this is wrong: " + ex.ToString(), "Error");
            }
            
        }

        public void Delete(String FileToDelete)
        {
            try {
            string OpenFileName = openFileDialog1.FileName;
            string FtpHost = FtpHostBox.Text.ToString();
            string FtpUsername = FtpUsernameBox.Text.ToString();
            string FtpPassword = FtpPasswordBox.Text.ToString();
            FileInfo toUpload = new FileInfo(FileToDelete);
            string fileName = OpenFileName;
            FtpWebRequest requestFileDelete = (FtpWebRequest)WebRequest.Create(FtpHost + toUpload.Name);
            requestFileDelete.Credentials = new NetworkCredential(FtpUsername, FtpPassword);
            requestFileDelete.Method = WebRequestMethods.Ftp.DeleteFile;
            
            FtpWebResponse responseFileDelete = (FtpWebResponse)requestFileDelete.GetResponse();

            MessageBox.Show(toUpload.Name + " has successfully been deleted from " + FtpHostBox.Text.ToString(), "FTP");
            System.Media.SystemSounds.Beep.Play();

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error");
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void OpenOnlineButton_Click(object sender, EventArgs e) {

            /*

                

                string OpenOnlineString = new WebClient().DownloadString(OpenOnlineFileURL);

                textBox2.Text = OpenOnlineString;
              
                */
                try {
                string OpenOnlineFileURL = OpenOnlineBox.Text;

                textBox2.Text = ReadTextFromUrl(OpenOnlineFileURL);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error: " + ex.ToString(), "Error");
                }
        
        }

            string ReadTextFromUrl(string url) {
            // WebClient is still convenient
            // Assume UTF8, but detect BOM - could also honor response charset I suppose
            using (var client = new WebClient())
            using (var stream = client.OpenRead(url))
            using (var textReader = new StreamReader(stream, Encoding.UTF8, true)) {
            return textReader.ReadToEnd();
            }
            }

            private void DeleteFromFTP_Click(object sender, EventArgs e)
            {
            string OpenFileName = openFileDialog1.FileName;
            Delete(OpenFileName);

            }

            public void RunLocalCodeFileInfo(String RunLocalCodeFileInfo)
            {
            FileInfo toRunLocal = new FileInfo(RunLocalCodeFileInfo);
            string TestFilePath = @"C:\Users\Anrijs\Documents\DNote\Data" + toRunLocal;
            }

            private void RunLocalCodeInBrowser_Click(object sender, EventArgs e)
            {

                string OpenFileName = openFileDialog1.FileName;

                string path = @"C:\Users\Anrijs\Documents\DNote_Data";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string RunLocalURL = @"C:\Users\Anrijs\Documents\DNote_Data\test.html";
                if(RunLocalFromExsistingFile.Checked == true) 
                {

                    webBrowser1.Navigate(OpenFileName);

                }
                else
                {
                    webBrowser1.Navigate(RunLocalURL);
                }


                string NewFilePath = @"C:\Users\Anrijs\Documents\DNote_Data\test.html";

                try
                {

                    if (File.Exists(NewFilePath))
                    {
                        File.Delete(NewFilePath);
                    }
                    using (StreamWriter sw = File.CreateText(NewFilePath))
                    {
                        sw.WriteLine(textBox2.Text);
                    }

                }
                catch (Exception ex)
                {

                    System.Media.SystemSounds.Beep.Play();
                    MessageBox.Show("Well, this is bad: " + ex.ToString(), "Error");

                }
               

            }

            private void RunOnlineCodeInBrowser_Click(object sender, EventArgs e)
            {

            webBrowser1.Navigate(OpenOnlineBox.Text.ToString());

            }


            private void label5_Click(object sender, EventArgs e)
            {
                
            }

            private void timer1_Tick(object sender, EventArgs e)
            {
                
            }

            private void RunLocalFromExsistingFile_CheckedChanged(object sender, EventArgs e)
            {

            }

            private void OpenFilePath_TextChanged_1(object sender, EventArgs e)
            {

            }

            private void rebToolStripMenuItem_Click(object sender, EventArgs e)
            {
                textBox2.ForeColor = Color.Red;
            }

            private void blueToolStripMenuItem_Click(object sender, EventArgs e)
            {
                textBox2.ForeColor = Color.Blue;
            }

            private void whiteToolStripMenuItem_Click(object sender, EventArgs e)
            {
                textBox2.ForeColor = Color.White;
            }

            private void openToolStripMenuItem_Click(object sender, EventArgs e)
            {
                openFileDialog1.ShowDialog();
            }

            private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
            {
                string OpenFileName = openFileDialog1.FileName;
                string File = System.IO.File.ReadAllText(OpenFileName);
                textBox2.Text = File;
                

            }

            private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
            {
                string SaveFileName = saveFileDialog1.FileName;

                try
                {

                    if (File.Exists(SaveFileName))
                    {
                        File.Delete(SaveFileName);
                    }
                    using (StreamWriter sw = File.CreateText(SaveFileName))
                    {
                        sw.WriteLine(textBox2.Text);
                    }

                }
                catch (Exception ex)
                {

                    System.Media.SystemSounds.Beep.Play();
                    MessageBox.Show("Well, this is bad: " + ex.ToString(), "Error");

                }
            }

            private void saveToolStripMenuItem_Click(object sender, EventArgs e)
            {
                saveFileDialog1.ShowDialog();
            }

            private void toolStripMenuItem2_Click(object sender, EventArgs e)
            {
                textBox2.Font = new Font(textBox2.Font.FontFamily, 12);
            }

            private void toolStripMenuItem3_Click(object sender, EventArgs e)
            {
                textBox2.Font = new Font(textBox2.Font.FontFamily, 14);
            }

            private void toolStripMenuItem4_Click(object sender, EventArgs e)
            {
                textBox2.Font = new Font(textBox2.Font.FontFamily, 16);
            }

            private void toolStripMenuItem5_Click(object sender, EventArgs e)
            {
                textBox2.Font = new Font(textBox2.Font.FontFamily, 18);
            }

            private void toolStripMenuItem6_Click(object sender, EventArgs e)
            {
                textBox2.Font = new Font(textBox2.Font.FontFamily, 20);
            }

            private void toolStripMenuItem7_Click(object sender, EventArgs e)
            {
                textBox2.Font = new Font(textBox2.Font.FontFamily, 22);
            }

            private void toolStripMenuItem8_Click(object sender, EventArgs e)
            {
                textBox2.Font = new Font(textBox2.Font.FontFamily, 24);
            }

            private void toolStripMenuItem9_Click(object sender, EventArgs e)
            {
                textBox2.Font = new Font(textBox2.Font.FontFamily, 26);
            }

            private void toolStripMenuItem10_Click(object sender, EventArgs e)
            {
                textBox2.Font = new Font(textBox2.Font.FontFamily, 28);
            }

            private void toolStripMenuItem11_Click(object sender, EventArgs e)
            {
                textBox2.Font = new Font(textBox2.Font.FontFamily, 30);
            }

            private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
            {
                MessageBox.Show("DNote created by Desmaize. DNote is made for text editing.", "About");
            }

            private void textBox2_TextChanged(object sender, EventArgs e)
            {

            }

            private void exitToolStripMenuItem_Click(object sender, EventArgs e)
            {
                this.Close();
            }

            

        }
    }
