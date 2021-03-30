using System.Collections.Generic;
using UnityEngine;

namespace StandWorld.Characters
{
    public class BaseStats
    {
        public Dictionary<Stats, StatsBase> stats;
        public Dictionary<Vitals, Vital> vitals;
        public Dictionary<Attributes, Attribute> attributes;

        public BaseStats()
        {
            stats = new Dictionary<Stats, StatsBase>();
            foreach (Stats stat in StatsUtils.stats)
            {
                stats.Add(stat, new StatsBase(stat.ToString()));
                stats[stat].baseValue = Random.Range(5, 10);
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

        protected virtual void LoadAttributes()
        {
            attributes[Attributes.WalkSpeed].AddModifier(new StatModifier(stats[Stats.Strength], .3f));
            attributes[Attributes.WalkSpeed].AddModifier(new StatModifier(stats[Stats.Endurance], .2f));

            attributes[Attributes.HealthRegen].AddModifier(new StatModifier(stats[Stats.Strength], .1f));
            attributes[Attributes.HealthRegen].AddModifier(new StatModifier(stats[Stats.Endurance], .2f));

            attributes[Attributes.EnergyRegen].AddModifier(new StatModifier(stats[Stats.Endurance], .2f));

            attributes[Attributes.PhysicalResistance].AddModifier(new StatModifier(stats[Stats.Strength], .1f));
            attributes[Attributes.MagicalResistance].AddModifier(new StatModifier(stats[Stats.Endurance], .3f));

            attributes[Attributes.PhysicalAttack].AddModifier(new StatModifier(stats[Stats.Strength], .3f));
            attributes[Attributes.PhysicalAttack].AddModifier(new StatModifier(stats[Stats.Agility], .2f));

            attributes[Attributes.ManaRegen].AddModifier(new StatModifier(stats[Stats.Intellect], .3f));
            attributes[Attributes.ManaRegen].AddModifier(new StatModifier(stats[Stats.Wisdom], .2f));

            attributes[Attributes.CriticalChance].AddModifier(new StatModifier(stats[Stats.Agility], .2f));
            attributes[Attributes.CriticalChance].AddModifier(new StatModifier(stats[Stats.Intellect], .2f));

            attributes[Attributes.Charisma].AddModifier(new StatModifier(stats[Stats.Wisdom], .2f));
            attributes[Attributes.Charisma].AddModifier(new StatModifier(stats[Stats.Strength], .2f));


            foreach (Attribute att in attributes.Values)
            {
                att.Update();
            }
        }

        protected virtual void LoadVitals()
        {
            vitals[Vitals.Health].AddModifier(new StatModifier(stats[Stats.Endurance], 20f));
            vitals[Vitals.Energy].AddModifier(new StatModifier(stats[Stats.Agility], 5f));
            vitals[Vitals.Energy].AddModifier(new StatModifier(stats[Stats.Strength], 5f));
            vitals[Vitals.Energy].AddModifier(new StatModifier(stats[Stats.Wisdom], 5f));
            vitals[Vitals.Mana].AddModifier(new StatModifier(stats[Stats.Intellect], 10f));

            foreach (Vital vital in vitals.Values)
            {
                vital.Update();
                vital.Fill();
            }
        }

        public virtual void Update()
        {
            if (vitals[Vitals.Energy].currentValue > 0)
            {
                vitals[Vitals.Energy].currentValue -= 0.02f;
            }
        }

        public override string ToString()
        {
            string str = "BaseStats(";
            foreach (StatsBase stat in stats.Values)
            {
                str += stat.name + ":" + stat.value + ",";
            }

            foreach (Vital stat in vitals.Values)
            {
                str += stat.name + ":" + stat.value + ",";
            }

            foreach (Attribute stat in attributes.Values)
            {
                str += stat.name + ":" + stat.value + ",";
            }

            return str + ")";
        }
    }
}