using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;

namespace AsmrOne.WinUI3.Contracts.Services;

partial class AsmrClient
{
    public async Task<WorksResponse> GetPopularAsync(
        bool isSubtitle,
        int index,
        CancellationToken token = default
    )
    {
        var PopularRequest = new PopularRequest()
        {
            Keyword = "",
            Page = index,
            Subtitle = isSubtitle == true ? 1 : 0,
            LocalSubtitledWorks = new(),
        };
        var jsonStr = JsonSerializer.Serialize(PopularRequest, JsonContext.Default.PopularRequest);
        var request = this.BuildRequest(
            $"{this.HostName}/api/recommender/popular",
            HttpMethod.Post,
            null,
            jsonStr,
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
