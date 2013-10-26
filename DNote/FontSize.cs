using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DNote
{
    public partial class FontSize : Form
    {
        public textBox1 MainWindow;

        public FontSize(textBox1 mainwindow)
        {
            InitializeComponent();
            this.MainWindow = mainwindow;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

            MainWindow.textBox2.Font = new Font(MainWindow.textBox2.Font.FontFamily, Convert.ToInt32(textBox1.Text));
            this.Close();

            }
            catch(Exception ex)
            {
                System.Media.SystemSounds.Beep.Play();
                MessageBox.Show("Error: " + ex.ToString());

            }
        }
    }
}
