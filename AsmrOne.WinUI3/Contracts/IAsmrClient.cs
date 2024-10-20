using System.Collections.Generic;
using System.Threading.Tasks;
using AsmrOne.WinUI3.Models.AsmrOne;

namespace AsmrOne.WinUI3.Contracts;

public interface IAsmrClient
{
    public bool IsLogin { get; set; }
    Task<(RegisterReponse, string)> RegisterAsync(string userName, string password);

    Task<(RegisterReponse, string)> LoginAsync(string userName, string password);

    Task<WorksResponse> GetWorksAsync(WorkOrder order, int page, bool isSubtitle);

    Task<(RidDetily, string)> GetWorkAsync(string rj);

    Task<(List<Child>, string)> GetWorkAudioAsync(string rj);

    void RegisterClient(string hostName);
    void SetToken(RegisterReponse token);
}
