using System.Collections.Generic;
using StandWorld.Entities;

namespace StandWorld.Definitions
{
    public class RecipeDef : Def
    {
        public Dictionary<TilableDef, int> reqs = new Dictionary<TilableDef, int>();
    }
}