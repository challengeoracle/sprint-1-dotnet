using Medix.Models;

namespace Medix.ViewModels
{
    public class DashboardViewModel
    {
        public string UserName { get; set; }
        public int TotalUnidades { get; set; }
        public int UnidadesAtivas { get; set; }
        public int UnidadesSuspensas { get; set; }
        public int UnidadesInativas { get; set; }
        public List<UnidadeMedica> AtividadeRecente { get; set; }
        public Dictionary<string, int> StatusDistribution { get; set; }
    }
}