using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models;
using Semesterprojekt2.Pages.Login;
using Semesterprojekt2.Service;
using Semesterprojekt2.Service.BookATimeService;
using Semesterprojekt2.Service.UserService.UserService;

namespace Semesterprojekt2.Pages.Profil.Dog
{
    public class DeleteDogModel : PageModel
    {
        private DogService _dogService;
        private IBookATimeService _bookATimeService;

        public DeleteDogModel(DogService dogService, IBookATimeService bookATimeService)
        {
            _dogService = dogService;
            _bookATimeService = bookATimeService;
            
        }
        [BindProperty]
        public Models.Dog dog { get; set; }
       
       

        public IActionResult OnGet(int id)
        {
            dog = _dogService.GetDogById(id);
          
            
            if (dog == null)
                return RedirectToPage("/Error/Error"); //NotFound er ikke defineret endnu

            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
      
            
        List<Models.BookATime.BookATime> deletedDogInBookATime = await _bookATimeService.DeleteDogInBookATime(dog.Id);

            Models.Dog deletedDog = await _dogService.DeleteDog(dog.Id);
            if (deletedDog == null)
                return RedirectToPage("/Error/Eroor"); //NotFound er ikke defineret endnu

            return RedirectToPage("/Profil/Profil");
        }
    }
}
