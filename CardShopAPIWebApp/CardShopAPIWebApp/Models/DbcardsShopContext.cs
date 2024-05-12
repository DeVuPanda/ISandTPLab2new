using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CardShopAPIWebApp.Model;

public partial class DbcardsShopContext : DbContext
{
    public DbcardsShopContext()
    {
    }

    public DbcardsShopContext(DbContextOptions<DbcardsShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CardDeck> CardDecks { get; set; }

    public virtual DbSet<Firm> Firms { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=Denis\\SQLEXPRESS; Database=DBCardsShop; Trusted_Connection=True; TrustServerCertificate=True; ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CardDeck>(entity =>
        {
            entity.HasKey(e => e.DeckId);

            entity.Property(e => e.DeckId).HasColumnName("deck_id");
            entity.Property(e => e.DeckName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("deck_name");
            entity.Property(e => e.DeckPrice).HasColumnName("deck_price");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.FirmId).HasColumnName("firm_id");
            entity.Property(e => e.NumberOfCards).HasColumnName("number_of_cards");

            entity.HasOne(d => d.Firm).WithMany(p => p.CardDecks)
                .HasForeignKey(d => d.FirmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CardDecks_Firms");
        });

        modelBuilder.Entity<Firm>(entity =>
        {
            entity.Property(e => e.FirmId).HasColumnName("firm_id");
            entity.Property(e => e.FirmName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("firm_name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.DeckId).HasColumnName("deck_id");
            entity.Property(e => e.DeliveryAddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("delivery_address");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("order_status");
            entity.Property(e => e.PersonName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("person_name");
            entity.Property(e => e.PersonPhone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("person_phone");

            entity.HasOne(d => d.Deck).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DeckId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_CardDecks");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.DeckId).HasColumnName("deck_id");
            entity.Property(e => e.PersonName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("person_name");
            entity.Property(e => e.Question1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("question");

            entity.HasOne(d => d.Deck).WithMany(p => p.Questions)
                .HasForeignKey(d => d.DeckId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Questions_CardDecks");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.Property(e => e.ReviewId).HasColumnName("review_id");
            entity.Property(e => e.DeckId).HasColumnName("deck_id");
            entity.Property(e => e.Review1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("review");

            entity.HasOne(d => d.Deck).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.DeckId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reviews_CardDecks");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
