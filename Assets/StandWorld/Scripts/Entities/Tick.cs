
using System.Collections.Generic;
using UnityEngine;

namespace StandWorld.Entities
{
    public delegate void TickDelegate();

    public class Tick
    {
        public int tick = 0;
        public int speed = 1;

        public Queue<TickDelegate> toAdd = new Queue<TickDelegate>();
        public Queue<TickDelegate> toDel = new Queue<TickDelegate>();
        public List<TickDelegate> updates = new List<TickDelegate>();

        public void DoTick()
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