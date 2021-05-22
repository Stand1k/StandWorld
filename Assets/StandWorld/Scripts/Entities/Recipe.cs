using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.World;
using UnityEngine;

namespace StandWorld.Entities
{
    public class Recipe : Tilable
    {
        public RecipeDef def;
        public Dictionary<TilableDef, Inventory> needs;
        public bool finished { get; protected set; }
        public Building building;

        public bool canBeComplete
        {
            get
            {
                foreach (KeyValuePair<TilableDef, Inventory> kv in needs)
                {
                    if (WorldUtilsHaul.Instance.StackableCount(kv.Key) < kv.Value.free)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public Recipe(RecipeDef def, Building building, Vector2Int position)
        {
            this.def = def;
            hidden = true;
            finished = false;
            this.building = building;
            this.position = position;
            needs = new Dictionary<TilableDef, Inventory>();
            foreach (KeyValuePair<TilableDef, int> kv in this.def.reqs)
            {
                needs.Add(kv.Key, new Inventory(
                    kv.Key, kv.Value
                ));
                needs[kv.Key].OnChangeCount = qty => { IsComplete(); };
            }


            WorldUtilsBuidling.Instance.recipes.Add(this);
        }

        public TilableDef FirstNeed()
        {
            foreach (Inventory inv in needs.Values)
            {
                if (inv.free > 0)
                {
                    return inv.def;
                }
            }

            return null;
        }

        public bool IsComplete()
        {
            if (!finished)
            {
                foreach (Inventory inv in needs.Values)
                {
                    if (!inv.full)
                    {
                        return false;
                    }
                }

                finished = true;
                building.Construct();
                WorldUtilsBuidling.Instance.recipes.Remove(this);
                return true;
            }

            return true;
        }
    }
}