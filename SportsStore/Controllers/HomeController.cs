using Microsoft.AspNetCore.Mvc;
using SportsStore.Data.Repositories;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoreRepository _repository;

        public HomeController(IStoreRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View(_repository.GetAll);
        }
        public IActionResult Details(string name)
        {
            return View(_repository.GetbyName(name));
        }
    }
}
