using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Windows.Forms;

namespace ennote
{
    public partial class Form1 : Form
    {
        string UserDir = "";
        string Password = "";
        string DefaultPassword = "";
        string FileName = "";
        string FileExt = ".rte";
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        long timestamp(long aAddVal = 0)
        {
            long epochTicks = new DateTime(1970, 1, 1).Ticks;
            long unixTime = ((DateTime.UtcNow.Ticks - epochTicks) / TimeSpan.TicksPerSecond);
            return unixTime + aAddVal;
        }
        string NewPassword()
        {
            var tmpItems = new List<string>();
            for (int i = 0, nLen = rng.Next(3, 4); i < nLen; i++)
                tmpItems.Add(string.Format("{0:X4}", rng.Next(0xffff)));
            tmpItems.Add(string.Format("{0:X3}", rng.Next(0xfff)));
            return Strings.Join(tmpItems.ToArray(),"--");
        }
        string GenerateFilename()
        {
            return "note_" +
                Convert.ToString(timestamp(), 16) + FileExt;
        }
        void DeleteNote(bool aNoExit=false)
        {
            try {
                File.Delete(SaveFilename());
            } catch { }
            if (!aNoExit) Close();
        }
        Random rng = new Random();
        bool PasswordArgSet=false;
        public Form1(string[] aArgs)
        {
            //MessageBox.Show("pass1");
            DefaultPassword = NewPassword();
            Password = DefaultPassword;
            if (aArgs.Length > 0)
            {
                var fi = new FileInfo(aArgs[0]);
                FileName = fi.Name;
            }
            if (Directory.Exists(FileName)) {
                UserDir = FileName;
                FileName = GenerateFilename();
            } else {
                UserDir = Directory.GetCurrentDirectory();
            }
            if (aArgs.Length > 1) {
                PasswordArgSet = true;
                Password = aArgs[1];
            }
            InitializeComponent();
            try { Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath); }
            catch { }
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            panel1.MouseDown += MoveShit;
            label1.MouseDown += MoveShit;
            button1.Click += delegate (object s, EventArgs e) {
                Process.Start(Application.ExecutablePath, '"' + UserDir + '"' + " " + Password);
            };
            button2.Click += delegate (object s, EventArgs e) { Application.Exit(); };
            rTextBox1.TextChanged += delegate (object s, EventArgs e) {
                SaveNote();
            };
            rTextBox1.AllowDrop = true;
            // etc
            ReadNote(SaveFilename());
            toolStripTextBox1.TextChanged += delegate (object s, EventArgs e) {
                DeleteNote(true);
                FileName = toolStripTextBox1.Text;
                label1.Text = FileName;
                SaveNote();
            };
        }
        void MoveShit(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        string SaveFilename()
        {
            return UserDir + "/" + FileName;
        }
        void SaveNote()
        {
            var sfn = SaveFilename();
            TrySaveFile(sfn);
        }
        void TrySaveFile(string aFileName) {
            try
            {
                File.WriteAllBytes(aFileName, AES256.EncryptString(rTextBox1.Rtf, Password));
                var fi = new FileInfo(aFileName);
                FileName = fi.Name;
            } catch { }
        }
        void ReadNote(string aFileName) {
            var fi = new FileInfo(aFileName);
            var feFlag = File.Exists(aFileName);
            byte[] ebuffer = new byte[] { };
            if (!PasswordArgSet) {
                var pwBox = new PasswordBox("Password / new password:" + (feFlag ? "\r\nFile: " + fi.Name : ""), "Enter note's password", Password);
                pwBox.StartPosition = FormStartPosition.CenterParent;
                pwBox.ShowDialog();
                Password = pwBox.ResponseValue;
            }
            if (feFlag) {
                try {
                    ebuffer = File.ReadAllBytes(aFileName);
                } catch {
                    MessageBox.Show("Failed to read file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FileName = GenerateFilename();
                }
                try {
                    rTextBox1.Rtf = AES256.DecryptBytes(ebuffer, Password);
                } catch {
                    MessageBox.Show("Failed to decrypt file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FileName = GenerateFilename();
                }
            }
            if (FileName == "") FileName = GenerateFilename();
            toolStripTextBox1.Text = FileName;
            label1.Text = FileName;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // etc
            ReadNote(SaveFilename());
        }
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rTextBox1.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rTextBox1.Redo();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rTextBox1.Paste();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rTextBox1.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rTextBox1.Copy();
        }

        private void topMostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;
            topMostToolStripMenuItem.Checked = TopMost;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = UserDir;
            ofd.Filter = "Encrypted Note|*" + FileExt + "|All Files|*.*";
            var dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
                ReadNote(ofd.FileName);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.InitialDirectory = UserDir;
            sfd.Filter = "Encrypted Note|*"+FileExt+"|All Files|*.*";
            var dr = sfd.ShowDialog();
            if (dr == DialogResult.OK) {
                var fi = new FileInfo(sfd.FileName);
                UserDir = fi.Directory.FullName;
                TrySaveFile(sfd.FileName);
            }
        }
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fd = new FontDialog();
            fd.Font = rTextBox1.SelectionFont;
            if (fd.ShowDialog() == DialogResult.OK) {
                rTextBox1.SelectionFont = fd.Font;
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cd = new ColorDialog();
            cd.Color = rTextBox1.SelectionColor;
            if (cd.ShowDialog() == DialogResult.OK) {
                rTextBox1.SelectionColor = cd.Color;
            }
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rTextBox1.WordWrap = !rTextBox1.WordWrap;
            wordWrapToolStripMenuItem.Checked = rTextBox1.WordWrap;
        }

        private void hideShowTaskbarIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowInTaskbar = !ShowInTaskbar;

        }
        void ToggleFontStyle(FontStyle aToggleStyle)
        {
            var sf = rTextBox1.SelectionFont;
            FontStyle fs = 0 != (sf.Style & aToggleStyle) ? sf.Style & ~aToggleStyle : sf.Style | aToggleStyle;
            var f = new Font(sf, fs);
            rTextBox1.SelectionFont = f;
        }
        private void boldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Bold);
        }

        private void italicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Italic);
        }

        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Underline);
        }

        private void strokeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Strikeout);
        }

        private void setCurrentDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new FolderBrowserDialog();
            ofd.SelectedPath = UserDir;
            if (ofd.ShowDialog() == DialogResult.OK)
                UserDir = ofd.SelectedPath;
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var mea = (MouseEventArgs)e;
            contextMenuStrip3.Show(new Point(button3.Location.X + Location.X, 32 + button3.Location.Y + Location.Y));
        }

        private void newPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pwBox = new PasswordBox("New Password:", "Enter note's new password", Password);
            pwBox.ShowDialog();
            if (0 == pwBox.ResponseButton)
                Password = pwBox.ResponseValue;
            SaveNote();
        }

        private void deleteNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteNote();
        }
        private void registerFileExtensionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            utils.RegisterFileAssoc();
        }

        private void openContainingFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var eep = Environment.ExpandEnvironmentVariables("%SystemRoot%\\explorer.exe");
            Process.Start(eep, '"' + UserDir + '"');
        }

        protected override void OnPaint(PaintEventArgs e) // you can safely omit this method if you want
        {
            e.Graphics.FillRectangle(Brushes.DimGray, RTop);
            e.Graphics.FillRectangle(Brushes.DimGray, RLeft);
            e.Graphics.FillRectangle(Brushes.DimGray, RRight);
            e.Graphics.FillRectangle(Brushes.DimGray, RBottom);
        }

        private const int
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17;

        const int RBSize = 4; // you can rename this variable if you like

        Rectangle RTop { get { return new Rectangle(0, 0, Width, RBSize); } }
        Rectangle RLeft { get { return new Rectangle(0, 0, RBSize, Height); } }
        Rectangle RBottom { get { return new Rectangle(0, Height - RBSize, Width, RBSize); } }
        Rectangle RRight { get { return new Rectangle(Width - RBSize, 0, RBSize, Height); } }
        Rectangle TopLeft { get { return new Rectangle(0, 0, RBSize, RBSize); } }
        Rectangle TopRight { get { return new Rectangle(Width - RBSize, 0, RBSize, RBSize); } }
        Rectangle BottomLeft { get { return new Rectangle(0, Height - RBSize, RBSize, RBSize); } }
        Rectangle BottomRight { get { return new Rectangle(Width - RBSize, Height - RBSize, RBSize, RBSize); } }


        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == 0x84) // WM_NCHITTEST
            {
                var cursor = this.PointToClient(Cursor.Position);

                if (TopLeft.Contains(cursor)) message.Result = (IntPtr)HTTOPLEFT;
                else if (TopRight.Contains(cursor)) message.Result = (IntPtr)HTTOPRIGHT;
                else if (BottomLeft.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMLEFT;
                else if (BottomRight.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMRIGHT;
                else if (RTop.Contains(cursor)) message.Result = (IntPtr)HTTOP;
                else if (RLeft.Contains(cursor)) message.Result = (IntPtr)HTLEFT;
                else if (RRight.Contains(cursor)) message.Result = (IntPtr)HTRIGHT;
                else if (RBottom.Contains(cursor)) message.Result = (IntPtr)HTBOTTOM;
            }
        }
    }
}