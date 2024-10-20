namespace AsmrOne.WinUI3.Models.Messagers;

public class RefreshToken
{
    public bool IsRefresh { get; set; }

    public RefreshToken(bool isRefresh)
    {
        IsRefresh = isRefresh;
    }
}
