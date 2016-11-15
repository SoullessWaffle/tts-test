using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Speech.Synthesis;

namespace TTS_Test
{
    public partial class TTSTest : Form
    {
        #region Fields and properties
        private NativeWindow thisWindow;
        private Form thisForm;

        private ToolTip formTooltip;
        private SaveFileDialog saveSpeechDialog;
        private OpenFileDialog loadTextDialog;

        private string baseTitle;
        private string _currentSpeechFile;
        public string CurrentSpeechFile
        {
            get
            {
                return _currentSpeechFile;
            }
            set
            {
                _currentSpeechFile = value;
                thisForm.Text = baseTitle + $" - {value}" + (SavedStatus ? "" : "*");
            }
        }

        private bool _savedStatus;
        private bool SavedStatus
        {
            get
            {
                return _savedStatus;
            }
            set
            {
                if (_savedStatus != value)
                {
                    _savedStatus = value;
                    if (value)
                    {
                        // Remove asterisk
                        thisForm.Text = thisForm.Text.Substring(0, thisForm.Text.Length - 1);
                    }
                    else
                    {
                        // Add asterisk
                        thisForm.Text = thisForm.Text + "*";
                    }
                }
            }
        }

        private readonly TimeSpan tts_max_response_time = TimeSpan.FromSeconds(3);
        
        private const int listView_voice_min_column_width = 60;

        // This has a memory leak :( 
        // https://connect.microsoft.com/VisualStudio/feedback/details/664196/system-speech-has-a-memory-leak
        private SpeechSynthesizer Synth;

        private List<VoiceInfo> voices;

        public enum SpeechStatus { Idle, Starting, Speaking, Stopping, SwitchingVoice, ChangingRate, ChangingVolume };

        private SpeechStatus _status;
        public SpeechStatus Status
        {
            get
            {
                return _status;
            }
            private set
            {
                _status = value;

                // Display status in UI
                string statusText;
                if (value == SpeechStatus.SwitchingVoice)
                    statusText = "Switching voice";
                else if (value == SpeechStatus.ChangingRate)
                    statusText = "Changing rate";
                else if (value == SpeechStatus.ChangingVolume)
                    statusText = "Changing volume";
                else
                    statusText = value.ToString();
                textBox_status.Text = statusText;

                switch (value)
                {
                    case SpeechStatus.Idle:
                        UnlockControls();
                        UnlockInputText();

                        button_speak.Text = "Speak";
                        break;
                    case SpeechStatus.Starting:
                        LockControls();
                        LockInputText();
                        break;
                    case SpeechStatus.Speaking:
                        UnlockControls();
                        LockInputText();

                        button_speak.Text = "Stop";
                        break;
                    case SpeechStatus.Stopping:
                        LockControls();
                        LockInputText();
                        break;
                    case SpeechStatus.SwitchingVoice:
                        LockControls();
                        LockInputText();
                        break;
                    case SpeechStatus.ChangingRate:
                        LockControls();
                        LockInputText();
                        break;
                    case SpeechStatus.ChangingVolume:
                        LockControls();
                        LockInputText();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("Status");
                }
            }
        }

        public string SpeechText { get; private set; }

        private string _currentWord;
        public string CurrentWord
        {
            get
            {
                return _currentWord;
            }
            private set
            {
                _currentWord = value;
                textBox_currentWord.Text = value;
            }
        }

        public int CurrentIndex { get; private set; }
        public int CurrentOffset { get; private set; }

        private bool setupDone = false;
        private ManualResetEvent startedSpeakingHandle = new ManualResetEvent(false);
        private ManualResetEvent stoppedSpeakingHandle = new ManualResetEvent(false);
        #endregion

        #region Setup
        public TTSTest ()
        {
            InitializeComponent();
            Setup();
        }

        private void Setup ()
        {
            thisWindow = new NativeWindow();
            thisWindow.AssignHandle(this.Handle);

            Status = SpeechStatus.Idle;
            SpeechText = richTextBox_text.Text;
            CurrentWord = "";

            Synth = new SpeechSynthesizer();
            Synth.SpeakStarted += Synth_SpeakStarted;
            Synth.SpeakCompleted += Synth_SpeakCompleted;
            Synth.SpeakProgress += Synth_SpeakProgress;

            trackBar_rate.Value = Synth.Rate;
            trackBar_volume.Value = Synth.Volume;

            UpdateVoiceList();
            setupDone = true;

            Console.WriteLine("Setup done");
            Console.WriteLine();
        }

