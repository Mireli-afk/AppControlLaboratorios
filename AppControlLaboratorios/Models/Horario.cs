namespace AppControlLaboratorios.Models
{
    public class Horario
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string HoraIni { get; set; }
        public string HoraFin { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public int LaboratorioId { get; set; }
        public virtual Laboratorio Laboratorio { get; set; }
        public int CursoId { get; set; }
        public virtual Curso Curso { get; set; }
    }
}
