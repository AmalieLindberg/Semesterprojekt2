using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service;
using Semesterprojekt2.Service.BookATimeService;
using System.Diagnostics;
using Semesterprojekt2.Models.BookATime;
using Semesterprojekt2.Service.UserService.UserService;
using Semesterprojekt2.Models;
using Semesterprojekt2.Pages.Login;
using System.Globalization;

namespace Semesterprojekt2.Pages.BookATime
{
    public class BookATimeModel : PageModel
    {
        private IWebHostEnvironment _webHostEnvironment;
        [BindProperty]

        public Models.BookATime.BookATime BookATime { get; set; } = new Models.BookATime.BookATime();
        [BindProperty]
        public Models.BookATime.Ydelse Ydelse { get; set; }

        [BindProperty]
        public Models.Users User { get; set; }
        [BindProperty]
        public Models.Dog Dog { get; set; }

        [BindProperty]
        public int Year { get; set; }

        [BindProperty]
        public int Month { get; set; }

        [BindProperty]
        public int Day { get; set; }

        [BindProperty]
        public DateTime fuldDatoOgTid { get; set; }
        [BindProperty]
        public string? Tidspunkt { get; set; }
        [BindProperty]
        public IFormFile? DogPhoto { get; set; }
        [BindProperty]
        public IFormFile? BathPhoto { get; set; }

        private readonly IBookATimeService _bookATimeService;
        private readonly YdelseService _ydelseService;
        private readonly IUserService _userService;
        private readonly DogService _dogService;
        public BookATimeModel(IBookATimeService bookATimeService, IUserService userService, YdelseService ydelseService, IWebHostEnvironment webHostEnvironment, DogService dogService)
        {
            _bookATimeService = bookATimeService;
            _userService = userService;
            _ydelseService = ydelseService;
            _webHostEnvironment = webHostEnvironment;
            _dogService = dogService;
        }

        public IEnumerable<Models.Dog> MyDogs { get; set; }

        public IActionResult OnGet()
        {
            if (LoginModel.LoggedInUser == null || LoginModel.LoggedInUser.UserId == 0 || LoginModel.LoggedInUser.UserId == null)
            {
                return RedirectToPage("/Login/Login");
            }

            int id = LoginModel.LoggedInUser.UserId;
            Users CurrentUser = _userService.GetUser(id);
            MyDogs = _userService.GetUserDogs(CurrentUser).Dog;

     

            User = _userService.GetUser(id);


            //Denne linje tjekker, om TempData indeholder en værdi med nøglen "FullDateTime"
            //og om denne værdi kan castes til en DateTime type.
            //Hvis ja, gemmes værdien i variablen fullDateTime og resten af koden i blokken udføres.
            if (TempData["FullDateTime"] is DateTime fullDateTime)
            {
                //Her udtrækkes år, måned og dag fra fullDateTime. Tidspunkt sættes til en streng repræsentation af tiden i fullDateTime,
                //formatteret til timer og minutter.
                // Gem værdierne i egenskaber, som kan bindes og vises i formen på siden
                Year = fullDateTime.Year;
                Month = fullDateTime.Month;
                Day = fullDateTime.Day;
                Tidspunkt = fullDateTime.ToString("HH:mm");
                //Denne del af koden forsøger at parse Tidspunkt som en TimeSpan (som repræsenterer tidsdelen af fullDateTime). Hvis parsingen fejler
                //(f.eks. hvis strengen ikke er i det korrekte format), sættes time til TimeSpan.Zero, hvilket repræsenterer midnat
                TimeSpan time;
                bool isTimeParsed = TimeSpan.TryParse(Tidspunkt, out time); // More robust parsing with error handling
                if (!isTimeParsed)
                {
                    // Handle parsing error, maybe set a default value or return an error
                    // For now, let's set it to the start of the day as a fallback
                    time = TimeSpan.Zero;
                }
                //Her oprettes et nyt DateTime objekt med det udtrukne år, måned og dag, og tiden fra time tilføjes til denne dato.
                //Dette skaber et komplet DateTime objekt med både dato og tid.
                // Brug 'time' til at oprette en ny DateTime værdi
                fuldDatoOgTid = new DateTime(Year, Month, Day).Add(time); // Combining the date and time components

               
                //Til sidst fjernes "FullDateTime" fra TempData, hvilket er nyttigt for at undgå at den gamle dato genbruges ved en senere forespørgsel,
                //især i web applikationer, hvor TempData bruges til at bevare data mellem requests.
                // Slet TempData værdien, hvis den ikke skal bruges igen
                
                TempData.Remove("FullDateTime"); 
              
            }
            if (fuldDatoOgTid == new DateTime(0001, 01, 01, 00, 00, 00))
            {
                // Omdiriger til 'BookATime' siden
                return RedirectToPage("/BookATime/Calendar");
            }
            return Page();

        }

        public async Task<IActionResult> OnPost(int id)
        {
            if (!ModelState.IsValid)
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

      
            int UserId = LoginModel.LoggedInUser.UserId;

            User = _userService.GetUser(UserId);
            BookATime.DateForOrder = DateTime.Now;
            BookATime.StatusForBooking = "Pending";
            await _ydelseService.AddYdelseAsync(Ydelse);

            BookATime.DogId = Dog.Id;
            BookATime.UserId = User.UserId;
            BookATime.YdelseId = Ydelse.Id;
         await _bookATimeService.addTidsbestilling(BookATime);

            return RedirectToPage("ThankYouForYourBooking", new { id = BookATime.Id });
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
