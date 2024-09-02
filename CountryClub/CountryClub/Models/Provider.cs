using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CountryClub.Models;

[Table("Provider")]
public partial class Provider
{
    [Key]
    [Column("providerId")]
    public int ProviderId { get; set; }

    [Column("firstname")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Firstname { get; set; }

    [Column("lastname")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Lastname { get; set; }

    [Column("cellphone")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Cellphone { get; set; }

    [Column("email")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("password")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Password { get; set; }

    [InverseProperty("Provider")]
    public virtual ICollection<ProvidedService> ProvidedServices { get; set; } = new List<ProvidedService>();
}
