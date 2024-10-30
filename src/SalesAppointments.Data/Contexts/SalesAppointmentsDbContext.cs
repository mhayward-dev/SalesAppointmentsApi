using SalesAppointments.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace SalesAppointments.Data.Contexts;

public partial class SalesAppointmentsDbContext : DbContext
{
    public SalesAppointmentsDbContext()
    {
    }

    public SalesAppointmentsDbContext(DbContextOptions<SalesAppointmentsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<SalesManager> SalesManagers { get; set; }

    public virtual DbSet<Slot> Slots { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Name=DatabaseConnection");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SalesManager>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sales_managers_pkey");

            entity.ToTable("sales_managers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustomerRatings)
                .HasColumnType("character varying(100)[]")
                .HasColumnName("customer_ratings");
            entity.Property(e => e.Languages)
                .HasColumnType("character varying(100)[]")
                .HasColumnName("languages");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .HasColumnName("name");
            entity.Property(e => e.Products)
                .HasColumnType("character varying(100)[]")
                .HasColumnName("products");
        });

        modelBuilder.Entity<Slot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("slots_pkey");

            entity.ToTable("slots");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Booked)
                .HasDefaultValue(false)
                .HasColumnName("booked");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.SalesManagerId).HasColumnName("sales_manager_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.SalesManager).WithMany(p => p.Slots)
                .HasForeignKey(d => d.SalesManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("slots_sales_manager_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
