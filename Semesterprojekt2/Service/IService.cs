namespace Semesterprojekt2.Service
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetObjectsAsync();
        Task AddObjectAsync(T obj);
        Task DeleteObjectAsync(T obj);
        Task UpdateObjectAsync(T obj);
        Task<T> GetObjectByIdAsync(int id);
        Task SaveObjects(List<T> objs);
    }
}
