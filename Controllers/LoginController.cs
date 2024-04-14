using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FinanceTracker.Models;
using MongoDB.Driver;

namespace FinanceTracker.Controllers
{
    public class LoginController : Controller
    {
        private static string? DATABASE_NAME;
        private static string? HOST;

        private static IConfiguration? config;
        private static MongoServerAddress? address;
        private static MongoClientSettings? clientSettings;
        private static MongoClient? client;
        public static IMongoDatabase? database;

        public static void Initialize(IConfiguration configuration)
        {
            config = configuration;
            var sections = config.GetSection("ConnectionStrings");
            DATABASE_NAME = sections.GetValue<string>("DatabaseName");
            HOST = sections.GetValue<string>("MongoConnection");
            address = new MongoServerAddress(HOST);
            clientSettings = new MongoClientSettings() { Server = address };
            client = new MongoClient(clientSettings);
            database = client.GetDatabase(DATABASE_NAME);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Registration model)
        {
            // Check if the username + password combination exists in the database
            if (IsValidUser(model.Username, model.Password))
            {
                // Call the Login method to sign in the user
                Login(model.Username);
                Console.WriteLine("kirjautui sisään");
                return RedirectToAction("Index");
            }

            // Invalid username or password, return to login page with error
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return View(model);
        }

        private bool IsValidUser(string username, string password)
        {
            if (database == null)
            {
                Initialize(config);
            }

            // Query the database for a document with the provided username
            var usersCollection = database.GetCollection<Registration>("Registration");
            var filter = Builders<Registration>.Filter.Eq(u => u.Username, username);
            var user = usersCollection.Find(filter).FirstOrDefault();

            // If no user found with the provided username, return false
            if (user == null)
            {
                return false;
            }

            // Check if the provided password matches the password of the found document
            if (user.Password == password)
            {
                return true;
            }

            return false;
        }

        private void Login(string username)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, username)
            };
            //if (name == "admin")
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, "admin"));
            //}
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                IsPersistent = true
            };
            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}
