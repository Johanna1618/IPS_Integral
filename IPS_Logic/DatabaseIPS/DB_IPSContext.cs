using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace IPS_Logic.DatabaseIPS
{
    public partial class DB_IPSContext : DbContext
    {
        public DB_IPSContext()
        {
        }

        public DB_IPSContext(DbContextOptions<DB_IPSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CitaMedica> CitaMedicas { get; set; }
        public virtual DbSet<Ciudad> Ciudads { get; set; }
        public virtual DbSet<Convenio> Convenios { get; set; }
        public virtual DbSet<Ips> Ips { get; set; }
        public virtual DbSet<Medico> Medicos { get; set; }
        public virtual DbSet<MedicoPorSede> MedicoPorSedes { get; set; }
        public virtual DbSet<Paciente> Pacientes { get; set; }
        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Sede> Sedes { get; set; }
        public virtual DbSet<Servicio> Servicios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=DB_IPS; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CitaMedica>(entity =>
            {
                entity.ToTable("CitaMedica");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdMedico).HasColumnName("idMedico");

                entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");

                entity.Property(e => e.IdSede).HasColumnName("idSede");

                entity.Property(e => e.TipoCita).HasColumnName("tipoCita");

                entity.Property(e => e.ValorCita).HasColumnName("valorCita");

                entity.HasOne(d => d.IdMedicoNavigation)
                    .WithMany(p => p.CitaMedicas)
                    .HasForeignKey(d => d.IdMedico)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CitaMedica_Medico");

                entity.HasOne(d => d.IdPacienteNavigation)
                    .WithMany(p => p.CitaMedicas)
                    .HasForeignKey(d => d.IdPaciente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CitaMedica_Paciente");

                entity.HasOne(d => d.IdSedeNavigation)
                    .WithMany(p => p.CitaMedicas)
                    .HasForeignKey(d => d.IdSede)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CitaMedica_Sede");
            });

            modelBuilder.Entity<Ciudad>(entity =>
            {
                entity.ToTable("Ciudad");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Convenio>(entity =>
            {
                entity.ToTable("Convenio");

                entity.Property(e => e.Descuento).HasColumnName("descuento");

                entity.Property(e => e.HorarioA)
                    .HasColumnType("date")
                    .HasColumnName("horarioA");

                entity.Property(e => e.HorarioB)
                    .HasColumnType("date")
                    .HasColumnName("horarioB");

                entity.Property(e => e.Mensualidad).HasColumnName("mensualidad");

                entity.Property(e => e.TipoConvenio).HasColumnName("tipoConvenio");
            });

            modelBuilder.Entity<Ips>(entity =>
            {
                entity.ToTable("IPS");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Medico>(entity =>
            {
                entity.ToTable("Medico");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

                entity.Property(e => e.IdPersona).HasColumnName("idPersona");

                entity.Property(e => e.IdServicio).HasColumnName("idServicio");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Medicos)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Medico_Persona");

                entity.HasOne(d => d.IdServicioNavigation)
                    .WithMany(p => p.Medicos)
                    .HasForeignKey(d => d.IdServicio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Medico_Servicios");
            });

            modelBuilder.Entity<MedicoPorSede>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MedicoPorSede");

                entity.Property(e => e.IdMedico).HasColumnName("idMedico");

                entity.Property(e => e.IdSede).HasColumnName("idSede");

                entity.HasOne(d => d.IdSedeNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdSede)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MedicoPorSede_Medico");

                entity.HasOne(d => d.IdSede1)
                    .WithMany()
                    .HasForeignKey(d => d.IdSede)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MedicoPorSede_Sede");
            });

            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.ToTable("Paciente");

                entity.Property(e => e.IdConvenio).HasColumnName("idConvenio");

                entity.Property(e => e.IdPersona).HasColumnName("idPersona");

                entity.HasOne(d => d.IdConvenioNavigation)
                    .WithMany(p => p.Pacientes)
                    .HasForeignKey(d => d.IdConvenio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Paciente_Convenio");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Pacientes)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Paciente_Persona");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.ToTable("Persona");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("apellidos");

                entity.Property(e => e.Cedula)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cedula");

                entity.Property(e => e.Contraseña)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("contraseña");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Sede>(entity =>
            {
                entity.ToTable("Sede");

                entity.Property(e => e.IdCiudad).HasColumnName("idCiudad");

                entity.Property(e => e.IdIps).HasColumnName("idIps");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.HasOne(d => d.IdCiudadNavigation)
                    .WithMany(p => p.Sedes)
                    .HasForeignKey(d => d.IdCiudad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sede_Ciudad");

                entity.HasOne(d => d.IdIpsNavigation)
                    .WithMany(p => p.Sedes)
                    .HasForeignKey(d => d.IdIps)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sede_IPS");
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.Property(e => e.CostoAfiliado).HasColumnName("costoAfiliado");

                entity.Property(e => e.CostoNoAfiliado).HasColumnName("costoNoAfiliado");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
