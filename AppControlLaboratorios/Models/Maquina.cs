namespace AppControlLaboratorios.Models
{
    public class Maquina
    {
        public int Id { get; set; }
        public string NumSerie { get; set; }
        public int LaboratorioId { get; set; }
        public virtual Laboratorio? Laboratorio { get; set; }
        public virtual ICollection<Asistencia> Asistencias { get; set; }
    }
}
