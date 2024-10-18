using DataLayer;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

namespace WebServer.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesControlle : ControllerBase
{
    private readonly IDataService _dataService;
    private readonly LinkGenerator _linkGenerator;

    public CategoriesControlle(
        IDataService dataService,
        LinkGenerator linkGenerator)
    {
        _dataService = dataService;
        _linkGenerator = linkGenerator;
    }
    [HttpGet]
    public IActionResult GetCategories()
    {
        var categories = _dataService.GetCategories()
                                     .Select(x => CreateCategoryModel(x));

        return Ok(categories);
    }

    [HttpGet("{id}", Name = nameof(GetCategory))]
    public IActionResult GetCategory(int id)
    {
        var category = _dataService.GetCategory(id);

        if (category == null)
        {
            return NotFound();
        }

        var model = CreateCategoryModel(category);

        return Ok(model);
    }

    private CategoryModel? CreateCategoryModel(Category? category)
    {
        if (category == null)
        {
            return null;
        }
        var model = category.Adapt<CategoryModel>(); // copies common attributes, value from category into model
        model.Url = GetUrl(category.Id);
        

        return model;
    }

    private string? GetUrl(int id)
    {
        return _linkGenerator.GetUriByName(HttpContext, nameof(GetCategory), new { id });
    } 
}
