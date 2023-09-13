using Microsoft.AspNetCore.Mvc;
using SportsStore.Data.Repositories;

namespace SportsStore.Componets;

public class NavMenuViewComponent : ViewComponent
{
    private IStoreRepository? _storeRepository;

    public NavMenuViewComponent(IStoreRepository? storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public IViewComponentResult Invoke()
    {
        ViewBag.SelectedCategory = RouteData.Values["category"];
        return View(
            _storeRepository?.GetAll
            .Select(x => x.Category)
            .Distinct()
            .OrderBy(x => x));
    }
}
