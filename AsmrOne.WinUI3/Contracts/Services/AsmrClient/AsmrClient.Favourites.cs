using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.Models.Enums;

namespace AsmrOne.WinUI3.Contracts.Services;

partial class AsmrClient
{
    public async Task<WorksResponse> GetMyFavouritesAsync(
        FavouritesType type,
        MyFavouritesOrder order,
        MyFarouritesSort sort,
        MyFarouritesFilter fillter = MyFarouritesFilter.None,
        int page = 1,
        CancellationToken token = default
    )
    {
        var queryValues = FarouritesExtension.GetOrderBy(order, sort);
        if (type == FavouritesType.Progress)
        {
            foreach (var item in FarouritesExtension.GetFillter(fillter))
            {
                queryValues.Add(item.Key, item.Value);
            }
        }
        var request = BuildRequest(
            $"{HostName}/api/review",
            HttpMethod.Get,
            queryValues,
            null,
            true
        );
        var response = await Client.SendAsync(request, token);
        var result = await CheckDataAsync<WorksResponse>(
            response,
            JsonContext.Default.WorksResponse,
            token
        );
        if (result.Item1 == null)
        {
            throw new Exception("错误！");
        }
        return result.Item1;
    }
}
