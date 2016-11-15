using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace TTS_Test
{
    public static class IOHandler
    {
        public const string TTSF = "TTSF.exe";
        private const int TTSFWaitMsPerChar = 3;

        public enum ExitCode
        {
            Success = 0,
            InvalidArgument = -1,
            InvalidVoice = -2,
            IOException = -3,
            UnhandledException = -4
        }

        private class TempTextFile : IDisposable
        {
            private readonly FileInfo _file;
            public FileInfo File { get { return _file; } }

            public TempTextFile (string contents)
            {
                string path = Path.GetTempFileName();

                using (StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8))
                {
                    sw.Write(contents);
                }

                this._file = new FileInfo(path);
            }

            #region IDisposable Support
            ~TempTextFile ()
            {
                Dispose(false);
            }

            public void Dispose ()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            private bool _isDisposed = false;
            protected virtual void Dispose (bool disposing)
            {
                if (!_isDisposed)
                {
                    _isDisposed = true;

                    TryDelete();
                }
            }

            private void TryDelete ()
            {
                try
                {
                    this.File.Delete();
                }
                catch (IOException) { }
                catch (UnauthorizedAccessException) { }
            }
            #endregion
        }

        public static async Task<TTSFResult> SaveSpeechFile (string filepath, string text, string voicename, int rate, int volume)
        {
            try
            {
                using (TempTextFile textFile = new TempTextFile(text))
                {
                    //int timeoutMs = 1;
                    int timeoutMs = text.Length * TTSFWaitMsPerChar;
                    string args = $"-i \"{textFile.File.FullName}\" -o \"{Path.GetFullPath(filepath)}\" --voice \"{voicename}\" -r {rate} -v {volume} --overwrite";
                    ProcessStartInfo procStartInfo = new ProcessStartInfo(TTSF, args)
                    {
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    };

                    using (Process proc = new Process())
                    {
                        proc.StartInfo = procStartInfo;

                        StringBuilder output = new StringBuilder();
                        proc.OutputDataReceived += new DataReceivedEventHandler((object sender, DataReceivedEventArgs e) =>
                        {
                            if (!string.IsNullOrEmpty(e.Data))
                            {
                                output.AppendLine(e.Data);
                                Console.WriteLine($"{TTSF} STDOUT: {e.Data}");
                            }
                        });

                        StringBuilder error = new StringBuilder();
                        proc.ErrorDataReceived += new DataReceivedEventHandler((object sender, DataReceivedEventArgs e) =>
                        {
                            if (!string.IsNullOrEmpty(e.Data))
                            {
                                error.AppendLine(e.Data);
                                Console.WriteLine($"{TTSF} STDERR: {e.Data}");
                            }
                        });

                        Console.WriteLine($"Starting {TTSF} with a timeout of {timeoutMs}ms and arguments {args}");

                        proc.Start();
                        proc.BeginOutputReadLine();
                        proc.BeginErrorReadLine();

                        using (CancellationTokenSource cts = new CancellationTokenSource(timeoutMs))
                        {
                            try
                            {
                                await proc.WaitForExitAsync(cts.Token);
                                Console.WriteLine($"{TTSF} finished with exit code {proc.ExitCode}: {((ExitCode)proc.ExitCode).ToString()}");
                                return new TTSFResult
                                {
                                    Success = proc.ExitCode == 0,
                                    Finished = true,
                                    ExitCode = (ExitCode)proc.ExitCode,
                                    Output = output.ToString(),
                                    Error = error.ToString(),
                                    Exception = null,
                                    Arguments = args
                                };
                            }
                            catch (TaskCanceledException)
                            {
                                Console.WriteLine($"{TTSF} did not finish in time, killing...");

                                try
                                {
                                    proc.Kill();
                                    Console.WriteLine($"{TTSF} killed succesfully");
                                    return new TTSFResult
                                    {
                                        Success = false,
                                        Finished = false,
                                        ExitCode = null,
                                        Output = output.ToString(),
                                        Error = error.ToString(),
                                        Exception = null,
                                        Arguments = args
                                    };
                                }
                                catch (Exception e) when (e is System.ComponentModel.Win32Exception || e is NotSupportedException || e is InvalidOperationException)
                                {
                                    Console.WriteLine($"Unable to kill {TTSF}:\n{e.ToString()}");
                                    return new TTSFResult
                                    {
                                        Success = false,
                                        Finished = false,
                                        ExitCode = null,
                                        Output = output.ToString(),
                                        Error = error.ToString(),
                                        Exception = e,
                                        Arguments = args
                                    };
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        public static async Task<string> LoadTextFile (string filepath)
        {
            // TODO: Add IO Exception handling
            string contents = "";
            using (System.IO.StreamReader streamReader = new System.IO.StreamReader(System.IO.Path.GetFullPath(filepath), Encoding.UTF8, true))
            {
                contents = await streamReader.ReadToEndAsync();
            }

            return contents;
        }
    }
}
