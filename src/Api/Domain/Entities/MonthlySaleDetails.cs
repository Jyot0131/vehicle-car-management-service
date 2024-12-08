using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Api.Domain.Entities;

[Table("MonthlySaleDetails")]
public class MonthlySaleDetails
{
    [Key]
    public int Id { get; set; }

    public int SalesmanId { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Class { get; set; }

    public int AudiSale { get; set; }

    public int JaguarSale { get; set; }

    public int LandRoverSale { get; set; }

    public int RenaultSale { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LastModifiedAt { get; set; }

    [ForeignKey("SalesmanId")]
    [InverseProperty("MonthlySaleDetails")]
    public virtual SalesmanDetails Salesman { get; set; }
}
