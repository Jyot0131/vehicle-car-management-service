using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Api.Domain.Entities;

[Table("CommissionDetails")]
public class CommissionDetails
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Brand { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal FixedCommission { get; set; }

    [Column("AClassCommission")]
    public int AclassCommission { get; set; }

    [Column("BClassCommission")]
    public int BclassCommission { get; set; }

    [Column("CClassCommission")]
    public int CclassCommission { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LastModifiedAt { get; set; }
}
