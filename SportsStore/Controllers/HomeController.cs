﻿using Microsoft.AspNetCore.Mvc;
using SportsStore.Data.Repositories;
using SportsStore.Models.ViewModels;

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

        //[HttpGet("page{pageNumber}")]
        public IActionResult Index(string? category = null, int pageNumber = 1)
        {
            ProductListViewModel ProductList = new()
            {
                Products = _repository.GetAll
                .Where(p => p.Category == category || category == null)
                .Skip((pageNumber - 1) * _pageSize)
                .Take(_pageSize),

                PagingInfo = new PagingInfoViewModel
                {
                    TotalItems = (category == null) ?
                    _repository.GetAll.Count() 
                    : _repository.GetAll.Where(p => p.Category == category).Count(),
                    
                    CurrentPage = pageNumber,
                    ItemsPerPage = _pageSize,
                },
                Category = category

            };

            ProductList.PagingInfo.PageSettings();

            return View(ProductList);

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

        [HttpGet("Home/Page{ProductPage}")]
        public IActionResult Pagination2(int ProductPage = 1)
        {
            ProductListViewModel ProductList = new()
            {
                Products = _repository.GetAll
                .Skip((ProductPage - 1) * _pageSize)
                .Take(_pageSize),
                PagingInfo = new PagingInfoViewModel
                {
                    CurrentPage = ProductPage,
                    ItemsPerPage = _pageSize,
                    TotalItems = _repository.GetAll.Count()
                }
            };
            return View(ProductList);
        }
    }
}
