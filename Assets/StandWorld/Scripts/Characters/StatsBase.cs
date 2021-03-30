using System.Collections.Generic;

namespace StandWorld.Characters
{
    public class StatsBase
    {
        public string name;
        
        public float baseValue;

        public float buffValue;

        public float value => baseValue + buffValue;

        public StatsBase(string name)
        {
            this.name = name;   
            baseValue = 0;
            buffValue = 0;
        }
    }

    //Модифікує неявні харакатеристики
    public class StatModifier
    {
        public StatsBase stat;

        public float ratio;

        public StatModifier(StatsBase stat, float ratio)
        {
            this.stat = stat;
            this.ratio = ratio;
        }
    }
    
    //Неявні(Implicit) характеристики (розраховується у співвідношенні до однієї або багатьох неявних(Explicit) характеристик)
    public class Attribute : StatsBase
    {
        private List<StatModifier> _modifiers;

        public Attribute(string name) : base(name)
        {
            _modifiers = new List<StatModifier>();
        }

        public void AddModifier(StatModifier modifier)
        {
            _modifiers.Add(modifier);
        }

        public void Update()
        {
            baseValue = 0;
            if (_modifiers.Count > 0)
            {
                foreach (StatModifier modifier in _modifiers)
                {
                    baseValue += (int) (modifier.stat.value * modifier.ratio);
                }
            }
        }
    }

    public class Vital : Attribute
    {
        private float _currentValue;

        public float currentValue
        {
            get
            {
                if (_currentValue > value)
                {
                    _currentValue = value;
                }

                if (_currentValue < 0)
                {
                    _currentValue = 0;
                }

                return _currentValue;
            }

            set
            {
                _currentValue = value;
            }
        }

        public Vital(string name) : base(name)
        {
            _currentValue = 0;
        }

        public void Fill()
        {
            _currentValue = value;
        }
    }
}
