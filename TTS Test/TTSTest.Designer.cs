namespace TTS_Test
{
    partial class TTSTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
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
        private void InitializeComponent ()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TTSTest));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richTextBox_text = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox_voice = new System.Windows.Forms.GroupBox();
            this.listView_voice = new System.Windows.Forms.ListView();
            this.voiceLang = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.voiceName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.voiceVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel_status = new System.Windows.Forms.Panel();
            this.label_status = new System.Windows.Forms.Label();
            this.textBox_status = new System.Windows.Forms.TextBox();
            this.panel_currentWord = new System.Windows.Forms.Panel();
            this.label_currentWord = new System.Windows.Forms.Label();
            this.textBox_currentWord = new System.Windows.Forms.TextBox();
            this.button_speak = new System.Windows.Forms.Button();
            this.trackBar_rate = new System.Windows.Forms.TrackBar();
            this.label_rate = new System.Windows.Forms.Label();
            this.panel_rate = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox_voice.SuspendLayout();
            this.panel_status.SuspendLayout();
            this.panel_currentWord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_rate)).BeginInit();
            this.panel_rate.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(714, 374);
            this.splitContainer1.SplitterDistance = 379;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richTextBox_text);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(379, 374);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Text";
            // 
            // richTextBox_text
            // 
            this.richTextBox_text.DetectUrls = false;
            this.richTextBox_text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_text.HideSelection = false;
            this.richTextBox_text.Location = new System.Drawing.Point(3, 16);
            this.richTextBox_text.Name = "richTextBox_text";
            this.richTextBox_text.Size = new System.Drawing.Size(373, 355);
            this.richTextBox_text.TabIndex = 1;
            this.richTextBox_text.Text = resources.GetString("richTextBox_text.Text");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel_rate);
            this.groupBox2.Controls.Add(this.groupBox_voice);
            this.groupBox2.Controls.Add(this.panel_status);
            this.groupBox2.Controls.Add(this.panel_currentWord);
            this.groupBox2.Controls.Add(this.button_speak);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(331, 374);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Speech";
            // 
            // groupBox_voice
            // 
            this.groupBox_voice.Controls.Add(this.listView_voice);
            this.groupBox_voice.Location = new System.Drawing.Point(6, 231);
            this.groupBox_voice.Name = "groupBox_voice";
            this.groupBox_voice.Size = new System.Drawing.Size(316, 137);
            this.groupBox_voice.TabIndex = 7;
            this.groupBox_voice.TabStop = false;
            this.groupBox_voice.Text = "Voice";
            // 
            // listView_voice
            // 
            this.listView_voice.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView_voice.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.voiceLang,
            this.voiceName,
            this.voiceVersion});
            this.listView_voice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_voice.FullRowSelect = true;
            this.listView_voice.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView_voice.HideSelection = false;
            this.listView_voice.Location = new System.Drawing.Point(3, 16);
            this.listView_voice.MultiSelect = false;
            this.listView_voice.Name = "listView_voice";
            this.listView_voice.ShowGroups = false;
            this.listView_voice.Size = new System.Drawing.Size(310, 118);
            this.listView_voice.TabIndex = 0;
            this.listView_voice.UseCompatibleStateImageBehavior = false;
            this.listView_voice.View = System.Windows.Forms.View.Details;
            this.listView_voice.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.listView_voice_ColumnWidthChanging);
            this.listView_voice.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView_voice_ItemSelectionChanged);
            // 
            // voiceLang
            // 
            this.voiceLang.Text = "Language";
            // 
            // voiceName
            // 
            this.voiceName.Text = "Name";
            // 
            // voiceVersion
            // 
            this.voiceVersion.Text = "Version";
            // 
            // panel_status
            // 
            this.panel_status.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_status.AutoSize = true;
            this.panel_status.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_status.Controls.Add(this.label_status);
            this.panel_status.Controls.Add(this.textBox_status);
            this.panel_status.Location = new System.Drawing.Point(3, 92);
            this.panel_status.Name = "panel_status";
            this.panel_status.Size = new System.Drawing.Size(319, 35);
            this.panel_status.TabIndex = 5;
            // 
            // label_status
            // 
            this.label_status.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_status.AutoSize = true;
            this.label_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_status.Location = new System.Drawing.Point(7, 6);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(60, 24);
            this.label_status.TabIndex = 2;
            this.label_status.Text = "Status";
            // 
            // textBox_status
            // 
            this.textBox_status.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_status.Enabled = false;
            this.textBox_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_status.Location = new System.Drawing.Point(74, 3);
            this.textBox_status.Name = "textBox_status";
            this.textBox_status.Size = new System.Drawing.Size(239, 29);
            this.textBox_status.TabIndex = 3;
            // 
            // panel_currentWord
            // 
            this.panel_currentWord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_currentWord.AutoSize = true;
            this.panel_currentWord.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_currentWord.Controls.Add(this.label_currentWord);
            this.panel_currentWord.Controls.Add(this.textBox_currentWord);
            this.panel_currentWord.Location = new System.Drawing.Point(3, 54);
            this.panel_currentWord.Name = "panel_currentWord";
            this.panel_currentWord.Size = new System.Drawing.Size(319, 35);
            this.panel_currentWord.TabIndex = 4;
            // 
            // label_currentWord
            // 
            this.label_currentWord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_currentWord.AutoSize = true;
            this.label_currentWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_currentWord.Location = new System.Drawing.Point(7, 6);
            this.label_currentWord.Name = "label_currentWord";
            this.label_currentWord.Size = new System.Drawing.Size(56, 24);
            this.label_currentWord.TabIndex = 2;
            this.label_currentWord.Text = "Word";
            // 
            // textBox_currentWord
            // 
            this.textBox_currentWord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_currentWord.Enabled = false;
            this.textBox_currentWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_currentWord.Location = new System.Drawing.Point(74, 3);
            this.textBox_currentWord.Name = "textBox_currentWord";
            this.textBox_currentWord.Size = new System.Drawing.Size(239, 29);
            this.textBox_currentWord.TabIndex = 3;
            // 
            // button_speak
            // 
            this.button_speak.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_speak.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_speak.Location = new System.Drawing.Point(3, 16);
            this.button_speak.Name = "button_speak";
            this.button_speak.Size = new System.Drawing.Size(325, 32);
            this.button_speak.TabIndex = 0;
            this.button_speak.Text = "Speak";
            this.button_speak.UseVisualStyleBackColor = true;
            this.button_speak.Click += new System.EventHandler(this.button_speak_Click);
            // 
            // trackBar_rate
            // 
            this.trackBar_rate.LargeChange = 1;
            this.trackBar_rate.Location = new System.Drawing.Point(74, 9);
            this.trackBar_rate.Minimum = -10;
            this.trackBar_rate.Name = "trackBar_rate";
            this.trackBar_rate.Size = new System.Drawing.Size(239, 45);
            this.trackBar_rate.TabIndex = 8;
            this.trackBar_rate.ValueChanged += new System.EventHandler(this.trackBar_rate_ValueChanged);
            this.trackBar_rate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trackBar_rate_MouseDown);
            this.trackBar_rate.MouseMove += new System.Windows.Forms.MouseEventHandler(this.trackBar_rate_MouseMove);
            this.trackBar_rate.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar_rate_MouseUp);
            // 
            // label_rate
            // 
            this.label_rate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_rate.AutoSize = true;
            this.label_rate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_rate.Location = new System.Drawing.Point(7, 9);
            this.label_rate.Name = "label_rate";
            this.label_rate.Size = new System.Drawing.Size(48, 24);
            this.label_rate.TabIndex = 9;
            this.label_rate.Text = "Rate";
            // 
            // panel_rate
            // 
            this.panel_rate.Controls.Add(this.trackBar_rate);
            this.panel_rate.Controls.Add(this.label_rate);
            this.panel_rate.Location = new System.Drawing.Point(3, 130);
            this.panel_rate.Name = "panel_rate";
            this.panel_rate.Size = new System.Drawing.Size(319, 64);
            this.panel_rate.TabIndex = 10;
            // 
            // TTSTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 374);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TTSTest";
            this.Text = "TTS Test";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox_voice.ResumeLayout(false);
            this.panel_status.ResumeLayout(false);
            this.panel_status.PerformLayout();
            this.panel_currentWord.ResumeLayout(false);
            this.panel_currentWord.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_rate)).EndInit();
            this.panel_rate.ResumeLayout(false);
            this.panel_rate.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_speak;
        private System.Windows.Forms.Label label_currentWord;
        private System.Windows.Forms.TextBox textBox_currentWord;
        private System.Windows.Forms.Panel panel_currentWord;
        private System.Windows.Forms.Panel panel_status;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.TextBox textBox_status;
        private System.Windows.Forms.RichTextBox richTextBox_text;
        private System.Windows.Forms.GroupBox groupBox_voice;
        private System.Windows.Forms.ListView listView_voice;
        private System.Windows.Forms.ColumnHeader voiceLang;
        private System.Windows.Forms.ColumnHeader voiceName;
        private System.Windows.Forms.ColumnHeader voiceVersion;
        private System.Windows.Forms.TrackBar trackBar_rate;
        private System.Windows.Forms.Panel panel_rate;
        private System.Windows.Forms.Label label_rate;
    }
}

