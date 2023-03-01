using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BeatySaloonApi.Models;

public partial class BeautySaloonBaseContext : DbContext
{
    public BeautySaloonBaseContext()
    {
    }

    public BeautySaloonBaseContext(DbContextOptions<BeautySaloonBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientService> ClientServices { get; set; }

    public virtual DbSet<DocumentByService> DocumentByServices { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceCategorye> ServiceCategoryes { get; set; }

    public virtual DbSet<ServicePhoto> ServicePhotos { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=BeautySaloonBase");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.GenderCode)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("login");
            entity.Property(e => e.Patronymic).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.PhotoPath).HasMaxLength(1000);
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");

            entity.HasOne(d => d.GenderCodeNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.GenderCode)
                .HasConstraintName("FK_Client_Gender");

            entity.HasOne(d => d.LoginNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.Login)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Client_Users");
        });

        modelBuilder.Entity<ClientService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ClientService");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.StartTime).HasColumnType("datetime");

            entity.HasOne(d => d.Client).WithMany(p => p.ClientServices)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClientService_Client");

            entity.HasOne(d => d.Service).WithMany(p => p.ClientServices)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_ClientService_Service");
        });

        modelBuilder.Entity<DocumentByService>(entity =>
        {
            entity.ToTable("DocumentByService");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ClientServiceId).HasColumnName("ClientServiceID");
            entity.Property(e => e.DocumentPath).HasMaxLength(1000);

            entity.HasOne(d => d.ClientService).WithMany(p => p.DocumentByServices)
                .HasForeignKey(d => d.ClientServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocumentByService_ClientService");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.ToTable("Gender");

            entity.Property(e => e.Code)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.Name).HasMaxLength(10);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UserRole");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Role1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Role");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Service");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cost).HasColumnType("money");
            entity.Property(e => e.MainImagePath).HasMaxLength(1000);
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Category).WithMany(p => p.Services)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Service_CategoryService");
        });

        modelBuilder.Entity<ServiceCategorye>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK_CategoryService");

            entity.Property(e => e.CategoryImage).HasColumnType("image");
            entity.Property(e => e.CategoryTitle).HasMaxLength(100);
        });

        modelBuilder.Entity<ServicePhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ServicePhoto");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PhotoPath).HasMaxLength(1000);
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

            entity.HasOne(d => d.Service).WithMany(p => p.ServicePhotos)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServicePhoto_Service");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserLogin).HasName("PK_Users_1");

            entity.Property(e => e.UserLogin)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Idrole)
                .HasDefaultValueSql("((2))")
                .HasColumnName("IDRole");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdroleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Idrole)
                .HasConstraintName("FK_Users_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
