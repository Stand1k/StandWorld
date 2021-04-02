using System;

namespace StandWorld.Characters
{
    public static class StatsUtils
    {
        public static Stats[] stats = (Stats[]) Enum.GetValues(typeof(Stats));
        public static Attributes[] attributes = (Attributes[]) Enum.GetValues(typeof(Attributes));
        public static Vitals[] vitals = (Vitals[]) Enum.GetValues(typeof(Vitals));
        public static Skills[] skills = (Skills[]) Enum.GetValues(typeof(Skills));
    }

    [Serializable]
    public enum Stats
    {
        Strength,
        Agility,
        Endurance,
        Intellect,
        Wisdom
    }
    
    [Serializable]
    public enum Attributes
    {
        WalkSpeed,              // [Strength + Endurance ]
        Charisma,               // [Wisdom + Strength]
        InventorySize,          // [Strength]
        PhysicalResistance,     // [Strength + Endurance]
        PhysicalAttack,         // [Strength + Agility]
        MagicalAttack,          // [Intellect + Wisdom]
        MagicalResistance,      // [Wisdom + Endurance]
        HealthRegen,            // [Endurance + Strength]
        EnergyRegen,            // [Wisdom + Endurance]
        ManaRegen,              // [Intellect + Wisdom]
        CriticalChance,         // [Agility + Intellect]
    }    
    
    [Serializable]
    public enum Vitals 
    {
        Health,  //[Endurance]
        Energy, //[Endurance]
        Mana,   //[Intillect]
    }

    [Serializable]
    public enum Skills
    {
        Healing,            // [Wisdom + Intellect]
        Building,           // [Agility + Intellect]
        Manufacturing,      // [Agility + Intellect]
        Entertaining,       // [Charisma]
        Growing,            // [Agility + Wisdom]
        Cutting,            // [Strength + Agility]
        Mining,             // [Strngth + Agility]
        Cooking,            // [Agility + Intellect]
    }
}

