using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Medix.Models; 

namespace Medix.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Mapeia o Model UnidadeMedica para uma tabela no banco chamada "UnidadesMedicas"
        public DbSet<UnidadeMedica> UnidadesMedicas { get; set; }
    }
}