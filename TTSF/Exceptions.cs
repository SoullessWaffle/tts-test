namespace TTSF
{
    namespace Exceptions
    {
        public class InvalidVoiceException : System.Exception
        {
            public InvalidVoiceException () { }
            public InvalidVoiceException (string voicename) : base(FormatMessage(voicename)) { }
            public InvalidVoiceException (string voicename, System.Exception inner) : base(FormatMessage(voicename), inner) { }
            protected InvalidVoiceException (
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
            private static string FormatMessage (string voicename)
            {
                return "Unable to select voice \"" + voicename + "\".";
            }
        }
    }
}
