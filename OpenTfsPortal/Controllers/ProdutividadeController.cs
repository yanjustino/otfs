using OpenTfsPortal.Models;
using Otfs.GestaoHoras.Core.Aplication;
using Otfs.GestaoHoras.Core.Domain.Model;
using Otfs.GestaoHoras.Data;
using Otfs.GestaoHoras.Data.Externa;
using System;
using System.Web.Mvc;


namespace OpenTfsPortal.Controllers
{
    [Authorize]
    public class ProdutividadeController : ApplicationController
    {
        private IOtfsProdutividadeService<OtfsProdutividadeRepository> _tfsRepository;
        private IOtfsProdutividadeService<OtfsHorasPontoRepository> _pontoRepository;
        private IOtfsProdutividadeService<OtfsHorasItopRepository> _itopRepository;
        private IOtfsLancamentoHoraService _otfsLancamentoHoraService;


        public ProdutividadeController(
            IOtfsProdutividadeService<OtfsProdutividadeRepository> tfsRepository,
            IOtfsProdutividadeService<OtfsHorasPontoRepository> pontoRepository,
            IOtfsProdutividadeService<OtfsHorasItopRepository> itopRepository,
            IOtfsLancamentoHoraService otfsLancamentoHoraService)
        {
            _tfsRepository = tfsRepository;
            _itopRepository = itopRepository;
            _pontoRepository = pontoRepository;
            _otfsLancamentoHoraService = otfsLancamentoHoraService;
        }

        public ActionResult ListarHoras(int taskId, string title)
        {
            var listarHoras = new ListaHorasViewModel
            {
                TaskId = taskId,
                Title = title,
                HorasLancadas = _otfsLancamentoHoraService.ListarHoras(taskId)
            };

            return View(listarHoras);
        }

        public ActionResult LancarHora(int taskId, string title)
        {
            var model = new LancamentoHoraViewModel
            {
                TaskId = taskId,
                UserId = this.UsuarioAutorizadoSession.Id,
                Data = DateTime.Today,
                DataLancamento = DateTime.Today,
                Title = title
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult LancarHora(LancamentoHora model)
        {
            _otfsLancamentoHoraService.InserirLancamentoHora(model);

            return Redirect("~/Dashboard/Desenvolvedor");
        }

        public ActionResult ExcluirHora(int id)
        {
            return View(_otfsLancamentoHoraService.BuscarHoraPorId(id));
        }

        [HttpPost]
        public ActionResult ExcluirHora(int id, int taskId, string title)
        {
            _otfsLancamentoHoraService.ExcluirHora(id);

            var listarHoras = new ListaHorasViewModel
            { 
                TaskId = taskId, 
                Title = title,
                HorasLancadas = _otfsLancamentoHoraService.ListarHoras(taskId)
            };

            return View("ListarHoras", listarHoras);
        }

        public ActionResult EditarHora(int id, string title)
        {
            var lancamentoHora = _otfsLancamentoHoraService.BuscarHoraPorId(id);

            var model = new LancamentoHoraViewModel
            {
                Id = lancamentoHora.Id,
                TaskId = lancamentoHora.TaskId,
                UserId = lancamentoHora.UserId,
                Data = lancamentoHora.Data,
                HoraTrabalhada = lancamentoHora.HoraTrabalhada,
                DataLancamento = lancamentoHora.DataLancamento,
                Historico = lancamentoHora.Historico,
                Title = title
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult EditarHora(LancamentoHora model)
        {
            _otfsLancamentoHoraService.AtualizarHora(model);

            var listarHoras = new ListaHorasViewModel 
            { 
                TaskId = model.TaskId, 
                HorasLancadas = _otfsLancamentoHoraService.ListarHoras(model.TaskId)
            };

            return View("ListarHoras", listarHoras);
        }

        public ActionResult DashboardHoras()
        {
            var data = DateTime.Parse("01/03/2015");
            var userId = this.UsuarioAutorizadoSession.Id;

            var horasTfs = _tfsRepository.ListarHorasMes(userId, data.Month);
            var horasponto = _pontoRepository.ListarHorasMes(userId, data.Month);
            var horasItop = _itopRepository.ListarHorasMes(userId, data.Month);

            return View(new DashboardHoraViewModel(data, horasTfs, horasponto, horasItop));
        }
    }
}