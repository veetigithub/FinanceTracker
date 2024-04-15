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
            if (model != null && User.Identity.IsAuthenticated)
            {
                // Modelstate didnt want to be valid
                // because of UserId so i reversed the if statement, GENIUS!!!
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

                    // really bad way to just step around problems
                    if (expense.Description != null && expense.Amount != null && expense.Date != null && expense.Category != null) 
                    {
                        AddExpense.AddExpenses(expense);
                    }
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors)) { Console.WriteLine(error.ErrorMessage); }

                    return PartialView(model);
                }
            }
            return PartialView();
        }
        [HttpPost]
        public IActionResult _SecondPage()
        {
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.Identity.Name;
                var expensesOfUser = new GetExpense();

                // Retrieve expenses for the user
                var expenses = expensesOfUser.GetExpensesOfUser(userId);
                return PartialView(expenses);
            }
            
            return PartialView();
        }
        [HttpPost]
        public IActionResult _ThirdPage()
        {
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.Identity.Name;
                var expensesOfUser = new GetExpense();

                // Retrieve expenses for the user
                var expenses = expensesOfUser.GetExpensesOfUser(userId);
                return PartialView(expenses);
            }
            return PartialView();
        }
    }
}
