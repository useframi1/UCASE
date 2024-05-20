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

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Certificate> Certificates { get; set; }

    public virtual DbSet<Governorate> Governorates { get; set; }

    public virtual DbSet<Industry> Industries { get; set; }

    public virtual DbSet<PreferredIndustry> PreferredIndustries { get; set; }

    public virtual DbSet<PreferredSubject> PreferredSubjects { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<University> Universities { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("applications_pkey");

            entity.ToTable("applications");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.BirthCertificate).HasColumnName("birth_certificate");
            entity.Property(e => e.GuardianCompany)
                .HasMaxLength(100)
                .HasColumnName("guardian_company");
            entity.Property(e => e.GuardianEmail)
                .HasMaxLength(100)
                .HasColumnName("guardian_email");
            entity.Property(e => e.GuardianName)
                .HasMaxLength(100)
                .HasColumnName("guardian_name");
            entity.Property(e => e.GuardianNumber)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("guardian_number");
            entity.Property(e => e.GuardianProfession)
                .HasMaxLength(100)
                .HasColumnName("guardian_profession");
            entity.Property(e => e.MilitaryForm2).HasColumnName("military_form_2");
            entity.Property(e => e.MilitaryForm6).HasColumnName("military_form_6");
            entity.Property(e => e.NationalId).HasColumnName("national_id");
            entity.Property(e => e.Passport).HasColumnName("passport");
            entity.Property(e => e.PersonalPhoto).HasColumnName("personal_photo");
            entity.Property(e => e.PersonalStatement).HasColumnName("personal_statement");
            entity.Property(e => e.RecommendationLetter).HasColumnName("recommendation_letter");
            entity.Property(e => e.ResidencyCopy).HasColumnName("residency_copy");
            entity.Property(e => e.SchoolCity)
                .HasMaxLength(100)
                .HasColumnName("school_city");
            entity.Property(e => e.SchoolCountry)
                .HasMaxLength(100)
                .HasColumnName("school_country");
            entity.Property(e => e.SchoolName)
                .HasMaxLength(100)
                .HasColumnName("school_name");
            entity.Property(e => e.Transcript).HasColumnName("transcript");
            entity.Property(e => e.YearOfGraduation).HasColumnName("year_of_graduation");

            entity.HasOne(d => d.EmailNavigation).WithOne(p => p.Application)
                .HasForeignKey<Application>(d => d.Email)
                .HasConstraintName("applications_email_fkey");
        });

        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.HasKey(e => new { e.Email, e.CertificateName }).HasName("certificates_pkey");

            entity.ToTable("certificates");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.CertificateName)
                .HasMaxLength(100)
                .HasColumnName("certificate_name");
            entity.Property(e => e.CertificatePhoto).HasColumnName("certificate_photo");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.Certificates)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("certificates_email_fkey");
        });

        modelBuilder.Entity<Governorate>(entity =>
        {
            entity.HasKey(e => new { e.GovName, e.Area }).HasName("governorates_pkey");

            entity.ToTable("governorates");

            entity.Property(e => e.GovName)
                .HasMaxLength(200)
                .HasColumnName("gov_name");
            entity.Property(e => e.Area)
                .HasMaxLength(200)
                .HasColumnName("area");
        });

        modelBuilder.Entity<Industry>(entity =>
        {
            entity.HasKey(e => e.IndustryName).HasName("industries_pkey");

            entity.ToTable("industries");

            entity.Property(e => e.IndustryName)
                .HasMaxLength(200)
                .HasColumnName("industry_name");
        });

        modelBuilder.Entity<PreferredIndustry>(entity =>
        {
            entity.HasKey(e => new { e.Email, e.Industry }).HasName("preferredindustries_pkey");

            entity.ToTable("preferred_industries");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Industry)
                .HasMaxLength(200)
                .HasColumnName("industry");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.PreferredIndustries)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("preferredindustries_email_fkey");
        });

        modelBuilder.Entity<PreferredSubject>(entity =>
        {
            entity.HasKey(e => new { e.Email, e.Subject }).HasName("preferredsubjects_pkey");

            entity.ToTable("preferred_subjects");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Subject)
                .HasMaxLength(200)
                .HasColumnName("subject");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.PreferredSubjects)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("preferredsubjects_email_fkey");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectName).HasName("subjects_pkey");

            entity.ToTable("subjects");

            entity.Property(e => e.SubjectName)
                .HasMaxLength(200)
                .HasColumnName("subject_name");
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.HasKey(e => e.UniName).HasName("universities_pkey");

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

            entity.HasMany(d => d.EmailsNavigation).WithMany(p => p.UniNames)
                .UsingEntity<Dictionary<string, object>>(
                    "UniversityChoice",
                    r => r.HasOne<Application>().WithMany()
                        .HasForeignKey("Email")
                        .HasConstraintName("university_choices_email_fkey"),
                    l => l.HasOne<University>().WithMany()
                        .HasForeignKey("UniName")
                        .HasConstraintName("university_choices_uni_name_fkey"),
                    j =>
                    {
                        j.HasKey("UniName", "Email").HasName("university_choices_pkey");
                        j.ToTable("university_choices");
                        j.IndexerProperty<string>("UniName")
                            .HasMaxLength(200)
                            .HasColumnName("uni_name");
                        j.IndexerProperty<string>("Email")
                            .HasMaxLength(100)
                            .HasColumnName("email");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.AddressLine1)
                .HasMaxLength(200)
                .HasColumnName("address_line1");
            entity.Property(e => e.AddressLine2)
                .HasMaxLength(200)
                .HasColumnName("address_line2");
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
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.PasswordSalt).HasColumnName("password_salt");
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("phone_no");
            entity.Property(e => e.StartUni).HasColumnName("start_uni");

            entity.HasMany(d => d.Universities).WithMany(p => p.Emails)
                .UsingEntity<Dictionary<string, object>>(
                    "FavoriteUniversity",
                    r => r.HasOne<University>().WithMany()
                        .HasForeignKey("University")
                        .HasConstraintName("fk_favorite_universities_universities"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("Email")
                        .HasConstraintName("fk_favorite_universities_users"),
                    j =>
                    {
                        j.HasKey("Email", "University").HasName("favorite_universities_pkey");
                        j.ToTable("favorite_universities");
                        j.IndexerProperty<string>("Email")
                            .HasMaxLength(100)
                            .HasColumnName("email");
                        j.IndexerProperty<string>("University")
                            .HasMaxLength(200)
                            .HasColumnName("university");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
