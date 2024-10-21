using DataLayer;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

namespace WebServer.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    IDataService _dataService;
    readonly LinkGenerator _linkGenerator;

    public CategoriesController(
        IDataService dataService,
        LinkGenerator linkGenerator)
    {
        _dataService = dataService;
        _linkGenerator = linkGenerator;
    }


    [HttpPost]
    public IActionResult CreateCategory(CreateCategoryModel model)
    {
        Console.WriteLine("create category");
        if (model == null)
        {
            return NotFound();
        }

        var category = _dataService.CreateCategory(model.Name, model.Description);
        Console.WriteLine("gogo cate " + category);
        if (category == null)
        {
            return NotFound();
        }


        return Created("GetCategory", CreateCategoryModel(category));
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
        var categories = _dataService.GetCategories()
                                     .Select(x => CreateCategoryModel(x));

        if (categories == null) return null;

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


    [HttpPut("{id}")]
    public IActionResult UpdateCategory(int id, CreateCategoryModel model)
    {
        if (model == null)
        {
            return NotFound();
        }

        bool updated = _dataService.UpdateCategory(id, model.Name, model.Description);
        if (!updated)
        {
            return NotFound();
        }


        return Ok(CreateCategoryModel(_dataService.GetCategory(id)));
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(int id)
    {
        var deleted = _dataService.DeleteCategory(id);
        if (!deleted)
        { 
            return NotFound();
        }
        return Ok();
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
