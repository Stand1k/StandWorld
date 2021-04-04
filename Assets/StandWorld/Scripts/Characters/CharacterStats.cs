using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace StandWorld.Characters
{
    public class CharacterStats
    {
        public Dictionary<Stats, Stat> stats { get; protected set; }
        public Dictionary<Vitals, Vital> vitals { get; protected set; }
        public Dictionary<Attributes, Attribute> attributes { get; protected set; }
        public bool sleep { get; protected set; }
        public Action onWakeUp = null;
        public Action onSleep = null;

        public CharacterStats()
        {
            sleep = false;
            stats = new Dictionary<Stats, Stat>();
            foreach (Stats stat in StatsUtils.stats)
            {
                stats.Add(stat, new Stat(stat.ToString()));
                stats[stat].baseValue = Random.Range(5, 15);
            }

            vitals = new Dictionary<Vitals, Vital>();
            foreach (Vitals vital in StatsUtils.vitals)
            {
                if (vital == Vitals.Hunger || vital == Vitals.Joy)
                {
                    vitals.Add(vital, new Vital(vital.ToString(), 100));
                }
                else
                {
                    vitals.Add(vital, new Vital(vital.ToString()));
                }
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
            attributes[Attributes.WalkSpeed].AddModifier(new StatModifier(stats[Stats.Strength], 0.3f));
            attributes[Attributes.WalkSpeed].AddModifier(new StatModifier(stats[Stats.Endurance], 0.2f));

            attributes[Attributes.HealthRegen].AddModifier(new StatModifier(stats[Stats.Strength], 0.1f));
            attributes[Attributes.HealthRegen].AddModifier(new StatModifier(stats[Stats.Endurance], 0.2f));

            attributes[Attributes.EnergyRegen].AddModifier(new StatModifier(stats[Stats.Endurance], 0.1f));

            attributes[Attributes.PhysicalResistance].AddModifier(new StatModifier(stats[Stats.Strength], 0.1f));
            attributes[Attributes.MagicalResistance].AddModifier(new StatModifier(stats[Stats.Endurance], 0.3f));

            attributes[Attributes.PhysicalAttack].AddModifier(new StatModifier(stats[Stats.Strength], 0.3f));
            attributes[Attributes.PhysicalAttack].AddModifier(new StatModifier(stats[Stats.Agility], 0.2f));

            attributes[Attributes.ManaRegen].AddModifier(new StatModifier(stats[Stats.Intellect], 0.3f));
            attributes[Attributes.ManaRegen].AddModifier(new StatModifier(stats[Stats.Wisdom], 0.2f));

            attributes[Attributes.CriticalChance].AddModifier(new StatModifier(stats[Stats.Agility], 0.2f));
            attributes[Attributes.CriticalChance].AddModifier(new StatModifier(stats[Stats.Intellect], 0.2f));

            attributes[Attributes.Charisma].AddModifier(new StatModifier(stats[Stats.Wisdom], 0.2f));
            attributes[Attributes.Charisma].AddModifier(new StatModifier(stats[Stats.Strength], 0.2f));


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
            if (!sleep)
            {
                if (vitals[Vitals.Energy].currentValue > 0)
                {
                    vitals[Vitals.Energy].currentValue -= 0.1f;
                }
            }
            else
            {
                if (vitals[Vitals.Energy].currentValue < vitals[Vitals.Energy].value)
                {
                    vitals[Vitals.Energy].currentValue += attributes[Attributes.EnergyRegen].value;
                }
            }

            if (vitals[Vitals.Hunger].currentValue > 0)
            {
                vitals[Vitals.Hunger].currentValue -= 0.1f;
            }
        }

        public override string ToString()
        {
            string str = "BaseStats(";
            foreach (Stat stat in stats.Values)
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

        public void Sleep()
        {
            sleep = true;
            if (onSleep != null)
            {
                onSleep();
            }
        }

        public void WakeUp()
        {
            sleep = false;
            if (onWakeUp != null)
            {
                onWakeUp();
            }
        }
    }
}