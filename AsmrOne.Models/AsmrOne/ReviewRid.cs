using System.Text.Json.Serialization;

namespace AsmrOne.WinUI3.Models.AsmrOne;

public class ReviewRidRequest
{
    [JsonPropertyName("work_id")]
    public long WorkId { get; set; }

    [JsonPropertyName("progress")]
    public string Progress { get; set; }
}

public class ReviewRidReponse
{
    [JsonPropertyName("message")]
    public string Message { get; set; }
}