        private void TTSTest_Load (object sender, EventArgs e)
        {
            thisForm = Application.OpenForms[0];
            Console.WriteLine("Form load!");
            formTooltip = new ToolTip();

            baseTitle = thisForm.Text;

            saveSpeechDialog = new SaveFileDialog()
            {
                Filter = "All files (*.*)|*.*|Audio file (*.wav)|*.wav",
                FilterIndex = 2,
                Title = "Save Speech"
            };

            loadTextDialog = new OpenFileDialog()
            {
                Filter = "All files (*.*)|*.*|Text file (*.txt)|*.txt",
                FilterIndex = 2,
                Title = "Load Text"
            };

            Panel1MinSize = splitContainer1.Panel1MinSize;
            thisForm.MinimumSize = new Size(Panel1MinSize + Panel2MaxSize + 10, thisForm.MinimumSize.Height);
            UpdatePanel1MinSize();
        }

        private readonly Regex msssttsvRegex = new Regex(@"^Microsoft Server Speech Text to Speech Voice \(\w+\-\w+, (?<name>[^\(\s\)]+)\)$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture);
        private readonly Regex msdttsRegex = new Regex(@"^Microsoft (?<name>[^\(\s\)]+) Desktop$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture);
        private string PrettyVoiceName (string voiceName)
        {
            Match m = msssttsvRegex.Match(voiceName);
            if (m.Success)
                return m.Groups["name"].Value;
            else
            {
                m = msdttsRegex.Match(voiceName);
                if (m.Success)
                    return m.Groups["name"].Value;
                else
                    return voiceName;
            }
        }

        private void UpdateVoiceList ()
        {
            VoiceInfo initialVoice = Synth.Voice;
            Console.WriteLine("Initial voice: {0}", initialVoice.Name);

            ReadOnlyCollection<InstalledVoice> installedVoices = Synth.GetInstalledVoices();
            voices = new List<VoiceInfo>(installedVoices.Count);
            
            foreach (InstalledVoice v in installedVoices)
            {
                if (v.Enabled)
                {
                    try
                    {
                        Console.WriteLine("---\nTesting {0}...", v.VoiceInfo.Name);
                        // Test the voice
                        Synth.SelectVoice(v.VoiceInfo.Name);
                        Console.WriteLine("{0} works", v.VoiceInfo.Name);

                        // Get the version if it exists
                        string version;
                        bool hasVersion = v.VoiceInfo.AdditionalInfo.TryGetValue("Version", out version);

                        voices.Add(v.VoiceInfo);
                        listView_voice.Items.Add(new ListViewItem(new string[] { v.VoiceInfo.Culture.DisplayName, PrettyVoiceName(v.VoiceInfo.Name), hasVersion ? version : "N/A" }));
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine("{0} does not work: {1}", v.VoiceInfo.Name, e.Message);
                    }
                }
            }

            // Restore the initial voice after testing
            try
            {
                Console.WriteLine("---\nRestoring initial voice {0}", initialVoice.Name);
                Synth.SelectVoice(initialVoice.Name);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Unable to restore initial voice {0}: {1}", initialVoice.Name, e.Message);
                if (voices.Count > 0)
                {
                    Console.WriteLine("Falling back to first available voice: {0}", voices[0].Name);
                    // This shouldn't fail. If it does, there's not much else we can do anyway.
                    Synth.SelectVoice(voices[0].Name);
                }
                else
                {
                    Console.WriteLine("No other available voices. Aborting...");
                    MessageBox.Show(thisWindow, "You do not have any usable Text-To-Speech voices installed.\n\nThe program will now exit.", "No voices available", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                }
            }

            // Select the active voice in the listView
            ListViewItem currentListViewItem = listView_voice.Items[voices.IndexOf(Synth.Voice)];
            currentListViewItem.Selected = true;

            // Set the column sizes
            listView_voice.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            foreach (ColumnHeader c in listView_voice.Columns)
            {
                c.Width = (c.Width < listView_voice_min_column_width ? listView_voice_min_column_width : c.Width);
            }
        }
        #endregion

        #region TTS events
        private void Synth_SpeakProgress (object sender, SpeakProgressEventArgs e)
        {
            if (Status == SpeechStatus.SwitchingVoice || Status == SpeechStatus.ChangingRate || Status == SpeechStatus.ChangingVolume)
                return;

            CurrentWord = e.Text;
            CurrentIndex = CurrentOffset + e.CharacterPosition;

            richTextBox_text.SelectionStart = CurrentIndex;
            richTextBox_text.SelectionLength = e.CharacterCount;
        }

        private void Synth_SpeakCompleted (object sender, SpeakCompletedEventArgs e)
        {
            Console.WriteLine("EndStream event");

            if (Status != SpeechStatus.SwitchingVoice && Status != SpeechStatus.ChangingRate && Status != SpeechStatus.ChangingVolume)
            {
                // Either we were stopped manually, or we have finished.

                // Clear the data
                SpeechText = "";
                CurrentIndex = 0;
                CurrentOffset = 0;
                CurrentWord = "";
                richTextBox_text.Select(0, 0);

                // We have finished speaking
                if (Status == SpeechStatus.Speaking)
                    Status = SpeechStatus.Idle;
            }

            stoppedSpeakingHandle.Set();
        }

        private void Synth_SpeakStarted (object sender, SpeakStartedEventArgs e)
        {
            Console.WriteLine("SpeakStarted event");

            startedSpeakingHandle.Set();
        }
        #endregion

        #region UI locking
        private bool _controlsLocked = false;
        public void LockControls ()
        {
            if (!_controlsLocked)
            {
                _controlsLocked = true;
                listView_voice.Enabled = false;
                button_speak.Enabled = false;
                trackBar_rate.Enabled = false;
                trackBar_volume.Enabled = false;
            }
        }

        public void UnlockControls ()
        {
            if (_controlsLocked)
            {
                _controlsLocked = false;
                listView_voice.Enabled = true;
                button_speak.Enabled = true;
                trackBar_rate.Enabled = true;
                trackBar_volume.Enabled = true;
            }
        }

        private bool _inputTextLocked = false;
        public void LockInputText ()
        {
            if (!_inputTextLocked)
            {
                _inputTextLocked = true;
                richTextBox_text.Enabled = false;
            }
        }

        public void UnlockInputText ()
        {
            if (_inputTextLocked)
            {
                _inputTextLocked = false;
                richTextBox_text.Enabled = true;
            }
        }

        public void ResetFocus ()
        {
            richTextBox_text.Select();
        }
        #endregion

        public async Task StopSpeaking ()
        {
            if (Status == SpeechStatus.Idle)
                return;

            if (Status != SpeechStatus.SwitchingVoice && Status != SpeechStatus.ChangingRate && Status != SpeechStatus.ChangingVolume)
                Status = SpeechStatus.Stopping;

            Console.WriteLine("StopSpeaking called");

            // TODO: Make this handle handling (heh) and try/catch Timeout block more generic, since we use it quite a bit

            // Reset handle
            stoppedSpeakingHandle.Reset();
            // Stop speaking
            Synth.SpeakAsyncCancelAll();

            try
            {
                await stoppedSpeakingHandle.AsTask(timeout: tts_max_response_time);
                
                Console.WriteLine("Stopped speaking");

                if (Status != SpeechStatus.SwitchingVoice && Status != SpeechStatus.ChangingRate && Status != SpeechStatus.ChangingVolume)
                    Status = SpeechStatus.Idle;
            }
            catch (TaskCanceledException)
            {
                // Task timed out
                throw new TimeoutException("TTS engine took too long to stop speaking.");
            }
        }

        public async Task StartSpeaking (int offset = 0)
        {
            if (Status == SpeechStatus.Speaking)
                return;

            if (Status != SpeechStatus.SwitchingVoice && Status != SpeechStatus.ChangingRate && Status != SpeechStatus.ChangingVolume)
                Status = SpeechStatus.Starting;

            SpeechText = richTextBox_text.Text;
            if (offset != 0)
            {
                Console.WriteLine("StartSpeaking called with offset {0}", offset);
                SpeechText = SpeechText.Substring(offset);
            }
            else
            {
                Console.WriteLine("StartSpeaking called");
            }

            // Reset handle
            startedSpeakingHandle.Reset();
            Synth.SpeakAsync(SpeechText);

            try
            {
                await startedSpeakingHandle.AsTask(timeout: tts_max_response_time);
                
                Console.WriteLine("Started speaking");

                if (Status != SpeechStatus.SwitchingVoice && Status != SpeechStatus.ChangingRate && Status != SpeechStatus.ChangingVolume)
                    Status = SpeechStatus.Speaking;
            }
            catch (TaskCanceledException)
            {
                // Task timed out
                throw new TimeoutException("TTS engine took too long to start speaking.");
            }
        }

        private async void SwitchVoice (VoiceInfo v)
        {
            Console.WriteLine("SwitchVoice to {0}", v.Name);

            switch (Status)
            {
                case SpeechStatus.Idle:
                    try
                    {
                        // Set the voice
                        Synth.SelectVoice(v.Name);

                        Console.WriteLine("Changed voice to {0}", v.Name);
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine("Failed to change voice to {0}: {1}", v.Name, e.Message);
                    }
                    break;
                case SpeechStatus.Starting:
                    throw new InvalidOperationException("SwitchVoice was somehow called while Status == Starting");
                case SpeechStatus.Speaking:
                    Status = SpeechStatus.SwitchingVoice;

                    // Stop the speech
                    CurrentOffset = CurrentIndex;
                    await StopSpeaking();
                    
                    try
                    {
                        // Set the voice
                        Synth.SelectVoice(v.Name);

                        Console.WriteLine("Changed voice to {0}", v.Name);
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine("Failed to change voice to {0}: {1}", v.Name, e.Message);
                    }
                    finally
                    {
                        // Start the speech
                        await StartSpeaking(CurrentOffset);
                        Status = SpeechStatus.Speaking;
                    }
                    break;
                case SpeechStatus.Stopping:
                    throw new InvalidOperationException("SwitchVoice was somehow called while Status == Stopping");
                case SpeechStatus.SwitchingVoice:
                    throw new InvalidOperationException("SwitchVoice was somehow called while Status == SwitchingVoice");
                case SpeechStatus.ChangingRate:
                    throw new InvalidOperationException("SwitchVoice was somehow called while Status == ChangingRate");
                case SpeechStatus.ChangingVolume:
                    throw new InvalidOperationException("SwitchVoice was somehow called while Status == ChangingVolume");
                default:
                    throw new ArgumentOutOfRangeException("Status");
            }
        }

        private async Task SetRate (int rate)
        {
            // TODO: Make this and SwitchVoice more generic, since we'll need this for volume too.
            switch (Status)
            {
                case SpeechStatus.Idle:
                    Synth.Rate = rate;
                    break;
                case SpeechStatus.Starting:
                    throw new InvalidOperationException("SetRate was somehow called while Status == Starting");
                case SpeechStatus.Speaking:
                    Status = SpeechStatus.ChangingRate;
                    CurrentOffset = CurrentIndex;

                    await StopSpeaking();

                    Synth.Rate = rate;

                    await StartSpeaking(CurrentOffset);

                    Status = SpeechStatus.Speaking;
                    break;
                case SpeechStatus.Stopping:
                    throw new InvalidOperationException("SetRate was somehow called while Status == Stopping");
                case SpeechStatus.SwitchingVoice:
                    throw new InvalidOperationException("SetRate was somehow called while Status == SwitchingVoice");
                case SpeechStatus.ChangingRate:
                    throw new InvalidOperationException("SetRate was somehow called while Status == ChangingRate");
                case SpeechStatus.ChangingVolume:
                    throw new InvalidOperationException("SetRate was somehow called while Status == ChangingVolume");
                default:
                    throw new ArgumentOutOfRangeException("Status");
            }
        }

        private async Task SetVolume (int volume)
        {
            switch (Status)
            {
                case SpeechStatus.Idle:
                    Synth.Volume = volume;
                    break;
                case SpeechStatus.Starting:
                    throw new InvalidOperationException("SetVolume was somehow called while Status == Starting");
                case SpeechStatus.Speaking:
                    Status = SpeechStatus.ChangingVolume;
                    CurrentOffset = CurrentIndex;

                    await StopSpeaking();

                    Synth.Volume = volume;

                    await StartSpeaking(CurrentOffset);

                    Status = SpeechStatus.Speaking;
                    break;
                case SpeechStatus.Stopping:
                    throw new InvalidOperationException("SetVolume was somehow called while Status == Stopping");
                case SpeechStatus.SwitchingVoice:
                    throw new InvalidOperationException("SetVolume was somehow called while Status == SwitchingVoice");
                case SpeechStatus.ChangingRate:
                    throw new InvalidOperationException("SetVolume was somehow called while Status == ChangingRate");
                case SpeechStatus.ChangingVolume:
                    throw new InvalidOperationException("SetVolume was somehow called while Status == ChangingVolume");
                default:
                    throw new ArgumentOutOfRangeException("Status");
            }
        }

        // TODO: Improve validation method
        private bool ValidateInput ()
        {
            return richTextBox_text.Text.Length > 0;
        }

        #region UI events
        // http://stackoverflow.com/q/4516350
        private void MoveTrackBarToMouseClickLocation (TrackBar a_tBar, int a_mouseX)
        {
            double fraction = ((double)a_mouseX / (double)a_tBar.Width);
            double newValue = ((1d - fraction) * a_tBar.Minimum) + (fraction * a_tBar.Maximum);
            a_tBar.Value = Convert.ToInt32(newValue).Clamp(a_tBar.Minimum, a_tBar.Maximum);
        }

        private async void button_speak_Click (object sender, EventArgs e)
        {
            ResetFocus();

            switch (Status)
            {
                case SpeechStatus.Idle:
                    if (ValidateInput())
                    {
                        await StartSpeaking();
                        Console.WriteLine("StartSpeaking is done!");
                    }
                    else
                    {
                        Console.WriteLine("Input is invalid");
                        MessageBox.Show(thisWindow, "Please input some text.", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    break;
                case SpeechStatus.Starting:
                    throw new InvalidOperationException("button_speak_Click was somehow called while Status == Starting");
                case SpeechStatus.Speaking:
                    await StopSpeaking();
                    Console.WriteLine("StopSpeaking is done!");
                    break;
                case SpeechStatus.Stopping:
                    throw new InvalidOperationException("button_speak_Click was somehow called while Status == Stopping");
                case SpeechStatus.SwitchingVoice:
                    throw new InvalidOperationException("button_speak_Click was somehow called while Status == SwitchingVoice");
                case SpeechStatus.ChangingRate:
                    throw new InvalidOperationException("button_speak_Click was somehow called while Status == ChangingRate");
                case SpeechStatus.ChangingVolume:
                    throw new InvalidOperationException("button_speak_Click was somehow called while Status == ChangingVolume");
                default:
                    throw new ArgumentOutOfRangeException("Status");
            }
        }

        private void listView_voice_ItemSelectionChanged (object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (setupDone && e.IsSelected)
            {
                ResetFocus();

                VoiceInfo selectedVoice = voices[e.ItemIndex];
                SwitchVoice(selectedVoice);
            }
        }

        private void listView_voice_ColumnWidthChanging (object sender, ColumnWidthChangingEventArgs e)
        {
            // Disable resizing columns
            e.NewWidth = listView_voice.Columns[e.ColumnIndex].Width;
            e.Cancel = true;

            ResetFocus();
        }

        private void trackBar_MouseMove (object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MoveTrackBarToMouseClickLocation((TrackBar)sender, e.X);
            }
        }

        private void trackBar_MouseDown (object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MoveTrackBarToMouseClickLocation((TrackBar)sender, e.X);
            }
        }

        private void trackBar_ValueChanged (object sender, EventArgs e)
        {
            TrackBar s = (TrackBar)sender;
            Console.WriteLine("Trackbar {0} value changed: {1}", s.Name, s.Value);

            if (s == trackBar_rate)
            {
                textBox_rate_value.Text = (trackBar_rate.Value > 0 ? "+" : "") + trackBar_rate.Value.ToString();
            }
            else if (s == trackBar_volume)
            {
                textBox_volume_value.Text = trackBar_volume.Value.ToString() + "%";
            }
        }

        private async void trackBar_rate_MouseUp (object sender, MouseEventArgs e)
        {
            ResetFocus();

            if (trackBar_rate.Value != Synth.Rate)
            {
                Console.WriteLine("Apply rate change {0} -> {1}", Synth.Rate, trackBar_rate.Value);
                await SetRate(trackBar_rate.Value);
            }
        }
        #endregion

        private async void trackBar_volume_MouseUp (object sender, MouseEventArgs e)
        {
            ResetFocus();

            if (trackBar_volume.Value != Synth.Volume)
            {
                Console.WriteLine("Apply volume change {0} -> {1}", Synth.Volume, trackBar_volume.Value);
                await SetVolume(trackBar_volume.Value);
            }
        }

        private void splitContainer1_SplitterMoved (object sender, SplitterEventArgs e)
        {
            // Remove focus from splitter
            ResetFocus();

            //Console.WriteLine("Panel 2 min size: {0}\nPanel 2 size: {1}\n{2}", splitContainer1.Panel2MinSize, splitContainer1.Panel2.Width, splitContainer1.Panel2MinSize <= splitContainer1.Panel2.Width ? "OK" : "WRONG");
        }

        private int Panel1MinSize;
        private const int Panel2MaxSize = 500;
        private void UpdatePanel1MinSize ()
        {
            splitContainer1.Panel1MinSize = Math.Max(thisForm.Width - TTSTest.Panel2MaxSize, this.Panel1MinSize);
        }

        private void TTSTest_Resize (object sender, EventArgs e)
        {
            UpdatePanel1MinSize();
        }

        private void FileDialogCleanup (FileDialog fd)
        {
            try
            {
                string currentDir = System.IO.Path.GetDirectoryName(fd.FileName);
                if (!string.IsNullOrWhiteSpace(currentDir))
                {
                    fd.InitialDirectory = currentDir;
                    fd.FileName = System.IO.Path.GetFileName(fd.FileName);
                }
            }
            catch (ArgumentException) { }
        }

        // Save Speech file
        private async void saveSpeechMenuItem_Click (object sender, EventArgs e)
        {
            await SaveSpeechFile();
        }

        // Save Speech file as
        private async void saveSpeechAsMenuItem_Click (object sender, EventArgs e)
        {
            await SaveSpeechFile(true);
        }

        private async Task SaveSpeechFile (bool forceDialog = false)
        {
            if (forceDialog || string.IsNullOrWhiteSpace(CurrentSpeechFile))
            {
                // Show dialog
                // TODO: Set default filename to textfilename.wav if text was loaded
                FileDialogCleanup(saveSpeechDialog);
                if (saveSpeechDialog.ShowDialog(thisWindow) == DialogResult.OK)
                {
                    CurrentSpeechFile = saveSpeechDialog.FileName;
                }
                else return;
            }

            // TODO: Don't freeze UI while saving and add better error handling
            SavedStatus = false;
            string voicename = Synth.Voice.Name;
            TTSFResult result = await IOHandler.SaveSpeechFile(CurrentSpeechFile, richTextBox_text.Text, voicename, Synth.Rate, Synth.Volume);
            if (!result.Success)
            {
                if (result.Finished)
                {
                    // TTSF error
                    switch (result.ExitCode)
                    {
                        case IOHandler.ExitCode.InvalidArgument:
                            throw new ArgumentException($"{IOHandler.TTSF} returned exitcode InvalidArgument for arguments {result.Arguments}\n{IOHandler.TTSF} STDOUT: {result.Output}\n\n{IOHandler.TTSF} STDERR: {result.Error}");
                        case IOHandler.ExitCode.InvalidVoice:
                            throw new ArgumentException($"{IOHandler.TTSF} returned exitcode InvalidVoice for voice {voicename}\n{IOHandler.TTSF} STDOUT: {result.Output}\n\n{IOHandler.TTSF} STDERR: {result.Error}");
                        case IOHandler.ExitCode.IOException:
                            // TODO: More specific error messages
                            if (MessageBox.Show(thisWindow, "There was a problem saving the speech.\nWould you like to try again?", "Problem Saving Speech", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                            {
                                SaveSpeechFile().Forget();
                            }
                            break;
                        case IOHandler.ExitCode.UnhandledException:
                            throw new UnhandledTTSFException(result);
                        default:
                            throw new ArgumentOutOfRangeException("result.ExitCode");
                    }
                }
                else if (result.Exception == null)
                {
                    // Killed successfully
                    // TODO: More specific error messages
                    if (MessageBox.Show(thisWindow, $"There was a problem saving the speech.\nWould you like to try again?", "Problem Saving Speech", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    {
                        SaveSpeechFile().Forget();
                    }
                }
                else
                {
                    // Not killed successfully
                    // TODO: More or perhaps less specific error message
                    if (MessageBox.Show(thisWindow, "There was a problem saving the speech, and the Text-To-Speech-File process has become unresponsive.\nWould you like to try again?", "Problem Saving Speech", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    {
                        SaveSpeechFile().Forget();
                    }
                }
            }
            else
            {
                //MessageBox.Show(thisWindow, "All done", "Saving Speech Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SavedStatus = true;
            }
        }

        // Load Text file
        private async void loadTextMenuItem_Click (object sender, EventArgs e)
        {
            FileDialogCleanup(loadTextDialog);
            if (loadTextDialog.ShowDialog(thisWindow) == DialogResult.OK)
            {
                WaitCursor wc = new WaitCursor();
                LockInputText();

                try
                {
                    string text = await IOHandler.LoadTextFile(loadTextDialog.FileName);
                    await StopSpeaking();
                    richTextBox_text.Text = text;
                }
                catch
                {
                    Console.WriteLine("Exception happened while loading text file");
                }
                finally
                {
                    wc.Dispose();
                    UnlockInputText();
                }
            }
        }

        private void exitMenuItem_Click (object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
