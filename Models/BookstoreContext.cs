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

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\;Database=bookstore;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__349DA5A661126EA1");

            entity.ToTable("Account");

            entity.Property(e => e.AccountId)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.AccountDes)
                .HasMaxLength(100)
                .HasColumnName("account_des");
            entity.Property(e => e.Address).HasMaxLength(256);
            entity.Property(e => e.Country).HasMaxLength(30);
            entity.Property(e => e.Email)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(32)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.IdAuthor).HasName("PK__author__7411B254F93419A0");

            entity.ToTable("author");

            entity.Property(e => e.IdAuthor).HasColumnName("id_author");
            entity.Property(e => e.NameAuthor)
                .HasMaxLength(100)
                .HasColumnName("name_author");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.IdBook).HasName("PK__book__DAE712E819D05A1F");

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
            entity.Property(e => e.StarSum).HasColumnName("star_sum");

            entity.HasOne(d => d.IdAuthorNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.IdAuthor)
                .HasConstraintName("fk_book_author");

            entity.HasOne(d => d.IdCateNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.IdCate)
                .HasConstraintName("fk_book_category");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.IdCartItem).HasName("PK__CartItem__8E5A0FCE0978502D");

            entity.ToTable("CartItem");

            entity.Property(e => e.IdCartItem).HasColumnName("id_CartItem");
            entity.Property(e => e.IdProduct)
                .HasMaxLength(30)
                .HasColumnName("id_product");
            entity.Property(e => e.IdShoppingCart).HasColumnName("id_shoppingCart");
            entity.Property(e => e.Price)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("price");
            entity.Property(e => e.QuantityItem).HasColumnName("quantity_item");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_id");

            entity.HasOne(d => d.IdShoppingCartNavigation).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.IdShoppingCart)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shoppingcart_id");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PK__category__E548B6736F3FB734");

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

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.IdComment).HasName("PK__Comment__7E14AC85DDFC00A2");

            entity.ToTable("Comment");

            entity.Property(e => e.IdComment)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("id_comment");
            entity.Property(e => e.CommentContent)
                .HasColumnType("ntext")
                .HasColumnName("comment_content");
            entity.Property(e => e.CommentTime)
                .HasColumnType("smalldatetime")
                .HasColumnName("comment_time");
            entity.Property(e => e.IdBook)
                .HasMaxLength(30)
                .HasColumnName("id_book");
            entity.Property(e => e.IdUser)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("id_user");
            entity.Property(e => e.StarRating).HasColumnName("star_rating");

            entity.HasOne(d => d.IdBookNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdBook)
                .HasConstraintName("fk_book_id");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("fk_user_id");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.IdNews).HasName("PK__news__389F1DA910334DBF");

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

        modelBuilder.Entity<ShoppingCart>(entity =>
        {
            entity.HasKey(e => e.IdCart).HasName("PK__Shopping__C71FE317C28C0517");

            entity.ToTable("ShoppingCart");

            entity.Property(e => e.IdCart).HasColumnName("id_cart");
            entity.Property(e => e.CartTime)
                .HasColumnType("smalldatetime")
                .HasColumnName("cart_time");
            entity.Property(e => e.IdUser)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("id_user");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.ShoppingCarts)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_account_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
