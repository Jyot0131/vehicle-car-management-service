namespace Api.Domain.ResponseMessages;

public class GetCommissionDetailsResponseMessage
{
    public FixedCommission BrandWiseFixedCommission { get; set; }
    public AdditionalCommission ClassWiseAdditionalCommission { get; set; }
    public decimal ProfitWiseAdditionalCommission { get; set; }
}

public class FixedCommission
{
    public decimal AudiCommission { get; set; }
    public decimal JaguarCommission { get; set; }
    public decimal LandRoverCommission { get; set; }
    public decimal RenaultCommission { get; set; }
}

public class AdditionalCommission
{
    public decimal ClassACommission { get; set; }
    public decimal ClassBCommission { get; set; }
    public decimal ClassCCommission { get; set; }
}