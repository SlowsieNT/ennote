using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace ennote
{
    public partial class DateDialog : Form
    {
        public string ResponseValue;
        /// <summary>
        /// 0 is OK, 1 is cancel
        /// </summary>
        public int ResponseButton=-1;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public System.Windows.Forms.CheckBox[] DayCheckboxes = null;
        int OrigHeight = 225, NoDaysHeight = 154;
        bool UseWeekdays = true;
        
        public DateDialog(string aTitle)
        {
            InitializeComponent();
            UpdateArgs(aTitle,true);
            FormClosing += DateDialog_FormClosing;
            LText.MouseDown += MoveShit;
            panel1.MouseDown += MoveShit;
            cbDay1.Text = "Sunday";
            cbDay2.Text = "Monday";
            cbDay3.Text = "Tuesday";
            cbDay4.Text = "Wednesday";
            cbDay5.Text = "Thursday";
            cbDay6.Text = "Friday";
            cbDay7.Text = "Saturday";
            DayCheckboxes = new System.Windows.Forms.CheckBox[7] {
                cbDay1, cbDay2, cbDay3, cbDay4,
                cbDay5, cbDay6, cbDay7
            };
        }
        public void UpdateArgs(string aTitle, bool aCtor=false) {
            LText.Text = aTitle;
            Text = aTitle;
            if (aCtor) return;
            ResponseButton = 0;
            var jns = Form1.JNoteSettings;
            UseWeekdays = (int)jns[0 + Form1.JNoteReminderOffset] == 1;
            // ui rendering
            for (int i = 0; i < DayCheckboxes.Length; i++) {
                DayCheckboxes[i].Checked = 1 == (int)jns[i + Form1.JNoteDaysOffset];
            }
            // weekly/every/once
            rbDaily.Checked = 1 == (int)jns[0 + Form1.JNoteReminderOffset];
            rbTimeX.Checked = 2 == (int)jns[0 + Form1.JNoteReminderOffset];
            rbTime1.Checked = 3 == (int)jns[0 + Form1.JNoteReminderOffset];
            // hh:mm:ss
            tbHH.Text = int.Parse("" + jns[0 + Form1.JNoteTimeOffset]).ToString("00");
            tbMM.Text = int.Parse("" + jns[1 + Form1.JNoteTimeOffset]).ToString("00");
            tbSS.Text = int.Parse("" + jns[2 + Form1.JNoteTimeOffset]).ToString("00");
            // shake duration
            tbDur.Text = "" + jns[1 + Form1.JNoteReminderOffset];
            tbArea.Text = "" + jns[2 + Form1.JNoteReminderOffset];
        }
        private void DateDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (-1 == ResponseButton)
                ResponseButton = 1;
        }

        void MoveShit(object s, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void DateDialog_Load(object sender, EventArgs e)
        {
            try {
                Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            } catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tbHH.Text, out int x))
                if (x > 23 && UseWeekdays)
                    tbHH.Text = "00";
                else { }
            else tbHH.Text = "00";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tbHH.Text, out int x))
                if (x > 59)
                tbHH.Text = "00";
            else { }
            else tbHH.Text = "00";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tbHH.Text, out int x))
                if (x > 59)
                    tbHH.Text = "00";
                else { }
            else tbHH.Text = "00";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResponseButton = 0;
            var jns = Form1.JNoteSettings;
            // enabled:
            if (rbDaily.Checked) jns[0 + Form1.JNoteReminderOffset] = 1;
            if (rbTimeX.Checked) jns[0 + Form1.JNoteReminderOffset] = 2;
            if (rbTime1.Checked) jns[0 + Form1.JNoteReminderOffset] = 3;
            if (int.TryParse(tbDur.Text.Trim(), out int a))
                jns[1 + Form1.JNoteReminderOffset] = a;
            if (int.TryParse(tbArea.Text.Trim(), out int b))
                jns[2 + Form1.JNoteReminderOffset] = b;
            // weekdays:
            // ui reading
            for (int i = 0; i < DayCheckboxes.Length; i++) {
                jns[i + Form1.JNoteDaysOffset] = DayCheckboxes[i].Checked ? 1 : 0;
            }
            // time:
            if (int.TryParse(tbHH.Text.Trim(), out int x)) jns[0 + Form1.JNoteTimeOffset] = x;
            if (int.TryParse(tbMM.Text.Trim(), out int y)) jns[1 + Form1.JNoteTimeOffset] = y;
            if (int.TryParse(tbSS.Text.Trim(), out int z)) jns[2 + Form1.JNoteTimeOffset] = z;
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ResponseButton = 1;
            Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            tbHH.Text = DateTime.Now.Hour.ToString("00");
            tbMM.Text = DateTime.Now.Minute.ToString("00");
            tbSS.Text = DateTime.Now.Second.ToString("00");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tbHH.Text = tbMM.Text = tbSS.Text = "00";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDaily.Checked) {
                Form1.JNoteSettings[0 + Form1.JNoteReminderOffset] = 1;
                Height = OrigHeight;
                UseWeekdays = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTimeX.Checked) {
                Form1.JNoteSettings[0 + Form1.JNoteReminderOffset] = 2;
                Height = NoDaysHeight;
                UseWeekdays = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            rbDaily.Checked = rbTimeX.Checked = rbTime1.Checked = false;
            Form1.JNoteSettings[0 + Form1.JNoteReminderOffset] = 0;
            Height = OrigHeight;
            UseWeekdays = true;
            ResponseButton = 0;
            Close();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTime1.Checked) {
                Form1.JNoteSettings[0 + Form1.JNoteReminderOffset] = 3;
                Height = NoDaysHeight;
                UseWeekdays = false;
            }
        }
    }
}
