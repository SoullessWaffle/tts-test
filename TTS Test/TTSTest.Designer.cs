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
            this.groupBox_text = new System.Windows.Forms.GroupBox();
            this.richTextBox_text = new System.Windows.Forms.RichTextBox();
            this.groupBox_speech = new System.Windows.Forms.GroupBox();
            this.panel_volume = new System.Windows.Forms.Panel();
            this.textBox_volume_value = new System.Windows.Forms.TextBox();
            this.trackBar_volume = new System.Windows.Forms.TrackBar();
            this.label_volume = new System.Windows.Forms.Label();
            this.panel_rate = new System.Windows.Forms.Panel();
            this.textBox_rate_value = new System.Windows.Forms.TextBox();
            this.trackBar_rate = new System.Windows.Forms.TrackBar();
            this.label_rate = new System.Windows.Forms.Label();
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
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSep1Separator = new System.Windows.Forms.ToolStripSeparator();
            this.saveSpeechMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSpeechAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSep2Separator = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox_text.SuspendLayout();
            this.groupBox_speech.SuspendLayout();
            this.panel_volume.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_volume)).BeginInit();
            this.panel_rate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_rate)).BeginInit();
            this.groupBox_voice.SuspendLayout();
            this.panel_status.SuspendLayout();
            this.panel_currentWord.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.groupBox_text);
            this.splitContainer1.Panel1MinSize = 320;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel2.Controls.Add(this.groupBox_speech);
            this.splitContainer1.Panel2MinSize = 320;
            this.splitContainer1.Size = new System.Drawing.Size(900, 544);
            this.splitContainer1.SplitterDistance = 535;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // groupBox_text
            // 
            this.groupBox_text.Controls.Add(this.richTextBox_text);
            this.groupBox_text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_text.Location = new System.Drawing.Point(0, 0);
            this.groupBox_text.Name = "groupBox_text";
            this.groupBox_text.Size = new System.Drawing.Size(535, 544);
            this.groupBox_text.TabIndex = 2;
            this.groupBox_text.TabStop = false;
            this.groupBox_text.Text = "Text";
            // 
            // richTextBox_text
            // 
            this.richTextBox_text.DetectUrls = false;
            this.richTextBox_text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_text.HideSelection = false;
            this.richTextBox_text.Location = new System.Drawing.Point(3, 16);
            this.richTextBox_text.Name = "richTextBox_text";
            this.richTextBox_text.Size = new System.Drawing.Size(529, 525);
            this.richTextBox_text.TabIndex = 1;
            this.richTextBox_text.Text = resources.GetString("richTextBox_text.Text");
            // 
            // groupBox_speech
            // 
            this.groupBox_speech.Controls.Add(this.panel_volume);
            this.groupBox_speech.Controls.Add(this.panel_rate);
            this.groupBox_speech.Controls.Add(this.groupBox_voice);
            this.groupBox_speech.Controls.Add(this.panel_status);
            this.groupBox_speech.Controls.Add(this.panel_currentWord);
            this.groupBox_speech.Controls.Add(this.button_speak);
            this.groupBox_speech.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_speech.Location = new System.Drawing.Point(0, 0);
            this.groupBox_speech.Name = "groupBox_speech";
            this.groupBox_speech.Size = new System.Drawing.Size(361, 544);
            this.groupBox_speech.TabIndex = 0;
            this.groupBox_speech.TabStop = false;
            this.groupBox_speech.Text = "Speech";
            // 
            // panel_volume
            // 
            this.panel_volume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_volume.Controls.Add(this.textBox_volume_value);
            this.panel_volume.Controls.Add(this.trackBar_volume);
            this.panel_volume.Controls.Add(this.label_volume);
            this.panel_volume.Location = new System.Drawing.Point(3, 200);
            this.panel_volume.Name = "panel_volume";
            this.panel_volume.Size = new System.Drawing.Size(355, 64);
            this.panel_volume.TabIndex = 11;
            // 
            // textBox_volume_value
            // 
            this.textBox_volume_value.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_volume_value.Enabled = false;
            this.textBox_volume_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_volume_value.Location = new System.Drawing.Point(287, 9);
            this.textBox_volume_value.Name = "textBox_volume_value";
            this.textBox_volume_value.Size = new System.Drawing.Size(67, 26);
            this.textBox_volume_value.TabIndex = 11;
            this.textBox_volume_value.Text = "100%";
            this.textBox_volume_value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // trackBar_volume
            // 
            this.trackBar_volume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar_volume.AutoSize = false;
            this.trackBar_volume.LargeChange = 1;
            this.trackBar_volume.Location = new System.Drawing.Point(74, 9);
            this.trackBar_volume.Maximum = 100;
            this.trackBar_volume.Name = "trackBar_volume";
            this.trackBar_volume.Size = new System.Drawing.Size(207, 45);
            this.trackBar_volume.TabIndex = 8;
            this.trackBar_volume.Value = 100;
            this.trackBar_volume.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            this.trackBar_volume.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trackBar_MouseDown);
            this.trackBar_volume.MouseMove += new System.Windows.Forms.MouseEventHandler(this.trackBar_MouseMove);
            this.trackBar_volume.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar_volume_MouseUp);
            // 
            // label_volume
            // 
            this.label_volume.AutoSize = true;
            this.label_volume.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_volume.Location = new System.Drawing.Point(7, 9);
            this.label_volume.Name = "label_volume";
            this.label_volume.Size = new System.Drawing.Size(63, 20);
            this.label_volume.TabIndex = 9;
            this.label_volume.Text = "Volume";
            // 
            // panel_rate
            // 
            this.panel_rate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_rate.Controls.Add(this.textBox_rate_value);
            this.panel_rate.Controls.Add(this.trackBar_rate);
            this.panel_rate.Controls.Add(this.label_rate);
            this.panel_rate.Location = new System.Drawing.Point(3, 130);
            this.panel_rate.Name = "panel_rate";
            this.panel_rate.Size = new System.Drawing.Size(355, 64);
            this.panel_rate.TabIndex = 10;
            // 
            // textBox_rate_value
            // 
            this.textBox_rate_value.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_rate_value.Enabled = false;
            this.textBox_rate_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_rate_value.Location = new System.Drawing.Point(287, 9);
            this.textBox_rate_value.Name = "textBox_rate_value";
            this.textBox_rate_value.Size = new System.Drawing.Size(67, 26);
            this.textBox_rate_value.TabIndex = 10;
            this.textBox_rate_value.Text = "0";
            this.textBox_rate_value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // trackBar_rate
            // 
            this.trackBar_rate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar_rate.AutoSize = false;
            this.trackBar_rate.LargeChange = 1;
            this.trackBar_rate.Location = new System.Drawing.Point(74, 9);
            this.trackBar_rate.Minimum = -10;
            this.trackBar_rate.Name = "trackBar_rate";
            this.trackBar_rate.Size = new System.Drawing.Size(207, 45);
            this.trackBar_rate.TabIndex = 8;
            this.trackBar_rate.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            this.trackBar_rate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trackBar_MouseDown);
            this.trackBar_rate.MouseMove += new System.Windows.Forms.MouseEventHandler(this.trackBar_MouseMove);
            this.trackBar_rate.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar_rate_MouseUp);
            // 
            // label_rate
            // 
            this.label_rate.AutoSize = true;
            this.label_rate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_rate.Location = new System.Drawing.Point(7, 9);
            this.label_rate.Name = "label_rate";
            this.label_rate.Size = new System.Drawing.Size(44, 20);
            this.label_rate.TabIndex = 9;
            this.label_rate.Text = "Rate";
            // 
            // groupBox_voice
            // 
            this.groupBox_voice.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_voice.Controls.Add(this.listView_voice);
            this.groupBox_voice.Location = new System.Drawing.Point(6, 270);
            this.groupBox_voice.Name = "groupBox_voice";
            this.groupBox_voice.Size = new System.Drawing.Size(352, 272);
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
            this.listView_voice.Size = new System.Drawing.Size(346, 253);
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
            this.panel_status.Controls.Add(this.label_status);
            this.panel_status.Controls.Add(this.textBox_status);
            this.panel_status.Location = new System.Drawing.Point(3, 92);
            this.panel_status.Name = "panel_status";
            this.panel_status.Size = new System.Drawing.Size(355, 35);
            this.panel_status.TabIndex = 5;
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_status.Location = new System.Drawing.Point(7, 6);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(56, 20);
            this.label_status.TabIndex = 2;
            this.label_status.Text = "Status";
            // 
            // textBox_status
            // 
            this.textBox_status.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_status.Enabled = false;
            this.textBox_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_status.Location = new System.Drawing.Point(81, 3);
            this.textBox_status.Name = "textBox_status";
            this.textBox_status.Size = new System.Drawing.Size(274, 26);
            this.textBox_status.TabIndex = 3;
            // 
            // panel_currentWord
            // 
            this.panel_currentWord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_currentWord.Controls.Add(this.label_currentWord);
            this.panel_currentWord.Controls.Add(this.textBox_currentWord);
            this.panel_currentWord.Location = new System.Drawing.Point(3, 54);
            this.panel_currentWord.Name = "panel_currentWord";
            this.panel_currentWord.Size = new System.Drawing.Size(355, 35);
            this.panel_currentWord.TabIndex = 4;
            // 
            // label_currentWord
            // 
            this.label_currentWord.AutoSize = true;
            this.label_currentWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_currentWord.Location = new System.Drawing.Point(7, 6);
            this.label_currentWord.Name = "label_currentWord";
            this.label_currentWord.Size = new System.Drawing.Size(47, 20);
            this.label_currentWord.TabIndex = 2;
            this.label_currentWord.Text = "Word";
            // 
            // textBox_currentWord
            // 
            this.textBox_currentWord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_currentWord.Enabled = false;
            this.textBox_currentWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_currentWord.Location = new System.Drawing.Point(81, 6);
            this.textBox_currentWord.Name = "textBox_currentWord";
            this.textBox_currentWord.Size = new System.Drawing.Size(274, 26);
            this.textBox_currentWord.TabIndex = 3;
            // 
            // button_speak
            // 
            this.button_speak.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_speak.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_speak.Location = new System.Drawing.Point(3, 16);
            this.button_speak.Name = "button_speak";
            this.button_speak.Size = new System.Drawing.Size(355, 32);
            this.button_speak.TabIndex = 0;
            this.button_speak.Text = "Speak";
            this.button_speak.UseVisualStyleBackColor = true;
            this.button_speak.Click += new System.EventHandler(this.button_speak_Click);
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.helpMenu});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.ShowItemToolTips = true;
            this.mainMenu.Size = new System.Drawing.Size(900, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "mainMenu";
            // 
            // fileMenu
            // 
            this.fileMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadTextMenuItem,
            this.fileSep1Separator,
            this.saveSpeechMenuItem,
            this.saveSpeechAsMenuItem,
            this.fileSep2Separator,
            this.exitMenuItem});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(37, 20);
            this.fileMenu.Text = "&File";
            // 
            // loadTextMenuItem
            // 
            this.loadTextMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loadTextMenuItem.Name = "loadTextMenuItem";
            this.loadTextMenuItem.ShortcutKeyDisplayString = "Ctrl+O";
            this.loadTextMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.loadTextMenuItem.Size = new System.Drawing.Size(236, 22);
            this.loadTextMenuItem.Text = "L&oad Text...";
            this.loadTextMenuItem.Click += new System.EventHandler(this.loadTextMenuItem_Click);
            // 
            // fileSep1Separator
            // 
            this.fileSep1Separator.Name = "fileSep1Separator";
            this.fileSep1Separator.Size = new System.Drawing.Size(233, 6);
            // 
            // saveSpeechMenuItem
            // 
            this.saveSpeechMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveSpeechMenuItem.Name = "saveSpeechMenuItem";
            this.saveSpeechMenuItem.ShortcutKeyDisplayString = "Ctrl+S";
            this.saveSpeechMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveSpeechMenuItem.Size = new System.Drawing.Size(236, 22);
            this.saveSpeechMenuItem.Text = "&Save Speech";
            this.saveSpeechMenuItem.Click += new System.EventHandler(this.saveSpeechMenuItem_Click);
            // 
            // saveSpeechAsMenuItem
            // 
            this.saveSpeechAsMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveSpeechAsMenuItem.Name = "saveSpeechAsMenuItem";
            this.saveSpeechAsMenuItem.ShortcutKeyDisplayString = "Ctrl+Shift+S";
            this.saveSpeechAsMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveSpeechAsMenuItem.Size = new System.Drawing.Size(236, 22);
            this.saveSpeechAsMenuItem.Text = "Save Speech &As...";
            this.saveSpeechAsMenuItem.Click += new System.EventHandler(this.saveSpeechAsMenuItem_Click);
            // 
            // fileSep2Separator
            // 
            this.fileSep2Separator.Name = "fileSep2Separator";
            this.fileSep2Separator.Size = new System.Drawing.Size(233, 6);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.ShortcutKeyDisplayString = "Ctrl+Q";
            this.exitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.exitMenuItem.Size = new System.Drawing.Size(236, 22);
            this.exitMenuItem.Text = "E&xit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutMenuItem});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(44, 20);
            this.helpMenu.Text = "&Help";
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.aboutMenuItem.Name = "aboutMenuItem";
            this.aboutMenuItem.Size = new System.Drawing.Size(214, 22);
            this.aboutMenuItem.Text = "A&bout Text-to-Speech Test";
            // 
            // TTSTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 571);
            this.Controls.Add(this.mainMenu);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.MinimumSize = new System.Drawing.Size(820, 551);
            this.Name = "TTSTest";
            this.Text = "Text-to-Speech Test";
            this.Load += new System.EventHandler(this.TTSTest_Load);
            this.Resize += new System.EventHandler(this.TTSTest_Resize);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox_text.ResumeLayout(false);
            this.groupBox_speech.ResumeLayout(false);
            this.panel_volume.ResumeLayout(false);
            this.panel_volume.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_volume)).EndInit();
            this.panel_rate.ResumeLayout(false);
            this.panel_rate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_rate)).EndInit();
            this.groupBox_voice.ResumeLayout(false);
            this.panel_status.ResumeLayout(false);
            this.panel_status.PerformLayout();
            this.panel_currentWord.ResumeLayout(false);
            this.panel_currentWord.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox_text;
        private System.Windows.Forms.GroupBox groupBox_speech;
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
        private System.Windows.Forms.Panel panel_volume;
        private System.Windows.Forms.TrackBar trackBar_volume;
        private System.Windows.Forms.Label label_volume;
        private System.Windows.Forms.TextBox textBox_rate_value;
        private System.Windows.Forms.TextBox textBox_volume_value;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripSeparator fileSep1Separator;
        private System.Windows.Forms.ToolStripMenuItem saveSpeechMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadTextMenuItem;
        private System.Windows.Forms.ToolStripSeparator fileSep2Separator;
        private System.Windows.Forms.ToolStripMenuItem saveSpeechAsMenuItem;
    }
}

