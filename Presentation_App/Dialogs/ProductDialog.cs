using Business.Factories;
using Business.Interfaces;
using Presentation_App.Interfaces;

namespace Presentation_App.Dialogs;

public class ProductDialog(IProductService productService) : IProductDialog
{
    private readonly IProductService _productService = productService;

    public async Task CreateProductOption()
    {
        Console.Clear();
        Console.WriteLine("#### CREATE PRODUCT ####");

        var product = ProductFactory.CreateRegistrationForm();
        Console.Write("Product Name: ");
        product.ProductName = Console.ReadLine()!;
        Console.Write("Product Description: ");
        product.ProductDescription = Console.ReadLine()!;
        Console.Write("Price: ");
        var priceInput = Console.ReadLine()!;

        if (!string.IsNullOrEmpty(priceInput) && decimal.TryParse(priceInput, out decimal price))
        {
            product.Price = price;
        }
        else
        {
            Console.WriteLine("Ivalid price input.");
        }


        var result = await _productService.CreateProductAsync(product);
        if (result != null)
            Console.WriteLine("\nProduct was created successfully.");
        else
            Console.WriteLine("\nProduct was not created.");

        Console.ReadKey();
    }

    public async Task ViewAllProductsOption()
    {
        Console.Clear();
        Console.WriteLine("#### VIEW ALL PRODUCTS ####");

        var results = await _productService.GetAllProductsAsync();

        if (results != null && results.Any())
        {
            foreach (var product in results)
                Console.WriteLine($"{product.Id}, {product.ProductName} {product.ProductDescription} {product.Price}");
        }
        else
            Console.WriteLine("No products was found.");

        Console.ReadKey();
    }

    public async Task ViewProductByIdOption()
    {
        Console.Clear();
        Console.WriteLine("#### VIEW PRODUCT ####");

        Console.Write("Product Id: ");
        var productId = Convert.ToInt32(Console.ReadLine())!;

        var product = await _productService.GetProductByIdAsync(productId);
        if (product != null)
            Console.WriteLine($"{product.Id}, {product.ProductName} {product.ProductDescription} {product.Price}");
        else
            Console.WriteLine("Product was not found.");

        Console.ReadKey();
    }

    public async Task UpdateProductOption()
    {
        Console.Clear();
        Console.WriteLine("#### UPDATE PRODUCT ####");

        Console.Write("Product Id: ");
        var productId = Convert.ToInt32(Console.ReadLine())!;

        var product = await _productService.GetProductByIdAsync(productId);
        if (product == null)
            Console.WriteLine("Product was not found.");
        else
        {
            Console.WriteLine($"Id: {product.Id}");
            Console.WriteLine($"ProductName: {product.ProductName}");
            Console.WriteLine($"ProductDescription: {product.ProductDescription}");
            Console.WriteLine($"ProductPrice: {product.Price}");
            Console.WriteLine("");

            var productUpdateForm = ProductFactory.CreateUpdateForm();
            productUpdateForm.Id = product.Id;

            Console.Write("ProductName: ");
            var productName = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(productName))
                productUpdateForm.ProductName = productName;

            Console.Write("Product Description: ");
            var productDescription = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(productDescription))
                productUpdateForm.ProductDescription = productDescription;

            Console.Write("Price: ");
            var priceInput = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(priceInput) && decimal.TryParse(priceInput, out decimal price))
                {
                    productUpdateForm.Price = price;
                }
            else
            {
                Console.WriteLine("Invalid price input.");
            }

            var updatedProduct = await _productService.UpdateProductAsync(product.Id, productUpdateForm);
            if (updatedProduct != false)
                Console.WriteLine($"{product.Id}, {product.ProductName} {product.ProductDescription} {product.Price}");
            else
                Console.WriteLine("Something went wrong!");
        }

        Console.ReadKey();
    }

    public async Task DeleteProductOption()
    {
        Console.Clear();
        Console.WriteLine("#### DELETE PRODUCT ####");

        Console.Write("Product Id: ");
        var productId = Convert.ToInt32(Console.ReadLine())!;

        var product = await _productService.GetProductByIdAsync(productId);
        if (product == null)
        {
            Console.WriteLine("Product was not found.");
            return;
        }

        var result = await _productService.DeleteProductAsync(product.Id);
        if (result)
            Console.WriteLine($"Product was deleted successfully.");
        else
            Console.WriteLine("Something went wrong!");


        Console.ReadKey();
    }

    public async Task MenuOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("#### MENU OPTIONS ####");
            Console.WriteLine("1. Create New Product");
            Console.WriteLine("2. View All Products");
            Console.WriteLine("3. View Product");
            Console.WriteLine("4. Update Product");
            Console.WriteLine("5. Delete Product");
            Console.WriteLine("6. Back to main menu.");
            Console.Write("SELECTED MENU OPTION: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateProductOption();
                    break;

                case "2":
                    await ViewAllProductsOption();
                    break;

                case "3":
                    await ViewProductByIdOption();
                    break;
                case "4":
                    await UpdateProductOption();
                    break;

                case "5":
                    await DeleteProductOption();
                    break;

                case "6":
                    Console.WriteLine("Returning to main menu..");
                    return;
            }
        }
    }

}
