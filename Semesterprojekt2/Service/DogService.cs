using Semesterprojekt2.Models;
using Semesterprojekt2.Models.BookATime;
using Semesterprojekt2.Service.BookATimeService;

namespace Semesterprojekt2.Service
{
    public class DogService
    {
        DbGenericService<Dog> _dbGenericService {  get; set; }

        public List<Dog> DogList { get; set; }
        public DogService(DbGenericService<Dog> dbGenericService)
        {
            _dbGenericService = dbGenericService;
            DogList = _dbGenericService.GetObjectsAsync().Result.ToList();


        }

        public void AddDogAsync(Dog dog)
        {
            DogList.Add(dog);


        }
    }
}
