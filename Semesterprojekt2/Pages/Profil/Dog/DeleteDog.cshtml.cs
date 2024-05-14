using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models;
using Semesterprojekt2.Service;
using Semesterprojekt2.Service.UserService.UserService;

namespace Semesterprojekt2.Pages.Profil.Dog
{
    public class DeleteDogModel : PageModel
    {
        private DogService _dogService;

        public DeleteDogModel(DogService dogService)
        {
            _dogService = dogService;
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
            Models.Dog deletedDog = await _dogService.DeleteDog(dog.Id);
            if (deletedDog == null)
                return RedirectToPage("/Error/Eroor"); //NotFound er ikke defineret endnu

            return RedirectToPage("/Profil/Profil");
        }
    }
}
