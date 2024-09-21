namespace AppControlLaboratorios.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public int RolId { get; set; }
        public virtual Rol Rol { get; set; }
        public virtual ICollection<Horario> Horarios { get; set; }
        public virtual ICollection<Asistencia> Asistencias { get; set; }
        public ICollection<UsuarioCurso> UsuarioCursos { get; set; }
    }
}
