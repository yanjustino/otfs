
namespace Common.Domain.Model
{
    public interface IOtfsContainer
    {
        T Resolver<T>();
        void Register();
        void UnRegister();
    }
}
