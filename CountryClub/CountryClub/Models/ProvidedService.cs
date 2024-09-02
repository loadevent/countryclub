using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CountryClub.Models;

[PrimaryKey("ServiceId", "ProviderId")]
[Table("ProvidedService")]
public partial class ProvidedService
{
    [Key]
    [Column("serviceId")]
    public int ServiceId { get; set; }

    [Key]
    [Column("providerId")]
    public int ProviderId { get; set; }

    [Column("serviceRate")]
    public double? ServiceRate { get; set; }

    [ForeignKey("ProviderId")]
    [InverseProperty("ProvidedServices")]
    public virtual Provider Provider { get; set; } = null!;

    [ForeignKey("ServiceId")]
    [InverseProperty("ProvidedServices")]
    public virtual Service Service { get; set; } = null!;
}
