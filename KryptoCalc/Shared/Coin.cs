using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    [Column(TypeName = "decimal(20,4)")]
    public decimal Price { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime UpdateTime { get; set; }
}

