using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.Models.ItemWrapper;

namespace AsmrOne.WinUI3.Contracts.Services;

partial class AsmrClient
{
    public async Task<List<TagList>> GetTagAsync(CancellationToken token = default)
    {
        var request = this.BuildRequest($"{HostName}/api/tags/", HttpMethod.Get, null, null, true);
        var reponse = await this.Client.SendAsync(request);
        var result = await this.CheckDataAsync<List<TagList>>(
            reponse,
            JsonContext.Default.ListTagList,
            token
        );
        return result.Item1;
    }

    public async Task<List<TagList>> GetCirclesAsync(CancellationToken token = default)
    {
        var request = this.BuildRequest($"{HostName}/api/circles/", HttpMethod.Get, null, null, true);
        var reponse = await this.Client.SendAsync(request);
        var result = await this.CheckDataAsync<List<TagList>>(
            reponse,
            JsonContext.Default.ListTagList,
            token
        );
        return result.Item1;
    }

    public async Task<List<VasList>> GetVasAsync(CancellationToken token = default)
    {
        var request = this.BuildRequest($"{HostName}/api/vas/", HttpMethod.Get, null, null, true);
        var reponse = await this.Client.SendAsync(request);
        var result = await this.CheckDataAsync<List<VasList>>(
            reponse,
            JsonContext.Default.ListVasList,
            token
        );
        return result.Item1;
    }

    public async Task<WorksResponse> SearchAsync(string keyword,IEnumerable<SearchTagWrapper> query,bool isSubtitle,CancellationToken token = default)
    {
        var queryString = BuildSearchQuery(query,keyword);
        return new WorksResponse();
    }

    private string BuildSearchQuery(IEnumerable<SearchTagWrapper> query,string keyword)
    {
        string value = " ";
        foreach (var item in query)
        {
            value += $"${item.Type}:{item.Name}$ ";
        }
        value += keyword;
        return value;
    }
}
