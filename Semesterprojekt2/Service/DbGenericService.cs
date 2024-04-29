using Semesterprojekt2.EFDbContext;
using Microsoft.EntityFrameworkCore;
namespace Semesterprojekt2.Service
{
    public class DbGenericService<T> : IService<T> where T : class
    {
        public async Task<IEnumerable<T>> GetObjectsAsync()
        {
            using (var context = new SemsterprojektDbContext())
            {
                return await context.Set<T>().AsNoTracking().ToListAsync();
            }
        }
        public async Task AddObjectAsync(T obj)
        {
            using (var context = new SemsterprojektDbContext())
            {
                context.Set<T>().Add(obj);
                await context.SaveChangesAsync();
            }
        }
        public async Task SaveObjects(List<T> objs)
        {
            using (var context = new SemsterprojektDbContext())
            {
                foreach (T obj in objs)
                {
                    context.Set<T>().Add(obj);
                    context.SaveChanges();
                }

                context.SaveChanges();
            }
        }

        public async Task DeleteObjectAsync(T obj)
        {
            using (var context = new SemsterprojektDbContext())
            {
                context.Set<T>().Remove(obj);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateObjectAsync(T obj)
        {
            using (var context = new SemsterprojektDbContext())
            {
                context.Set<T>().Update(obj);
                await context.SaveChangesAsync();

            }
        }

        public async Task<T> GetObjectByIdAsync(int id)
        {
            using (var context = new SemsterprojektDbContext())
            {
                return await context.Set<T>().FindAsync(id);
            }
        }


    }
}
