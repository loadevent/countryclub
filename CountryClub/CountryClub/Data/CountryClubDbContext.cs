using System;
using System.Collections.Generic;
using CountryClub.Models;
using Microsoft.EntityFrameworkCore;

namespace CountryClub.Data;

public partial class CountryClubDbContext : DbContext
{
    public CountryClubDbContext()
    {
    }

    public CountryClubDbContext(DbContextOptions<CountryClubDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ProvidedService> ProvidedServices { get; set; }

    public virtual DbSet<Provider> Providers { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceBooking> ServiceBookings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=CountryClubDB;Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_Admin_1");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK_Client_1");
        });

        modelBuilder.Entity<ProvidedService>(entity =>
        {
            entity.HasOne(d => d.Provider).WithMany(p => p.ProvidedServices).HasConstraintName("FK_ProvidedService_Provider");

            entity.HasOne(d => d.Service).WithMany(p => p.ProvidedServices).HasConstraintName("FK_ProvidedService_Service");
        });

        modelBuilder.Entity<ServiceBooking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK_ServiceBooking_1");

            entity.HasOne(d => d.Admin).WithMany(p => p.ServiceBookings).HasConstraintName("FK_ServiceBooking_Admin");

            entity.HasOne(d => d.Client).WithMany(p => p.ServiceBookings).HasConstraintName("FK_ServiceBooking_Client");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceBookings).HasConstraintName("FK_ServiceBooking_Service");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
