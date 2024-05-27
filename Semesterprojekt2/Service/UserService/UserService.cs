using Microsoft.AspNetCore.Identity;
using Semesterprojekt2.MockData.Profil;
using Semesterprojekt2.Models;
using Semesterprojekt2.DAO;
using Semesterprojekt2.MockData.Profil;

namespace Semesterprojekt2.Service.UserService.UserService
{
    /// <summary>
    /// Indeholder forskellige CRUD metoder til management af user, samt tilknyttede hunde, produkt- og tidsbestillinger.
    /// </summary>
    public class UserService : IUserService
    {
        private UserDbService DbService { get; set; }
        public List<Users> _users { get; set; }
        private JsonFileUserService jsonFileUserService { get; set; }

        private string genericPW = "ResetPW1234";

        private PasswordHasher<string> passwordHasher;

        /// <summary>
        /// Initialiserer en ny instans af UserService-klassen
        /// </summary>
        /// <param name="userDbService">Database service til user operations.</param>
        /// <param name="userService">Service for håndtering af user data gennem JSON filer. Bruges kun hvis vi har ingen Database. </param>
        public UserService(UserDbService userDbService, JsonFileUserService userService)
        {
            DbService = userDbService;
            jsonFileUserService = userService;
            _users = DbService.GetObjectsAsync().Result.ToList();
            passwordHasher = new PasswordHasher<string>();

            /* Nedenstående Komponenter har vi brugt til data persistens i Mockdata/JSON filer indtil vi skiftede til en Relationel Database.
            
            _users = MockUsers.GetMockUser(); //Used to populate User.json once to create Admin Login
            _users = jsonFileUserService.GetJsonUsers().ToList();
            DbService.SaveObjects(_users);
            _jsonFileUserService.SaveJsonUsers(_users);

            */

        }


        /// <summary>
        /// Tilføjer en ny user asyknront.
        /// </summary>
        /// <param name="user">User som skal oprettes i systemet.</param>
        /// <returns>User som blev oprettet i systemet. </returns>
        public async Task<Users> AddUser(Users user)
        {
            _users.Add(user);
            await DbService.AddObjectAsync(user);
            return user;
        }

        /// <summary>
        /// Henter en user fra DB ved brug af User ID.
        /// </summary>
        /// <param name="id">ID af user some skal hentes.</param>
        /// <returns> User, hvis den findes. Ellers null.</returns>
        public Users GetUser(int id)
        {

            foreach (Users user in _users)
            {
                if (user.UserId == id)
                    return user;
            }

            return null;
        }

        /// <summary>
        /// Opdater en user asynkront.
        /// </summary>
        /// <param name="user">User med detaljer som skal opdateres.</param>
        /// <returns>Opdateret user.</returns>
        public async Task<Users> UpdateUser(Users user)
        {
            if (user != null)
            {
                foreach (Users u in _users)
                {
                    if (u.UserId == user.UserId)
                    {
                        u.UserId = user.UserId;
                        u.Name = user.Name;
                        u.Email = user.Email;

                        u.Telefonnummer = user.Telefonnummer;
                        u.Password = passwordHasher.HashPassword(null, user.Password);
                        // Kan bruges til debugging
                        // Console.WriteLine($"Hashed Password: {u.Password}");
                        u.Role = user.Role;
                        if (user.ProfileImages == null)
                        {
                            user.ProfileImages = u.ProfileImages;
                        }
                        else
                        {
                            u.ProfileImages = user.ProfileImages;
                        }
                        u.Bio = user.Bio;

                        await DbService.UpdateObjectAsync(u);
                        return u;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Sletter en user fra Databasen asynkront, ved at bruger User ID
        /// </summary>
        /// <param name="userId">ID af user som skal slettes.</param>
        /// <returns>Den slettede user.</returns>
        public async Task<Users> DeleteUser(int? userId)
        {
            Users userToBeDeleted = null;
            foreach (Users user in _users)
            {
                if (user.UserId == userId)
                {
                    userToBeDeleted = user;
                    break;
                }
            }

            if (userToBeDeleted != null)
            {
                _users.Remove(userToBeDeleted);
                await DbService.DeleteObjectAsync(userToBeDeleted);
            }

            return userToBeDeleted;
        }

        /// <summary>
        /// Henter alle user fra Databasen.
        /// </summary>
        /// <returns>Liste med alle user</returns>
        public List<Users> GetUsers()
        {
            _users = DbService.GetObjectsAsync().Result.ToList();
            return _users;
        }

        /// <summary>
        /// Henter brugerens tidsbestillinger fra Databasen asynkront
        /// </summary>
        /// <param name="user">Bruger hvilken tidsbestillinger skal hentes.</param>
        /// <returns>Brugerens tidsbestillinger.</returns>
        public Users GetUserTidsbestillingOrders(Users user)
        {
            return DbService.GetTidsbestillingOrdersByUserIdAsync(user.UserId).Result;
        }

        /// <summary>
        /// Henter et brugerens informationer fra Databasen, via Id.
        /// </summary>
        /// <param name="id">ID af user som skal hentes fra Databasen.</param>
        /// <returns>User Objekt man har anmodt .</returns>
        public Users GetUserById(int id)
        {
            return DbService.GetObjectByIdAsync(id).Result;
        }

        /// <summary>
        /// Henter bruger objekt og dens associerede hunde objekter fra Databasen.
        /// </summary>
        /// <param name="user">Bruger objektet, hvilken hunde skal hentes.</param>
        /// <returns> Bruger-objektet og dens associerede hunde objekter eller null hvis brugeren ikke eksisterer.</returns>
        public Users GetUserDogs(Users user)
        {
            return DbService.GetDogByUserIdAsync(user.UserId).Result;
        }

        /// <summary>
        /// Henter brugeren og deres produkt ordrer fra Databasen
        /// </summary>
        /// <param name="user">Bruger objektet, som skal hentes.</param>
        /// <returns> Brugeren og dens associerede produktordrer (eller null hvis brugeren ikke findes).</returns>
        public Users GetUserProductOrders(Users user)
        {
            return DbService.GetOrdersByUserIdAsync(user.UserId).Result;
        }

        /*
        public IEnumerable<ProductOrderDAO> GetUserProductOrders(Users users)
        {
            return DbService.GetOrdersByUserIdAsync(users.UserId).Result;
        }
        */


        /// <summary>
        /// "Nullstiller" kodeord af en user. Kodeord ændres til genericPW.
        /// </summary>
        /// <param name="user">Den user hvis adgangskode bliver ændret.</param>
        /// <returns>Den user med den nulstillede adgangskode.</returns>
        public async Task<Users> ResetPassword(Users user)
        {
            if (user != null)
            {
                foreach (Users u in _users)
                {
                    if (u.UserId == user.UserId)
                    {
                        u.Password = passwordHasher.HashPassword(null, genericPW);
                        Console.WriteLine($"Hashed Password: {user.Password}");

                        await DbService.UpdateObjectAsync(u);
                        return u;

                    }
                }

            }

            return null;

        }





    }
}
