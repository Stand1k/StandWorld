﻿using StandWorld.Definitions;

namespace StandWorld.Entities
{
    public class Item : Tilable
    {
        private InventoryTilable _inventory;

        public Item(TilableDef def)
        {
            this.def = def;
        }

    }
}