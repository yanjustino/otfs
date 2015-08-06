using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenTfsPortal.Models
{
    public class DashboardHoraViewModel
    {
        public List<RegistroHoraViewModel> RegistrosHora { get; private set; }

        public DashboardHoraViewModel(DateTime dataInicial, 
            Dictionary<DateTime, double> horasTfs,
            Dictionary<DateTime, double> horasPonto,
            Dictionary<DateTime,double> horasItop)
        {
            this.RegistrosHora = GerarCalendario(dataInicial).ToList();

            PreencherCalendario(horasTfs, horasPonto, horasItop);
        }

        private IEnumerable<RegistroHoraViewModel> GerarCalendario(DateTime dataInicial)
        {
            for (int i = 0; i < DateTime.DaysInMonth(dataInicial.Year, dataInicial.Month); i++)
            {
                yield return new RegistroHoraViewModel()
                {
                    Data = dataInicial.AddDays(i),
                    HorasProjetos = 0,
                    HorasIncidente = 0,
                    HorasTotais = 0
                };
            }
        }

        private void PreencherCalendario(Dictionary<DateTime, double> horasTfs, Dictionary<DateTime, double> horasPonto, Dictionary<DateTime, double> horasItop)
        {
            foreach (var item in this.RegistrosHora)
            {
                item.HorasProjetos = horasTfs.Where(x => x.Key == item.Data).FirstOrDefault().Value;
                item.HorasIncidente = horasItop.Where(x => x.Key == item.Data).FirstOrDefault().Value;
                item.HoraPonto = horasPonto.Where(x => x.Key == item.Data).FirstOrDefault().Value;
            }
        }
    }
}