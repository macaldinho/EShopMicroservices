namespace Basket.API.Data.Registries;

public class ShoppingCartSchemaRegistry : MartenRegistry
{
    public ShoppingCartSchemaRegistry()
    {
        For<ShoppingCart>().Identity(x => x.UserName);
    }
}