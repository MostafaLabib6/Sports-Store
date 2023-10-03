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
            mockRepo.Setup(m => m.GetAll()).Returns((new Product[] {
             p1, p2
             }).AsQueryable<Product>());
            // - create a cart
            CartService testCart = new();
            testCart.AddItem(p1, 2);
            testCart.AddItem(p2, 1);

            //Mock<HttpContext> mockContext = new Mock<HttpContext>();
            // Action
            CartModel cartModel = new CartModel(mockRepo.Object, testCart);
            cartModel.OnGet("myUrl");
            //Assert
            Assert.Equal(2, cartModel.Cart?.Lines.Count());
            Assert.Equal("myUrl", cartModel.ReturnUrl);


        }
        [Fact]
        public void OnPost_validateCartAddedProducts_ValidCartProducts()
        {
            // Arrange
            // - create a mock repository
            Product p1 = new Product { ProductId = 1, Name = "P1" };
            Mock<IStoreRepository> mockRepo = new Mock<IStoreRepository>();
            mockRepo.Setup(m => m.GetAll()).Returns((new Product[] {
             p1
             }).AsQueryable<Product>());
            CartService testCart = new();
            testCart.AddItem(p1, 2);
            CartModel cartModel = new CartModel(mockRepo.Object, testCart);
            cartModel.OnPost(1, "myUrl");

            //Assertion 
            // Default return of ReturnUrl is "/"
            Assert.Equal("/",cartModel.ReturnUrl);
            Assert.Single(cartModel.Cart.Lines);
            Assert.Equal("P1",cartModel.Cart.Lines.First().Product.Name );

        }
    }
}
