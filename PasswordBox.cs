using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ennote
{
    public partial class PasswordBox : Form
    {
        public string ResponseValue;
        /// <summary>
        /// 0 is OK, 1 is cancel
        /// </summary>
        public int ResponseButton;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public PasswordBox(string aPrompt, string aTitle, string aDefaultValue)
        {
            InitializeComponent();
            label2.Text = aPrompt;
            label1.Text = aTitle;
            textBox1.Text = aDefaultValue;
            ResponseValue = aDefaultValue;
            button4.Click += delegate (object s, EventArgs e) {
                // cancel
                ResponseButton = 1;
                Close();
            };
            button3.Click += delegate (object s, EventArgs e) {
                // ok
                ResponseButton = 0;
                ResponseValue = textBox1.Text;
                Close();
            };
            label1.MouseDown += MoveShit;
            panel1.MouseDown += MoveShit;
        }

        void MoveShit(object s, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void PasswordBox_Load(object sender, EventArgs e)
        {
            textBox1.Select();
            try {
                Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            } catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.UseSystemPasswordChar = !checkBox1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }

    }
}
