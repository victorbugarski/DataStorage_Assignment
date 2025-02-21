using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ProductFactory
{
    public static ProductRegistrationForm CreateRegistrationForm() => new();
    public static ProductUpdateForm CreateUpdateForm() => new();


    public static ProductEntity? Create(ProductRegistrationForm form) => form == null ? null : new()
    {
        ProductName = form.ProductName,
        ProductDescription = form.ProductDescription,
        Price = form.Price
    };

    public static Product? Create(ProductEntity entity) => entity == null ? null : new()
    {
        Id = entity.Id,
        ProductName = entity.ProductName,
        ProductDescription = entity.ProductDescription,
        Price = entity.Price
    };

    public static ProductUpdateForm Create(Product product) => new()
    {
        Id = product.Id,
        ProductName = product.ProductName,
        ProductDescription = product.ProductDescription,
        Price = product.Price
    };
    public static ProductEntity Create(ProductEntity productEntity, ProductUpdateForm form) => new()
    {
        Id = productEntity.Id,
        ProductName = form.ProductName,
        ProductDescription = form.ProductDescription,
        Price = form.Price
    };
}
