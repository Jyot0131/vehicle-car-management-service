using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Api.Domain.Entities;

[Table("VehicleDetails")]
public partial class VehicleDetails
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Brand { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Class { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string ModelName { get; set; }

    [Required]
    [StringLength(10)]
    [Unicode(false)]
    public string ModelCode { get; set; }

    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string Description { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ManufacturingDate { get; set; }

    public bool Active { get; set; }

    public int? SortOrder { get; set; }
}
