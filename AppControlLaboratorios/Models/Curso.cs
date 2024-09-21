namespace AppControlLaboratorios.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string CursoNombre { get; set; }
        public virtual ICollection<Horario> Horarios { get; set; }
        public ICollection<UsuarioCurso> UsuarioCursos { get; set; }
    }
}
