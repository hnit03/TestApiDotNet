using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Task3.Models
{
    public partial class TaskThreeContext : DbContext
    {
        public TaskThreeContext()
        {
        }

        public TaskThreeContext(DbContextOptions<TaskThreeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.StatementID);

                entity.ToTable("Invoice");

                entity.Property(e => e.StatementID)
                    .ValueGeneratedNever()
                    .HasColumnName("statementID");

                entity.Property(e => e.CurCharges).HasColumnName("curCharges");

                entity.Property(e => e.LateFees).HasColumnName("lateFees");

                entity.Property(e => e.PreBalance).HasColumnName("preBalance");

                entity.Property(e => e.StatementDate)
                    .HasColumnType("date")
                    .HasColumnName("statementDate");

                entity.Property(e => e.Total).HasColumnName("total");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("FK_Invoice_Member");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.ToTable("Member");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(50)
                    .HasColumnName("fullname");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
