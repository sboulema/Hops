using Hops.Mappers;
using Hops.Models;
using Hops.Models.Malts;
using Hops.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hops.Repositories;

public class MaltRepository : IMaltRepository
{
    private readonly BrewDBContext _context;

    public MaltRepository(BrewDBContext context)
    {
        _context = context;
    }

    public async Task<Malt?> Get(long id)
        => await _context.Malt!.FirstOrDefaultAsync(malt => malt.Id == id);

    public async Task<Malt?> Get(string name)
        => await _context.Malt!.FirstOrDefaultAsync(malt => EF.Functions.Like(malt.Name, $"%{name}%"));

    public async Task<Malt?> GetRandom()
    {
        var count = await _context.Malt!.CountAsync();
        return await Get(new Random().Next(1, count + 1));
    }

    public async Task<ListModel<Malt>> Search(string searchTerm, int page)
    {
        var results = await _context.Malt!
            .Where(malt => EF.Functions.Like(malt.Name, $"%{searchTerm}%"))
            .OrderBy(malt => malt.Name)
            .ToListAsync();

        return ResultMapper.Map(results, searchTerm, page);
    }

    public async Task<ListModel<Malt>> Search(List<long> maltIds, int page)
    {
        var results = await _context.Malt!
            .Where(malt => maltIds.Contains(malt.Id))
            .OrderBy(malt => malt.Name)
            .ToListAsync();

        return ResultMapper.Map(results, "Inventory", page);
    }

    public async Task<List<string>> Autocomplete(string searchTerm)
        => await _context.Malt!
            .Where(malt => EF.Functions.Like(malt.Name, $"%{searchTerm}%"))
            .OrderBy(malt => malt.Name)
            .Select(malt => malt.Name)
            .ToListAsync();
}
