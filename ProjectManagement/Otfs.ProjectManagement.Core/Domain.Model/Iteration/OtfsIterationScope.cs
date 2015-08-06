using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otfs.ProjectManagement.Core.Domain.Model.Iteration
{
    public partial class OtfsIteration
    {

        public static Func<OtfsIteration, bool> Agendado
        {
            get
            {
                return x => x.EndDate.HasValue && x.StartDate.HasValue;
            }
        }


        public static Func<OtfsIteration, bool> IteracaoCorrente
        {
            get
            {
                return x => OtfsIteration.Agendado(x) &&
                            (DateTime.Today >= x.StartDate.Value &&
                             DateTime.Today <= x.EndDate.Value);
            }
        }


        public static Func<OtfsIteration, bool> IteracaoAnterior(OtfsIteration corrente)
        {
            return x => x.EndDate < corrente.StartDate;
        }

    }
}
