using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Api.Domain.Entities;

[Table("SalesmanDetails")]
public class SalesmanDetails
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    [Unicode(false)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(20)]
    [Unicode(false)]
    public string LastName { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal AnnualSaleAmount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LastModifiedAt { get; set; }

    [InverseProperty("Salesman")]
    public virtual ICollection<MonthlySaleDetails> MonthlySaleDetails { get; set; } = new List<MonthlySaleDetails>();
}
