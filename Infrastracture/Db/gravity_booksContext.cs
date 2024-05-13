﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AppCore.Models;

namespace Infrastracture.Db;

public partial class gravity_booksContext : DbContext
{

    public  DbSet<address> address { get; set; }

    public virtual DbSet<address_status> address_status { get; set; }

    public virtual DbSet<author> author { get; set; }

    public  DbSet<book> book { get; set; }

    public virtual DbSet<book_language> book_language { get; set; }

    public virtual DbSet<country> country { get; set; }

    public virtual DbSet<cust_order> cust_order { get; set; }

    public virtual DbSet<customer> customer { get; set; }

    public virtual DbSet<customer_address> customer_address { get; set; }

    public virtual DbSet<order_history> order_history { get; set; }

    public virtual DbSet<order_line> order_line { get; set; }

    public virtual DbSet<order_status> order_status { get; set; }

    public virtual DbSet<publisher> publisher { get; set; }

    public virtual DbSet<shipping_method> shipping_method { get; set; }


    public gravity_booksContext(DbContextOptions<gravity_booksContext> options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=gravity_books;Username=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<address>(entity =>
        {
            entity.HasKey(e => e.address_id).HasName("pk_address");

            entity.Property(e => e.address_id).ValueGeneratedNever();

            entity.HasOne(d => d.country).WithMany(p => p.address).HasConstraintName("fk_addr_ctry");
        });

        modelBuilder.Entity<address_status>(entity =>
        {
            entity.HasKey(e => e.status_id).HasName("pk_addr_status");

            entity.Property(e => e.status_id).ValueGeneratedNever();
        });

        modelBuilder.Entity<author>(entity =>
        {
            entity.HasKey(e => e.author_id).HasName("pk_author");

            entity.Property(e => e.author_id).ValueGeneratedNever();

            //entity.HasMany(e => e.book_author).WithOne();

        });

        modelBuilder.Entity<book>(entity =>
        {
            entity.HasKey(e => e.book_id).HasName("pk_book");

            entity.Property(e => e.book_id).ValueGeneratedNever();

            entity.HasOne(d => d.language).WithMany(p => p.book).HasConstraintName("fk_book_lang");

            entity.HasOne(d => d.publisher).WithMany(p => p.book).HasConstraintName("fk_book_pub");

            //entity.HasMany(d => d.book_author).WithOne();
                //.UsingEntity<Dictionary<string, object>>(
                //    "book_author",
                //    r => r.HasOne<author>().WithMany()
                //        .HasForeignKey("author_id")
                //        .OnDelete(DeleteBehavior.ClientSetNull)
                //        .HasConstraintName("fk_ba_author"),
                //    l => l.HasOne<book>().WithMany()
                //        .HasForeignKey("book_id")
                //        .OnDelete(DeleteBehavior.ClientSetNull)
                //        .HasConstraintName("fk_ba_book"),
                //    j =>
                //    {
                //        j.HasKey("book_id", "author_id").HasName("pk_bookauthor");
                //    });
        });

        modelBuilder.Entity<book_language>(entity =>
        {
            entity.HasKey(e => e.language_id).HasName("pk_language");

            entity.Property(e => e.language_id).ValueGeneratedNever();
        });

        modelBuilder.Entity<country>(entity =>
        {
            entity.HasKey(e => e.country_id).HasName("pk_country");

            entity.Property(e => e.country_id).ValueGeneratedNever();
        });

        modelBuilder.Entity<cust_order>(entity =>
        {
            entity.HasKey(e => e.order_id).HasName("pk_custorder");

            entity.HasOne(d => d.customer).WithMany(p => p.cust_order).HasConstraintName("fk_order_cust");

            entity.HasOne(d => d.dest_address).WithMany(p => p.cust_order).HasConstraintName("fk_order_addr");

            entity.HasOne(d => d.shipping_method).WithMany(p => p.cust_order).HasConstraintName("fk_order_ship");
        });

        modelBuilder.Entity<customer>(entity =>
        {
            entity.HasKey(e => e.customer_id).HasName("pk_customer");

            entity.Property(e => e.customer_id).ValueGeneratedNever();
        });

        modelBuilder.Entity<customer_address>(entity =>
        {
            entity.HasKey(e => new { e.customer_id, e.address_id }).HasName("pk_custaddr");

            entity.HasOne(d => d.address).WithMany(p => p.customer_address)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ca_addr");

            entity.HasOne(d => d.customer).WithMany(p => p.customer_address)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ca_cust");
        });

        modelBuilder.Entity<order_history>(entity =>
        {
            entity.HasKey(e => e.history_id).HasName("pk_orderhist");

            entity.HasOne(d => d.order).WithMany(p => p.order_history).HasConstraintName("fk_oh_order");

            entity.HasOne(d => d.status).WithMany(p => p.order_history).HasConstraintName("fk_oh_status");
        });

        modelBuilder.Entity<order_line>(entity =>
        {
            entity.HasKey(e => e.line_id).HasName("pk_orderline");

            entity.HasOne(d => d.book).WithMany(p => p.order_line).HasConstraintName("fk_ol_book");

            entity.HasOne(d => d.order).WithMany(p => p.order_line).HasConstraintName("fk_ol_order");
        });

        modelBuilder.Entity<order_status>(entity =>
        {
            entity.HasKey(e => e.status_id).HasName("pk_orderstatus");

            entity.Property(e => e.status_id).ValueGeneratedNever();
        });

        modelBuilder.Entity<publisher>(entity =>
        {
            entity.HasKey(e => e.publisher_id).HasName("pk_publisher");

            entity.Property(e => e.publisher_id).ValueGeneratedNever();
        });

        modelBuilder.Entity<shipping_method>(entity =>
        {
            entity.HasKey(e => e.method_id).HasName("pk_shipmethod");

            entity.Property(e => e.method_id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}