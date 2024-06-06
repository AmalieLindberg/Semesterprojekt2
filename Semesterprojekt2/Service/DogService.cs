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

        public async Task<Dog> AddDogAsync(Dog dog)
        {
            DogList.Add(dog);
            await _dbGenericService.AddObjectAsync(dog);
            return dog;

        }

        public Dog? GetDogById(int id)
        {
            foreach (var dog in DogList)
            {
             if (id == dog.Id)
                    return dog;
            }
            return null;
        }
        public async Task<Dog> UpdateDog(Dog dog)
        {
            if (dog != null)
            {
                foreach (Dog d in DogList)
                {
                    if (d.Id == dog.Id)
                    {
                        d.Name = dog.Name;
                        d.Age = dog.Age;
                        d.Race = dog.Race;
                        d.Comments = dog.Comments;
                        if (dog.DogImage == null)
                        {
                            dog.DogImage = d.DogImage;
                        }
                        else
                        {
                            d.DogImage = dog.DogImage;
                        }
                        await _dbGenericService.UpdateObjectAsync(d);
                        return d;
                    }
                }
			}
            return null;
        }
        public async Task<Dog> DeleteDog(int? dogId)
        {
            Dog dogToBeDeleted = null;
            foreach (Dog d in DogList)
            {
                if (d.Id == dogId)
                {
                    dogToBeDeleted = d;
                    break;
                }
            }

            if (dogToBeDeleted != null)
            {
                DogList.Remove(dogToBeDeleted);
                await _dbGenericService.DeleteObjectAsync(dogToBeDeleted);
            }

            return dogToBeDeleted;
        }


    }
}
