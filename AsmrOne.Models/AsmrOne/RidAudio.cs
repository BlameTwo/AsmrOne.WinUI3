using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AsmrOne.WinUI3.Models.AsmrOne;

public class Child
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("hash")]
    public string Hash { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("children")]
    public List<Child> Children { get; set; }

    [JsonPropertyName("work")]
    public Work Work { get; set; }

    [JsonPropertyName("workTitle")]
    public string WorkTitle { get; set; }

    [JsonPropertyName("mediaStreamUrl")]
    public string MediaStreamUrl { get; set; }

    [JsonPropertyName("mediaDownloadUrl")]
    public string MediaDownloadUrl { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("duration")]
    public double? Duration { get; set; }

    [JsonPropertyName("streamLowQualityUrl")]
    public string StreamLowQualityUrl { get; set; }
}

public class Work
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("source_id")]
    public string SourceId { get; set; }

    [JsonPropertyName("source_type")]
    public string SourceType { get; set; }
}
