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
        int OrigHeight = 0;
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
            // ui rendering
            for (int i = 0; i < DayCheckboxes.Length; i++) {
                DayCheckboxes[i].Checked = 1 == (int)jrs[1 + i];
            }
            // every/days
            checkBox8.Checked = 1 == (int)jrs[0];
            checkBox9.Checked = 2 == (int)jrs[0];
            // etc
            textBox1.Text = int.Parse("" + jrs[8]).ToString("00");
            textBox2.Text = int.Parse("" + jrs[9]).ToString("00");
            textBox3.Text = int.Parse("" + jrs[10]).ToString("00");
            // time:
            if (int.TryParse(textBox1.Text.Trim(), out int x)) jrs[8] = x;
            if (int.TryParse(textBox2.Text.Trim(), out int y)) jrs[9] = y;
            if (int.TryParse(textBox3.Text.Trim(), out int z)) jrs[10] = z;
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
            //textBox1.Select();
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
                if (x > 23 && !checkBox9.Checked)
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
            if (checkBox8.Checked) jrs[0] = 1;
            if (checkBox9.Checked) jrs[0] = 2;
            // weekdays:
            jrs[1] = checkBox1.Checked ? 1 : 0;
            jrs[2] = checkBox2.Checked ? 1 : 0;
            jrs[3] = checkBox3.Checked ? 1 : 0;
            jrs[4] = checkBox4.Checked ? 1 : 0;
            jrs[5] = checkBox5.Checked ? 1 : 0;
            jrs[6] = checkBox6.Checked ? 1 : 0;
            jrs[7] = checkBox7.Checked ? 1 : 0;
            // time:
            if (int.TryParse(textBox1.Text.Trim(), out int x)) jrs[8] = x;
            if (int.TryParse(textBox2.Text.Trim(), out int y)) jrs[9] = y;
            if (int.TryParse(textBox3.Text.Trim(), out int z)) jrs[10] = z;
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

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            checkBox8.Checked = false;
            foreach (var cb in DayCheckboxes)
                if (checkBox9.Checked) cb.Hide(); else cb.Show();
            if (checkBox9.Checked)
                Height = 128;
            else Height = OrigHeight;
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            checkBox9.Checked = false;
            foreach (var cb in DayCheckboxes)
                if (checkBox9.Checked) cb.Hide(); else cb.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text = textBox3.Text = "00";
        }
    }
}
