using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CountryClub.Models;

[Table("Service")]
public partial class Service
{
    [Key]
    [Column("serviceId")]
    public int ServiceId { get; set; }

    [Column("serviceDesc")]
    [Unicode(false)]
    public string? ServiceDesc { get; set; }

    [InverseProperty("Service")]
    public virtual ICollection<ProvidedService> ProvidedServices { get; set; } = new List<ProvidedService>();

    [InverseProperty("Service")]
    public virtual ICollection<ServiceBooking> ServiceBookings { get; set; } = new List<ServiceBooking>();
}
