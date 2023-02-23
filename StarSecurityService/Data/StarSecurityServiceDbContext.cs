using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StarSecurityService.Models;

namespace StarSecurityService.Data;

public partial class StarSecurityServiceDbContext : DbContext
{
    public StarSecurityServiceDbContext()
    {
    }

    public StarSecurityServiceDbContext(DbContextOptions<StarSecurityServiceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Feed> Feeds { get; set; }

    public virtual DbSet<FeedImg> FeedImgs { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Guard> Guards { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=starsecurityservice;Integrated Security=True;MultipleActiveResultSets=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__F267253E2F19DD28");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts).HasConstraintName("FK__Account__roleID__35BCFE0A");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment__CDDE91BDF50C4423");

            entity.HasOne(d => d.Feed).WithMany(p => p.Comments).HasConstraintName("FK__Comment__feedID__38996AB5");
        });

        modelBuilder.Entity<Feed>(entity =>
        {
            entity.HasKey(e => e.FeedId).HasName("PK__Feeds__A0A7D53F74E77D8E");
        });

        modelBuilder.Entity<FeedImg>(entity =>
        {
            entity.HasKey(e => e.FeedimgId).HasName("PK__FeedImg__9426699EC538D1E5");

            entity.HasOne(d => d.Feed).WithMany(p => p.FeedImgs).HasConstraintName("FK__FeedImg__feedID__36B12243");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__2613FDC42606923C");

            entity.HasOne(d => d.Service).WithMany(p => p.Feedbacks).HasConstraintName("FK__Feedback__servic__37A5467C");
        });

        modelBuilder.Entity<Guard>(entity =>
        {
            entity.HasKey(e => e.GuardId).HasName("PK__Guard__17D718315613E8CF");

            entity.HasOne(d => d.Service).WithMany(p => p.Guards).HasConstraintName("FK__Guard__serviceID__398D8EEE");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__0809337D2375614C");

            entity.HasOne(d => d.Account).WithMany(p => p.Orders).HasConstraintName("FK__Order__accountID__3B75D760");

            entity.HasOne(d => d.Service).WithMany(p => p.Orders).HasConstraintName("FK__Order__serviceID__3A81B327");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderdetailsId).HasName("PK__OrderDet__6497933891AC32F4");

            entity.HasOne(d => d.Guard).WithMany(p => p.OrderDetails).HasConstraintName("FK__OrderDeta__guard__3C69FB99");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails).HasConstraintName("FK__OrderDeta__order__3D5E1FD2");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__CD98460AD0382E40");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Service__4550733FF324B268");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
