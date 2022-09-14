using Hops.Models.Yeasts;
using Hops.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Hops.Controllers;

[Route("[controller]")]
public class YeastController : Controller
{
    private readonly IYeastRepository _yeastRepository;

    public YeastController(IYeastRepository yeastRepository)
    {
        _yeastRepository = yeastRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var results = await _yeastRepository.Search(string.Empty, 1);

        return View(results);
    }

    [HttpGet("page/{page}")]
    public async Task<IActionResult> Index(int page)
    {
        var results = await _yeastRepository.Search(string.Empty, page);

        return View("~/Views/Yeast/Index.cshtml", results);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Detail(long id)
    {
        var yeast = await _yeastRepository.Get(id);

        return View(yeast);
    }

    [HttpGet("inventory/{searchTerm}/{page:int?}")]
    public async Task<IActionResult> Inventory(string searchTerm, int page = 1)
    {
        var results = await _yeastRepository.Search(searchTerm.Split(',').Select(s => long.Parse(s)).ToList(), page);

        return View("~/Views/Yeast/List.cshtml", results);
    }

    [HttpGet("{name}/attenuation")]
    public async Task<int> GetAttenuation(string name)
    {
        var yeast = await _yeastRepository.Get(name);

        if (yeast == null)
        {
            return 0;
        }

        return (yeast.AttenuationMin + yeast.AttenuationMax) / 2;
    }

    [HttpGet("{name}/lab")]
    public async Task<string> GetLab(string name)
    {
        var yeast = await _yeastRepository.Get(name);

        if (yeast == null)
        {
            return string.Empty;
        }

        return ((YeastLabEnum)yeast.Lab).Wordify();
    }
}
