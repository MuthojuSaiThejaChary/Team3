using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TwitterClone.Model
{
    public partial class TwitterDatabaseContext : DbContext
    {
        public TwitterDatabaseContext()
        {
        }

        public TwitterDatabaseContext(DbContextOptions<TwitterDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Following> Followings { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Tweet> Tweets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source = SAITHEJA\\SQLEXPRESS;Initial Catalog = TwitterDatabase; Integrated Security = True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Following>(entity =>
            {
                entity.ToTable("Following");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FollowingId)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Following_Id");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.FollowingNavigation)
                    .WithMany(p => p.FollowingFollowingNavigations)
                    .HasForeignKey(d => d.FollowingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Following__Follo__2D27B809");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FollowingUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Following__user___2C3393D0");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Person__206A9DF884FC043C");

                entity.ToTable("Person");

                entity.Property(e => e.UserId)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("User_id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Joined).HasColumnType("date");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tweet>(entity =>
            {
                entity.ToTable("Tweet");

                entity.Property(e => e.TweetId).HasColumnName("tweet_id");

                entity.Property(e => e.Created).HasColumnType("date");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("User_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tweets)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tweet__User_id__267ABA7A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
