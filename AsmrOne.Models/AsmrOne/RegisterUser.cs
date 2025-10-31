using System.Text.Json.Serialization;

namespace AsmrOne.WinUI3.Models.AsmrOne;

public class RegisterUserRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("recommenderUuid")]
    public string RecommenderUuid { get; set; }
}

public class RegisterReponse
{
    [JsonPropertyName("user")]
    public User User { get; set; }

    [JsonPropertyName("token")]
    public string Token { get; set; }
}

public class User
{
    [JsonPropertyName("loggedIn")]
    public bool LoggedIn { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("group")]
    public string Group { get; set; }

    [JsonPropertyName("recommenderUuid")]
    public string RecommenderUuid { get; set; }
}
