using Microsoft.AspNetCore.Mvc;
using SportsStore.Data.Repositories;
using SportsStore.Models;

namespace SportsStore.Controllers;

public class OrderController : Controller
{
    private readonly IOrderRepository _context;
    private readonly Cart _cart;
    public OrderController(IOrderRepository context, Cart cart)
    {
        _context = context;
        _cart = cart;
    }

    public IActionResult Checkout()
    {
        return View(new Order());
    }
    [HttpPost]
    public IActionResult Checkout(Order order)
    {
        if (order is null || _cart.Lines.Count < 1)
            ModelState.AddModelError("", "Sorry your Cart is Empty!!!");
        if (ModelState.IsValid)
        {
            order!.Lines = _cart.Lines.ToArray();
            _context.SaveChanges(order!);
            _cart.Clear();
            return RedirectToPage("/Complete", order.OrderId);

        }
        else
            return View();
    }

}
