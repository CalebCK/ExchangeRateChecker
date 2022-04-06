using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CurrencyConvertor.Models.Data.MainContext
{
    public partial class CurrencyConvertorDBContext : DbContext
    {
        public CurrencyConvertorDBContext()
        {
        }

        public CurrencyConvertorDBContext(DbContextOptions<CurrencyConvertorDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ExchangeRateRequest> ExchangeRateRequests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "English_Ghana.1252");

            modelBuilder.Entity<ExchangeRateRequest>(entity =>
            {
                entity.ToTable("ExchangeRateRequest");

                entity.Property(e => e.Created).HasColumnType("date");

                entity.Property(e => e.CreatedBy).IsRequired();

                entity.Property(e => e.RequestDate).HasColumnType("date");

                entity.Property(e => e.Response)
                    .IsRequired()
                    .HasColumnType("json");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
