using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.Entities;

namespace StandWorld.World
{
    public class WorldUtilsHaul : Singleton<WorldUtilsHaul>
    {
        public readonly Dictionary<TilableDef, List<Stackable>> stackables = new Dictionary<TilableDef, List<Stackable>>();
        public readonly Dictionary<TilableDef, int> stackablesCount = new Dictionary<TilableDef, int>();


        public int StackableCount(TilableDef def)
        {
            if (stackablesCount.ContainsKey(def))
            {
                return stackablesCount[def];
            }

            return 0;
        }

        public void AddStackable(TilableDef def, Stackable stackable)
        {
            if (!stackables.ContainsKey(def))
            {
                stackables.Add(def, new List<Stackable>());
                stackablesCount.Add(def, 0);
            }

            stackables[def].Add(stackable);
            stackablesCount[def] += stackable.inventory.count;
        }

        public void ClearStackable(TilableDef def, Stackable stackable)
        {
            if (stackables.ContainsKey(def))
            {
                stackables[def].Remove(stackable);
            }
        }

        public void UpdateStackableCount(TilableDef def, int qty)
        {
            if (stackablesCount.ContainsKey(def))
            {
                stackablesCount[def] += qty;
                if (stackablesCount[def] < 0)
                {
                    stackablesCount[def] = 0;
                }
            }
        }
    }
}