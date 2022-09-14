using Hops.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Hops.Mappers;
using System.Threading.Tasks;

namespace Hops.Controllers;

[Route("[controller]")]
public class SearchController : Controller
{
    private readonly IHopRepository _hopRepository;
    private readonly IMaltRepository _maltRepository;
    private readonly IYeastRepository _yeastRepository;

    public SearchController(IHopRepository hopRepository,
        IMaltRepository maltRepository,
        IYeastRepository yeastRepository)
    {
        _hopRepository = hopRepository;
        _maltRepository = maltRepository;
        _yeastRepository = yeastRepository;
    }

    [HttpGet]
    public IActionResult Index()
        => View();

    [HttpGet("{searchTerm}")]
    public async Task<IActionResult> Results(string searchTerm)
        => await Results(searchTerm, 1);

    [HttpGet("{searchTerm}/page/{page:int}")]
    public async Task<IActionResult> Results(string searchTerm, int page)
    {
        var results = await _hopRepository.Search(SlugMapper.SlugToString(searchTerm), page);

        return results.List.Count() switch
        {
            0 => View("NoResults", await _hopRepository.GetRandomHop()),
            1 => Redirect($"/hop/{results.List.First().Slug()}"),
            _ => View(results),
        };
    }

    [HttpGet("inventory/{page:int?}")]
    public IActionResult Inventory(int page = 1)
        => View(page);

    [HttpGet("aroma/{profile:int}")]
    public async Task<IActionResult> Results(int profile)
        => await Results(profile, 1);

    [HttpGet("aroma/{profile:int}/page/{page:int}")]
    public async Task<IActionResult> Results(int profile, int page)
    {
        var results = await _hopRepository.Search(profile, page);

        return View(results);
    }

    [HttpGet("autocomplete/{searchTerm}")]
    public async Task<List<string>> AutoComplete(string searchTerm)
        => await _hopRepository.Autocomplete(searchTerm);

    [HttpGet("autocompletemalt/{searchTerm}")]
    public async Task<List<string>> AutoCompleteMalt(string searchTerm)
        => await _maltRepository.Autocomplete(searchTerm);

    [HttpGet("autocompleteyeast/{searchTerm}")]
    public async Task<List<string>> AutoCompleteYeast(string searchTerm)
        => await _yeastRepository.Autocomplete(searchTerm);

    [HttpGet("freetext/{searchterm}")]
    public async Task<IActionResult> FreeTextResults(string searchterm)
        => await FreeTextResults(searchterm, 1);


    [HttpGet("freetext/{searchterm}/page/{page:int}")]
    public async Task<IActionResult> FreeTextResults(string searchterm, int page)
    {
        var results = await _hopRepository.FreeTextSearch(searchterm, page);

        return View("Results", results);
    }
}
