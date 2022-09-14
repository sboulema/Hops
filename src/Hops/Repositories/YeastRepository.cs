using Hops.Mappers;
using Hops.Models;
using Hops.Models.ViewModels;
using Hops.Models.Yeasts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hops.Repositories;

public class YeastRepository : IYeastRepository
{
    private readonly BrewDBContext _context;

    public YeastRepository(BrewDBContext context)
    {
        _context = context;
    }

    public async Task<Yeast?> Get(long id)
        => await _context.Yeast!.FirstOrDefaultAsync(yeast => yeast.Id == id);

    public async Task<Yeast?> Get(string name)
        => await _context.Yeast!.FirstOrDefaultAsync(yeast => EF.Functions.Like(yeast.Name, $"%{name}%"));

    public async Task<Yeast?> GetRandom()
    {
        var count = await _context.Yeast!.CountAsync();
        return await Get(new Random().Next(1, count + 1));
    }

    public async Task<ListModel<Yeast>> Search(string searchTerm, int page)
    {
        var results = await _context.Yeast!
            .Where(yeast => EF.Functions.Like(yeast.Name, $"%{searchTerm}%"))
            .OrderBy(m => m.Name)
            .ToListAsync();

        return ResultMapper.Map(results, searchTerm, page);
    }

    public async Task<ListModel<Yeast>> Search(List<long> ids, int page)
    {
        var results = await _context.Yeast!
            .Where(r => ids.IndexOf(r.Id) != -1)
            .OrderBy(m => m.Name)
            .ToListAsync();

        return ResultMapper.Map(results, "Inventory", page);
    }

    public async Task<List<string>> Autocomplete(string searchTerm)
        => await _context.Yeast!
            .Where(yeast => EF.Functions.Like(yeast.Name, $"%{searchTerm}%"))
            .Select(m => m.Name)
            .ToListAsync();
}
