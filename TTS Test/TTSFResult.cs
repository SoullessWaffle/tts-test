using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTS_Test
{
    [Serializable]
    public struct TTSFResult
    {
        public bool Success;
        public bool Finished;
        public IOHandler.ExitCode? ExitCode;
        public string Output;
        public string Error;
        public Exception Exception;
        public string Arguments;

        public override string ToString ()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Success: {Success}");
            sb.AppendLine($"Finished: {Finished}");
            sb.AppendLine($"ExitCode: {(ExitCode != null ? ExitCode.ToString() : "null")}");
            sb.AppendLine($"Output: {Output}");
            sb.AppendLine($"Error: {Error}");
            sb.AppendLine($"Arguments: {Arguments}");

            return sb.ToString();
        }
    }
}
