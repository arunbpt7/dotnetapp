using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PDFTestCore.Models
{
    public partial class EformsContext : DbContext
    {
        public EformsContext()
        {
        }

        public EformsContext(DbContextOptions<EformsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ll30Forms> Ll30Forms { get; set; }
        public virtual DbSet<Ll30Snippets> Ll30Snippets { get; set; }

//         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//         {
//             if (!optionsBuilder.IsConfigured)
//             {
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                 optionsBuilder.UseSqlServer("Server=d2e1cldb14\\sql16DEVE;Database=Eforms;Trusted_Connection=True");
//             }
//         }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<Ll30Forms>(entity =>
            {
                entity.HasKey(e => e.FormId);

                entity.ToTable("LL30_Forms");

                entity.HasIndex(e => e.FormNumber)
                    .HasName("UK_LL30_Forms_FormNumber")
                    .IsUnique();

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FormNumber)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Ll30Snippets>(entity =>
            {
                entity.HasKey(e => e.SnippetId);

                entity.ToTable("LL30_Snippets");

                entity.HasIndex(e => new { e.FormSnippetCodeId, e.LangId })
                    .HasName("Uk_LL30_Snippets_FormSnippetCodeId_LangId")
                    .IsUnique();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SnippetText).IsRequired();

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });
        }
    }
}
