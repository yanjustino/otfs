using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otfs.ProjectManagement.Core.Domain.Model.BacklogItem
{
    public partial class OtfsBacklogItem
    {
        public static Func<OtfsBacklogItem, bool> QueryBacklogItemPendente 
        {
            get 
            {
                return x => x.State == OtfsBacklogItemState.New;
            } 
        }

        public static Func<OtfsBacklogItem, bool> QueryBacklogItemAndamento
        {
            get
            {
                return x => x.State == OtfsBacklogItemState.Approved;
            }
        }

        public static Func<OtfsBacklogItem, bool> QueryBacklogItemParaTestes
        {
            get
            {
                return x => x.State == OtfsBacklogItemState.Committed && !x.Aprovado() && !x.Publicado();
            }
        }

        public static Func<OtfsBacklogItem, bool> QueryBacklogItemConcluidas
        {
            get
            {
                return x => x.State == OtfsBacklogItemState.Done;
            }
        }

        public static Func<OtfsBacklogItem, bool> QueryBacklogItemParaBuild
        {
            get
            {
                return x => x.State == OtfsBacklogItemState.Committed && x.Aprovado() && !x.Publicado();
            }
        }

        public static Func<OtfsBacklogItem, bool> QueryBacklogItemPublicado
        {
            get
            {
                return x => x.State == OtfsBacklogItemState.Committed && x.Publicado();
            }
        }
    }
}
