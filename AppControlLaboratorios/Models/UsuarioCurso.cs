namespace AppControlLaboratorios.Models
{
    public class UsuarioCurso
    {
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int CursoId { get; set; }
        public Curso Curso { get; set; }
    }
}
