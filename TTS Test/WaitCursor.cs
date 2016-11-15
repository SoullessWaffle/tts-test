using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTS_Test
{
    class WaitCursor : IDisposable
    {
        public WaitCursor ()
        {
            Application.UseWaitCursor = true;
        }

        public void Dispose ()
        {
            Dispose(true);
        }

        private bool _isDisposed = false;
        protected virtual void Dispose (bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    Application.UseWaitCursor = false;
                }

                _isDisposed = true;
            }
        }
    }
}
