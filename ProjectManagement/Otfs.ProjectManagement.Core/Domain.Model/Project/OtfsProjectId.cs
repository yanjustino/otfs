
namespace Otfs.ProjectManagement.Core.Domain.Model.Project
{
    public class OtfsProjectId
    {
        public int Id { get; private set; }

        public OtfsProjectId(int id)
        {
            this.Id = id;
        }
    }
}
