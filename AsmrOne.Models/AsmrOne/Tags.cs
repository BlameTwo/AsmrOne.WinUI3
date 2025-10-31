using System.Collections.Generic;
using System.Text.Json.Serialization;
using WinRT;

namespace AsmrOne.WinUI3.Models.AsmrOne;

[Windows.UI.Xaml.Data.Bindable]
public partial class TagList
{
    [JsonPropertyName("id")]
    public object Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("i18n")]
    public I18n I18n { get; set; }

    [JsonPropertyName("count")]
    public int Count { get; set; }
}
