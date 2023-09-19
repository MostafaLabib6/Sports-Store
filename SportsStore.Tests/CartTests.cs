using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Tests;

public class CartTests
{
    [Fact]
    public void AddItem_AddItemsToCart_ProductsAddedSuccessfullyToCart()
    {
        //Assert for Product
        Product P1 = new Product
        {
            ProductId = 1,
            Name = "p1",
            Price = 350.3m,
        };

        Product P2 = new Product
        {
            ProductId = 2,
            Name = "p2",
            Price = 35.3m,
        };

        Product P3 = new Product
        {
            ProductId = 3,
            Name = "p3",
            Price = 50.3m,
        };

        //assert Cart
        Cart cart = new();

        //act
        cart.AddItem(P1, 1);
        cart.AddItem(P2, 1);
        cart.AddItem(P3, 1);

        //assert
        Assert.Equal(3, cart.Lines.Count());
        Assert.True(cart.Lines[0].Product.Name == P1.Name);
        Assert.True(cart.Lines[1].Product.Name == P2.Name);
        Assert.True(cart.Lines[2].Product.Name == P3.Name);

    }
    [Fact]
    public void RemoveItem_RemoveItemsFromCart_ProductsRemovedSuccessfully()
    {
        //Assert for Product
        Product P1 = new Product
        {
            ProductId = 1,
            Name = "p1",
            Price = 350.3m,
        };

        Product P2 = new Product
        {
            ProductId = 2,
            Name = "p2",
            Price = 35.3m,
        };

        Product P3 = new Product
        {
            ProductId = 3,
            Name = "p3",
            Price = 50.3m,
        };

        //assert Cart
        Cart cart = new();

        //act
        cart.AddItem(P1, 1);
        cart.AddItem(P2, 1);
        cart.AddItem(P3, 1);

        cart.RemoveLine(P1);
        //assert
        Assert.Equal(2, cart.Lines.Count());
        Assert.True(cart.Lines[0].Product.Name == P2.Name);
        Assert.True(cart.Lines[1].Product.Name == P3.Name);

    }

    [Fact]
    public void AddItem_addItemswasAddedtoCartBefore_QuantityIncreamented()
    {
        //Assert for Product
        Product P1 = new Product
        {
            ProductId = 1,
            Name = "p1",
            Price = 350.3m,
        };

        Product P2 = new Product
        {
            ProductId = 2,
            Name = "p2",
            Price = 35.3m,
        };

        Product P3 = new Product
        {
            ProductId = 3,
            Name = "p3",
            Price = 50.3m,
        };

        //assert Cart
        Cart cart = new();

        //act
        cart.AddItem(P1, 1);
        cart.AddItem(P2, 1);
        cart.AddItem(P3, 1);
        cart.AddItem(P2, 11);
        cart.AddItem(P3, 3);

        //assert
        Assert.Equal(3, cart.Lines.Count());
        Assert.True(cart.Lines[0].Quantity == 1);
        Assert.True(cart.Lines[1].Quantity == 12);
        Assert.True(cart.Lines[2].Quantity == 4);

    }
    [Fact]
    public void Clear_ClearCart_ZeroProductInCart()
    {
        //Assert for Product
        Product P1 = new Product
        {
            ProductId = 1,
            Name = "p1",
            Price = 350.3m,
        };

        Product P2 = new Product
        {
            ProductId = 2,
            Name = "p2",
            Price = 35.3m,
        };

        Product P3 = new Product
        {
            ProductId = 3,
            Name = "p3",
            Price = 50.3m,
        };

        //assert Cart
        Cart cart = new();

        //act
        cart.AddItem(P1, 1);
        cart.AddItem(P2, 1);
        cart.AddItem(P3, 1);
        cart.AddItem(P2, 11);
        cart.AddItem(P3, 3);

        cart.Clear();
        //assert
        Assert.True(0 == cart.Lines.Count());


    }
}
