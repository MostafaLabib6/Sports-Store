using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Moq;
using SportsStore.Controllers;
using SportsStore.Data.Repositories;
using SportsStore.Models;

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

}