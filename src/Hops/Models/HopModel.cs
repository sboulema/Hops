using System.Collections.Generic;

namespace Hops.Models
{
    public class HopModel
    {
        public Hop Hop;
        public List<Hop> Substitutions;
        public List<string> Aliases;
        public List<AromaProfileEnum> Aromas;
    }
}
