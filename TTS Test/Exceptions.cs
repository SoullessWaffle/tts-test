namespace TTS_Test
{
    public class UnhandledTTSFException : System.Exception
    {
        public UnhandledTTSFException () { }
        public UnhandledTTSFException (TTSFResult result) : base(result.ToString(), result.Exception)
        {
            this.Data.Add("TTSFResult", result);
        }
        public UnhandledTTSFException (TTSFResult result, System.Exception inner) : base(result.ToString(), inner)
        {
            this.Data.Add("TTSFResult", result);
        }
        protected UnhandledTTSFException (
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
