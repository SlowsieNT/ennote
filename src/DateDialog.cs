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
        int OrigHeight = 0, NoDaysHeight = 154;
        bool UseWeekdays = true;
        public DateDialog(string aTitle)
        {
            InitializeComponent();
            OrigHeight = Height;
            FormClosing += DateDialog_FormClosing;
            label1.Text = aTitle;
            Text = aTitle;
            checkBox1.Text = "Sunday";
            checkBox2.Text = "Monday";
            checkBox3.Text = "Tuesday";
            checkBox4.Text = "Wednesday";
            checkBox5.Text = "Thursday";
            checkBox6.Text = "Friday";
            checkBox7.Text = "Saturday";
            label1.MouseDown += MoveShit;
            panel1.MouseDown += MoveShit;
            ResponseButton = 0;
            DayCheckboxes = new System.Windows.Forms.CheckBox[7] {
                checkBox1, checkBox2, checkBox3, checkBox4,
                checkBox5, checkBox6, checkBox7
            };
            var jrs = Form1.JReminderSettings;
            UseWeekdays = (int)jrs[0] == 1;
            // ui rendering
            for (int i = 0; i < DayCheckboxes.Length; i++) {
                DayCheckboxes[i].Checked = 1 == (int)jrs[i + Form1.JReminderDaysOffset];
            }
            // weekly/every/once
            radioButton1.Checked = 1 == (int)jrs[0];
            radioButton2.Checked = 2 == (int)jrs[0];
            radioButton3.Checked = 3 == (int)jrs[0];
            // hh:mm:ss
            textBox1.Text = int.Parse("" + jrs[0 + Form1.JReminderTimeOffset]).ToString("00");
            textBox2.Text = int.Parse("" + jrs[1 + Form1.JReminderTimeOffset]).ToString("00");
            textBox3.Text = int.Parse("" + jrs[2 + Form1.JReminderTimeOffset]).ToString("00");
            // shake duration
            textBox4.Text = "" + jrs[0 + Form1.JReminderShakeOffset];
            textBox5.Text = "" + jrs[1 + Form1.JReminderShakeOffset];
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
            if (int.TryParse(textBox1.Text, out int x))
                if (x > 23 && UseWeekdays)
                    textBox1.Text = "00";
                else { }
            else textBox1.Text = "00";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int x))
                if (x > 59)
                textBox1.Text = "00";
            else { }
            else textBox1.Text = "00";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int x))
                if (x > 59)
                    textBox1.Text = "00";
                else { }
            else textBox1.Text = "00";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResponseButton = 0;
            var jrs = Form1.JReminderSettings;
            // enabled:
            if (radioButton1.Checked) jrs[0] = 1;
            if (radioButton2.Checked) jrs[0] = 2;
            if (radioButton3.Checked) jrs[0] = 3;
            if (int.TryParse(textBox4.Text.Trim(), out int a))
                jrs[0 + Form1.JReminderShakeOffset] = a;
            if (int.TryParse(textBox5.Text.Trim(), out int b))
                jrs[1 + Form1.JReminderShakeOffset] = b;
            // weekdays:
            // ui reading
            for (int i = 0; i < DayCheckboxes.Length; i++) {
                jrs[i + Form1.JReminderDaysOffset] = DayCheckboxes[i].Checked ? 1 : 0;
            }
            // time:
            if (int.TryParse(textBox1.Text.Trim(), out int x)) jrs[0 + Form1.JReminderTimeOffset] = x;
            if (int.TryParse(textBox2.Text.Trim(), out int y)) jrs[1 + Form1.JReminderTimeOffset] = y;
            if (int.TryParse(textBox3.Text.Trim(), out int z)) jrs[2 + Form1.JReminderTimeOffset] = z;
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ResponseButton = 1;
            Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = DateTime.Now.Hour.ToString("00");
            textBox2.Text = DateTime.Now.Minute.ToString("00");
            textBox3.Text = DateTime.Now.Second.ToString("00");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text = textBox3.Text = "00";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) {
                Form1.JReminderSettings[0] = 1;
                Height = OrigHeight;
                UseWeekdays = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked) {
                Form1.JReminderSettings[0] = 2;
                Height = NoDaysHeight;
                UseWeekdays = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = radioButton2.Checked = radioButton3.Checked = false;
            Form1.JReminderSettings[0] = 0;
            Height = OrigHeight;
            UseWeekdays = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked) {
                Form1.JReminderSettings[0] = 3;
                Height = NoDaysHeight;
                UseWeekdays = false;
            }
        }
    }
}
