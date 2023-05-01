using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ennote
{
    public partial class PwDialog : Form
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
        public void UpdateArgs(string aPrompt, string aTitle, string aDefaultValue)
        {
            LCtx.Text = aPrompt;
            LText.Text = aTitle;
            tbPwd.Text = aDefaultValue;
        }
        public PwDialog(string aPrompt, string aTitle, string aDefaultValue)
        {
            InitializeComponent();
            UpdateArgs(aPrompt, aTitle, aDefaultValue);
            ResponseValue = aDefaultValue;
            btnCancel.Click += delegate (object s, EventArgs e) {
                // cancel
                ResponseButton = 1;
                Close();
            };
            btnOK.Click += delegate (object s, EventArgs e) {
                // ok
                ResponseButton = 0;
                ResponseValue = tbPwd.Text;
                Close();
            };
            LText.MouseDown += MoveShit;
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
            Text = "Enter Password";
            tbPwd.Select();
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
            tbPwd.UseSystemPasswordChar = !cbSPwd.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbPwd.Text);
        }

    }
}
