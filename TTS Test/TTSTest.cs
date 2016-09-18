using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpeechLib;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace TTS_Test
{
    public partial class TTSTest : Form
    {
        #region Voice wrapper class
        private class Voice
        {
            private static readonly Regex voiceInfoRegex = new Regex(@"(?<lang>[^_\-\\\s]+(\-[^_\-\\\s]+)+)_(?<name>[^_\-\\\s]+)_(?<version>\d+(\.\d+)*)$", RegexOptions.Compiled | RegexOptions.ExplicitCapture);

            public SpObjectToken spot;
            public bool infoFound;
            public string lang;
            public string name;
            public string version;

            public Voice (SpObjectToken spot)
            {
                Console.WriteLine("Voice ID: {0}", spot.Id);
                Console.WriteLine("Key data: {0}", ReadReg(spot.Id));
                this.spot = spot;
                this.infoFound = GetVoiceInfo(spot.Id, out this.lang, out this.name, out this.version);
            }

            private static object ReadReg (string key)
            {
                return Registry.GetValue(key, "", "nuthing");
            }

            public static explicit operator ListViewItem (Voice v)
            {
                return new ListViewItem(new string[] { v.lang, v.name, v.version });
            }

            private bool GetVoiceInfo (string id, out string lang, out string name, out string version)
            {
                lang = "";
                name = "";
                version = "";

                int lastSep = id.LastIndexOf('\\');
                if (lastSep == -1)
                {
                    Console.WriteLine("id contains no \\ char: " + id);
                    return false;
                }
                else
                {
                    int remainingLength = id.Length - (lastSep + 1);
                    if (remainingLength < 7)
                    {
                        // id cannot possibly satisfy the regex
                        Console.WriteLine("id can't satisfy regex: " + id);
                        return false;
                    }
                    else
                    {
                        Match m = voiceInfoRegex.Match(id, lastSep + 1);
                        if (!m.Success)
                        {
                            Console.WriteLine("id did not satisfy regex: " + id);
                            return false;
                        }

                        lang = m.Groups["lang"].ToString();
                        name = m.Groups["name"].ToString();
                        version = m.Groups["version"].ToString();
                        //Console.WriteLine("id satisfied regex, lang {0}, name {1}", lang, name);
                        return true;
                    }
                }
            }
        }
        #endregion

        #region Fields and properties
        private NativeWindow thisWindow;

        private readonly TimeSpan tts_max_response_time = TimeSpan.FromSeconds(3);
        
        private const int listView_voice_min_column_width = 60;

        private SpVoice voice;
        private List<Voice> voices;

        public enum SpeechStatus { Idle, Starting, Speaking, Stopping, SwitchingVoice };

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

            voice = new SpVoice();
            voice.StartStream += Voice_StartStream;
            voice.EndStream += Voice_EndStream;
            voice.Word += Voice_Word;

            UpdateVoiceList();
            setupDone = true;

            Console.WriteLine("Setup done");
            Console.WriteLine();
        }

        private void UpdateVoiceList ()
        {
            voices = new List<Voice>();

            ISpeechObjectTokens _vs = voice.GetVoices();
            for (int i = 0; i < _vs.Count; i++)
            {
                voices.Add(new Voice(_vs.Item(i)));
            }

            foreach (Voice v in voices)
            {
                //Console.WriteLine("---");
                //Console.WriteLine("Voice: " + v.spot.Id);
                //Console.WriteLine("InfoFound: " + v.infoFound);
                //Console.WriteLine("Lang: " + v.lang);
                //Console.WriteLine("Name: " + v.name);
                //Console.WriteLine("Version: " + v.version);

                listView_voice.Items.Add((ListViewItem)v);
            }

            Voice currentVoice = new Voice(voice.Voice);
            ListViewItem currentListViewItem = listView_voice.Items.Cast<ListViewItem>().First(v => v.SubItems[1].Text == currentVoice.name);
            currentListViewItem.Selected = true;

            Console.WriteLine("Initially selected voice: {0}", currentListViewItem.SubItems[1].Text);

            listView_voice.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            foreach (ColumnHeader c in listView_voice.Columns)
            {
                c.Width = (c.Width < listView_voice_min_column_width ? listView_voice_min_column_width : c.Width);
            }
        }
        #endregion

        #region TTS events
        private void Voice_Word (int StreamNumber, object StreamPosition, int CharacterPosition, int Length)
        {
            if (Status == SpeechStatus.SwitchingVoice)
                return;

            CurrentWord = SpeechText.Substring(CharacterPosition, Length);
            CurrentIndex = CurrentOffset + CharacterPosition;

            richTextBox_text.SelectionStart = CurrentIndex;
            richTextBox_text.SelectionLength = Length;
        }

        private void Voice_EndStream (int StreamNumber, object StreamPosition)
        {
            Console.WriteLine("EndStream event");

            if (Status != SpeechStatus.SwitchingVoice)
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

        private void Voice_StartStream (int StreamNumber, object StreamPosition)
        {
            Console.WriteLine("StartStream event");

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
            if (Status != SpeechStatus.SwitchingVoice)
                Status = SpeechStatus.Stopping;

            Console.WriteLine("StopSpeaking called");

            // Reset handle
            stoppedSpeakingHandle.Reset();
            voice.Skip("Sentence", int.MaxValue);

            try
            {
                await stoppedSpeakingHandle.AsTask(timeout: tts_max_response_time);
                
                Console.WriteLine("Stopped speaking");

                if (Status != SpeechStatus.SwitchingVoice)
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
            if (Status != SpeechStatus.SwitchingVoice)
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
            voice.Speak(SpeechText, SpeechVoiceSpeakFlags.SVSFlagsAsync);

            try
            {
                await startedSpeakingHandle.AsTask(timeout: tts_max_response_time);
                
                Console.WriteLine("Started speaking");

                if (Status != SpeechStatus.SwitchingVoice)
                    Status = SpeechStatus.Speaking;
            }
            catch (TaskCanceledException)
            {
                // Task timed out
                throw new TimeoutException("TTS engine took too long to start speaking.");
            }
        }

        private async void SwitchVoice (Voice v)
        {
            Console.WriteLine("SwitchVoice to {0}", v.name);

            switch (Status)
            {
                case SpeechStatus.Idle:
                    // Set the voice
                    voice.Voice = v.spot;
                    break;
                case SpeechStatus.Starting:
                    throw new InvalidOperationException("SwitchVoice was somehow called while Status == Starting");
                case SpeechStatus.Speaking:
                    Status = SpeechStatus.SwitchingVoice;
                    CurrentOffset = CurrentIndex;
                    // Stop speaking
                    await StopSpeaking();

                    // Set the voice
                    voice.Voice = v.spot;

                    // Start speaking
                    await StartSpeaking(CurrentOffset);

                    Status = SpeechStatus.Speaking;
                    break;
                case SpeechStatus.Stopping:
                    throw new InvalidOperationException("SwitchVoice was somehow called while Status == Stopping");
                case SpeechStatus.SwitchingVoice:
                    throw new InvalidOperationException("SwitchVoice was somehow called while Status == SwitchingVoice");
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
                default:
                    throw new ArgumentOutOfRangeException("Status");
            }
        }

        private void listView_voice_ItemSelectionChanged (object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (setupDone && e.IsSelected)
            {
                ResetFocus();

                Voice selectedVoice = voices.First(v => v.name == e.Item.SubItems[1].Text);
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

        private void trackBar_rate_MouseMove (object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MoveTrackBarToMouseClickLocation(trackBar_rate, e.X);
            }
        }

        private void trackBar_rate_MouseDown (object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MoveTrackBarToMouseClickLocation(trackBar_rate, e.X);
            }
        }
        #endregion

        private void trackBar_rate_ValueChanged (object sender, EventArgs e)
        {
            Console.WriteLine("Trackbar value changed: {0}", trackBar_rate.Value);
        }

        private void trackBar_rate_MouseUp (object sender, MouseEventArgs e)
        {
            if (trackBar_rate.Value != voice.Rate)
            {
                Console.WriteLine("Apply rate change {0} -> {1}", voice.Rate, trackBar_rate.Value);
                voice.Rate = trackBar_rate.Value;
            }
        }
    }
}
