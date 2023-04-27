using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookStoreWeb.Models;

public partial class BookstoreContext : DbContext
{
    public BookstoreContext()
    {
    }

    public BookstoreContext(DbContextOptions<BookstoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CustOrder> CustOrders { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<OrderHistory> OrderHistories { get; set; }

    public virtual DbSet<OrderLine> OrderLines { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=bookstore;Trusted_Connection=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__349DA5A65854A990");

            entity.ToTable("Account");

            entity.Property(e => e.AccountId)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(32)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.IdAuthor).HasName("PK__author__7411B2546D734381");

            entity.ToTable("author");

            entity.Property(e => e.IdAuthor)
                .ValueGeneratedNever()
                .HasColumnName("id_author");
            entity.Property(e => e.NameAuthor)
                .HasMaxLength(100)
                .HasColumnName("name_author");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.IdBook).HasName("PK__book__DAE712E853DA0484");

            entity.ToTable("book");

            entity.Property(e => e.IdBook)
                .HasMaxLength(30)
                .HasColumnName("id_book");
            entity.Property(e => e.Author)
                .HasMaxLength(100)
                .HasColumnName("author");
            entity.Property(e => e.Company)
                .HasMaxLength(100)
                .HasColumnName("company");
            entity.Property(e => e.Dealpercent).HasColumnName("dealpercent");
            entity.Property(e => e.Desbook).HasColumnName("desbook");
            entity.Property(e => e.IdAuthor).HasColumnName("id_author");
            entity.Property(e => e.IdCate)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("id_cate");
            entity.Property(e => e.Imageurl)
                .IsUnicode(false)
                .HasColumnName("imageurl");
            entity.Property(e => e.LoaiBia)
                .HasMaxLength(100)
                .HasColumnName("loai_bia");
            entity.Property(e => e.NameBook).HasColumnName("name_book");
            entity.Property(e => e.Newcash).HasColumnName("newcash");
            entity.Property(e => e.NhaXuatBan)
                .HasMaxLength(100)
                .HasColumnName("nha_xuat_ban");
            entity.Property(e => e.Oldcash).HasColumnName("oldcash");
            entity.Property(e => e.PublishDate)
                .HasColumnType("date")
                .HasColumnName("publish_date");
            entity.Property(e => e.Size)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("size");

            entity.HasOne(d => d.IdAuthorNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.IdAuthor)
                .HasConstraintName("fk_book_author");

            entity.HasOne(d => d.IdCateNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.IdCate)
                .HasConstraintName("fk_book_category");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PK__category__E548B673975A9226");

            entity.ToTable("category");

            entity.Property(e => e.IdCategory)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("id_category");
            entity.Property(e => e.CssClass)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.NameCategory)
                .HasMaxLength(30)
                .HasColumnName("name_category");
        });

        modelBuilder.Entity<CustOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__cust_ord__46596229583EA42E");

            entity.ToTable("cust_order");

            entity.Property(e => e.OrderId)
                .ValueGeneratedNever()
                .HasColumnName("order_id");
            entity.Property(e => e.Andress)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("andress");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.OrderDate)
                .HasColumnType("date")
                .HasColumnName("order_date");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustOrders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("fk_cust_order");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__customer__CD65CB85FECD6133");

            entity.ToTable("customer");

            entity.Property(e => e.CustomerId)
                .ValueGeneratedNever()
                .HasColumnName("customer_id");
            entity.Property(e => e.Andress)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("andress");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("customer_name");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Phone).HasColumnName("phone");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.IdNews).HasName("PK__news__389F1DA954C68361");

            entity.ToTable("news");

            entity.Property(e => e.IdNews)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("id_news");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.NewsContent).HasColumnName("news_content");
            entity.Property(e => e.NewsTag)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("news_tag");
            entity.Property(e => e.NewsTitle)
                .HasMaxLength(30)
                .HasColumnName("news_title");
            entity.Property(e => e.SummaryContent).HasColumnName("summary_content");

            entity.HasOne(d => d.Author).WithMany(p => p.News)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("fk_author_ID");

            entity.HasOne(d => d.NewsTagNavigation).WithMany(p => p.News)
                .HasForeignKey(d => d.NewsTag)
                .HasConstraintName("fk_tag_news");
        });

        modelBuilder.Entity<OrderHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__order_hi__096AA2E97BBCBB6F");

            entity.ToTable("order_history");

            entity.Property(e => e.HistoryId)
                .ValueGeneratedNever()
                .HasColumnName("history_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.OrderStatus).HasColumnName("order_status");
            entity.Property(e => e.StatusDate)
                .HasColumnType("date")
                .HasColumnName("status_date");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderHistories)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("fk_order_history");
        });

        modelBuilder.Entity<OrderLine>(entity =>
        {
            entity.HasKey(e => e.LineId).HasName("PK__order_li__F5AE5F62EDD21F0C");

            entity.ToTable("order_line");

            entity.Property(e => e.LineId)
                .ValueGeneratedNever()
                .HasColumnName("line_id");
            entity.Property(e => e.BookId)
                .HasMaxLength(30)
                .HasColumnName("book_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderLines)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("fk_line_order");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
