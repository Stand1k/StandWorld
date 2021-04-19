namespace StandWorld.Characters
{
    public class HumanStats : CharacterStats
    {
        protected override void LoadAttributes()
        {
            base.LoadAttributes();

            attributes[Attributes.InventorySize].AddModifier(new StatModifier(stats[Stats.Strength], 3f));

            foreach (Attribute att in attributes.Values)
            {
                att.Update();
            }
        }
        
        public override void Update()
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
                    vitals[Vitals.Energy].currentValue += 0.2f;
                }
            }
            
            if (vitals[Vitals.Hunger].currentValue > 0)
            {
                vitals[Vitals.Hunger].currentValue -= 0.1f;
            }
        }

        public override string ToString()
        {
            string str = "HumanStats(";
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
    }
}