using Otfs.ProjectManagement.Core.Domain.Model;
using Otfs.ProjectManagement.Core.Domain.Model.BacklogItem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenTfsPortal.Models
{
    public class DashboardAuditorViewModel
    {
        public string Interation { get; set; }
        public DateTime? FimSprint { get; set; }
        public DateTime? InicioSprint { get; set; }
        public IEnumerable<OtfsBacklogItem> ItemsPendentes { get; set; }
        public IEnumerable<OtfsBacklogItem> ItemsAndamento { get; set; }
        public IEnumerable<OtfsBacklogItem> ItemsComitadas { get; set; }
        public IEnumerable<OtfsBacklogItem> ItemsParaBuild { get; set; }
        public IEnumerable<OtfsBacklogItem> ItemsPublicados { get; set; }
        public IEnumerable<OtfsBacklogItem> ItemsConcluidos { get; set; }

        public string DataFimSprintFormatada
        {
            get
            {
                return FimSprint.HasValue ? FimSprint.Value.ToShortDateString() : string.Empty;
            }
        }

        public string DataInicioSprintFormatada
        {
            get
            {
                return InicioSprint.HasValue ? InicioSprint.Value.ToShortDateString() : string.Empty;
            }
        }

        public string DiasParaFimIteracao
        {
            get
            {
                if (FimSprint.HasValue)
                {
                    TimeSpan ts = FimSprint.Value - DateTime.Now;
                    return ts.Days > 0 ? ts.Days + " Para fim da iteração" : "Iteração concluída";
                }
                return string.Empty;
            }
        }

        public double TotalDeTarefas
        {
            get
            {
                return ItemsPendentes.Count() + 
                       ItemsAndamento.Count() + 
                       ItemsComitadas.Count() + 
                       ItemsParaBuild.Count() +
                       ItemsPublicados.Count();
            }
        }

        public double PercentualTarefasPendentes
        {
            get
            {
                return TotalDeTarefas > 0 ? Math.Round(ItemsPendentes.Count() / TotalDeTarefas * 100, 2) : 0;
            }
        }

        public double PercentualTarefasEmaAndamento
        {
            get
            {
                return TotalDeTarefas > 0 ? Math.Round((ItemsAndamento.Count() + ItemsComitadas.Count()) / TotalDeTarefas * 100, 2) : 0;
            }
        }


        public double PercentualTarefasConcluidas
        {
            get
            {
                return TotalDeTarefas > 0 ? Math.Round((ItemsParaBuild.Count() + ItemsPublicados.Count()) / TotalDeTarefas * 100, 2) : 0;
            }
        }

        public double PercentualEsforcoRealizado
        {
            get
            {
                var quantidadeDiasDoSprint = FimSprint.Value - InicioSprint.Value;
                var quantidadeDiasPassados = DateTime.Now - InicioSprint.Value;

                var mediaDeTarefasPorDia = Math.Round(TotalDeTarefas / (double)quantidadeDiasDoSprint.Days, 2);
                var expectativaDeTarefas = quantidadeDiasPassados.Days * mediaDeTarefasPorDia;
                return expectativaDeTarefas > 0 ? ItemsComitadas.Count() / expectativaDeTarefas * 100 : 0;
            }
        }
    }
}