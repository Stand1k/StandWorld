using StandWorld.Definitions;

namespace StandWorld.Entities
{
    public class Item : Tilable
    {
        private Inventory _inventory;

        public Item(TilableDef tilableDef)
        {
            this.tilableDef = tilableDef;
        }

    }
}
