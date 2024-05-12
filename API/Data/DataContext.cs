using System;
using System.Collections.Generic;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public partial class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FavoriteUniversity> FavoriteUniversities { get; set; }

    public virtual DbSet<Governorate> Governorates { get; set; }

    public virtual DbSet<Major> Majors { get; set; }

    public virtual DbSet<PreferredIndustry> PreferredIndustries { get; set; }

    public virtual DbSet<PreferredSubject> PreferredSubjects { get; set; }

    public virtual DbSet<University> Universities { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FavoriteUniversity>(entity =>
        {
            entity.HasKey(e => new { e.Email, e.University }).HasName("favorite_universities_pkey");

            entity.ToTable("favorite_universities");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.University)
                .HasMaxLength(200)
                .HasColumnName("university");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.FavoriteUniversities)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("favorite_universities_email_fkey");
        });

        modelBuilder.Entity<Governorate>(entity =>
        {
            entity.HasKey(e => new { e.GovName, e.Area }).HasName("governorate_pkey");

            entity.ToTable("governorates");

            entity.Property(e => e.GovName)
                .HasMaxLength(200)
                .HasColumnName("gov_name");
            entity.Property(e => e.Area)
                .HasMaxLength(200)
                .HasColumnName("area");
        });

        modelBuilder.Entity<Major>(entity =>
        {
            entity.HasKey(e => e.MajorName).HasName("majors_pkey");

            entity.ToTable("majors");

            entity.Property(e => e.MajorName)
                .HasMaxLength(200)
                .HasColumnName("major_name");
            entity.Property(e => e.MajorRequirements).HasColumnName("major_requirements");
        });

        modelBuilder.Entity<PreferredIndustry>(entity =>
        {
            entity.HasKey(e => new { e.Email, e.Industry }).HasName("user_industry_pkey");

            entity.ToTable("preferred_industries");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Industry)
                .HasMaxLength(200)
                .HasColumnName("industry");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.PreferredIndustries)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("user_industry_email_fkey");
        });

        modelBuilder.Entity<PreferredSubject>(entity =>
        {
            entity.HasKey(e => new { e.Email, e.Subject }).HasName("user_subjects_pkey");

            entity.ToTable("preferred_subjects");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Subject)
                .HasMaxLength(200)
                .HasColumnName("subject");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.PreferredSubjects)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("user_subjects_email_fkey");
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.HasKey(e => e.UniName).HasName("university_pkey");

            entity.ToTable("universities");

            entity.Property(e => e.UniName)
                .HasMaxLength(200)
                .HasColumnName("uni_name");
            entity.Property(e => e.AcceptanceRate).HasColumnName("acceptance_rate");
            entity.Property(e => e.Area)
                .HasMaxLength(200)
                .HasColumnName("area");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.GeneralRequirements).HasColumnName("general_requirements");
            entity.Property(e => e.GovName)
                .HasMaxLength(200)
                .HasColumnName("gov_name");
            entity.Property(e => e.Link)
                .HasMaxLength(200)
                .HasColumnName("link");
            entity.Property(e => e.Logo).HasColumnName("logo");
            entity.Property(e => e.Ranking).HasColumnName("ranking");

            entity.HasOne(d => d.Governorate).WithMany(p => p.Universities)
                .HasForeignKey(d => new { d.GovName, d.Area })
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("university_gov_name_area_fkey");

            entity.HasMany(d => d.Majors).WithMany(p => p.UniNames)
                .UsingEntity<Dictionary<string, object>>(
                    "Universitymajor",
                    r => r.HasOne<Major>().WithMany()
                        .HasForeignKey("Major")
                        .HasConstraintName("universitymajors_major_fkey"),
                    l => l.HasOne<University>().WithMany()
                        .HasForeignKey("UniName")
                        .HasConstraintName("universitymajors_uni_name_fkey"),
                    j =>
                    {
                        j.HasKey("UniName", "Major").HasName("universitymajors_pkey");
                        j.ToTable("universitymajors");
                        j.IndexerProperty<string>("UniName")
                            .HasMaxLength(200)
                            .HasColumnName("uni_name");
                        j.IndexerProperty<string>("Major")
                            .HasMaxLength(200)
                            .HasColumnName("major");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Addressline1)
                .HasMaxLength(200)
                .HasColumnName("addressline1");
            entity.Property(e => e.Addressline2)
                .HasMaxLength(200)
                .HasColumnName("addressline2");
            entity.Property(e => e.Area)
                .HasMaxLength(200)
                .HasColumnName("area");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .HasColumnName("gender");
            entity.Property(e => e.GovName)
                .HasMaxLength(200)
                .HasColumnName("gov_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.Nationality)
                .HasMaxLength(100)
                .HasColumnName("nationality");
            entity.Property(e => e.Passwordhash).HasColumnName("passwordhash");
            entity.Property(e => e.Passwordsalt).HasColumnName("passwordsalt");
            entity.Property(e => e.Phoneno)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("phoneno");

            entity.HasOne(d => d.Governorate).WithMany(p => p.Users)
                .HasForeignKey(d => new { d.GovName, d.Area })
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("users_gov_name_area_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
