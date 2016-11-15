using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Speech.AudioFormat;
using CommandLine;
using CommandLine.Text;

namespace TTSF
{
    public static class ExitCodeExtensions
    {
        public static string Message (this Program.ExitCode exitCode, Exception e = null)
        {
            switch (exitCode)
            {
                case Program.ExitCode.Success:
                    return "Success";
                case Program.ExitCode.InvalidArgument:
                    return "Invalid Argument Exception";
                case Program.ExitCode.InvalidVoice:
                    return "Invalid Voice Exception";
                case Program.ExitCode.IOException:
                    return "IO Exception";
                case Program.ExitCode.UnhandledException:
                    return "Unhandled Exception" + (e == null ? "" : "\n" + e.ToString());
                default:
                    throw new ArgumentOutOfRangeException("exitCode");
            }
        }
    }

    public class Options
    {
        private readonly string usageString1 = $"Usage:\t{Program.Name} -t \"Hello World!\" -o hello.wav --overwrite";
        private readonly string usageString2 = $"\t{Program.Name} -i text.txt -o speech.wav --overwrite";

        [Option('i', "inputfile", HelpText = "The path of the file to read text from. Required if the text argument is not specified.")]
        public string InputFile { get; set; }

        [Option('t', "text", HelpText = "The text to speak. Required if the inputfile argument is not specified.")]
        public string Text { get; set; }

        [Option('o', "outputfile", Required = true, HelpText = "The path of the file to output speech to.")]
        public string OutputFile { get; set; }

        [Option("overwrite", HelpText = "Overwrite the output file if it already exists. False by default.")]
        public bool OverwriteOutputFile { get; set; }

        [Option('r', "rate", HelpText = "The rate (speed) of the speech. An integer value from -10 to 10.")]
        public int Rate { get; set; }

        [Option('v', "volume", DefaultValue = 100, HelpText = "The volume of the speech. An integer value from 0 to 100.")]
        public int Volume { get; set; }

        [Option("voice", HelpText = "The name of the voice to use.")]
        public string Voice { get; set; }

        [HelpOption]
        public string GetUsage ()
        {
            HelpText help = new HelpText
            {
                Heading = new HeadingInfo(Program.Name, Program.Version.ToString()),
                Copyright = new CopyrightInfo("Soullesswaffle", 2016),
                AdditionalNewLineAfterOption = true,
                AddDashesToOption = true
            };

            help.AddPreOptionsLine("MIT License");
            help.AddPreOptionsLine("");

            help.AddPreOptionsLine(usageString1);
            help.AddPreOptionsLine(usageString2);
            help.AddOptions(this);

            return help;
        }

        public string GetUsageWithoutMetadata ()
        {
            HelpText help = new HelpText
            {
                AdditionalNewLineAfterOption = true,
                AddDashesToOption = true
            };

            help.AddPreOptionsLine(usageString1);
            help.AddPreOptionsLine(usageString2);
            help.AddOptions(this);

            return help;
        }
    }

    public class OptionValidator
    {
        public readonly Options Options;

        public OptionValidator (Options options)
        {
            this.Options = options;
        }

        public bool ValidateArguments (out string text)
        {
            text = "";

            // Validate Rate
            if (Options.Rate < -10 || Options.Rate > 10)
            {
                Console.WriteLine(GetInvalidValueText(nameof(Options.Rate)));
                return false;
            }

            // Validate Volume
            if (Options.Volume < 0 || Options.Volume > 100)
            {
                Console.WriteLine(GetInvalidValueText(nameof(Options.Volume)));
                return false;
            }

            // Validate output file path
            bool outputPathExists;
            if (!ValidatePath(Options.OutputFile, out outputPathExists))
            {
                Console.WriteLine(GetInvalidValueText(nameof(Options.OutputFile)));
                return false;
            }
            else if (outputPathExists && !Options.OverwriteOutputFile)
            {
                Console.WriteLine(GetInvalidValueText(nameof(Options.OutputFile)));
                Console.WriteLine("Output file already exists. If you want to overwrite it, use the --overwrite flag.");
                return false;
            }

            // If Text option is defined
            if (!string.IsNullOrWhiteSpace(Options.Text))
            {
                // If InputFile option is also defined
                if (!string.IsNullOrWhiteSpace(Options.InputFile))
                {
                    Console.WriteLine("Please provide either a text or inputfile argument, but not both.");
                    return false;
                }
                else if (ValidateText(Options.Text))
                {
                    // Use Text argument
                    text = Options.Text;
                }
                else
                {
                    // Text is not usable
                    Console.WriteLine(GetInvalidValueText(nameof(Options.Text)));
                    Console.WriteLine("Text argument does not contain usable text.");
                    return false;
                }
            }
            // If InputFile option is defined (but Text option is not)
            else if (!string.IsNullOrWhiteSpace(Options.InputFile))
            {
                bool inputPathExists;
                if (ValidatePath(Options.InputFile, out inputPathExists))
                {
                    if (inputPathExists)
                    {
                        // Validate input file contents
                        string fileContents = ReadTextFile(Options.InputFile);
                        if (ValidateText(fileContents))
                        {
                            // Use file contents
                            text = fileContents;
                        }
                        else
                        {
                            // File contents are not usable
                            Console.WriteLine(GetInvalidValueText(nameof(Options.InputFile)));
                            Console.WriteLine("Input file does not contain usable text.");
                            return false;
                        }
                    }
                    else
                    {
                        // File does not exist
                        Console.WriteLine(GetInvalidValueText(nameof(Options.InputFile)));
                        Console.WriteLine("Input file not found.");
                        return false;
                    }
                }
                else
                {
                    // Path is not valid
                    Console.WriteLine(GetInvalidValueText(nameof(Options.InputFile)));
                    return false;
                }
            }
            // If neither Text or InputFile option is defined
            else
            {
                Console.WriteLine("Please provide either a text or inputfile argument.");
                return false;
            }

            // All options are valid
            return true;
        }

