namespace Api.Domain.Dtos;

public class CommissionDetailsDto
{
    public int SalesmanId { get; set; }
    public string SalesmanName { get; set; }
    
    public string Class { get; set; }

    public int AudiSale { get; set; }

    public int JaguarSale { get; set; }

    public int LandRoverSale { get; set; }

    public int RenaultSale { get; set; }
    public decimal AnnualSaleAmount { get; set; }
}