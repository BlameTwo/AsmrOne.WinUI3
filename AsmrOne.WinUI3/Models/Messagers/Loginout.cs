namespace AsmrOne.WinUI3.Models.Messagers;

public class Loginout
{
    public bool IsRefresh { get; init; }

    public Loginout(bool isRefresh)
    {
        IsRefresh = isRefresh;
    }
}
