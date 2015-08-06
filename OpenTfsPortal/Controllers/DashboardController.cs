using OpenTfsPortal.Models;
using Otfs.ProjectManagement.Core.Application;
using Otfs.ProjectManagement.Core.Domain.Model;
using Otfs.ProjectManagement.Core.Domain.Model.BacklogItem;
using Otfs.ProjectManagement.Core.Domain.Model.Project;
using System;
using System.Web.Mvc;

namespace OpenTfsPortal.Controllers
{
    [Authorize]
    [RoutePrefix("Dashboard")]
    public class DashboardController : ApplicationController
    {
        private IOtfsBacklogItemServices _otfsBacklogItemServices;
        private IOtfsProjectServices _otfsProjectServices;
        private IOtfsTaskServices _otfsTaskServices;

        public DashboardController(
            IOtfsBacklogItemServices otfsBacklogItemServices,
            IOtfsProjectServices otfsProjectServices,
            IOtfsTaskServices otfsTaskServices)
        {
            _otfsTaskServices = otfsTaskServices;
            _otfsProjectServices = otfsProjectServices;
            _otfsBacklogItemServices = otfsBacklogItemServices;
        }


        [Route("Desenvolvedor")]
        public ActionResult DashboardDesenvolvedor()
        {
            var dashboardDesenvolvedor = new DashboardDesenvolvedorViewModel
            {
                UserId = UsuarioAutorizadoSession.Id,
                Tarefas = _otfsTaskServices.RetornarTarefas(),
                Desenvolvedor = UsuarioAutorizadoSession.Nome
            };

            return View(dashboardDesenvolvedor);
        }


        [Route("Timer")]
        public ActionResult DashboardTimer()
        {
            var id = new OtfsProjectId(202);

            var backlogsItemsPendentes = _otfsBacklogItemServices.RetorarBacklogItens(id, "documentosfiscais.set.govrn\\Implementações de Abril");

            var model = new DashboardAuditorViewModel
            {
                Interation = "documentosfiscais.set.govrn\\Implementações de Abril",
                FimSprint = null,
                InicioSprint = null,
                ItemsPendentes = backlogsItemsPendentes.BacklogsItemsPendentes,
                ItemsAndamento = backlogsItemsPendentes.BacklogsItemsAndamento,
                ItemsComitadas = backlogsItemsPendentes.BacklogsItemsParaTestes,
                ItemsParaBuild = backlogsItemsPendentes.BacklogsItemsParaBuild,
                ItemsPublicados = backlogsItemsPendentes.BacklogsItemsPublicados
            };

            return View(model);
        }


        [Route("Auditor")]
        public ActionResult DashboardAuditorFiscal(int projectId, string path, DateTime? inicio, DateTime? fim)
        {
            var id = new OtfsProjectId(projectId);

            var backlogsItemsPendentes = _otfsBacklogItemServices.RetorarBacklogItens(id, path);

            var model = new DashboardAuditorViewModel
            {
                Interation = path,
                FimSprint = fim,
                InicioSprint = inicio,
                ItemsPendentes = backlogsItemsPendentes.BacklogsItemsPendentes,
                ItemsAndamento = backlogsItemsPendentes.BacklogsItemsAndamento,
                ItemsComitadas = backlogsItemsPendentes.BacklogsItemsParaTestes,
                ItemsParaBuild = backlogsItemsPendentes.BacklogsItemsParaBuild,
                ItemsPublicados = backlogsItemsPendentes.BacklogsItemsPublicados
            };

            return View(model);
        }

        [Route("Infra")]
        public ActionResult DashboardInfraestrutura()
        {
            var projetos = _otfsProjectServices.RetornarTodosProjetos();
            var estorias = _otfsBacklogItemServices.RetornarBacklogsItemsParaBuild();

            return View(new DashboardInfraViewModel(projetos, estorias));
        }

        [HttpPost]
        public ActionResult Approve(int backlogItemId, string iteration)
        {
            var backlogItem = _otfsBacklogItemServices.AprovarBackLogItemParaCompilacao(new OtfsBacklogItemId(backlogItemId), iteration);

            var routeObject = new
            {
                projectId = backlogItem.ProjectId.Id,
                path = backlogItem.Iteration.Path,
                inicio = backlogItem.Iteration.StartDate,
                fim = backlogItem.Iteration.EndDate
            };

            return RedirectToAction("DashboardAuditorFiscal", routeObject);
        }

        [HttpPost]
        public ActionResult Concluir(int backlogItemId, string iteration)
        {
            var backlogItem = _otfsBacklogItemServices.ConcluirBackLogItemParaCompilacao(new OtfsBacklogItemId(backlogItemId), iteration);

            var routeObject = new
            {
                projectId = backlogItem.ProjectId.Id,
                path = backlogItem.Iteration.Path,
                inicio = backlogItem.Iteration.StartDate,
                fim = backlogItem.Iteration.EndDate
            };

            return RedirectToAction("DashboardAuditorFiscal", routeObject);
        }

        [HttpPost]
        public ActionResult Deny(int backlogItemId, string motivo, string iteration)
        {
            var mensagem = string.Format("Tarefa reprovada por {0} motivo: {1}", User.Identity.Name, motivo);
            var backlogItem = _otfsBacklogItemServices.RejeitarBackLogItem(new OtfsBacklogItemId(backlogItemId), iteration, mensagem);

            var routeObject = new
            {
                projectId = backlogItem.ProjectId.Id,
                path = backlogItem.Iteration.Path,
                inicio = backlogItem.Iteration.StartDate,
                fim = backlogItem.Iteration.EndDate
            };

            return RedirectToAction("DashboardAuditorFiscal", routeObject);
        }
        
        public ActionResult MarcarProjetoPublicado(int id)
        {
            _otfsBacklogItemServices.MarcarBackLogItemPublicado(new OtfsProjectId(id));

            return RedirectToAction("DashboardInfraestrutura");
        }

        public ActionResult Preview()
        {
            return View();
        }
    }
}
