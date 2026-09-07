using System.Text.Json.Serialization;

namespace TCGManager.Integration.YuGiOh.Dtos;

public class YgoCardDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("desc")]
    public string? Desc { get; set; }

    [JsonPropertyName("atk")]
    public int? Atk { get; set; }

    [JsonPropertyName("def")]
    public int? Def { get; set; }

    [JsonPropertyName("level")]
    public int? Level { get; set; }

    [JsonPropertyName("race")]
    public string? Race { get; set; }

    [JsonPropertyName("attribute")]
    public string? Attribute { get; set; }

    [JsonPropertyName("card_sets")]
    public List<YgoCardSetRefDto> CardSets { get; set; } = [];

    [JsonPropertyName("card_images")]
    public List<YgoCardImageDto> CardImages { get; set; } = [];

    [JsonPropertyName("misc_info")]
    public List<YgoMiscInfoDto> MiscInfo { get; set; } = [];
}

public class YgoCardSetRefDto
{
    [JsonPropertyName("set_name")]
    public string SetName { get; set; } = string.Empty;

    [JsonPropertyName("set_code")]
    public string SetCode { get; set; } = string.Empty;

    [JsonPropertyName("set_rarity")]
    public string SetRarity { get; set; } = string.Empty;

    [JsonPropertyName("set_rarity_code")]
    public string SetRarityCode { get; set; } = string.Empty;

    [JsonPropertyName("set_price")]
    public string? SetPrice { get; set; }
}

public class YgoCardImageDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("image_url")]
    public string ImageUrl { get; set; } = string.Empty;
}

public class YgoMiscInfoDto
{
    [JsonPropertyName("name_fr")]
    public string? NameFr { get; set; }

    [JsonPropertyName("desc_fr")]
    public string? DescFr { get; set; }
}