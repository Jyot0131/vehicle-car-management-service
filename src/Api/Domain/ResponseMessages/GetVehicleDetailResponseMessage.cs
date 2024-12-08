namespace Api.Domain.ResponseMessages;

public class GetVehicleDetailResponseMessage
{
    public string Brand { get; set; }
    public string Class { get; set; }
    public string ModelName { get; set; }
    public string ModelCode { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTime ManufacturingDate { get; set; }
}