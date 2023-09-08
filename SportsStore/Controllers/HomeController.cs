using Microsoft.AspNetCore.Mvc;
using SportsStore.Data.Repositories;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoreRepository _repository;
        private int _pageSize = 4;

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
        [HttpGet("Pagination/{ProductPage}")]
        public IActionResult Pagination(int ProductPage = 1)
        {
            return View("Index",
                _repository.GetAll
                .Skip((ProductPage - 1) * _pageSize)
                .Take(_pageSize));
        }
    }
}
