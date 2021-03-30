using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StandWorld.Characters
{
    public class HumanStats : BaseStats
    {
        public HumanStats() : base()
        {
        }

        protected override void LoadAttributes()
        {
            base.LoadAttributes();
            
            attributes[Attributes.InventorySize].AddModifier(new StatModifier(stats[Stats.Strength], 3f));

            foreach (Attribute att in attributes.Values)
            {
                att.Update();
            }
        }
        

        public override string ToString()
        {
            string str = "HumanStats(";
            foreach (StatsBase statsBase in stats.Values)
            {
                str += statsBase.name + ": " + statsBase.value + " | ";
            }
            
            foreach (Vital vital in vitals.Values)
            {
                str += vital.name + ": " + vital.value + " | ";
            }
            
            foreach (Attribute att in attributes.Values)
            {
                str += att.name + ": " + att.value + " | ";
            }

            return str + ")";
        }
    }
}
