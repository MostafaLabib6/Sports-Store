using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Moq;
using SportsStore.Controllers;
using SportsStore.Data.Repositories;
using SportsStore.Models;
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
        mock.Setup(p => p.GetAll).Returns((new Product[]{
            new Product{
                Name="product 1",Price=1.00m,
                Description="this description for product 1",
                Category="Categroy 1"
            },

            new Product{
                Name="product 2",Price=2.00m,
                Description="this description for product 2",
                Category="Categroy 2"
            },

            new Product{
                Name="product 3",Price=3.00m,
                Description="this description for product 3",
                Category="Categroy 3"
            },
    }).AsQueryable<Product>());

        HomeController controller = new HomeController(mock.Object);

        //Act
        IEnumerable<Product>? Result = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;



        //Assert
        Product[] Actual = Result?.ToArray()
            ?? Array.Empty<Product>();
        Assert.True(Actual.Length == 3);


    }
    [Fact]
    public void ProductDetails_ValidateProductName_ValidName()
    {
        //Arrnge
        string _expectedName = "product 1";
        Mock<IStoreRepository> mock = new();
        mock.Setup(p => p.GetbyName(_expectedName)).Returns(new Product { Name = "product 1" });

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
        mock.Setup(p => p.GetAll)
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
        FieldInfo? _pageSize = typeof(HomeController).GetField("_pageSize",BindingFlags.NonPublic | BindingFlags.Instance);
        
        _pageSize?.SetValue(controller, 3);

        IEnumerable<Product> result = (controller.Pagination(2) as ViewResult)?.Model as IEnumerable<Product>??Enumerable.Empty<Product>();

        Product[] Actual = result.ToArray();


        Assert.NotEmpty(Actual);
        Assert.Equal(_expectedNames[3],Actual[0].Name);
        Assert.Equal(_expectedNames[4],Actual[1].Name);

 
    }


}