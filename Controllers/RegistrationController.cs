using FinanceTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public IActionResult Index(Registration model)
        {
            if (ModelState.IsValid)
            {
                var registration = new Registration()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Username = model.Username,
                    Password = model.Password
                }; 
                Register.RegisterUser(registration);
                return View(model);
            }

            // If model state is not valid, return to the registration form with validation errors
            return View(model);
        }
    }
}
