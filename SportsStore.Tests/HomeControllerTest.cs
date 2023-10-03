using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Moq;
using SportsStore.Componets;
using SportsStore.Controllers;
using SportsStore.Data.Repositories;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System.Reflection;

namespace SportsStore.Tests;

public class HomeControllerTest
{
    //MethodUnderTest_Scenario_ExpectedResult:
    [Fact]
    public void Index_ValidateProductsCount_ValidCount()
    {
        //Arrnge
        Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
        mock.Setup(p => p.GetAll()).Returns((new Product[]{
            new Product{
                Name="product 1",Price=1.00m,
                Description="this description for product 1",
                Category = "Category 1"
            },

            new Product{
                Name="product 2",Price=2.00m,
                Description="this description for product 2",
                Category = "Category 2"
            },

            new Product{
                Name="product 3",Price=3.00m,
                Description="this description for product 3",
                Category = "Category 3"
            },
    }).AsQueryable<Product>());

        HomeController controller = new HomeController(mock.Object);

        //Act
        ProductListViewModel? Result = (controller.Index() as ViewResult)?.Model as ProductListViewModel ?? new();



        //Assert
        Product[] Actual = Result.Products?.ToArray()
            ?? Array.Empty<Product>();
        Assert.True(Actual.Length == 3);


    }
    [Fact]
    public void ProductDetails_ValidateProductName_ValidName()
    {
        //Arrnge
        string _expectedName = "product 1";
        Mock<IStoreRepository> mock = new();
        mock.Setup(p => p.Get(n => n.Name == _expectedName)).Returns(new Product { Name = "product 1" });

        HomeController controller = new(mock.Object);
        //Act
        Product? result = (controller.Details(_expectedName) as ViewResult)?.Model as Product;

        Assert.NotNull(result);
        Assert.True(_expectedName == result.Name);
    }
    [Fact]
    public void Pagination_ValidateRenderedProductsNames_ValidProductsNames()
    {
        //Arrnge
        string[] _expectedNames = { "p1", "p2", "p3", "p4", "p5" };
        Mock<IStoreRepository> mock = new();
        mock.Setup(p => p.GetAll())
            .Returns((new Product[]{
                new Product{ Name = _expectedNames[0]},
                new Product{ Name = _expectedNames[1]},
                new Product{ Name = _expectedNames[2]},
                new Product{ Name = _expectedNames[3]},
                new Product{ Name = _expectedNames[4]}
                ,
            }).AsQueryable<Product>);

        HomeController controller = new(mock.Object);

        //getting private field pageSize via Reflaction and set _pageSize to 3 instead of 4
        FieldInfo? _pageSize = typeof(HomeController).GetField("_pageSize", BindingFlags.NonPublic | BindingFlags.Instance);

        _pageSize?.SetValue(controller, 3);

        IEnumerable<Product> result = (controller.Pagination(2) as ViewResult)?.Model as IEnumerable<Product> ?? Enumerable.Empty<Product>();

        Product[] Actual = result.ToArray();


        Assert.NotEmpty(Actual);
        Assert.Equal(_expectedNames[3], Actual[0].Name);
        Assert.Equal(_expectedNames[4], Actual[1].Name);


    }
    [Fact]
    public void Pagination2_validateSendPaginationViewModel_ValidPagination()
    {
        //Arrnge
        Mock<IStoreRepository> mock = new();
        mock.Setup(p => p.GetAll()).Returns((new Product[]
        {
             new Product { Name = "P1"},
             new Product { Name = "P2"},
             new Product { Name = "P3"},
             new Product { Name = "P4"},
             new Product { Name = "P5"}
        }).AsQueryable<Product>);

        HomeController controller = new(mock.Object);

        FieldInfo? _pageSize = typeof(HomeController).GetField("_pageSize", BindingFlags.NonPublic | BindingFlags.Instance);

        _pageSize?.SetValue(controller, 3);

        //Act
        ProductListViewModel result = (controller.Pagination2(2) as ViewResult)?.Model as ProductListViewModel ?? new();


        //assert
        PagingInfoViewModel pageInfo = result.PagingInfo;
        Assert.Equal(2, pageInfo.CurrentPage);
        Assert.Equal(3, pageInfo.ItemsPerPage);
        Assert.Equal(5, pageInfo.TotalItems);
        Assert.Equal(2, pageInfo.TotalPages);
    }
    [Fact]
    public void Index_FilterbyCategoryandPage_ValidFilter()
    {
        //Arrnge
        Mock<IStoreRepository> mock = new();
        mock.Setup(p => p.GetAll()).Returns((new Product[]{
            new Product{ Name="p1",Category="Cat1"},
            new Product{ Name="p1",Category="Cat2"},
            new Product{ Name="p1",Category="Cat1"},
            new Product{ Name="p1",Category="Cat2"},
            new Product{ Name="p1",Category="Cat1"},
            new Product{ Name="p1",Category="Cat2"},
            new Product{ Name="p1",Category="Cat3"},
            new Product{ Name="p1",Category="Cat3"},
            }).AsQueryable<Product>);
        HomeController controller = new(mock.Object);

        FieldInfo? _pageSize = typeof(HomeController).GetField("_pageSize", BindingFlags.NonPublic | BindingFlags.Instance);

        _pageSize?.SetValue(controller, 8);

        //Act
        ProductListViewModel result = (controller.Index("Cat2", 1) as ViewResult)?.Model as ProductListViewModel ?? new();
        Product[] products = result.Products.ToArray();

        //Assert
        Assert.NotNull(products);
        Assert.Equal(3, products.Length);
        Assert.Equal("Cat2", products[0].Category);
        Assert.Equal("Cat2", products[1].Category);
        Assert.Equal("Cat2", products[2].Category);



    }
    [Fact]
    public void NavMenu_ValidateCategoriesNames_ValidCategoriesNames()
    {
        //Arrange
        Mock<IStoreRepository> mock = new();
        mock.Setup(p => p.GetAll()).Returns((new Product[]{
            new Product{ Name="p1" , Category="C1" },
            new Product{ Name="p2" , Category="C2" },
            new Product{ Name="p3" , Category="C1" },
            new Product{ Name="p4" , Category="C1" },
            new Product{ Name="p5" , Category="C4" },
            new Product{ Name="p6" , Category="C5" },
            }).AsQueryable<Product>
        );

        NavMenuViewComponent navMenu = new NavMenuViewComponent(mock.Object);

        // To avoid null referance exception when set the category ;
        // so i initialized it with new instance;
        navMenu.ViewComponentContext = new ViewComponentContext()
        {
            ViewContext = new ViewContext
            {
                RouteData = new Microsoft.AspNetCore.Routing.RouteData()
            }
        };


        //Act == Check here as ViewViewComponentResult
        IEnumerable<string> cats = (IEnumerable<string>?)(navMenu.Invoke() as ViewViewComponentResult)?.ViewData?.Model ?? Enumerable.Empty<string>();
        string[] categories = cats.ToArray();

        Assert.NotNull(categories);
        Assert.Equal(4, categories.Length);
        Assert.True(Enumerable.SequenceEqual(
            new string[]{
            "C1",
            "C2",
            "C4",
            "C5",
            }
            , categories
        ));

    }

