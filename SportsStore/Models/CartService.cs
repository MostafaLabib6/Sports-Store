using SportsStore.Infrastructure;
using System.Text.Json.Serialization;

namespace SportsStore.Models
{
    public class CartService : Cart
    {
        [JsonIgnore]
        public ISession? Session { get; set; }
        public static Cart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()
            .HttpContext?.Session;
            CartService cart = session?.GetJson<CartService>("Cart")
            ?? new CartService();
            cart.Session = session;
            return cart;
        }
        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            Session?.SetJson("Cart", this);
        }
        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);
            Session?.SetJson("Cart",this);
        }

        public override void Clear()
        {
            base.Clear();
            Session?.Remove("Cart");

        }

    }
}
