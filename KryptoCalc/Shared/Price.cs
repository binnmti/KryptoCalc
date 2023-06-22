using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KryptoCalc.Shared;

public record Price
{
    [Key]
    [MaxLength(100)]
    public required string Id { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Usd { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Aed { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Ars { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Aud { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Bdt { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Bhd { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Bmd { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Brl { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Cad { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Chf { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Clp { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Cny { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Czk { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Dkk { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Eur { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Gbp { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Hkd { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Huf { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Idr { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Ils { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Inr { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Jpy { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Krw { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Kwd { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Lkr { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Mmk { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Mxn { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Myr { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Ngn { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Nok { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Nzd { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Php { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Pkr { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Pln { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Rub { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Sar { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Sek { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Sgd { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Thb { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Try { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Twd { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Uah { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Vef { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Vnd { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Zar { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Xdr { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Xag { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Xau { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Bits { get; set; }
    [Column(TypeName = "decimal(20,4)")]
    public decimal Sat { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime UpdateTime { get; set; }
}
