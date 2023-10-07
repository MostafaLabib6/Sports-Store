using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using SportsStore.Controllers;
using SportsStore.Data.Repositories;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Tests;

public class OrderControllerTests
{
    [Fact]
    public void Checkout_ValidateEmptyCart_Failed()
    {
        //arrange
        Mock<IOrderRepository> mockRepository = new Mock<IOrderRepository>();

        Cart cart = new Cart();
        Order order = new();

        //passing empty cart
        OrderController orderController = new(mockRepository.Object, cart);
        var result = orderController.Checkout(order) as ViewResult;

        //assert
        Assert.False(orderController.ViewData.ModelState.IsValid);

        //check that saveChanges method not visited
        // data did't saved
        mockRepository.Verify(o => o.SaveOrder(It.IsAny<Order>()), Times.Never);

        //check that the method return default view,
        Assert.True(string.IsNullOrEmpty(result?.ViewName));

    }
    [Fact]
    public void Checkout_ValidateOrderWithNotValidShippingDetails_Error()
    {
        Mock<IOrderRepository> mockRepository = new Mock<IOrderRepository>();

        Product product = new Product { Name = "p1", Category = "c1" };
        Cart cart = new Cart();
        cart.AddItem(product, 3);
        Order order = new();

        //passing empty cart
        OrderController orderController = new(mockRepository.Object, cart);

        //adding error to controller
        orderController.ModelState.AddModelError("error", "error");
        var result = orderController.Checkout(order) as ViewResult;


        //assert
        Assert.False(orderController.ViewData.ModelState.IsValid);

        //check that saveChanges method not visited
        mockRepository.Verify(o => o.SaveOrder(It.IsAny<Order>()), Times.Never);

        //check that the method return default view,
        Assert.True(string.IsNullOrEmpty(result?.ViewName));

    }

    [Fact]
    public void Checkout_ValidateOrderValidShippingDetails_NavigateToComplatePage()
    {
        Mock<IOrderRepository> mockRepository = new Mock<IOrderRepository>();

        Product product = new Product { Name = "p1", Category = "c1" };
        Cart cart = new Cart();
        cart.AddItem(product, 3);
        Order order = new();

        //passing empty cart
        OrderController orderController = new(mockRepository.Object, cart);

        var result = orderController.Checkout(order) as RedirectToPageResult;


        //assert
        Assert.True(orderController.ViewData.ModelState.IsValid);

        //check that saveChanges method not visited
        mockRepository.Verify(o => o.SaveOrder(It.IsAny<Order>()), Times.Once);

        //check that the method return default view,
        Assert.Equal("/Complete", result?.PageName);

    }
}
