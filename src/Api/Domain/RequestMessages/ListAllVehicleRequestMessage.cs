using System.ComponentModel.DataAnnotations;

namespace Api.Domain.RequestMessages;

public class ListAllVehicleRequestMessage : RequestMessageBase
{
    public string ModelName { get; set; }
    [Length(10, 10, ErrorMessage = "ModelCode length must be 10 characters")]
    
    public string ModelCode { get; set; }
    
    [AllowedValues(null, "asc", "desc", ErrorMessage = "SortOrder value must be one of the following \"asc\", \"desc\"")]
    public string SortOrder { get; set; }

    [AllowedValues(null, "ManufacturingDate", ErrorMessage = "OrderBy value must be the following \"ManufacturingDate\"")]
    public string OrderBy { get; set; }
}