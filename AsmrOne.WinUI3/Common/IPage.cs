using System;

namespace AsmrOne.WinUI3.Common;

public interface IPage : IDisposable
{
    public Type PageType { get; }
}
