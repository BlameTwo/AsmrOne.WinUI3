using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AsmrOne.WinUI3.Models.AsmrOne;

public class PopularRequest
{
    [JsonPropertyName("keyword")]
    public string Keyword { get; set; }

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("subtitle")]
    public int Subtitle { get; set; }

    [JsonPropertyName("localSubtitledWorks")]
    public List<object> LocalSubtitledWorks { get; set; }
}
