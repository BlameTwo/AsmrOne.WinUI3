using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.Models.Enums;

namespace AsmrOne.WinUI3.Contracts;

public interface IAsmrClient
{
    public bool IsLogin { get; }

    public HttpClient Client { get; set; }
    public string UserName { get; set; }
    Task<(RegisterReponse, string)> RegisterAsync(
        string userName,
        string password,
        CancellationToken token = default
    );

    Task<(RegisterReponse, string)> LoginAsync(
        string userName,
        string password,
        CancellationToken token = default
    );

    Task<WorksResponse> GetWorksAsync(
        WorkOrder order,
        int page,
        bool isSubtitle,
        CancellationToken token = default
    );

    Task<(RidDetily, string)> GetWorkAsync(string rj, CancellationToken token = default);
    Task<WorksResponse> GetPopularAsync(
        bool isSubtitle,
        int index,
        CancellationToken token = default
    );
    Task<(List<Child>, string)> GetWorkAudioAsync(string rj, CancellationToken token = default);
    Task<WorksResponse> GetMyFavouritesAsync(
        FavouritesType type,
        MyFavouritesOrder order,
        MyFarouritesSort sort,
        MyFarouritesFilter fillter = MyFarouritesFilter.None,
        int page = 1,
        CancellationToken token = default
    );
    void RegisterClient(string hostName);
    void SetToken(RegisterReponse token);
    void Loginout();
}
