using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public partial class HogwartsContext : DbContext
    {
        public HogwartsContext()
        {
        }

        public HogwartsContext(DbContextOptions<HogwartsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdmissionRequest> AdmissionRequests { get; set; } = null!;
        public virtual DbSet<Aspirant> Aspirants { get; set; } = null!;
        public virtual DbSet<House> Houses { get; set; } = null!;
        public virtual DbSet<VAdmissionApplication> VAdmissionApplications { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseSqlServer("Server= localhost;Database=Hogwarts;Integrated Security=true;Encrypt=false;TrustServerCertificate=true");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdmissionRequest>(entity =>
            {
                entity.ToTable("AdmissionRequest", "School");

                entity.HasComment("Tabla de referencia cruzada que asigna un aspirante y una casa para su solicitud de ingreso.");

                entity.HasIndex(e => e.AspirantId, "IX_AdmissionApplication_AspirantID");

                entity.HasIndex(e => e.HouseId, "IX_AdmissionApplication_HouseID");

                entity.Property(e => e.AdmissionRequestId)
                    .HasColumnName("AdmissionRequestID")
                    .HasComment("Primary key de la Solicitud de ingreso registrada.");

                entity.Property(e => e.AspirantId)
                    .HasColumnName("AspirantID")
                    .HasComment("Identificador del Aspirante. Foreign Key to Aspirant.AspirantID.");

                entity.Property(e => e.EndingDate)
                    .HasColumnType("datetime")
                    .HasComment("Fecha en la que finaliza la solicitud.");

                entity.Property(e => e.HouseId)
                    .HasColumnName("HouseID")
                    .HasComment("Identificador de la casa a la que se aspira. Foreign Key to House.HouseID.");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Fecha en la que fue hecha la solicitud.");

                entity.HasOne(d => d.Aspirant)
                    .WithMany(p => p.AdmissionRequests)
                    .HasForeignKey(d => d.AspirantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AdmissionApplication_Aspirant");

                entity.HasOne(d => d.House)
                    .WithMany(p => p.AdmissionRequests)
                    .HasForeignKey(d => d.HouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AdmissionApplication_House");
            });

            modelBuilder.Entity<Aspirant>(entity =>
            {
                entity.ToTable("Aspirant", "School");

                entity.HasComment("Informacion personal del Aspirante.");

                entity.HasIndex(e => e.Dni, "IX_Aspirant_Dni")
                    .IsUnique();

                entity.Property(e => e.AspirantId)
                    .HasColumnName("AspirantID")
                    .HasComment("Primary key del Aspirante registrado.");

                entity.Property(e => e.Age).HasComment("Fecha de nacimiento del aspirante registrado. Utilizada para obtener la edad del aspirante registrado.");

                entity.Property(e => e.Dni).HasComment("Identificacion del aspirante registrado.");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Primer nombre del aspirante registrado.");

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsFixedLength()
                    .HasComment("Genero: M = Masculino F = Femenino.");

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Apellido del aspirante registrado.");
            });

            modelBuilder.Entity<House>(entity =>
            {
                entity.ToTable("House", "School");

                entity.HasComment("Tabla de busqueda que contiene las distintas casas de Hogwarts.");

                entity.Property(e => e.HouseId)
                    .HasColumnName("HouseID")
                    .HasComment("Primary key de la casa registrada.");

                entity.Property(e => e.Founder)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasComment("Nombre completo del fundador de la casa registrada.");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Nombre de la casa registrada.");
            });

            modelBuilder.Entity<VAdmissionApplication>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vAdmissionApplication", "School");

                entity.HasComment("Datos completos de la solicitud de ingreso.");

                entity.Property(e => e.AdmissionApplicationId).HasColumnName("AdmissionApplicationID");

                entity.Property(e => e.AdmissionApplicationStartDate).HasColumnType("datetime");

                entity.Property(e => e.AspirantBirthDate).HasColumnType("date");

                entity.Property(e => e.AspirantFirstName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AspirantId).HasColumnName("AspirantID");

                entity.Property(e => e.AspirantLastName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.HouseId).HasColumnName("HouseID");

                entity.Property(e => e.HouseName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
