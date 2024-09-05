using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CountryClub.Models;

[Table("Admin")]
public partial class Admin
{
    [Key]
    [Column("userId")]
    [StringLength(15)]
    [Unicode(false)]
    public string UserId { get; set; } = null!;

    [Column("cellphoneNO")]
    [StringLength(15)]
    [Unicode(false)]
    public string CellphoneNo { get; set; } = null!;

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

    [Column("password")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Password { get; set; }

    [InverseProperty("Admin")]
    public virtual ICollection<ServiceBooking> ServiceBookings { get; set; } = new List<ServiceBooking>();

  
}
