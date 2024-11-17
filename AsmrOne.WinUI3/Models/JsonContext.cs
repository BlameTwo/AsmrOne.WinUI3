using System.Collections.Generic;
using System.Text.Json.Serialization;
using AsmrOne.WinUI3.Models.AsmrOne;

namespace AsmrOne.WinUI3.Models;

[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default)]
[JsonSerializable(typeof(RegisterUserRequest))]
[JsonSerializable(typeof(RegisterReponse))]
[JsonSerializable(typeof(User))]
[JsonSerializable(typeof(LoginUser))]
[JsonSerializable(typeof(Child))]
[JsonSerializable(typeof(List<Child>))]
#region Works
[JsonSerializable(typeof(Circle))]
[JsonSerializable(typeof(EnUs))]
[JsonSerializable(typeof(History))]
[JsonSerializable(typeof(I18n))]
[JsonSerializable(typeof(JaJp))]
[JsonSerializable(typeof(LanguageEdition))]
[JsonSerializable(typeof(OtherLanguageEditionsInDb))]
[JsonSerializable(typeof(Pagination))]
[JsonSerializable(typeof(PlaylistStatus))]
[JsonSerializable(typeof(Rank))]
[JsonSerializable(typeof(RateCountDetail))]
[JsonSerializable(typeof(WorksResponse))]
[JsonSerializable(typeof(Tag))]
[JsonSerializable(typeof(TranslationInfo))]
[JsonSerializable(typeof(Va))]
[JsonSerializable(typeof(RidDetily))]
[JsonSerializable(typeof(ZhCn))]
[JsonSerializable(typeof(TranslationBonusLangs))]
[JsonSerializable(typeof(List<List<TranslationBonusLangs>>))]
[JsonSerializable(typeof(ReviewRidRequest))]
[JsonSerializable(typeof(ReviewRidReponse))]
#endregion
#region Popular
[JsonSerializable(typeof(PopularRequest))]
[JsonSerializable(typeof(TagList))]
[JsonSerializable(typeof(List<TagList>))]
[JsonSerializable(typeof(List<VasList>))]
[JsonSerializable(typeof(VasList))]
#endregion
public partial class JsonContext : JsonSerializerContext { }
