using Hops.Extensions;
using Hops.Mappers;
using Hops.Models;
using Hops.Models.Hops;
using Hops.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hops.Repositories;

public class HopRepository : IHopRepository
{
    private readonly BrewDBContext _context;

    public HopRepository(BrewDBContext context)
    {
        _context = context;
    }

    public async Task<Hop?> GetHopById(long id, bool includeSubstitutions = true)
    {
        var hop = await _context.Hop!.FirstOrDefaultAsync(hop => hop.Id == id);

        if (includeSubstitutions)
        {
            hop?.Substitutions?.ForEach(async sub => sub.Hop = await GetHopById(sub.SubId));
        }

        return hop;
    }

    public async Task<Hop?> GetHopByName(string name)
    {
        var hop = await _context.Hop!
            .FirstOrDefaultAsync(hop => EF.Functions.Like(hop.Name, $"%{name}%") ||
                hop.Aliases!.Any(alias => EF.Functions.Like(alias.Name, $"%{name}%")));

        hop?.Substitutions?.ForEach(async sub => sub.Hop = await GetHopById(sub.SubId, false));

        return hop;
    }

    public async Task<Hop?> GetHopBySlug(string slug)
    {
        var hop = await _context.Hop!
            .FirstOrDefaultAsync(hop => EF.Functions.Like(hop.Name, $"%{SlugMapper.SlugToString(slug)}%"));

        hop?.Substitutions?.ForEach(async sub => sub.Hop = await GetHopById(sub.SubId, false));

        return hop;
    }

    public async Task<Hop?> GetRandomHop()
    {
        var count = await _context.Hop!.CountAsync();

        return await GetHopById(new Random().Next(1, count + 1));
    }

    public async Task<ListModel<Hop>> Search(string searchTerm, int page)
    {
        var hops = await _context.Hop!
            .Where(hop => EF.Functions.Like(hop.Name, $"%{searchTerm}%") ||
                          hop.Aliases!.Any(alias => EF.Functions.Like(alias.Name, $"%{searchTerm}%")))
            .OrderBy(hop => hop.Name)
            .ToListAsync();

        hops.ForEach(hop => hop.Substitutions?.ForEach(async sub => sub.Hop = await GetHopById(sub.SubId, false)));

        return ResultMapper.Map(hops, searchTerm, page);
    }

    public async Task<ListModel<Hop>> FreeTextSearch(string searchTerm, int page)
    {
        var hops = await _context.Hop!
            .Where(hop => EF.Functions.Like(hop.Name, $"%{searchTerm}%") ||
                          hop.Aliases!.Any(alias => EF.Functions.Like(alias.Name, $"%{searchTerm}%")) ||
                          EF.Functions.Like(hop.Info, $"%{searchTerm}%") ||
                          EF.Functions.Like(hop.Pedigree, $"%{searchTerm}%") ||
                          EF.Functions.Like(hop.Styles, $"%{searchTerm}%") ||
                          EF.Functions.Like(hop.Trade, $"%{searchTerm}%") ||
                          EF.Functions.Like(hop.Aroma, $"%{searchTerm}%"))
            .OrderBy(hop => hop.Name)
            .ToListAsync();

        hops.ForEach(hop => hop.Substitutions?.ForEach(async sub => sub.Hop = await GetHopById(sub.SubId, false)));

        return ResultMapper.Map(
            list: hops,
            searchTerm: searchTerm,
            page: page,
            highlight: searchTerm,
            searchType: "freetext");
    }

    public async Task<ListModel<Hop>> Search(List<long> hopIds, int page)
    {
        var hops = await _context.Hop!
            .Where(hop => hopIds.Contains(hop.Id))
            .OrderBy(hop => hop.Name)
            .ToListAsync();

        hops.ForEach(hop => hop.Substitutions?.ForEach(async sub => sub.Hop = await GetHopById(sub.SubId, false)));

        return ResultMapper.Map(hops, "Inventory", page);
    }

    public async Task<ListModel<Hop>> Search(int aromaProfile, int page)
    {
        var hops = await _context.Hop!
            .Where(hop => hop.Aromas!.Any(a => a.Profile == aromaProfile))
            .OrderBy(hop => hop.Name)
            .ToListAsync();

        hops.ForEach(hop => hop.Substitutions?.ForEach(async sub => sub.Hop = await GetHopById(sub.SubId, false)));

        return ResultMapper.Map(
            list: hops,
            searchTerm: ((AromaProfileEnum)aromaProfile).Wordify(),
            page: page,
            aromaProfile: aromaProfile);
    }

    public async Task<ListModel<Hop>> TopSubstitutors()
    {
        var results = await _context.Substitution!
            .GroupBy(sub => sub.SubId)
            .Select(group => new { SubId = group.Key, Count = group.Count() })
            .OrderByDescending(group => group.Count)
            .Take(10)
            .ToListAsync();

        var hops = new List<Hop>();

        foreach (var substitute in results)
        {
            var hop = await GetHopById(substitute.SubId, false);

            if (hop != null)
            {
                hops.Add(hop);
            }
        }

        return ResultMapper.Map(hops, string.Empty, 1);
    }

    public async Task<List<string>> Autocomplete(string searchTerm)
    {
        var results = await _context.Hop!
            .Where(hop => EF.Functions.Like(hop.Name, $"%{searchTerm}%") ||
                  hop.Aliases.Any(alias => EF.Functions.Like(alias.Name, $"%{searchTerm}%")) == true)
            .OrderBy(hop => hop.Name)
            .ToListAsync();

        var autocompleteList = new List<string>();

        foreach (var hop in results)
        {
            autocompleteList.Add(hop.Name);

            if (hop.Aliases?.Any() == true)
            {
                foreach (var alias in hop.Aliases)
                {
                    autocompleteList.Add(alias.Name);
                }
            }
        }

        return autocompleteList;
    }
}
