using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StandWorld.Characters
{
    public class HumanStats
    {
        public Dictionary<Stats, StatsBase> stats;
        public Dictionary<Vitals, Vital> vitals;
        public Dictionary<Attributes, Attribute> attributes;

        public HumanStats()
        {
            stats = new Dictionary<Stats, StatsBase>();
            foreach (Stats stat in StatsUtils.stats)
            {
                stats.Add(stat, new StatsBase(stat.ToString()));
                stats[stat].baseValue = Random.Range(1, 10);
            }

            vitals = new Dictionary<Vitals, Vital>();
            foreach (Vitals vital in StatsUtils.vitals)
            {
                vitals.Add(vital, new Vital(vital.ToString()));
            }
            LoadVitals();

            attributes = new Dictionary<Attributes, Attribute>();
            foreach (Attributes att in StatsUtils.attributes)
            {
                attributes.Add(att, new Attribute(att.ToString()));
            }

            LoadAttributes();
        }

        private void LoadAttributes()
        {
            attributes[Attributes.InventorySize].AddModifier(new StatModifier(stats[Stats.Strength], 3f));
            
            attributes[Attributes.WalkSpeed].AddModifier(new StatModifier(stats[Stats.Strength], 1f));
            attributes[Attributes.WalkSpeed].AddModifier(new StatModifier(stats[Stats.Endurance], 1f));
            
            attributes[Attributes.HealthRegen].AddModifier(new StatModifier(stats[Stats.Strength], 0.5f));
            attributes[Attributes.HealthRegen].AddModifier(new StatModifier(stats[Stats.Endurance], 0.5f));
            
            attributes[Attributes.EnergyRegen].AddModifier(new StatModifier(stats[Stats.Wisdom], 0.5f));
            attributes[Attributes.EnergyRegen].AddModifier(new StatModifier(stats[Stats.Endurance], 0.5f));

            foreach (Attribute att in attributes.Values)
            {
                att.Update();
            }
        }

        private void LoadVitals()
        {
            vitals[Vitals.Healt].AddModifier(new StatModifier(stats[Stats.Endurance], 20f));
            vitals[Vitals.Energy].AddModifier(new StatModifier(stats[Stats.Agility], 2f));
            vitals[Vitals.Energy].AddModifier(new StatModifier(stats[Stats.Strength], 2f));
            vitals[Vitals.Energy].AddModifier(new StatModifier(stats[Stats.Wisdom], 2f));
            vitals[Vitals.Mana].AddModifier(new StatModifier(stats[Stats.Intellect], 10f));
            
            foreach (Vital vital in vitals.Values)
            {
                vital.Update();
                vital.Fill();
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

            return str;
        }
    }
}
