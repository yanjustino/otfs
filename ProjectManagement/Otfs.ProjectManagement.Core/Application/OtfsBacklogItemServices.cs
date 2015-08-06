using Otfs.ProjectManagement.Core.Domain.Model;
using Otfs.ProjectManagement.Core.Domain.Model.BacklogItem;
using Otfs.ProjectManagement.Core.Domain.Model.Iteration;
using Otfs.ProjectManagement.Core.Domain.Model.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otfs.ProjectManagement.Core.Application
{
    public class OtfsBacklogItemServices : IOtfsBacklogItemServices
    {
        private IOtfsIterationRepository _otfsIterationRepository;
        private IOtfsBacklogItemRepository _otfsBacklogItemRepository;

        public OtfsBacklogItemServices(
            IOtfsIterationRepository otfsIterationRepository, 
            IOtfsBacklogItemRepository otfsBacklogItemRepository)
        {
            _otfsIterationRepository = otfsIterationRepository;
            _otfsBacklogItemRepository = otfsBacklogItemRepository;
        }

        public IEnumerable<OtfsBacklogItem> RetornarBacklogsItemsParaBuild()
        {
            var iterations = _otfsIterationRepository.Correntes();

            return _otfsBacklogItemRepository.ItensParaBuild(iterations.ToArray());
        }

        public OtfsBacklogItemCollection RetorarBacklogItens(OtfsProjectId projectId, string iteration = null)
        {
            var sprint = _otfsIterationRepository.PorIteracao(projectId, iteration);

            return _otfsBacklogItemRepository.LocalizarPorIteracao(sprint);
        }

        public OtfsBacklogItem AprovarBackLogItemParaCompilacao(OtfsBacklogItemId backlogItemId, string iteration)
        {
            var backlogItem = _otfsBacklogItemRepository.Localizar(backlogItemId);
            var interationPath = _otfsIterationRepository.PorIteracao(backlogItem.ProjectId, iteration);
            
            backlogItem.MarcarComoAprovado("Tarefa Aprovado em ambiente de Aceitação");
            backlogItem.Comitar(interationPath);

            _otfsBacklogItemRepository.Atualizar(backlogItem);

            return backlogItem;
        }
        
        public OtfsBacklogItem ConcluirBackLogItemParaCompilacao(OtfsBacklogItemId backlogItemId, string iteration)
        {
            var backlogItem = _otfsBacklogItemRepository.Localizar(backlogItemId);
            var interationPath = _otfsIterationRepository.PorIteracao(backlogItem.ProjectId, iteration);

            backlogItem.AlterarStatusParaDone();
            backlogItem.Comitar(interationPath);

            _otfsBacklogItemRepository.Atualizar(backlogItem);

            return backlogItem;
        }

        public OtfsBacklogItem RejeitarBackLogItem(OtfsBacklogItemId backlogItemId, string iteration, string motivo)
        {
            var backlogItem = _otfsBacklogItemRepository.Localizar(backlogItemId);
            var interationPath = _otfsIterationRepository.PorIteracao(backlogItem.ProjectId, iteration);

            backlogItem.MarcarComoRejeitado(motivo);
            backlogItem.Comitar(interationPath);

            _otfsBacklogItemRepository.Atualizar(backlogItem);

            return backlogItem;
        }

        public void MarcarBackLogItemPublicado(OtfsProjectId projetoId)
        {
            var interation = _otfsIterationRepository.Correntes().Where(dado => dado.ProjectId.Id == projetoId.Id).FirstOrDefault();

            var backlogItems = _otfsBacklogItemRepository.ItensParaBuild(interation);
            foreach (var item in backlogItems)
            {
                item.MarcarComoPublicado("Aplicação publicada com sucesso");
                item.Comitar(interation);
                _otfsBacklogItemRepository.Atualizar(item);
            }
        }
    }
}
