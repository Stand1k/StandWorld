using System;
using System.Collections.Generic;
using StandWorld.Definitions;

namespace StandWorld.Entities
{
    public class Inventory
    {
        public Queue<Item> listItems = new Queue<Item>();

        public int max
        {
            get
            {
                if (_max != -1)
                {
                    return _max;
                }

                if (_parent == null || _parent.tilableDef == null)
                {
                    return 0;
                }

                return _parent.tilableDef.maxStack;
            }
        }

        public int count => listItems.Count;
        public int free => max - count;
        public bool full => free <= 0;

        private int _max;
        private Stackable _parent;
        private TilableDef _def;

        public Action OnClear;
        public Action OnAdd;
        public Action<int> OnChangeCount;

        public TilableDef def
        {
            get
            {
                if (_parent != null)
                {
                    return _parent.tilableDef;
                }

                return _def;
            }
        }

        public Inventory(int max = -1)
        {
            _max = max;
        }

        public Inventory(TilableDef def = null, int max = -1)
        {
            _max = max;
            _def = def;
        }

        public Inventory(Stackable parent, int count = 1, int max = -1)
        {
            _max = max;
            _parent = parent;

            Create(count);
        }

        private void Create(int count)
        {
            for (int i = 0; i < count; i++)
            {
                if (full)
                {
                    break;
                }

                listItems.Enqueue(new Item(_parent.tilableDef));
            }

            if (OnAdd != null)
            {
                OnAdd();
            }
        }

        public void InitInventory(TilableDef def)
        {
            _def = def;
            listItems = new Queue<Item>();
        }

        public void TransfertTo(Inventory to, int qty)
        {
            if ((to.def == null || to.def == def) && !to.full)
            {
                if (to.def == null)
                {
                    to.InitInventory(def);
                }

                int added = 0;
                while (listItems.Count > 0 && added < qty && !to.full)
                {
                    to.listItems.Enqueue(listItems.Dequeue());
                    added++;
                }

                if (listItems.Count == 0)
                {
                    if (OnClear != null)
                    {
                        OnClear();
                    }

                    if (_parent == null)
                    {
                        _def = null;
                    }
                    else
                    {
                        _parent.Destroy();
                    }
                }

                if (added != 0)
                {
                    if (OnChangeCount != null)
                    {
                        OnChangeCount(added * -1);
                    }

                    if (to.OnChangeCount != null)
                    {
                        to.OnChangeCount(added);
                    }
                }
            }
        }
    }
}