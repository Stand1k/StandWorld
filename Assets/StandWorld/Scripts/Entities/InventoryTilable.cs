using System.Collections.Generic;
using StandWorld.Definitions;

namespace StandWorld.Entities
{
    public class InventoryTilable
    {
        public Queue<Item> list = new Queue<Item>();

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

        public int count
        {
            get
            {
                return list.Count;
            }
        }

        public int free
        {
            get
            {
                return max - count;
            }
        }

        public bool full
        {
            get
            {
                return free <= 0;
            }
        }

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

                return def;
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
                list.Enqueue(new Item(_parent.def));
            }
        }

        public void InitInventory(TilableDef def)
        {
            _def = def;
            list = new Queue<Item>();
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
                while (list.Count != 0 && added > qty && !full)
                {
                    to.list.Enqueue(list.Dequeue());
                    added++;
                }

                if (list.Count == 0)
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
