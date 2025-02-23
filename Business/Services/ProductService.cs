﻿using System.Diagnostics;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Repositories;


namespace Business.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<Product?> CreateProductAsync(ProductRegistrationForm form)
    {

        var entity = await _productRepository.GetAsync(x => x.ProductName == form.ProductName);
        var productEntity = ProductFactory.Create(form);
        await _productRepository.CreateAsync(productEntity!);

        return ProductFactory.Create(productEntity!);
    }

    public async Task<IEnumerable<Product?>> GetAllProductsAsync()
    {
        var productEntity = await _productRepository.GetAllAsync();
        return productEntity.Select(ProductFactory.Create);
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        var productEntity = await _productRepository.GetAsync(x => x.Id == id);
        return productEntity != null ? ProductFactory.Create(productEntity!) : null;
    }

    // DENNA KOD ÄR DELVIS IFRÅN CHATGPT.
    public async Task<bool> UpdateProductAsync(int id, ProductUpdateForm form)
    {

        if (form == null)
        {
            return false;
        }
        var existingProduct = await _productRepository.GetAsync(x => x.Id == id);
        if (existingProduct != null)
        {
            existingProduct.ProductName = form.ProductName;
            existingProduct.ProductDescription = form.ProductDescription;
            existingProduct.Price = form.Price;
            existingProduct.Id = form.Id;

            await _productRepository.UpdateAsync(existingProduct);
        }
        return true;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var productEntity = await _productRepository.GetAsync(x => x.Id == id);
        if (productEntity == null)
            return false;

        try
        {
            var result = await _productRepository.DeleteAsync(productEntity);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }


}


