using Common.Domain.Model;
using Microsoft.Practices.Unity;
using Otfs.GestaoHoras.Core.Aplication;
using Otfs.GestaoHoras.Core.Domain.Model.Repository;
using Otfs.GestaoHoras.Data;
using Otfs.GestaoHoras.Data.Externa;
using Otfs.IdentityAccess.Core.Application;
using Otfs.IdentityAccess.Core.Domain.Model.Interfaces;
using Otfs.IdentityAccess.Data;
using Otfs.ProjectManagement.Core.Application;
using Otfs.ProjectManagement.Core.Domain.Model.BacklogItem;
using Otfs.ProjectManagement.Core.Domain.Model.Iteration;
using Otfs.ProjectManagement.Core.Domain.Model.Project;
using Otfs.ProjectManagement.Core.Domain.Model.Task;
using Otfs.ProjectManagement.Data;
using System.Web.Mvc;
using Unity.Mvc5;

namespace OpenTfsPortal
{
    public static class UnityConfig
    {
		private static UnityContainer container = new UnityContainer();

        public static void RegisterComponents()
        {
            ResolverRepositories();
            ResolverServices();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static void ResolverRepositories()
        {
            container.RegisterType<IOtfsContainer, OtfsContainer>(new HierarchicalLifetimeManager());

            container.RegisterType<IIdentityAccessRepository, IdentityAccessRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IOtfsProjectRepository, OtfsProjectRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IOtfsIterationRepository, OtfsIterationRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IOtfsTaskRepository, OtfsTaskRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IOtfsBacklogItemRepository, OtfsBacklogItemRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IOtfsLancamentoHoraRepository, OtfsLancamentoHoraRepository>(new HierarchicalLifetimeManager());
        }

        private static void ResolverServices()
        {
            container.RegisterType<IOtfsTaskServices, OtfsTaskServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IOtfsBacklogItemServices, OtfsBacklogItemServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IOtfsProjectServices, OtfsProjectServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IIdentityAccessServices, IdentityAccessServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IOtfsProdutividadeService<OtfsProdutividadeRepository>, OtfsProdutividadeService<OtfsProdutividadeRepository>>(new HierarchicalLifetimeManager());
            container.RegisterType<IOtfsProdutividadeService<OtfsHorasPontoRepository>, OtfsProdutividadeService<OtfsHorasPontoRepository>>(new HierarchicalLifetimeManager());
            container.RegisterType<IOtfsProdutividadeService<OtfsHorasItopRepository>, OtfsProdutividadeService<OtfsHorasItopRepository>>(new HierarchicalLifetimeManager());
            container.RegisterType<IOtfsLancamentoHoraService, OtfsLancamentoHoraService>(new HierarchicalLifetimeManager());
        }
    }
}
