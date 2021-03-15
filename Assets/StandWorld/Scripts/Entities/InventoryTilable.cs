using System.Collections.Generic;
using StandWorld.Definitions;

namespace StandWorld.Entities
{
    public class InventoryTilable
    {
        public Queue<Item> inventoryQueue = new Queue<Item>();

        public int max
        {
            get
            {
                if (_max != -1)
                {
                    return _max;
                }

                if (_parent == null || _parent.def == null)
                {
                    return 0;
                }

                return _parent.def.maxStack;
            }
        }

        public int count => inventoryQueue.Count;

        public int free => max - count;

        public bool full => free <= 0;

        private int _max;

        private Stackable _parent;

        private TilableDef _def;

        public TilableDef def
        {
            get
            {
                if (_parent != null)
                {
                    return _parent.def;
                }

                return _def;
            }
        }

        public InventoryTilable(TilableDef def = null, int max = -1)
        {
            _max = max;
            _def = def;
        }

        public InventoryTilable(Stackable parent,int count = 1, int max = -1)
        {
            _max = max;
            _parent = parent;

            if (count > 1)
            {
                Create(count);
            }
        }

        private void Create(int count)
        {
            for (int i = 0; i < count; i++)
            {
                if (full)
                {
                    break;
                }
                inventoryQueue.Enqueue(new Item(_parent.def));
            }
        }

        public void InitInventory(TilableDef def)
        {
            _def = def;
            inventoryQueue = new Queue<Item>();
        }

        public void TransfertTo(ref InventoryTilable to, int qty)
        {
            if ((to.def == null || to.def == def) && !to.full)
            {
                if (to.def == null)
                {
                    to.InitInventory(def);
                }

                int added = 0;
                while (inventoryQueue.Count != 0 && added > qty && !full)
                {
                    to.inventoryQueue.Enqueue(inventoryQueue.Dequeue());
                    added++;
                }

                if (inventoryQueue.Count == 0)
                {
                    if (_parent == null)
                    {
                        _def = null;
                    }
                    else
                    {
                        _parent.Destroy();
                    }
                }
            }
        }
        
    }
}
