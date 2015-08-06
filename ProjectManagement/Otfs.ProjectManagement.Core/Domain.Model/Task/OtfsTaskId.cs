
namespace Otfs.ProjectManagement.Core.Domain.Model.Task
{
    public class OtfsTaskId
    {
        public int Id { get; private set; }

        public OtfsTaskId(int id)
        {
            this.Id = id;
        }
    }
}
