using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AsmrOne.WinUI3.Models.AsmrOne
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class Circle
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("source_id")]
        public string SourceId { get; set; }

        [JsonPropertyName("source_type")]
        public string SourceType { get; set; }
    }

    public class EnUs
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("history")]
        public List<History> History { get; set; }

        [JsonPropertyName("censored")]
        public string Censored { get; set; }
    }

    public class History
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("deprecatedAt")]
        public long DeprecatedAt { get; set; }
    }

    public class I18n
    {
        [JsonPropertyName("en-us")]
        public EnUs EnUs { get; set; }

        [JsonPropertyName("ja-jp")]
        public JaJp JaJp { get; set; }

        [JsonPropertyName("zh-cn")]
        public ZhCn ZhCn { get; set; }
    }

    public class JaJp
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("censored")]
        public string Censored { get; set; }
    }

    public class LanguageEdition
    {
        [JsonPropertyName("lang")]
        public string Lang { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("workno")]
        public string Workno { get; set; }

        [JsonPropertyName("edition_id")]
        public int EditionId { get; set; }

        [JsonPropertyName("edition_type")]
        public string EditionType { get; set; }

        [JsonPropertyName("display_order")]
        public int DisplayOrder { get; set; }
    }

    public class OtherLanguageEditionsInDb
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("lang")]
        public string Lang { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("source_id")]
        public string SourceId { get; set; }

        [JsonPropertyName("is_original")]
        public bool IsOriginal { get; set; }

        [JsonPropertyName("source_type")]
        public string SourceType { get; set; }
    }

    public class Pagination
    {
        [JsonPropertyName("currentPage")]
        public int CurrentPage { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }
    }

    public class PlaylistStatus
    {
        public object _3c9adebc188e4081B4853ef54e973803 { get; set; }
    }

    public class Rank
    {
        [JsonPropertyName("term")]
        public string Term { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("rank")]
        public int Ranks { get; set; }

        [JsonPropertyName("rank_date")]
        public string RankDate { get; set; }
    }

    public class RateCountDetail
    {
        [JsonPropertyName("review_point")]
        public int ReviewPoint { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("ratio")]
        public int Ratio { get; set; }
    }

    public class WorksResponse
    {
        [JsonPropertyName("works")]
        public List<RidDetily> Works { get; set; }

        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; set; }
    }

    public class Tag
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("i18n")]
        public I18n I18n { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class TranslationInfo
    {
        [JsonPropertyName("lang")]
        public string Lang { get; set; }

        [JsonPropertyName("is_child")]
        public bool IsChild { get; set; }

        [JsonPropertyName("is_parent")]
        public bool IsParent { get; set; }

        [JsonPropertyName("is_original")]
        public bool IsOriginal { get; set; }

        [JsonPropertyName("is_volunteer")]
        public bool IsVolunteer { get; set; }

        [JsonPropertyName("child_worknos")]
        public List<object> ChildWorknos { get; set; }

        [JsonPropertyName("parent_workno")]
        public string ParentWorkno { get; set; }

        [JsonPropertyName("original_workno")]
        public string OriginalWorkno { get; set; }

        [JsonPropertyName("is_translation_agree")]
        public bool IsTranslationAgree { get; set; }

        [JsonPropertyName("translation_bonus_langs")]
        public object TranslationBonusLangs { get; set; }

        [JsonPropertyName("is_translation_bonus_child")]
        public bool IsTranslationBonusChild { get; set; }

        [JsonPropertyName("production_trade_price_rate")]
        public int ProductionTradePriceRate { get; set; }
    }

    public class TranslationBonusLangs
    {
        [JsonPropertyName("price")]
        public int Price { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("price_tax")]
        public int PriceTax { get; set; }

        [JsonPropertyName("child_count")]
        public int ChildCount { get; set; }

        [JsonPropertyName("price_in_tax")]
        public int PriceInTax { get; set; }

        [JsonPropertyName("recipient_max")]
        public int RecipientMax { get; set; }

        [JsonPropertyName("recipient_available_count")]
        public int RecipientAvailableCount { get; set; }
    }

    public class Va
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class RidDetily : ObservableObject
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("circle_id")]
        public int CircleId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("nsfw")]
        public bool Nsfw { get; set; }

        [JsonPropertyName("release")]
        public string Release { get; set; }

        [JsonPropertyName("dl_count")]
        public int DlCount { get; set; }

        [JsonPropertyName("price")]
        public int Price { get; set; }

        [JsonPropertyName("review_count")]
        public int ReviewCount { get; set; }

        [JsonPropertyName("rate_count")]
        public int RateCount { get; set; }

        [JsonPropertyName("rate_average_2dp")]
        public double RateAverage2dp { get; set; }

        [JsonPropertyName("rate_count_detail")]
        public List<RateCountDetail> RateCountDetail { get; set; }

        [JsonPropertyName("rank")]
        public List<Rank> Rank { get; set; }

        [JsonPropertyName("has_subtitle")]
        public bool HasSubtitle { get; set; }

        [JsonPropertyName("create_date")]
        public string CreateDate { get; set; }

        [JsonPropertyName("vas")]
        public List<Va> Vas { get; set; }

        [JsonPropertyName("tags")]
        public List<Tag> Tags { get; set; }

        [JsonPropertyName("language_editions")]
        public List<LanguageEdition> LanguageEditions { get; set; }

        [JsonPropertyName("original_workno")]
        public string OriginalWorkno { get; set; }

        [JsonPropertyName("other_language_editions_in_db")]
        public List<OtherLanguageEditionsInDb> OtherLanguageEditionsInDb { get; set; }

        [JsonPropertyName("translation_info")]
        public TranslationInfo TranslationInfo { get; set; }

        [JsonPropertyName("work_attributes")]
        public string WorkAttributes { get; set; }

        [JsonPropertyName("age_category_string")]
        public string AgeCategoryString { get; set; }

        [JsonPropertyName("duration")]
        public double Duration { get; set; }

        [JsonPropertyName("source_type")]
        public string SourceType { get; set; }

        [JsonPropertyName("source_id")]
        public string SourceId { get; set; }

        [JsonPropertyName("source_url")]
        public string SourceUrl { get; set; }

        [JsonPropertyName("userRating")]
        public object UserRating { get; set; }

        [JsonPropertyName("playlistStatus")]
        public PlaylistStatus PlaylistStatus { get; set; }

        [JsonPropertyName("circle")]
        public Circle Circle { get; set; }

        [JsonPropertyName("samCoverUrl")]
        public string SamCoverUrl { get; set; }

        [JsonPropertyName("thumbnailCoverUrl")]
        public string ThumbnailCoverUrl { get; set; }

        [JsonPropertyName("mainCoverUrl")]
        public string MainCoverUrl { get; set; }
    }

    public class ZhCn
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("history")]
        public List<History> History { get; set; }

        [JsonPropertyName("censored")]
        public string Censored { get; set; }
    }
}
