namespace ennote
{
    partial class DateDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.LText = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tbMM = new System.Windows.Forms.TextBox();
            this.tbHH = new System.Windows.Forms.TextBox();
            this.tbSS = new System.Windows.Forms.TextBox();
            this.cbDay1 = new System.Windows.Forms.CheckBox();
            this.cbDay2 = new System.Windows.Forms.CheckBox();
            this.cbDay3 = new System.Windows.Forms.CheckBox();
            this.cbDay4 = new System.Windows.Forms.CheckBox();
            this.cbDay5 = new System.Windows.Forms.CheckBox();
            this.cbDay6 = new System.Windows.Forms.CheckBox();
            this.cbDay7 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNow = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClr = new System.Windows.Forms.Button();
            this.rbDaily = new System.Windows.Forms.RadioButton();
            this.rbTimeX = new System.Windows.Forms.RadioButton();
            this.rbTime1 = new System.Windows.Forms.RadioButton();
            this.tbDur = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbArea = new System.Windows.Forms.TextBox();
            this.btnDisable = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.panel1.Controls.Add(this.LText);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(360, 32);
            this.panel1.TabIndex = 2;
            // 
            // LText
            // 
            this.LText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.LText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LText.Location = new System.Drawing.Point(3, 9);
            this.LText.Name = "LText";
            this.LText.Size = new System.Drawing.Size(320, 23);
            this.LText.TabIndex = 2;
            this.LText.Text = "label1";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button2.Location = new System.Drawing.Point(329, 1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(30, 30);
            this.button2.TabIndex = 1;
            this.button2.Text = "x";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnOk.Location = new System.Drawing.Point(290, 47);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(58, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.button3_Click);
            // 
            // tbMM
            // 
            this.tbMM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMM.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMM.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tbMM.Location = new System.Drawing.Point(51, 118);
            this.tbMM.MaxLength = 2;
            this.tbMM.Name = "tbMM";
            this.tbMM.Size = new System.Drawing.Size(30, 26);
            this.tbMM.TabIndex = 9;
            this.tbMM.Text = "00";
            this.tbMM.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // tbHH
            // 
            this.tbHH.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbHH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbHH.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbHH.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tbHH.Location = new System.Drawing.Point(15, 118);
            this.tbHH.MaxLength = 2;
            this.tbHH.Name = "tbHH";
            this.tbHH.Size = new System.Drawing.Size(30, 26);
            this.tbHH.TabIndex = 10;
            this.tbHH.Text = "00";
            this.tbHH.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // tbSS
            // 
            this.tbSS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbSS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSS.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSS.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tbSS.Location = new System.Drawing.Point(86, 118);
            this.tbSS.MaxLength = 2;
            this.tbSS.Name = "tbSS";
            this.tbSS.Size = new System.Drawing.Size(30, 26);
            this.tbSS.TabIndex = 11;
            this.tbSS.Text = "00";
            this.tbSS.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // cbDay1
            // 
            this.cbDay1.AutoSize = true;
            this.cbDay1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDay1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbDay1.Location = new System.Drawing.Point(15, 152);
            this.cbDay1.Name = "cbDay1";
            this.cbDay1.Size = new System.Drawing.Size(106, 24);
            this.cbDay1.TabIndex = 12;
            this.cbDay1.Text = "checkBox1";
            this.cbDay1.UseVisualStyleBackColor = true;
            // 
            // cbDay2
            // 
            this.cbDay2.AutoSize = true;
            this.cbDay2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDay2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbDay2.Location = new System.Drawing.Point(127, 152);
            this.cbDay2.Name = "cbDay2";
            this.cbDay2.Size = new System.Drawing.Size(106, 24);
            this.cbDay2.TabIndex = 13;
            this.cbDay2.Text = "checkBox2";
            this.cbDay2.UseVisualStyleBackColor = true;
            // 
            // cbDay3
            // 
            this.cbDay3.AutoSize = true;
            this.cbDay3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDay3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbDay3.Location = new System.Drawing.Point(242, 152);
            this.cbDay3.Name = "cbDay3";
            this.cbDay3.Size = new System.Drawing.Size(106, 24);
            this.cbDay3.TabIndex = 14;
            this.cbDay3.Text = "checkBox3";
            this.cbDay3.UseVisualStyleBackColor = true;
            // 
            // cbDay4
            // 
            this.cbDay4.AutoSize = true;
            this.cbDay4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDay4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbDay4.Location = new System.Drawing.Point(15, 173);
            this.cbDay4.Name = "cbDay4";
            this.cbDay4.Size = new System.Drawing.Size(106, 24);
            this.cbDay4.TabIndex = 15;
            this.cbDay4.Text = "checkBox4";
            this.cbDay4.UseVisualStyleBackColor = true;
            // 
            // cbDay5
            // 
            this.cbDay5.AutoSize = true;
            this.cbDay5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDay5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbDay5.Location = new System.Drawing.Point(127, 173);
            this.cbDay5.Name = "cbDay5";
            this.cbDay5.Size = new System.Drawing.Size(106, 24);
            this.cbDay5.TabIndex = 16;
            this.cbDay5.Text = "checkBox5";
            this.cbDay5.UseVisualStyleBackColor = true;
            // 
            // cbDay6
            // 
            this.cbDay6.AutoSize = true;
            this.cbDay6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDay6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbDay6.Location = new System.Drawing.Point(242, 173);
            this.cbDay6.Name = "cbDay6";
            this.cbDay6.Size = new System.Drawing.Size(106, 24);
            this.cbDay6.TabIndex = 17;
            this.cbDay6.Text = "checkBox6";
            this.cbDay6.UseVisualStyleBackColor = true;
            // 
            // cbDay7
            // 
            this.cbDay7.AutoSize = true;
            this.cbDay7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDay7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbDay7.Location = new System.Drawing.Point(15, 194);
            this.cbDay7.Name = "cbDay7";
            this.cbDay7.Size = new System.Drawing.Size(106, 24);
            this.cbDay7.TabIndex = 18;
            this.cbDay7.Text = "checkBox7";
            this.cbDay7.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Location = new System.Drawing.Point(12, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 16);
            this.label2.TabIndex = 19;
            this.label2.Text = "Time (24h format):";
            // 
            // btnNow
            // 
            this.btnNow.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnNow.Location = new System.Drawing.Point(122, 120);
            this.btnNow.Name = "btnNow";
            this.btnNow.Size = new System.Drawing.Size(43, 23);
            this.btnNow.TabIndex = 21;
            this.btnNow.Text = "Now";
            this.btnNow.UseVisualStyleBackColor = true;
            this.btnNow.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCancel.Location = new System.Drawing.Point(290, 73);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(58, 23);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnClr
            // 
            this.btnClr.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClr.Location = new System.Drawing.Point(171, 120);
            this.btnClr.Name = "btnClr";
            this.btnClr.Size = new System.Drawing.Size(48, 23);
            this.btnClr.TabIndex = 24;
            this.btnClr.Text = "Clear";
            this.btnClr.UseVisualStyleBackColor = true;
            this.btnClr.Click += new System.EventHandler(this.button5_Click);
            // 
            // rbDaily
            // 
            this.rbDaily.AccessibleName = "repeattype";
            this.rbDaily.AutoSize = true;
            this.rbDaily.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDaily.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbDaily.Location = new System.Drawing.Point(15, 47);
            this.rbDaily.Name = "rbDaily";
            this.rbDaily.Size = new System.Drawing.Size(56, 20);
            this.rbDaily.TabIndex = 26;
            this.rbDaily.TabStop = true;
            this.rbDaily.Text = "Daily";
            this.rbDaily.UseVisualStyleBackColor = true;
            this.rbDaily.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // rbTimeX
            // 
            this.rbTimeX.AccessibleName = "repeattype";
            this.rbTimeX.AutoSize = true;
            this.rbTimeX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbTimeX.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbTimeX.Location = new System.Drawing.Point(77, 47);
            this.rbTimeX.Name = "rbTimeX";
            this.rbTimeX.Size = new System.Drawing.Size(81, 20);
            this.rbTimeX.TabIndex = 27;
            this.rbTimeX.TabStop = true;
            this.rbTimeX.Text = "hh:mm:ss";
            this.rbTimeX.UseVisualStyleBackColor = true;
            this.rbTimeX.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // rbTime1
            // 
            this.rbTime1.AccessibleName = "repeattype";
            this.rbTime1.AutoSize = true;
            this.rbTime1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbTime1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbTime1.Location = new System.Drawing.Point(164, 47);
            this.rbTime1.Name = "rbTime1";
            this.rbTime1.Size = new System.Drawing.Size(116, 20);
            this.rbTime1.TabIndex = 28;
            this.rbTime1.TabStop = true;
            this.rbTime1.Text = "Once hh:mm:ss";
            this.rbTime1.UseVisualStyleBackColor = true;
            this.rbTime1.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // tbDur
            // 
            this.tbDur.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbDur.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDur.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDur.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tbDur.Location = new System.Drawing.Point(165, 72);
            this.tbDur.MaxLength = 9;
            this.tbDur.Name = "tbDur";
            this.tbDur.Size = new System.Drawing.Size(58, 26);
            this.tbDur.TabIndex = 29;
            this.tbDur.Text = "5";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label3.Location = new System.Drawing.Point(12, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 16);
            this.label3.TabIndex = 30;
            this.label3.Text = "Shake duration && range:";
            // 
            // tbArea
            // 
            this.tbArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbArea.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tbArea.Location = new System.Drawing.Point(227, 72);
            this.tbArea.MaxLength = 4;
            this.tbArea.Name = "tbArea";
            this.tbArea.Size = new System.Drawing.Size(42, 26);
            this.tbArea.TabIndex = 31;
            this.tbArea.Text = "16";
            // 
            // btnDisable
            // 
            this.btnDisable.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDisable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDisable.Location = new System.Drawing.Point(290, 99);
            this.btnDisable.Name = "btnDisable";
            this.btnDisable.Size = new System.Drawing.Size(58, 23);
            this.btnDisable.TabIndex = 32;
            this.btnDisable.Text = "Disable";
            this.btnDisable.UseVisualStyleBackColor = true;
            this.btnDisable.Click += new System.EventHandler(this.button6_Click);
            // 
            // DateDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(360, 225);
            this.Controls.Add(this.btnDisable);
            this.Controls.Add(this.tbArea);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbDur);
            this.Controls.Add(this.rbTime1);
            this.Controls.Add(this.rbTimeX);
            this.Controls.Add(this.rbDaily);
            this.Controls.Add(this.btnClr);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnNow);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbDay7);
            this.Controls.Add(this.cbDay6);
            this.Controls.Add(this.cbDay5);
            this.Controls.Add(this.cbDay4);
            this.Controls.Add(this.cbDay3);
            this.Controls.Add(this.cbDay2);
            this.Controls.Add(this.cbDay1);
            this.Controls.Add(this.tbSS);
            this.Controls.Add(this.tbHH);
            this.Controls.Add(this.tbMM);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DateDialog";
            this.Text = "Date Dialog";
            this.Load += new System.EventHandler(this.DateDialog_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label LText;
        private System.Windows.Forms.TextBox tbMM;
        private System.Windows.Forms.TextBox tbHH;
        private System.Windows.Forms.TextBox tbSS;
        private System.Windows.Forms.CheckBox cbDay1;
        private System.Windows.Forms.CheckBox cbDay2;
        private System.Windows.Forms.CheckBox cbDay3;
        private System.Windows.Forms.CheckBox cbDay4;
        private System.Windows.Forms.CheckBox cbDay5;
        private System.Windows.Forms.CheckBox cbDay6;
        private System.Windows.Forms.CheckBox cbDay7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnNow;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClr;
        private System.Windows.Forms.RadioButton rbDaily;
        private System.Windows.Forms.RadioButton rbTimeX;
        private System.Windows.Forms.RadioButton rbTime1;
        private System.Windows.Forms.TextBox tbDur;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbArea;
        private System.Windows.Forms.Button btnDisable;
    }
}