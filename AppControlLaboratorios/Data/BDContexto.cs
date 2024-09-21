using AppControlLaboratorios.Models;
using Microsoft.EntityFrameworkCore;

namespace AppControlLaboratorios.Data
{
    public class BDContexto : DbContext
    {
        public BDContexto(DbContextOptions options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Asistencia> Asistencias { get; set; }
        public DbSet<Laboratorio> Laboratorios { get; set; }
        public DbSet<Maquina> Maquinas { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<UsuarioCurso> UsuarioCursos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación Usuario y Rol (1 a Muchos)
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.RolId)
                .OnDelete(DeleteBehavior.Restrict);  // Evitar el borrado en cascada

            // Relación Usuario y Horario (1 a Muchos)
            modelBuilder.Entity<Horario>()
                .HasOne(h => h.Usuario)
                .WithMany(u => u.Horarios)
                .HasForeignKey(h => h.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);  // Evitar el borrado en cascada

            // Relación Usuario y Asistencia (1 a Muchos)
            modelBuilder.Entity<Asistencia>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Asistencias)
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);  // Evitar el borrado en cascada

            // Relación Asistencia y Maquina (1 a Muchos)
            modelBuilder.Entity<Asistencia>()
                .HasOne(a => a.Maquina)
                .WithMany(m => m.Asistencias)
                .HasForeignKey(a => a.MaquinaId)
                .OnDelete(DeleteBehavior.Restrict);  // Evitar el borrado en cascada

            // Relación Horario y Laboratorio (1 a Muchos)
            modelBuilder.Entity<Horario>()
                .HasOne(h => h.Laboratorio)
                .WithMany(l => l.Horarios)
                .HasForeignKey(h => h.LaboratorioId)
                .OnDelete(DeleteBehavior.Restrict);  // Evitar el borrado en cascada

            // Relación Horario y Curso (1 a Muchos)
            modelBuilder.Entity<Horario>()
                .HasOne(h => h.Curso)
                .WithMany(c => c.Horarios)
                .HasForeignKey(h => h.CursoId)
                .OnDelete(DeleteBehavior.Restrict);  // Evitar el borrado en cascada

            //Relación UsuarioCurso
            modelBuilder.Entity<UsuarioCurso>()
                .HasKey(uc => new { uc.UsuarioId, uc.CursoId }); // Definir clave primaria compuesta

            modelBuilder.Entity<UsuarioCurso>()
                .HasOne(uc => uc.Usuario)
                .WithMany(u => u.UsuarioCursos)
                .HasForeignKey(uc => uc.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UsuarioCurso>()
                .HasOne(uc => uc.Curso)
                .WithMany(c => c.UsuarioCursos)
                .HasForeignKey(uc => uc.CursoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de las tablas
            modelBuilder.Entity<Curso>().ToTable("Curso");
            modelBuilder.Entity<Horario>().ToTable("Horario");
            modelBuilder.Entity<Laboratorio>().ToTable("Laboratorio");
            modelBuilder.Entity<Maquina>().ToTable("Maquina");
            modelBuilder.Entity<Rol>().ToTable("Rol");
            modelBuilder.Entity<Asistencia>().ToTable("Asistencia");
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<UsuarioCurso>().ToTable("UsuarioCurso");
        }
    }
}
