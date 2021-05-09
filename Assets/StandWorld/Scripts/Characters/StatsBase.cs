using System.Collections.Generic;
using StandWorld.Helpers;

namespace StandWorld.Characters
{
    public class Stat
    {
        public string name;

        public float baseValue;

        public float buffValue;

        public float value => baseValue + buffValue;

        public Stat(string name)
        {
            this.name = name;
            baseValue = 0;
            buffValue = 0;
        }

        public Stat(string name, int max)
        {
            this.name = name;
            baseValue = max;
            buffValue = 0;
        }
    }

    public class StatModifier
    {
        public Stat stat;

        public float ratio;

        public StatModifier(Stat stat, float ratio)
        {
            this.stat = stat;
            this.ratio = ratio;
        }
    }

    public class Attribute : Stat
    {
        private List<StatModifier> _modifiers;
        
        public Attribute(string name) : base(name)
        {
            _modifiers = new List<StatModifier>();
        }
        
        public Attribute(string name, int max) : base(name, max)
        {
            _modifiers = new List<StatModifier>();
        }

        public void AddModifier(StatModifier modifier)
        {
            _modifiers.Add(modifier);
        }

        public void Update()
        {
            if (baseValue == 0)
            {
                if (_modifiers.Count > 0)
                {
                    foreach (StatModifier modifier in _modifiers)
                    {
                        baseValue += (int) (modifier.stat.value * modifier.ratio);
                    }
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
            set { _currentValue = value; }
        }

        public bool ValueInfToPercent(float v)
        {
            if (v >= GameUtils.Normalize(0, value, currentValue))
            {
                return true;
            }

            return false;
        }

        public Vital(string name) : base(name)
        {
            _currentValue = 0;
        }

        public Vital(string name, int max) : base(name, max)
        {
            baseValue = max;
            _currentValue = max;
        }

        public void Fill()
        {
            _currentValue = value;
        }
    }
}

