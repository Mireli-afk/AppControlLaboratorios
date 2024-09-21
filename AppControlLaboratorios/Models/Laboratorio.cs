namespace AppControlLaboratorios.Models
{
    public class Laboratorio
    {
        public int Id { get; set; }
        public string LaboratorioNombre { get; set; }
        public ICollection<Horario> Horarios { get; set; }
        public ICollection<Maquina> Maquinas { get; set; }
    }
}
