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

        // Ensure these properties are bound correctly in the form
        //N�r du s�tter SupportsGet = true, fort�ller man ASP.NET Core,
        //at det er sikkert at binde v�rdier fra foresp�rgselsstrengen til egenskaben under GET foresp�rgsler.
        //Dette er nyttigt i scenarier, hvor du �nsker at opretholde eller pr�-indstille data gennem en URL, s�som filtrering,
        //sortering eller vedligeholdelse af tilstande over flere foresp�rgsler.
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
   
        public IActionResult OnGet()
        {

            if (LoginModel.LoggedInUser == null || LoginModel.LoggedInUser.UserId == 0 || LoginModel.LoggedInUser.UserId == null)
            {
                return RedirectToPage("/Login/Login");
            }

            int id = LoginModel.LoggedInUser.UserId;

            User = _userService.GetUser(id);


            //Denne linje tjekker, om TempData indeholder en v�rdi med n�glen "FullDateTime"
            //og om denne v�rdi kan castes til en DateTime type.
            //Hvis ja, gemmes v�rdien i variablen fullDateTime og resten af koden i blokken udf�res.
            if (TempData["FullDateTime"] is DateTime fullDateTime)
            {
                //Her udtr�kkes �r, m�ned og dag fra fullDateTime. Tidspunkt s�ttes til en streng repr�sentation af tiden i fullDateTime,
                //formatteret til timer og minutter.
                // Gem v�rdierne i egenskaber, som kan bindes og vises i formen p� siden
                Year = fullDateTime.Year;
                Month = fullDateTime.Month;
                Day = fullDateTime.Day;
                Tidspunkt = fullDateTime.ToString("HH:mm");
                //Denne del af koden fors�ger at parse Tidspunkt som en TimeSpan (som repr�senterer tidsdelen af fullDateTime). Hvis parsingen fejler
                //(f.eks. hvis strengen ikke er i det korrekte format), s�ttes time til TimeSpan.Zero, hvilket repr�senterer midnat
                TimeSpan time;
                bool isTimeParsed = TimeSpan.TryParse(Tidspunkt, out time); // More robust parsing with error handling
                if (!isTimeParsed)
                {
                    // Handle parsing error, maybe set a default value or return an error
                    // For now, let's set it to the start of the day as a fallback
                    time = TimeSpan.Zero;
                }
                //Her oprettes et nyt DateTime objekt med det udtrukne �r, m�ned og dag, og tiden fra time tilf�jes til denne dato.
                //Dette skaber et komplet DateTime objekt med b�de dato og tid.
                // Brug 'time' til at oprette en ny DateTime v�rdi
                fuldDatoOgTid = new DateTime(Year, Month, Day).Add(time); // Combining the date and time components

               
                //Til sidst fjernes "FullDateTime" fra TempData, hvilket er nyttigt for at undg� at den gamle dato genbruges ved en senere foresp�rgsel,
                //is�r i web applikationer, hvor TempData bruges til at bevare data mellem requests.
                // Slet TempData v�rdien, hvis den ikke skal bruges igen
                
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

                // Udskriver fejl til output-vinduet eller logger dem.
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var modelStateVal = ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        // Her kan du logge fejlen eller se den i debug-vinduet
                        Debug.WriteLine(error.ErrorMessage);
                    }
                }
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
                if (Dog.DogImage != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "/ImagesForBookATime", "Bath", Dog.DogImage);
                    System.IO.File.Delete(filePath);
                }

                Dog.DogImage = ProcessUploadedDogFile();
            }
            int UserId = LoginModel.LoggedInUser.UserId;

            User = _userService.GetUser(UserId);
            BookATime.DateForOrder = DateTime.Now;
            BookATime.StatusForBooking = "Pending";
            await _ydelseService.AddYdelseAsync(Ydelse);
          await _dogService.AddDogAsync(Dog);
            BookATime.DogId = Dog.Id;
            BookATime.UserId = User.UserId;
            BookATime.YdelseId = Ydelse.Id;
         await _bookATimeService.addTidsbestilling(BookATime);

            return RedirectToPage("ThankYouForYourBooking", new { id = BookATime.Id });
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