        private static string ReadTextFile (string filepath)
        {
            // TODO: Add IO Exception handling
            string contents = "";
            using (System.IO.StreamReader streamReader = new System.IO.StreamReader(System.IO.Path.GetFullPath(filepath), Encoding.UTF8, true))
            {
                contents = streamReader.ReadToEnd();
            }

            return contents;
        }

        private static bool ValidateText (string text)
        {
            // TODO: This is just a placeholder. Consider better text validation.
            return !string.IsNullOrWhiteSpace(text);
        }

        private bool ValidatePath (string filepath, out bool exists)
        {
            System.IO.FileInfo fi = null;
            try
            {
                fi = new System.IO.FileInfo(System.IO.Path.GetFullPath(filepath));
            }
            catch (ArgumentException) { }
            catch (System.IO.PathTooLongException) { }
            catch (NotSupportedException) { }
            if (ReferenceEquals(fi, null))
            {
                // The path is invalid; return false
                exists = false;
                return false;
            }
            else
            {
                exists = fi.Exists;
                return true;
            }
        }

        private string GetInvalidValueText (string optionname)
        {
            string value;
            try
            {
                value = Convert.ToString(Options.GetType().GetProperty(optionname).GetValue(Options));
            }
            catch (NullReferenceException e)
            {
                throw new ArgumentException("optionname is invalid", "optionname", e);
            }

            string helpText;
            if (GetHelpText(optionname, out helpText))
                return $"Invalid value of {value} for argument {optionname}.\nDefinition: {helpText}";
            else
                return $"Invalid value of {value} for argument {optionname}.";
        }

        private bool GetHelpText (string optionname, out string helpText)
        {
            try
            {
                OptionAttribute oa = (OptionAttribute)System.Attribute.GetCustomAttribute(Options.GetType().GetProperty(optionname), typeof(OptionAttribute));
                helpText = oa.HelpText;
                return true;
            }
            catch (ArgumentNullException)
            {
                helpText = "";
                return false;
            }
        }
    }

    public class Program
    {
        private static readonly Lazy<string> _name = new Lazy<string>(() => System.AppDomain.CurrentDomain.FriendlyName);
        public static string Name
        {
            get
            {
                return _name.Value;
            }
        }

        private static readonly Lazy<System.Version> _version = new Lazy<System.Version>(() => System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
        public static System.Version Version
        {
            get
            {
                return _version.Value;
            }
        }

        private static System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

        public static int Main (string[] args)
        {
            sw.Start();

            try
            {
                Options options = new Options();

                if (args.Contains("--help"))
                {
                    Console.WriteLine(options.GetUsage());
                    return Exit(ExitCode.Success);
                }

                if (CommandLine.Parser.Default.ParseArguments(args, options))
                {
                    OptionValidator validator = new OptionValidator(options);
                    string text;
                    if (validator.ValidateArguments(out text))
                    {
                        try
                        {
                            WriteSpeechFile(options.OutputFile, text, options.Voice, options.Rate, options.Volume);
                        }
                        catch (Exceptions.InvalidVoiceException)
                        {
                            return Exit(ExitCode.InvalidVoice);
                        }
                    }
                    else
                    {
                        Console.WriteLine(options.GetUsageWithoutMetadata());
                        return Exit(ExitCode.InvalidArgument);
                    }
                }
                else
                {
                    // When ParseArguments fails it automatically prints usage text for us, so there is no need to do that here.
                    return Exit(ExitCode.InvalidArgument);
                }

                return Exit(ExitCode.Success);
            }
            catch (Exception e)
            {
                return Exit(ExitCode.UnhandledException, e);
            }
        }

        private static int Exit (ExitCode exitCode, Exception e = null)
        {
            sw.Stop();
            Console.WriteLine($"Took {sw.ElapsedMilliseconds}ms");
            Console.WriteLine($"Exit code {(int)exitCode}: {exitCode.Message(e)}");

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine();
                Console.WriteLine("Press enter to exit...");
                Console.ReadLine();
            }
            
            return (int)exitCode;
        }

        public enum ExitCode
        {
            Success = 0,
            InvalidArgument = -1,
            InvalidVoice = -2,
            IOException = -3,
            UnhandledException = -4
        }

        private static void WriteSpeechFile (string filepath, string text, string voicename, int rate, int volume)
        {
            using (SpeechSynthesizer synth = new SpeechSynthesizer())
            {
                // TODO: Add IO Exception handling and test it. Maybe on a file that is read-only.
                synth.SetOutputToWaveFile(System.IO.Path.GetFullPath(filepath), new SpeechAudioFormatInfo(32000, AudioBitsPerSample.Sixteen, AudioChannel.Mono));

                if (!string.IsNullOrEmpty(voicename))
                {
                    try
                    {
                        synth.SelectVoice(voicename);
                    }
                    catch (ArgumentException e)
                    {
                        throw new Exceptions.InvalidVoiceException(voicename, e);
                    }
                }

                synth.Rate = rate;
                synth.Volume = volume;
                
                synth.Speak(text);
            }
        }
    }
}
