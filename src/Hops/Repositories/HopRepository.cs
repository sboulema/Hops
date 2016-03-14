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

        public List<Hop> GetAll()
        {
            var hops = context.Hops.ToList();
            return hops;
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
    }
}
