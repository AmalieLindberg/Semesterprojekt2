using Semesterprojekt2.Models;
using Semesterprojekt2.Models.BookATime;

namespace Semesterprojekt2.Service
{
    public class DogService
    {

        public List<Dog> DogList { get; set; }
        public DogService()
        {


        }

        public void AddDogAsync(Dog dog)
        {
            DogList.Add(dog);


        }
    }
}
