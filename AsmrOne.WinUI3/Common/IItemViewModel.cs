using System;

namespace AsmrOne.WinUI3.Common
{
    public interface IItemViewModel<T> : IDisposable
    {
        public void SetData(T value);
    }
}
