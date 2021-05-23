
using System.Collections.Generic;
using UnityEngine;

namespace StandWorld.Entities
{
    public delegate void TickDelegate();

    public class Tick
    {
        public int tick = 0;
        public float speed = 1;
        public bool isStop = false;

        public Queue<TickDelegate> toAdd = new Queue<TickDelegate>();
        public Queue<TickDelegate> toDel = new Queue<TickDelegate>();
        public List<TickDelegate> updates = new List<TickDelegate>();

        public void DoTick()
        {
            if (!isStop)
            {
                tick++;

                while (toDel.Count != 0) 
                {
                    updates.Remove(toDel.Dequeue());
                }
            
                while (toAdd.Count != 0) 
                {
                    updates.Add(toAdd.Dequeue());
                }

                for (int i = 0; i < updates.Count; i++)
                {
                    updates[i].Invoke();
                }
            }
        }
    }

}