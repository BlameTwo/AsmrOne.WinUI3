using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.Models.ItemWrapper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AsmrOne.Core;

partial class AsmrClient
{
    public async Task<List<TagList>> GetTagAsync(TagType tags, CancellationToken token = default)
    {
        var request = this.BuildRequest($"{HostName}/api/{tags.Memory}/", HttpMethod.Get, null, null, true);
        var reponse = await this.Client.SendAsync(request);
        var result = await this.CheckDataAsync<List<TagList>>(
            reponse,
            JsonContext.Default.ListTagList,
            token
        );
        return result.Item1;
    }


    public async Task<WorksResponse> SearchAsync(IEnumerable<SearchTagWrapper> query, WorkOrder order,int page, int pageSize,bool isSubtitle,CancellationToken token = default)
    {
        var queryString = BuildSearchQuery(query);
        var request = this.BuildRequest($"{HostName}/api/search/{queryString}",HttpMethod.Get,new Dictionary<string, object>()
        {
            {"order","create_date" },
            {"pageSize",pageSize },
            {"page",page },
            {"sort","desc" },
        },null,true);
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
        if (result.Item1 == null)
        {
            throw new Exception("错误！");
        }
        return result.Item1;
    }

    private string BuildSearchQuery(IEnumerable<SearchTagWrapper> query)
    {
        string value = " ";
        foreach (var item in query)
        {
            value += $"${item.Type}:{item.Name}$ ";
        }
        return value;
    }
}
