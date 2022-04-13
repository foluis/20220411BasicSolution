using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using _2022_02_11.Entities.DTOs;

namespace _2022_02_11.API.Context
{
    public partial class _20220211DatabaseContext : DbContext
    {
        public _20220211DatabaseContext()
        {
        }

        public _20220211DatabaseContext(DbContextOptions<_20220211DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblUserProfile> TblUserProfiles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=2022-02-11Database;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblUserProfile>(entity =>
            {
                entity.ToTable("TblUserProfile");

                entity.ToTable(tb => tb.IsTemporal(ttb =>
    {
        ttb.UseHistoryTable("UserProfile_HISTORY", "dbo");
        ttb
            .HasPeriodStart("SysStart")
            .HasColumnName("SysStart");
        ttb
            .HasPeriodEnd("SysEnd")
            .HasColumnName("SysEnd");
    }
));

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
