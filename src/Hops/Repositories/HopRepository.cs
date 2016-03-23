using Hops.Models;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace Hops.Repositories
{
    public class HopRepository : IHopRepository
    {
        private readonly HopContext context;
        private readonly IUrlHelper urlHelper;

        public HopRepository(HopContext context, IHttpContextAccessor contextAccessor)
        {
            this.context = context;
            urlHelper = contextAccessor.HttpContext.RequestServices.GetRequiredService<IUrlHelper>();
        }

        public ListModel GetPage(int page)
        {
            var hops = context.Hops;

            var results = new ListModel();
            results.List = hops.OrderBy(h => h.Name).Skip((page - 1) * 15).Take(15).ToList();
            results.NumberOfPages = (hops.Count() / 15) + 1;
            results.CurrentPageIndex = page;
            return results;
        }

        public Hop Get(long id)
        {
            var hop = context.Hops.First(t => t.Id == id);
            return hop;
        }

        public List<Hop> GetSubstitutions(long id)
        {
            var substitutions = context.Substitutions.Where(s => s.HopId == id).ToList();

            var hops = new List<Hop>();
            foreach (var substitute in substitutions)
            {
                hops.Add(Get(substitute.SubId));
            }

            return hops;
        }

        public List<string> GetAliases(long id)
        {
            var aliases = context.Alias.Where(a => a.HopId == id).Select(a => a.Name).ToList();
            return aliases;
        }
    }
}
