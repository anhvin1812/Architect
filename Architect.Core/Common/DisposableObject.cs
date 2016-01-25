using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architect.Core.Common
{
    public abstract class DisposableObject: IDisposable
    {
        protected DisposableObject()
        {
            Disposables = new List<IDisposable>();
        }

        protected void Dispose(bool isDisposing)
        {
            if(_isDisposed) return;

            _isDisposed = true;

            if (isDisposing)
            {
                foreach (var disposable in Disposables)
                {
                    disposable.Dispose();
                }
            }
        }

        private void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        void IDisposable.Dispose()
        {
            Dispose();
        }

        private bool _isDisposed = false;

        protected IList<IDisposable> Disposables { get; private set; }
    }
}
