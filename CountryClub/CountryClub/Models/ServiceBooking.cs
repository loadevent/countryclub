using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CountryClub.Models;

[Table("ServiceBooking")]
public partial class ServiceBooking
{
    [Key]
    [Column("bookingId")]
    public int BookingId { get; set; }

    [Column("serviceId")]
    public int ServiceId { get; set; }

    [Column("clientId")]
    [StringLength(15)]
    [Unicode(false)]
    public string ClientId { get; set; } = null!;

    [Column("bookingDate")]
    public DateOnly BookingDate { get; set; }

    [Column("adminId")]
    [StringLength(15)]
    [Unicode(false)]
    public string AdminId { get; set; } = null!;

    [ForeignKey("AdminId")]
    [InverseProperty("ServiceBookings")]
    public virtual Admin Admin { get; set; } = null!;

    [ForeignKey("ClientId")]
    [InverseProperty("ServiceBookings")]
    public virtual Client Client { get; set; } = null!;

    [ForeignKey("ServiceId")]
    [InverseProperty("ServiceBookings")]
    public virtual Service Service { get; set; } = null!;
}
