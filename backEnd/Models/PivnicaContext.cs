using Microsoft.EntityFrameworkCore;

namespace backEnd.Models
{
    public class PivnicaContext : DbContext
    {
        public DbSet<Pivnica> Pivnice { get; set; }
        public DbSet <Sto> Stolovi { get; set; }
        public DbSet<PivoHrana> PivaHrana { get; set; }
        public DbSet<Meni> Meni { get; set; }
        public DbSet <Narudzbina> Narudzbine { get; set; }
       


        public PivnicaContext(DbContextOptions options) : base(options)
        {

        }

    }
}