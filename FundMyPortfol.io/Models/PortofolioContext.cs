using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FundMyPortfol.io.Models
{
    public partial class PortofolioContext : DbContext
    {
        public PortofolioContext()
        {
        }

        public PortofolioContext(DbContextOptions<PortofolioContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Backer> Backer { get; set; }
        public virtual DbSet<BackerBuyPackage> BackerBuyPackage { get; set; }
        public virtual DbSet<BackerDetails> BackerDetails { get; set; }
        public virtual DbSet<BackerFollowCreator> BackerFollowCreator { get; set; }
        public virtual DbSet<CreatorDetails> CreatorDetails { get; set; }
        public virtual DbSet<Package> Package { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<ProjectCreator> ProjectCreator { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Portofolio;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Backer>(entity =>
            {
                entity.HasIndex(e => e.BackerDetailsId)
                    .HasName("UQ__Backer__B6E33C68D5A37371")
                    .IsUnique();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.BackerDetails)
                    .WithOne(p => p.Backer)
                    .HasForeignKey<Backer>(d => d.BackerDetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Backer__BackerDe__46E78A0C");
            });

            modelBuilder.Entity<BackerBuyPackage>(entity =>
            {
                entity.HasKey(e => new { e.BackerId, e.PackageId });

                entity.HasOne(d => d.Backer)
                    .WithMany(p => p.BackerBuyPackage)
                    .HasForeignKey(d => d.BackerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BackerBuy__Backe__5629CD9C");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.BackerBuyPackage)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BackerBuy__Packa__571DF1D5");
            });

            modelBuilder.Entity<BackerDetails>(entity =>
            {
                entity.Property(e => e.Country).HasMaxLength(20);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Street).HasMaxLength(100);

                entity.Property(e => e.Town).HasMaxLength(20);
            });

            modelBuilder.Entity<BackerFollowCreator>(entity =>
            {
                entity.HasKey(e => new { e.BackerId, e.ProjectCreatorId });

                entity.HasOne(d => d.Backer)
                    .WithMany(p => p.BackerFollowCreator)
                    .HasForeignKey(d => d.BackerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BackerFol__Backe__59FA5E80");

                entity.HasOne(d => d.ProjectCreator)
                    .WithMany(p => p.BackerFollowCreator)
                    .HasForeignKey(d => d.ProjectCreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BackerFol__Proje__5AEE82B9");
            });

            modelBuilder.Entity<CreatorDetails>(entity =>
            {
                entity.Property(e => e.Country).HasMaxLength(20);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Street).HasMaxLength(100);

                entity.Property(e => e.Town).HasMaxLength(20);
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.MoneyCost)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.PackageName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Package)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Package__Project__534D60F1");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasIndex(e => e.Title)
                    .HasName("UQ__Project__2CB664DCE007C97C")
                    .IsUnique();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.ExpireDate).HasColumnType("date");

                entity.Property(e => e.Link)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MoneyGoal).HasColumnType("money");

                entity.Property(e => e.MoneyReach)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.PablishDate).HasColumnType("date");

                entity.Property(e => e.ProjectImage).HasColumnType("image");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Project__Creator__4D94879B");
            });

            modelBuilder.Entity<ProjectCreator>(entity =>
            {
                entity.HasIndex(e => e.BrandName)
                    .HasName("UQ__ProjectC__2206CE9BA9C0636B")
                    .IsUnique();

                entity.HasIndex(e => e.CreatorDetailsId)
                    .HasName("UQ__ProjectC__F097422047C4B175")
                    .IsUnique();

                entity.Property(e => e.About).HasColumnType("ntext");

                entity.Property(e => e.BirthDay).HasColumnType("date");

                entity.Property(e => e.BrandName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Link)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ProfileImage).HasColumnType("image");

                entity.HasOne(d => d.CreatorDetails)
                    .WithOne(p => p.ProjectCreator)
                    .HasForeignKey<ProjectCreator>(d => d.CreatorDetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProjectCr__Creat__3F466844");
            });
        }
    }
}
