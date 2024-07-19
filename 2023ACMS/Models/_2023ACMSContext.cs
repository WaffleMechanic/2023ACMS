using Microsoft.EntityFrameworkCore;

namespace _2023ACMS.Models
{
    public partial class _2023ACMSContext : DbContext
    {
        public _2023ACMSContext()
        {
        }

        public _2023ACMSContext(DbContextOptions<_2023ACMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Artwork> Artwork { get; set; } = null!;
        public virtual DbSet<Default> Default { get; set; } = null!;
        public virtual DbSet<Media> Media { get; set; } = null!;
        public virtual DbSet<MediaSpecialty> MediaSpecialty { get; set; } = null!;
        public virtual DbSet<Person> Person { get; set; } = null!;
        public virtual DbSet<Student> Student { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //                optionsBuilder.UseSqlServer("Server=DESKTOP-UHK8BJ6\\SQLEXPRESS; Database=2023ACMS; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artwork>(entity =>
            {
                entity.Property(e => e.ArtworkId).HasColumnName("ArtworkID");

                entity.Property(e => e.MediaId).HasColumnName("MediaID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.Artwork)
                    .HasForeignKey(d => d.MediaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Artwork_Media");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Artwork)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_Artwork_User");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Artwork)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_Artwork_Student");
            });

            modelBuilder.Entity<Default>(entity =>
            {
                entity.Property(e => e.DefaultId).HasColumnName("DefaultID");
            });

            modelBuilder.Entity<Media>(entity =>
            {
                entity.Property(e => e.MediaId).HasColumnName("MediaID");

                entity.Property(e => e.Media1)
                    .HasMaxLength(50)
                    .HasColumnName("Media");
            });

            modelBuilder.Entity<MediaSpecialty>(entity =>
            {
                entity.Property(e => e.MediaSpecialtyId).HasColumnName("MediaSpecialtyID");

                entity.Property(e => e.MediaId).HasColumnName("MediaID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.MediaSpecialty)
                    .HasForeignKey(d => d.MediaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MediaSpecialty_Media");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.MediaSpecialty)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_MediaSpecialty_User");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(1);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.MediaPreferenceId).HasColumnName("MediaPreferenceID");

                entity.Property(e => e.ParentEmail).HasMaxLength(50);

                entity.Property(e => e.ParentName).HasMaxLength(50);

                entity.Property(e => e.StudentEmail).HasMaxLength(50);

                entity.Property(e => e.StudentFirstName)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.StudentLastName).HasMaxLength(50);

                entity.Property(e => e.TeacherEmail).HasMaxLength(50);

                entity.Property(e => e.TeacherName).HasMaxLength(50);

                entity.HasOne(d => d.MediaPreference)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.MediaPreferenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_Media");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
