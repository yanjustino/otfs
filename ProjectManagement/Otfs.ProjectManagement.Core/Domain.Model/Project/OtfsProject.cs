using Otfs.ProjectManagement.Core.Domain.Model.Iteration;
using System.Collections.Generic;
using System.Linq;

namespace Otfs.ProjectManagement.Core.Domain.Model.Project
{
    public class OtfsProject
    {
        private List<OtfsIteration> _iterations;

        public string Name { get; private set; }
        public OtfsProjectId ProjectId { get; private set; }
        public IEnumerable<OtfsIteration> Iterations { get { return _iterations; } }

        public OtfsProject(OtfsProjectId projectId, string name)
        {
            this.Name = name;
            this.ProjectId = projectId;
            _iterations = new List<OtfsIteration>();
        }

        public void AgendarIteracao(OtfsIteration iteration)
        {
            _iterations.Add(iteration);
        }

        public OtfsIteration RecuperarIteracaoCorrente()
        {
            return this.Iterations.OrderBy(x => x.StartDate).LastOrDefault();
        }

        public OtfsIteration RecuperarIteracaoAnterior()
        {

            if (!PossuiIteracaoCorrente())
                return null;
            else
            {

                var corrente = RecuperarIteracaoCorrente();

                return this.Iterations.Where(OtfsIteration.IteracaoAnterior(corrente))
                                      .OrderBy(x => x.StartDate)
                                      .LastOrDefault();
            }
        }

        public bool PossuiIteracaoCorrente()
        {
            return RecuperarIteracaoCorrente() != null;
        }
    }
}
