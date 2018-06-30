using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FundMyPortfol.io.Models
{
    public partial class PortofolioContext : IdentityDbContext<User, IdentityRole<long>, long>
    {
        public PortofolioContext()
        {
        }

        public PortofolioContext(DbContextOptions<PortofolioContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BackerBuyPackage> BackerBuyPackage { get; set; }
        public virtual DbSet<BackerFollowCreator> BackerFollowCreator { get; set; }
        public virtual DbSet<Package> Package { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserDetails> UserDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BackerBuyPackage>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(d => d.BackerNavigation)
                    .WithMany(p => p.BackerBuyPackage)
                    .HasForeignKey(d => d.Backer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BackerBuy__Backe__4F7CD00D");

                entity.HasOne(d => d.PackageNavigation)
                    .WithMany(p => p.BackerBuyPackage)
                    .HasForeignKey(d => d.Package)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BackerBuy__Packa__5070F446");

                entity.Property(e => e.DeliveryDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<BackerFollowCreator>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(d => d.BackerNavigation)
                    .WithMany(p => p.BackerFollowCreatorBackerNavigation)
                    .HasForeignKey(d => d.Backer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BackerFol__Backe__534D60F1");

                entity.HasOne(d => d.ProjectCreatorNavigation)
                    .WithMany(p => p.BackerFollowCreatorProjectCreatorNavigation)
                    .HasForeignKey(d => d.ProjectCreator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BackerFol__Proje__5441852A");
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.PackageName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PledgeAmount)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0.00))");

                entity.HasOne(d => d.ProjectNavigation)
                    .WithMany(p => p.Package)
                    .HasForeignKey(d => d.Project)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Package__Project__4CA06362");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasIndex(e => e.Title)
                    .HasName("UQ__Project__2CB664DCA569949C")
                    .IsUnique();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.ExpireDate).HasColumnType("date");

                entity.Property(e => e.MoneyGoal).HasColumnType("money");

                entity.Property(e => e.MoneyReach)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.PablishDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProjectImage).HasColumnType("ntext");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CategoryString)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('innovation')");

                entity.HasOne(d => d.ProjectCtratorNavigation)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.ProjectCtrator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Project__Project__46E78A0C");

            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.UserDetails)
                    .HasName("UQ__User__096601D9463255F4")
                    .IsUnique();
                
                entity.HasOne(d => d.UserDetailsNavigation)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.UserDetails)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__UserDetail__3F466844");
            });

            modelBuilder.Entity<UserDetails>(entity =>
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

                entity.Property(e => e.ProfileImage).HasColumnType("ntext");

                entity.Property(e => e.Street).HasMaxLength(100);

                entity.Property(e => e.Town).HasMaxLength(20);
            });
        }
    }
}











