namespace AppControlLaboratorios.Models
{
    public class Asistencia
    {
        public int Id { get; set; }
        public string Observaciones { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public int MaquinaId { get; set; }
        public virtual Maquina Maquina { get; set; }
        public int HorarioId { get; set; }
        public virtual Horario Horario { get; set; }
    }
}
