using FinanceTracker.Models;
using Microsoft.AspNetCore.Mvc;

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
            if (ModelState.IsValid)
            {
                var expense = new Expense
                {
                    Description = model.Description,
                    Amount = model.Amount,
                    Date = DateTime.Now, // You may want to change this to allow users to specify the date
                    CategoryId = model.CategoryId,
                    Category = model.Category
                };
                AddExpense.AddExpenses(expense); // Implement method to save expense to database
                return PartialView(model); // Redirect to home page or another appropriate page
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
