using System.Text.Json.Serialization;

namespace TCGManager.Integration.YuGiOh.Dtos;

public class YgoSetDto
{
    [JsonPropertyName("set_name")]
    public string SetName { get; set; } = string.Empty;

    [JsonPropertyName("set_code")]
    public string SetCode { get; set; } = string.Empty;

    [JsonPropertyName("num_of_cards")]
    public int NumOfCards { get; set; }

    [JsonPropertyName("tcg_date")]
    public string? TcgDate { get; set; }

    [JsonPropertyName("set_image")]
    public string? SetImage { get; set; }
}