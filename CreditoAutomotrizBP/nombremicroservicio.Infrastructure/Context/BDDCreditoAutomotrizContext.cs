using CreditoAutomotriz.Entities.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CreditoAutomotriz.Infrastructure.Context
{
    public partial class BDDCreditoAutomotrizContext : DbContext 
    {
        public BDDCreditoAutomotrizContext(DbContextOptions<BDDCreditoAutomotrizContext> options) : base(options)
        {
        }


        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Ejecutivo> Ejecutivo { get; set; }
        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<Patio> Patio { get; set; }
        public virtual DbSet<Vehiculo> Vehiculo { get; set; }
        public virtual DbSet<AsignacionCliente> AsignacionCliente { get; set; }
        public virtual DbSet<SolicitudCredito> SolicitudCredito { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AI");

            modelBuilder.Entity<AsignacionCliente>(entity =>
            {
                //entity.HasOne(d => d.ClienteAsignacion)
                //    .WithMany(p => p.AsignacionClientes)
                //    .HasForeignKey(d => d.IdCliente)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_asignacion_cliente_cliente");

                //entity.HasOne(d => d.PatioAsignacion)
                //    .WithMany(p => p.AsignacionClientesPatio)
                //    .HasForeignKey(d => d.IdPatio)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_asignacion_cliente_patio");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.Identificacion)
                    .IsUnicode(false);

                entity.Property(e => e.Nombres)
                    .IsUnicode(false);

                entity.Property(e => e.Apellidos)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .IsUnicode(false);

                entity.Property(e => e.IdentificacionConyuge)
                    .IsUnicode(false);

                entity.Property(e => e.NombresConyuge)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ejecutivo>(entity =>
            {
                entity.Property(e => e.Identificacion)
                    .IsUnicode(false);

                entity.Property(e => e.Nombres)
                    .IsUnicode(false);

                entity.Property(e => e.Apellidos)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .IsUnicode(false);

                //entity.HasOne(d => d.PatioEjecutivo)
                //   .WithMany(p => p.EjecutivosPatio)
                //   .HasForeignKey(d => d.IdPatioLabora)
                //   .OnDelete(DeleteBehavior.ClientSetNull)
                //   .HasConstraintName("FK_patio_ejecutivo");
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.Property(e => e.Modelo)
                    .IsUnicode(false);

                //entity.HasOne(d => d.MarcaVehiculo)
                //    .WithMany(p => p.VehiculosMarca)
                //    .HasForeignKey(d => d.IdMarca)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_marca_vehiculo");
            });

            //modelBuilder.Entity<SolicitudCredito>(entity =>
            //{
            //    //entity.HasOne(d => d.ClienteSolicitud)
            //    //    .WithMany(p => p.Solicitudes)
            //    //    .HasForeignKey(d => d.IdCliente)
            //    //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    //    .HasConstraintName("FK_cliente_solicitud_credito");

            //    //entity.HasOne(d => d.EjecutivoSolicitud)
            //    //    .WithMany(p => p.SolicitudesEjecutivo)
            //    //    .HasForeignKey(d => d.IdEjecutivo)
            //    //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    //    .HasConstraintName("FK_ejecutivo_solicitud_credito");

            //    //entity.HasOne(d => d.PatioSolicitud)
            //    //    .WithMany(p => p.SolicitudesPatio)
            //    //    .HasForeignKey(d => d.IdPatio)
            //    //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    //    .HasConstraintName("FK_patio_solicitud_credito");

            //    //entity.HasOne(d => d.VehiculoSolicitud)
            //    //    .WithMany(p => p.SolicitudesVehiculo)
            //    //    .HasForeignKey(d => d.IdVehiculo)
            //    //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    //    .HasConstraintName("FK_vehiculo_solicitud_credito");
            //});

            //OnModelCreatingPartial(modelBuilder);
        }

    }
}
