using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models.BookATime;
using Semesterprojekt2.Service;
using Semesterprojekt2.Service.BookATimeService;

namespace Semesterprojekt2.Pages.Orderhistory.BookATimeOrder
{
    public class EditOrderModel : PageModel
    {
        private IBookATimeService _BookATimeService;
        private DogService _DogService;
        private YdelseService _YdelseService;
        private IWebHostEnvironment _webHostEnvironment;
        [BindProperty]
        public Models.BookATime.BookATime BookATime { get; set; }
        [BindProperty]
        public Models.BookATime.Ydelse Ydelse { get; set; }
        [BindProperty]
        public Models.Dog Dog { get; set; }
        public IFormFile? DogPhoto { get; set; }
        [BindProperty]
        public IFormFile? BathPhoto { get; set; }
        public EditOrderModel(IBookATimeService bookATimeService, YdelseService ydelseService, DogService dogService, IWebHostEnvironment webHostEnvironment)
        {
            _BookATimeService = bookATimeService;
            _YdelseService = ydelseService;
            _DogService = dogService;
            _webHostEnvironment = webHostEnvironment;
        }
        
      
        public IActionResult OnGet(int id)
        {
            

            BookATime = _BookATimeService.GetBookATimeById(id);
         
            Dog = _DogService.GetDogById(BookATime.DogId);
            
            Ydelse =  _YdelseService.GetYdelseById(BookATime.YdelseId);
            if (BookATime == null)
                return RedirectToPage("/Error/Error"); //NotFound er ikke defineret endnu

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (BookATime == null || BookATime.Id == 0)
            {
                return RedirectToPage("/Error/Error");
            }
            if (BathPhoto != null)
            {
                if (BookATime.BathRoomImage != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "/ImagesForBookATime", "Bath", BookATime.BathRoomImage);
                    System.IO.File.Delete(filePath);
                }

                BookATime.BathRoomImage = ProcessUploadedBathFile();

            }

            if (DogPhoto != null)
            {
                if (BookATime.DogImage != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "/ImagesForBookATime", "Bath", BookATime.DogImage);
                    System.IO.File.Delete(filePath);
                }

                BookATime.DogImage = ProcessUploadedDogFile();
            }
           
            await _BookATimeService.UpdateBookATime(BookATime);
           
           await _DogService.UpdateDog(Dog);
           
           await _YdelseService.UpdateYdelse(Ydelse);
            
            return RedirectToPage("/Index");
        }
        private string ProcessUploadedDogFile()
        {
            string uniqueFileName = null;
            if (DogPhoto != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "ImagesForBookATime", "Dog");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + DogPhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    DogPhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        private string ProcessUploadedBathFile()
        {
            string uniqueFileName = null;
            if (BathPhoto != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "ImagesForBookATime", "Bath");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + BathPhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    BathPhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
