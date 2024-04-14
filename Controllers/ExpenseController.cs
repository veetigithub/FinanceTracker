using FinanceTracker.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FinanceTracker.Controllers
{
    public class ExpenseController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult _FirstPage(Expense model)
        {
            if (User.Identity.IsAuthenticated)
            {
                // modelstate didnt want to be valid so i reversed the if statement, GENIUS!!!
                if (!ModelState.IsValid)
                {
                    // get the currently logged in users username
                    string username = User.Identity.Name;

                    var expense = new Expense
                    {
                        Description = model.Description,
                        Amount = model.Amount,
                        Date = model.Date,
                        UserId = username, // and assign it to UserId
                        Category = model.Category
                    };
                    Console.WriteLine(model.Date);
                    AddExpense.AddExpenses(expense);
                    return PartialView(model);
                } else { foreach (var error in ModelState.Values.SelectMany(v => v.Errors)) { Console.WriteLine(error.ErrorMessage); } }
            } 
            
            // If model state is not valid, return to the add expense form with validation errors
            return PartialView(model);
        }
        [HttpPost]
        public IActionResult _SecondPage()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult _ThirdPage()
        {
            return PartialView();
        }
    }
}
