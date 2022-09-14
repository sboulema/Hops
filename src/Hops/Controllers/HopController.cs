using Hops.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Hops.Controllers;

[Route("[controller]")]
public class HopController : Controller
{
    private readonly IHopRepository _hopRepository;

    public HopController(IHopRepository hopRepository)
    {
        _hopRepository = hopRepository;
    }

    [HttpGet]
    [Route("/")]
    public async Task<IActionResult> Index()
    {
        var results = await _hopRepository.Search(string.Empty, 1);

        return View(results);
    }

    [HttpGet]
    public async Task<IActionResult> IndexHop()
    {
        var results = await _hopRepository.Search(string.Empty, 1);

        return View("Index", results);
    }

    [HttpGet("page/{page}", Name = "Page")]
    public async Task<IActionResult> Index(int page)
    {
        var results = await _hopRepository.Search(string.Empty, page);

        return View("~/Views/Hop/Index.cshtml", results);
    }

    [HttpGet("{id:long}", Name = "Detail")]
    public async Task<IActionResult> Detail(long id)
    {
        var hop = await _hopRepository.GetHopById(id);

        return View(hop);
    }

    [HttpGet("{slug}", Name = "DetailBySlug")]
    public async Task<IActionResult> DetailBySlug(string slug)
    {
        var hop = await _hopRepository.GetHopBySlug(slug);

        return View("Detail", hop);
    }

    [HttpGet("[action]/{searchTerm}/{page:int?}")]
    public async Task<IActionResult> Inventory(string searchTerm, int page = 1)
    {
        var results = await _hopRepository.Search(searchTerm.Split(',').Select(s => long.Parse(s)).ToList(), page);

        return View("~/Views/Hop/List.cshtml", results);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Top()
    {
        var results = await _hopRepository.TopSubstitutors();

        return View("Index", results);
    }

    [HttpGet("{name}/alphaacid")]
    public async Task<double> GetAlphaAcid(string name)
    {
        var hop = await _hopRepository.GetHopByName(name);

        if (hop == null)
        {
            return 0;
        }

        return (hop.AlphaMin + hop.AlphaMax) / 2;
    }
}
