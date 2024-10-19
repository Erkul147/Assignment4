using DataLayer;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using WebServer.Models;
namespace WebServer.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    
    IDataService _dataService;
    readonly LinkGenerator _linkGenerator;

    public ProductController(
        IDataService dataService,
        LinkGenerator linkGenerator)
    {
        _dataService = dataService;
        _linkGenerator = linkGenerator;
    }


    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var product = _dataService.GetProduct(id);

        if (product == null)
        {
            return NotFound();
        }
        var model = CreateProductModel(product);
        return Ok(model);
    }

    
    [HttpGet("category/{id}")]
    public IActionResult GetProductsByCategoryID(int id)
    {

        var products = _dataService.GetProductByCategory(id)
                                            .Select(x => CreateProductModel(x));

        if (products == null || products.Count() == 0)
        {
            return NotFound(products);
        }

        return Ok(products);
    }


    [HttpGet]
    public IActionResult GetProductsByContainsName([FromQuery] string name)
    {
        var products = _dataService.GetProductByName(name)
                                            .Select(x => CreateProductModel(x));

        if (products == null || products.Count() == 0)
        {
            return NotFound(products);
        }

        Console.WriteLine("Query: " + name);
        foreach (var item in products)
        {
            Console.WriteLine("productname: " + item.ProductName + " category " + item.CategoryName);
        }
        Console.WriteLine();

        return Ok(products.ToList());
    }

    private ProductModel? CreateProductModel(Product? product)
    {
        if (product == null)
        {
            return null;
        }
        var model = product.Adapt<ProductModel>(); // copies common attributes, value from product into model
        model.ProductName = product.Name;
        model.Url = GetUrl(product.Id);

        return model;
    }

    private string? GetUrl(int id)
    {
        return _linkGenerator.GetUriByName(HttpContext, nameof(GetProduct), new { id });
    }
}