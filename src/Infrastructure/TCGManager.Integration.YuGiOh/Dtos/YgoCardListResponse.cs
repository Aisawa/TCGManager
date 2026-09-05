using System.Text.Json.Serialization;

namespace TCGManager.Integration.YuGiOh.Dtos;

public class YgoCardListResponse
{
    [JsonPropertyName("data")]
    public List<YgoCardDto> Data { get; set; } = [];

    [JsonPropertyName("meta")]
    public YgoMetaDto Meta { get; set; } = new();
}

public class YgoMetaDto
{
    [JsonPropertyName("total_rows")]
    public int TotalRows { get; set; }

    [JsonPropertyName("rows_returned")]
    public int RowsReturned { get; set; }

    [JsonPropertyName("next_page_offset")]
    public int? NextPageOffset { get; set; }
}