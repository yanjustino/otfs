using Otfs.GestaoHoras.Core.Domain.Model;
using System.Data.Entity;

namespace Otfs.GestaoHoras.Data
{
    public class DataContextGestaoHora : DbContext
    {
        public DbSet<HistoricoHora> HistoricoHora { get; set; }
        public DbSet<LancamentoHora> LancamentoHora { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HistoricoHora>().ToTable("HistoricoHoras");
            modelBuilder.Entity<LancamentoHora>().ToTable("LancamentoHoras");
            
            base.OnModelCreating(modelBuilder);
        }
    }
}