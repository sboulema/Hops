using Hops.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Hops.Controllers;

[Route("[controller]")]
public class MaltController : Controller
{
    private readonly IMaltRepository _maltRepository;

    public MaltController(IMaltRepository maltRepository)
    {
        _maltRepository = maltRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var malt = await _maltRepository.Search(string.Empty, 1);

        return View(malt);
    }

    [HttpGet("page/{page}")]
    public async Task<IActionResult> Index(int page)
    {
        var results = await _maltRepository.Search(string.Empty, page);

        return View("~/Views/Malt/Index.cshtml", results);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Detail(long id)
    {
        var malt = await _maltRepository.Get(id);

        return View(malt);
    }

    [HttpGet("inventory/{searchTerm}/{page:int?}")]
    public async Task<IActionResult> Inventory(string searchTerm, int page = 1)
    {
        var results = await _maltRepository.Search(searchTerm.Split(',').Select(s => long.Parse(s)).ToList(), page);

        return View("~/Views/Malt/List.cshtml", results);
    }

    [HttpGet("{name}/color")]
    public async Task<double> GetColor(string name)
    {
        var malt = await _maltRepository.Get(name);

        if (malt == null)
        {
            return 0;
        }

        return (malt.EBCMin + malt.EBCMax) / 2;
    }

    [HttpGet("{name}/yield")]
    public async Task<double> GetYield(string name)
    {
        var malt = await _maltRepository.Get(name);
        return malt?.Yield ?? 75;
    }
}
