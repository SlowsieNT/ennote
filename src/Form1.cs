﻿using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ennote
{
    public partial class Form1 : Form
    {
        string UserDir = "";
        string Password = "";
        string DefaultPassword = "";
        string FileName = "";
        string FileExt = ".rte";
        DateDialog DateDialog1 = new DateDialog("");
        PwDialog PwDialog1 = new PwDialog("", "", "");
        // System.Web.Extensions.dll
        public static System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        public static object[] JNoteSettings = new object[] {
            0,
            0, // on/off [1]
            4, 32, // shake duration [2], range [3]
            0,0,0,0,0,0,0, // days [4...10]
            0,0,0 // time [11...13]
            // [14]
        };
        public static int JNoteAllowThemes = 0;
        public static int JNoteReminderOffset = 1;
        public static int JNoteDaysOffset = 4;
        public static int JNoteTimeOffset = 4 + 7;
        public static int JNoteThemeOffset = 3 + 11;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        #region border
        protected override void OnPaint(PaintEventArgs e) // you can safely omit this method if you want
        {
            e.Graphics.FillRectangle(Brushes.Transparent, RTop);
            e.Graphics.FillRectangle(Brushes.Transparent, RLeft);
            e.Graphics.FillRectangle(Brushes.Transparent, RRight);
            e.Graphics.FillRectangle(Brushes.Transparent, RBottom);
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
        #endregion
        
        long timestamp(bool aMS = false, long aAddVal = 0)
        {
            long epochTicks = new DateTime(1970, 1, 1).Ticks;
            long unixTime = ((DateTime.UtcNow.Ticks - epochTicks) / (aMS ? TimeSpan.TicksPerMillisecond : TimeSpan.TicksPerSecond));
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
        string htimestamp() { return Convert.ToString(timestamp(), 16); }
        string GenerateFilename()
        {
            return "note_" + htimestamp() + FileExt;
        }
        void DeleteNote(bool aNoExit=false)
        {
            try {
                File.Delete(SaveFilename());
                File.Delete(SettingsFilename());
            } catch { }
            if (!aNoExit) Close();
        }
        Random rng = new Random();
        bool PasswordArgSet=false, CanAutoSave=false, swPositionSet=false;
        int swX=0, swY=0;
        NotifyIcon ni = new NotifyIcon();
        bool m_FireReminder = false;
        long m_ReminderEpoch = 0L;
        long m_ReminderLastEpoch = 0L;
        public static Dictionary<string, Form> FormsDict = null;
        public Form1(string[] aArgs)
        {
            // insert form handles
            FormsDict = new Dictionary<string, Form>();
            FormsDict.Add("mf", this);
            FormsDict.Add("pw", PwDialog1);
            FormsDict.Add("dd", DateDialog1);
            // etc
            DefaultPassword = NewPassword();
            Password = DefaultPassword;
            UserDir = Directory.GetCurrentDirectory();
            FileInfo fi = null;
            if (aArgs.Length > 0) {
                fi = new FileInfo(aArgs[0]);
                if (fi.Exists) {
                    FileName = fi.Name;
                    UserDir = fi.Directory.FullName;
                }
                else if (Directory.Exists(aArgs[0])) {
                    FileName = GenerateFilename();
                    UserDir = aArgs[0];
                }
            }
            if (aArgs.Length > 1) {
                Password = aArgs[1];
                PasswordArgSet = true;
            }
            if (aArgs.Length > 2) {
                var wpos = aArgs[2].Split('x');
                if (wpos.Length < 1) return;
                int.TryParse(wpos[0], out swX);
                int.TryParse(wpos[1], out swY);
                swPositionSet = true;
            }
            InitializeComponent();
            ActiveControl = rTextBox1;
            try {
                Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
                ni.Icon = Icon;
            }
            catch { }
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            panel1.MouseDown += MoveOnMouseDown;
            label1.MouseDown += MoveOnMouseDown;
            button1.Click += delegate (object s, EventArgs e) {
                UserDir = new DirectoryInfo(UserDir).FullName;
                var strwPos = (Left + 32) + "x" + (Top + 32);
                Process.Start(Application.ExecutablePath, '"' + UserDir + '"' + " " + Password + " " + strwPos);
            };
            button2.Click += delegate (object s, EventArgs e) { Application.Exit(); };
            rTextBox1.TextChanged += delegate (object s, EventArgs e) {
                SaveNote();
            };
            rTextBox1.AllowDrop = true;
            Move += Form1_Move;
            // etc
            ReadNote(SaveFilename());
            textBox1.KeyDown += delegate (object s, KeyEventArgs e) {
                if (e.KeyCode == Keys.Escape) {
                    textBox1.Hide();
                    return;
                }
                if (e.KeyCode == Keys.Enter) {
                    DeleteNote(true);
                    string nFname = textBox1.Text;
                    if (nFname.ToLower().IndexOf(FileExt) < 0)
                        if (DialogResult.OK == MessageBox.Show("Missing file extension, add it?", "Missing file extension", MessageBoxButtons.OKCancel))
                            nFname += FileExt;
                    FileName = nFname;
                    label1.Text = FileName;
                    SaveNote();
                    textBox1.Hide();
                }
            };
            textBox1.Hide();
            long lastclickdate = 0;
            label1.MouseDown += delegate (object s, MouseEventArgs e) {
                if (lastclickdate > 0 && timestamp(true, -lastclickdate) < 235) {
                    lastclickdate = 0;
                    ShowRename();
                }
                lastclickdate = timestamp(true);
            };
            cms1.ShowImageMargin = false;
            cms2.ShowImageMargin = false;
            cms3.ShowImageMargin = false;
            cms1.Renderer = new ToolStripDarkRenderer();
            cms2.Renderer = new ToolStripDarkRenderer();
            cms3.Renderer = new ToolStripDarkRenderer();
            //etc
            ni.DoubleClick += Ni_DoubleClick;
        }
        int FormX, FormY;
        private void Form1_Move(object sender, EventArgs e) {
            if (!m_FireReminder) {
                FormX = Left;
                FormY = Top;
            } else {
                var deltaX = Math.Abs((FormX) - Left);
                var deltaY = Math.Abs((FormY) - Top);
                var maxDelta = 1 + (int)JNoteSettings[JNoteReminderOffset + 2] * 2;
                if (deltaX > maxDelta || deltaY > maxDelta) {
                    StopReminder();
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e) {
            if (swPositionSet) {
                Location = new Point(swX, swY);
                rTextBox1.Focus();
            }
        }
        void StopReminder() {
            Top = FormY;
            Left = FormX;
            m_FireReminder = false;
            m_ReminderLastEpoch = timestamp();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            int reminderType = (int)JNoteSettings[0 + JNoteReminderOffset],
                shakeDuration = (int)JNoteSettings[1 + JNoteReminderOffset],
                shakeArea = (int)JNoteSettings[2 + JNoteReminderOffset];
            var dn = DateTime.Now;
            if (shakeDuration < 1) shakeDuration = 1;
            if (2 == reminderType || 3 == reminderType) {
                var timeSecs = (int)JNoteSettings[0 + JNoteTimeOffset] * 3600 + (int)JNoteSettings[1 + JNoteTimeOffset] * 60 + (int)JNoteSettings[2 + JNoteTimeOffset];
                if (timeSecs < 1) timeSecs = 1;
                if (0 == m_ReminderLastEpoch) {
                    m_ReminderLastEpoch = timestamp();
                }
                if (!m_FireReminder && timeSecs < timestamp() - m_ReminderLastEpoch) {
                    m_FireReminder = true;
                    m_ReminderEpoch = timestamp();
                }
            } else if (1 == reminderType) {
                var useDayed = false;
                for (var i = 0; i < 7; i++)
                    if ((int)JNoteSettings[i + JNoteDaysOffset] == 1) {
                        useDayed = true;
                        break;
                    }
                // days [1...7]
                // time [8...10]
                if ((int)JNoteSettings[0 + JNoteTimeOffset] == dn.Hour && (int)JNoteSettings[1 + JNoteTimeOffset] == dn.Minute && (int)JNoteSettings[2 + JNoteTimeOffset] == dn.Second) {
                    if (useDayed) {
                        var dow = (int)dn.DayOfWeek;
                        if (1 == (int)JNoteSettings[dow + JNoteDaysOffset]) {
                            m_FireReminder = true;
                            m_ReminderEpoch = timestamp();
                        }
                    } else {
                        m_FireReminder = true;
                        m_ReminderEpoch = timestamp();
                    }
                }
            }
            if (m_FireReminder) {
               // timer1.Enabled = false;
                if (timestamp() - m_ReminderEpoch >= shakeDuration) {
                    StopReminder();
                    if (3 == reminderType)
                        timer1.Enabled = false;
                } else {
                    Show();
                    Activate();
                    Top = FormY + rng.Next(0, shakeArea);
                    Left = FormX + rng.Next(0, shakeArea);
                }
            }
        }
        
        private void Ni_DoubleClick(object sender, EventArgs e)
        {
            Activate();
            Show();
        }

        void ShowRename()
        {
            int fnePos = FileName.LastIndexOf(".");
            textBox1.Text = FileName;
            textBox1.Show();
            textBox1.Focus();
            if (fnePos > 0)
                textBox1.Select(0, fnePos);
        }
        void MoveOnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        string SettingsFilename()
        {
            return SaveFilename() + ".json";
        }
        string SaveFilename()
        {
            return UserDir + "/" + FileName;
        }
        void ReadSettings() {
            try {
                JNoteSettings = jss.Deserialize<object[]>(File.ReadAllText(SettingsFilename()));
                enableThemeEditToolStripMenuItem.Checked = 1 == (int)JNoteSettings[JNoteAllowThemes];
                if (enableThemeEditToolStripMenuItem.Checked) {
                    // divide arrays
                    var xList = new List<object>();
                    var xList2 = new List<object>();
                    for (int i = 0, toLen = JNoteSettings.Length; i < toLen; i++)
                        if (i < JNoteThemeOffset)
                            xList.Add(JNoteSettings[i]);
                        else xList2.Add(JNoteSettings[i]);
                    // import theme
                    Themer.ImportTheme(xList2.ToArray());
                }
            } catch { }
        }
        void SaveSettings(object[] aJTheme = null, bool aNewTheme = false) {
            // divide arrays
            var xList = new List<object>();
            var xList2 = new List<object>();
            for (int i = 0, toLen = JNoteSettings.Length; i < toLen; i++)
                if (i < JNoteThemeOffset)
                    xList.Add(JNoteSettings[i]);
                else xList2.Add(JNoteSettings[i]);
            // handle enable/disable
            if (1 == (int)JNoteSettings[JNoteAllowThemes]) {
                if (null != aJTheme)
                    Themer.ImportTheme(aJTheme);
                else aJTheme = Themer.GetCurrentTheme();
                // (re)allocate to ram
                xList.AddRange(aJTheme);
            }
            JNoteSettings = xList.ToArray();
            try {
                File.WriteAllText(SettingsFilename(), jss.Serialize(JNoteSettings));
            } catch { }
        }
        void SaveNote() {
            if (!CanAutoSave) return;
            var sfn = SaveFilename();
            TrySaveFile(sfn);
            SaveSettings();
        }
        void TrySaveFile(string aFileName, bool aAsPlain=false) {
            try {
                if (aAsPlain)
                    File.WriteAllText(aFileName, rTextBox1.Text);
                else File.WriteAllBytes(aFileName, AES256.EncryptString(rTextBox1.Rtf, Password));
                var fi = new FileInfo(aFileName);
                FileName = fi.Name;
                ni.Text = Text = FileName;
                SaveSettings();
            } catch { }
        }
        void ReadNote(string aFileName) {
            CanAutoSave = false;
            var fi = new FileInfo(aFileName);
            var feFlag = File.Exists(aFileName);
            byte[] ebuffer = new byte[] { };
            if (!PasswordArgSet) {
                var pwBox = new PwDialog("Password / new password:" + (feFlag ? "\r\nFile: " + fi.Name : ""), "Enter note's password", Password);
                pwBox.StartPosition = FormStartPosition.CenterParent;
                pwBox.ShowDialog();
                Password = pwBox.ResponseValue;
            }
            if (fi.Directory.FullName != UserDir)
                UserDir = fi.Directory.FullName;
            if (feFlag) {
                FileName = fi.Name;
                try {
                    ebuffer = File.ReadAllBytes(aFileName);
                } catch {
                    MessageBox.Show("Failed to read file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FileName = GenerateFilename();
                }
                bool readOk = false;
                try {
                    rTextBox1.Rtf = AES256.DecryptBytes(ebuffer, Password);
                    m_ReminderLastEpoch = 0;
                    timer1.Enabled = true;
                    readOk = true;
                } catch {
                    var dr = MessageBox.Show("Failed to decrypt file.\r\nRead as text?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    try {
                        if (dr == DialogResult.Yes) {
                            rTextBox1.Text = File.ReadAllText(aFileName);
                            CanAutoSave = true;
                            var cfn = fi.Name;
                            if (fi.Extension.Length > 0) {
                                cfn = fi.Name.Substring(0, cfn.Length - fi.Extension.Length);
                            }
                            FileName = cfn + "-" + htimestamp() + FileExt;
                            SaveNote();
                        }
                    } catch {
                        MessageBox.Show("Failed to read file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FileName = GenerateFilename();
                    }
                }
                if (readOk)
                    ReadSettings();
            }
            if (FileName == "") FileName = GenerateFilename();
            label1.Text = FileName;
            CanAutoSave = true;
            ni.Text = Text = FileName;
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

        private void backgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cd = new ColorDialog();
            cd.Color = rTextBox1.SelectionBackColor;
            if (cd.ShowDialog() == DialogResult.OK) {
                rTextBox1.SelectionBackColor = cd.Color;
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

        private void button3_Click(object sender, EventArgs e)
        {
            cms3.Show(new Point(button3.Location.X +4 + Location.X, 32 + button3.Location.Y + Location.Y));
        }

        private void openToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = UserDir;
            ofd.Filter = "Encrypted Note|*" + FileExt + "|All Files|*.*";
            var dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
                ReadNote(ofd.FileName);
        }

        private void saveAsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.InitialDirectory = UserDir;
            sfd.Filter = "Encrypted Note|*"+FileExt+ "|Text Documents|*.txt|All Files|*.*";
            var dr = sfd.ShowDialog();
            if (dr == DialogResult.OK) {
                var fi = new FileInfo(sfd.FileName);
                bool asPlain = false;
                if (".txt" == fi.Extension) {
                    asPlain = DialogResult.OK == MessageBox.Show("Save as plain text; not encrypted?", "Save As...", MessageBoxButtons.OKCancel);
                }
                UserDir = fi.Directory.FullName;
                TrySaveFile(sfd.FileName, asPlain);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveAsToolStripMenuItem1_Click(sender, e);
        }

        private void acceptTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rTextBox1.AcceptsTab = !rTextBox1.AcceptsTab;
            acceptTabToolStripMenuItem.Checked = rTextBox1.AcceptsTab;
        }

        private void toggleTaskbarIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowInTaskbar = !ShowInTaskbar;
        }

        private void toggleTrayIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ni.Visible = !ni.Visible;
        }

        private void hideShowTrayIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            ni.Visible = true;
        }

        private void topMostToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;
            topMostToolStripMenuItem1.Checked = TopMost;
        }

        private void setReminderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool aliveTmr1 = timer1.Enabled;
            timer1.Enabled = false;
            var dd = DateDialog1;
            dd.UpdateArgs("Set reminder");
            dd.StartPosition = FormStartPosition.CenterParent;
            dd.ShowDialog();
            if (0 == dd.ResponseButton) {
                SaveNote();
                m_ReminderLastEpoch = 0;
                timer1.Enabled = true;
            }
            if (aliveTmr1)
                timer1.Enabled = true;
        }

        private void exportDefaultThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tied = Themer.GetCurrentTheme();
            File.WriteAllText(UserDir + "/default.theme.json", jss.Serialize(tied));
        }

        private void importThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = UserDir;
            ofd.Filter = "JSON File|*.json|All Files|*.*";
            var dr = ofd.ShowDialog();
            if (dr == DialogResult.OK) {
                var theme = jss.Deserialize<object[]>(File.ReadAllText(ofd.FileName));
                JNoteSettings[JNoteAllowThemes] = 1;
                SaveSettings(theme, true);
                enableThemeEditToolStripMenuItem.Checked = true;
            }
        }

        private void enableThemeEditToolStripMenuItem_Click(object sender, EventArgs e) {
            if (0 == (int)JNoteSettings[JNoteAllowThemes]) {
                JNoteSettings[JNoteAllowThemes] = 1;
            } else {
                JNoteSettings[JNoteAllowThemes] = 0;
                // remove irrelevant
                var xList = new List<object>();
                for (var i = 0; i < JNoteThemeOffset; i++)
                    xList.Add(JNoteSettings[i]);
                JNoteSettings = xList.ToArray();
            }
            enableThemeEditToolStripMenuItem.Checked = 1 == (int)JNoteSettings[JNoteAllowThemes];
            SaveSettings();
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
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
            openToolStripMenuItem2_Click(sender, e);
        }

        private void newPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pwBox = PwDialog1;
            pwBox.UpdateArgs("New Password:", "Enter note's new password", Password);
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
        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowRename();
        }
        
    }
}
