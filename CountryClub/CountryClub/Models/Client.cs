using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CountryClub.Models;

[Table("Client")]
public partial class Client
{
    [Key]
    [Column("clientId")]
    [StringLength(15)]
    [Unicode(false)]
    public string ClientId { get; set; } = null!;

    [Column("cellphoneNo")]
    [StringLength(20)]
    [Unicode(false)]
    public string CellphoneNo { get; set; } = null!;

    [Column("idNumber")]
    [StringLength(15)]
    [Unicode(false)]
    public string? IdNumber { get; set; }

    [Column("firstname")]
    [StringLength(25)]
    [Unicode(false)]
    public string Firstname { get; set; } = null!;

    [Column("lastname")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Lastname { get; set; }

    [Column("email")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("gender")]
    [StringLength(6)]
    [Unicode(false)]
    public string? Gender { get; set; }

    [Column("password")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Password { get; set; }

    [InverseProperty("Client")]
    public virtual ICollection<ServiceBooking> ServiceBookings { get; set; } = new List<ServiceBooking>();
}
