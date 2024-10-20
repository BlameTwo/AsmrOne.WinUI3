﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;

namespace AsmrOne.WinUI3.Contracts.Services;

public class AsmrClient : IAsmrClient
{
    HttpClient _client = null;
    private string token;

    public bool IsLogin { get; set; } = false;
    public string HostName { get; private set; }
    public string Token
    {
        get { return this.token; }
        set
        {
            this.token = value;
            IsLogin = true;
        }
    }
    public string CurrentId { get; private set; }

    private static async Task<long> Ping(string host)
    {
        Ping ping = new();
        PingReply reply = await ping.SendPingAsync(host);
        if (reply.Status == IPStatus.Success)
        {
            return reply.RoundtripTime;
        }
        return -1;
    }

    public static async Task<ObservableCollection<PingResult>> GetPingAsync()
    {
        ObservableCollection<PingResult> result = new();
        foreach (var item in IpHost)
        {
            result.Add(new Models.PingResult() { HostName = item, Time = await Ping(item) });
        }
        return result;
    }

    public static readonly List<string> IpHost = new List<string>()
    {
        "asmr-300.com",
        "asmr-200.com",
        "asmr-100.com",
        "asmr.one",
    };

    public async Task<(RegisterReponse, string)> RegisterAsync(string userName, string password)
    {
        var requestModel = new RegisterUserRequest()
        {
            Name = userName,
            Password = password,
            RecommenderUuid = Guid.NewGuid().ToString().ToLower(),
        };
        var content = JsonSerializer.Serialize(
            requestModel,
            JsonContext.Default.RegisterUserRequest
        );
        var request = new HttpRequestMessage(HttpMethod.Post, $"{HostName}/api/auth/reg");
        request.Content = new StringContent(content, Encoding.UTF8, "application/json");
        var result = await _client.SendAsync(request);
        var str = await result.Content.ReadAsStringAsync();
        var jsonObj = JsonObject.Parse(str);
        if (jsonObj["error"] != null)
        {
            return new(null, jsonObj["error"].GetValue<string>());
        }
        var value = JsonSerializer.Deserialize(jsonObj, JsonContext.Default.RegisterReponse);
        return new(value, "注册成功");
    }

    public async Task<(RegisterReponse, string)> LoginAsync(string userName, string password)
    {
        var requestModel = new LoginUser() { Name = userName, Password = password };
        var content = JsonSerializer.Serialize(requestModel, JsonContext.Default.LoginUser);
        var request = new HttpRequestMessage(HttpMethod.Post, $"{HostName}/api/auth/me");
        request.Content = new StringContent(content, Encoding.UTF8, "application/json");
        var result = await _client.SendAsync(request);
        var str = await result.Content.ReadAsStringAsync();
        var jsonObj = JsonObject.Parse(str);
        if (jsonObj["error"] != null)
        {
            return new(null, jsonObj["error"].GetValue<string>());
        }
        var value = JsonSerializer.Deserialize(jsonObj, JsonContext.Default.RegisterReponse);
        return new(value, "登录成功");
    }

    public void RegisterClient(string hostName)
    {
        this.BuildClient(hostName);
    }

    private void BuildClient(string hostName)
    {
        this._client = new HttpClient();
        _client.DefaultRequestHeaders.Add(
            "User-Accept",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/130.0.0.0 Safari/537.36 Edg/130.0.0.0"
        );
        this.HostName = ("https://api." + hostName);
    }

    public void SetToken(RegisterReponse item1)
    {
        this.Token = item1.Token;
        this.CurrentId = item1.User.RecommenderUuid;
    }

    public async Task<WorksResponse> GetWorksAsync(WorkOrder order, int page, bool isSubtitle)
    {
        var orderDict = order.GetValue();
        var queryValues = new Dictionary<string, object>()
        {
            { "page", page },
            { "subtitle", isSubtitle ? 1 : 0 },
            { "withPlaylistStatus[]", this.CurrentId },
        };
        foreach (var item in orderDict)
        {
            queryValues.Add(item.Key, item.Value);
        }
        var request = BuildRequest(
            $"{HostName}/api/works",
            HttpMethod.Get,
            queryValues,
            null,
            true
        );

        var response = await _client.SendAsync(request);
        var result = await CheckDataAsync<WorksResponse>(
            response,
            JsonContext.Default.WorksResponse
        );
        if (result.Item1 == null)
        {
            throw new Exception("错误！");
        }
        return result.Item1;
    }

    public async Task<(RidDetily, string)> GetWorkAsync(string rj)
    {
        //https://api.asmr-300.com/api/workInfo/01249781
        var request = this.BuildRequest($"{HostName}/api/workInfo/{rj}", HttpMethod.Get);
        var response = await _client.SendAsync(request);
        var result = await CheckDataAsync<RidDetily>(response, JsonContext.Default.RidDetily);
        if (result.Item1 == null)
        {
            throw new Exception("错误！");
        }
        return result;
    }

    public async Task<(List<Child>, string)> GetWorkAudioAsync(string rj)
    {
        var request = this.BuildRequest($"{HostName}/api/tracks/{rj}?v=1", HttpMethod.Get);
        var response = await _client.SendAsync(request);
        var result = await CheckDataAsync<List<Child>>(response, JsonContext.Default.ListChild);
        if (result.Item1 == null)
        {
            throw new Exception("错误！");
        }
        return result;
    }

    public HttpRequestMessage BuildRequest(
        string path,
        HttpMethod method,
        Dictionary<string, object> values = null,
        string content = null,
        bool isToken = false
    )
    {
        var request = new HttpRequestMessage();
        request.Method = method;
        var resultPath = path;
        if (method == HttpMethod.Post)
        {
            request.Content = new StringContent(content, Encoding.UTF8, "application/json");
        }
        if (method == HttpMethod.Get && values != null)
        {
            var str = values.Select(x => $"{x.Key}={x.Value}");
            var query = string.Join("&", str);
            resultPath += $"?{query}";
        }
        request.RequestUri = new Uri(resultPath);
        if (isToken && !string.IsNullOrWhiteSpace(this.Token))
        {
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer",
                this.Token
            );
        }
        return request;
    }

    public async Task<(T?, string)> CheckDataAsync<T>(
        HttpResponseMessage message,
        JsonTypeInfo info
    )
        where T : class, new()
    {
        var str = await message.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(str))
            return new(null, "错误的请求");
        var jsonObj = JsonNode.Parse(str);
        if (jsonObj is JsonArray js)
        {
            var obj = (T)js.Deserialize(info);
            return (obj, "请求成功");
        }
        if (jsonObj["error"] != null)
        {
            return new(null, jsonObj["error"].GetValue<string>());
        }
        var value = (T)JsonSerializer.Deserialize(jsonObj, info);
        return (value, "请求成功");
    }
}