    [Fact]
    public void NavManu_ValidateSelectedCategory_ValidCategory()
    {
        // Arrange
        string categoryToSelect = "Apples";
        Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
        mock.Setup(m => m.GetAll()).Returns((new Product[] {

        new Product {ProductId = 1, Name = "P1", Category = "Apples"},
        new Product {ProductId  = 4, Name = "P2", Category = "Oranges"},

        }).AsQueryable<Product>());

        NavMenuViewComponent target = new NavMenuViewComponent(mock.Object);
        target.ViewComponentContext = new ViewComponentContext
        {
            ViewContext = new ViewContext
            {
                RouteData = new Microsoft.AspNetCore.Routing.RouteData()
            }
        };
        target.RouteData.Values["category"] = categoryToSelect;
        // Action
        string? result = (string?)(target.Invoke()
        as ViewViewComponentResult)?.ViewData?["SelectedCategory"];

        // Assert
        Assert.Equal(categoryToSelect, result);
    }
    [Fact]
    public void Index_Category_Specific_Product_Count_ValidCount()
    {
        Mock<IStoreRepository> mock = new();
        mock.Setup(p => p.GetAll()).Returns((new Product[]{

            new Product {ProductId = 1, Category="Cat1", Name = "p1"},
            new Product {ProductId = 2, Category="Cat1", Name = "p1"},
            new Product {ProductId = 3, Category="Cat2", Name = "p1"},
            new Product {ProductId = 4, Category="Cat3", Name = "p1"},
            new Product {ProductId = 5, Category="Cat2", Name = "p1"},

        }).AsQueryable<Product>);

        HomeController controller = new HomeController(mock.Object);

        Func<ViewResult, ProductListViewModel?> Models =
            M => M?.ViewData?.Model
                 as ProductListViewModel;

        int? category1 = Models((ViewResult)controller.Index("Cat1"))?.PagingInfo.TotalItems;
        int? category2 = Models((ViewResult)controller.Index("Cat2"))?.PagingInfo.TotalItems;
        int? category3 = Models((ViewResult)controller.Index("Cat3"))?.PagingInfo.TotalItems;
        int? categories = Models((ViewResult)controller.Index(null))?.PagingInfo.TotalItems;

        Assert.Equal(categories, 5);
        Assert.Equal(category1, 2);
        Assert.Equal(category2, 2);
        Assert.Equal(category3, 1);

    }


}