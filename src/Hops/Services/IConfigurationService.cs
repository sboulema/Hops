using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hops.Services
{
    public interface IConfigurationService
    {
        string Get(string key);
    }
}
