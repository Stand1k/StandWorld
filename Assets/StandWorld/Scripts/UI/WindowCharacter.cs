using StandWorld.Characters;
using StandWorld.Definitions;

namespace StandWorld.UI
{
    public class WindowCharacter : Window
    {
        private BaseCharacter _character;

        public WindowCharacter(BaseCharacter character)
        {
            _character = character;
            SetTitle(_character.name);
            AddTab("Character");
            AddTab("Infos");
        }

        public override void Content()
        {
            if (activeTab == 0)
            {
                vGrid.H2("Vitals");
                foreach (Vital vital in _character.stats.vitals.Values)
                {
                    WindowComponents.FillableBarWithLabelValue(
                        vGrid.GetNewRect(20f),
                        vital.name,
                        vital,
                        Defs.namedColorPallets["cols_vitals"].colors[vital.name]
                    );
                }

                vGrid.H2("Stats");
                foreach (StatsBase stat in _character.stats.stats.Values)
                {
                    WindowComponents.SimpleStat(vGrid.GetNewRect(20f), stat.name, stat.value, stat.baseValue);
                }
            }
            else if (activeTab == 1)
            {
                vGrid.H2("Info");
                vGrid.Paragraph(_character.def.shortDescription);
                vGrid.H2("Detailled Stats");
                foreach (StatsBase attr in _character.stats.attributes.Values)
                {
                    WindowComponents.SimpleStat(vGrid.GetNewRect(20f), attr.name, attr.value, attr.baseValue);
                }
            }
        }
    }
}