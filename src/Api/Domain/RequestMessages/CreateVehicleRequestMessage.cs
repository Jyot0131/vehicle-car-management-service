using System.ComponentModel.DataAnnotations;

namespace Api.Domain.RequestMessages;

public class CreateVehicleRequestMessage : RequestMessageBase
{
    [Required]
    [AllowedValues("Audi", "Jaguar", "Land rover", "Renault", ErrorMessage = "Brand value must be one of the following \"Audi\", \"Jaguar\", \"Land rover\", \"Renault\"")]
    public string Brand { get; set; }

    [Required]
    [AllowedValues("A-Class", "B-Class", "C-Class", ErrorMessage = "Class value must be one of the following \"A-Class\", \"B-Class\", \"C-Class\"")]
    public string Class { get; set; }

    [Required]
    public string ModelName { get; set; }

    [Required]
    [Length(10, 10, ErrorMessage = "ModelCode length must be 10 characters")]
    public string ModelCode { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    [Range(1, 10000000)]
    public decimal Price { get; set; }

    [Required]
    public DateTime? ManufacturingDate { get; set; }
}