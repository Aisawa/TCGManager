using TCGManager.Domain.Common;

namespace TCGManager.Domain.Entities;

public class UserProduct : BaseEntity
{
    public string UserId { get; set; } = string.Empty;
    public long ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; } = 1;
    public int OpenedQuantity { get; set; } = 0;
    public DateTime AcquiredAt { get; set; } = DateTime.UtcNow;
}
