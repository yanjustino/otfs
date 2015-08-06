
namespace Otfs.ProjectManagement.Core.Domain.Model.BacklogItem
{
    public class OtfsBacklogItemId
    {
        public int Id { get; private set; }

        public OtfsBacklogItemId(int id)
        {
            this.Id = id;
        }
    }
}
