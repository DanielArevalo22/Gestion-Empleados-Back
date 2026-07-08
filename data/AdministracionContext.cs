using backCommerce.models;
using Microsoft.EntityFrameworkCore;

namespace backCommerce.Data
{
    public class AdministracionContext : DbContext
    {
        public AdministracionContext(DbContextOptions<AdministracionContext> options)
            : base(options)
        {

        }

        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<EmpleadoProyecto> EmpleadoProyectos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.HasKey(d => d.idDepartamento);
                entity.Property(d => d.departamento).HasMaxLength(100).IsRequired();
                entity.Property(d => d.activo).HasDefaultValue(true);
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.idEmpleado);
                entity.Property(e => e.nombres).HasMaxLength(100).IsRequired();
                entity.Property(e => e.apellidos).HasMaxLength(100).IsRequired();
                entity.Property(e => e.cargo).HasMaxLength(100).IsRequired();
                entity.Property(e => e.activo).HasDefaultValue(true);

                entity.HasOne(e => e.departamento)
                    .WithMany(d => d.empleados)
                    .HasForeignKey(e => e.idDepartamento)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Proyecto>(entity =>
            {
                entity.HasKey(p => p.idProyecto);
                entity.Property(p => p.proyecto).HasMaxLength(150).IsRequired();
                entity.Property(p => p.activo).HasDefaultValue(true);
                entity.Property(p => p.presupuesto).HasPrecision(18, 2);
            });

            modelBuilder.Entity<EmpleadoProyecto>(entity =>
            {
                entity.HasKey(ep => new { ep.idEmpleado, ep.idProyecto });
                entity.Property(ep => ep.rol).HasMaxLength(100).IsRequired();

                entity.HasOne(ep => ep.empleado)
                    .WithMany(e => e.empleadoProyectos)
                    .HasForeignKey(ep => ep.idEmpleado)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ep => ep.proyecto)
                    .WithMany(p => p.empleadoProyectos)
                    .HasForeignKey(ep => ep.idProyecto)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
