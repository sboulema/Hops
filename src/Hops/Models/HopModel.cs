using System.Collections.Generic;

namespace Hops.Models
{
    public class HopModel
    {
        public Hop Hop;
        public List<Hop> Substitutions = new List<Hop>();
        public List<string> Aliases = new List<string>();
        public List<AromaProfileEnum> Aromas = new List<AromaProfileEnum>();
    }
}
