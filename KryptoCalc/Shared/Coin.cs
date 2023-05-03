using System.ComponentModel.DataAnnotations;
namespace KryptoCalc.Shared;


public record Coin
{
    [Key]
    [MaxLength(100)]
    public required string Id { get; set; }
    [MaxLength(100)]
    public required string Symbol { get; set; }
    [MaxLength(100)]
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime UpdateTime { get; set; }
}

