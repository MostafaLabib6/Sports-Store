using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Data.Repositories;
using SportsStore.Models;
using SportsStore.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace SportsStore.Tests
{
    public class CartPagesTests
    {
        [Fact]
        public void OnGet_ValidateCartAddedProducts_ValidCartProducts()
        {
            // Arrange
            // - create a mock repository
            Product p1 = new Product { ProductId = 1, Name = "P1" };
            Product p2 = new Product { ProductId = 2, Name = "P2" };
            Mock<IStoreRepository> mockRepo = new Mock<IStoreRepository>();
            mockRepo.Setup(m => m.GetAll).Returns((new Product[] {
             p1, p2
             }).AsQueryable<Product>());
            // - create a cart
            Cart testCart = new Cart();
            testCart.AddItem(p1, 2);
            testCart.AddItem(p2, 1);
            // - create a mock page context and session
            Mock<ISession> mockSession = new Mock<ISession>();
            byte[] data =
            Encoding.UTF8.GetBytes(JsonSerializer.Serialize(testCart));
            mockSession.Setup(c => c.TryGetValue(It.IsAny<string>(),
           out data));
            Mock<HttpContext> mockContext = new Mock<HttpContext>();
            mockContext.SetupGet(c => c.Session).Returns(mockSession.Object);
            // Action
            CartModel cartModel = new CartModel(mockRepo.Object)
            {
                PageContext = new PageContext(new ActionContext
                {
                    HttpContext = mockContext.Object,
                    RouteData = new RouteData(),
                    ActionDescriptor = new PageActionDescriptor()
                })
            };
            cartModel.OnGet("myUrl");
            //Assert
            Assert.Equal(2, cartModel.Cart?.Lines.Count());
            Assert.Equal("myUrl", cartModel.ReturnUrl);


        }
    }
}
